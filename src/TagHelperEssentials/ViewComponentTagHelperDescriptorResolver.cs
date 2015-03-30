﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Mvc.ViewComponents;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;
using Microsoft.Framework.Internal;

namespace TagHelperEssentials
{
    public class ViewComponentTagHelperDescriptorResolver : TagHelperDescriptorResolver, ITagHelperDescriptorResolver
    {
        private static readonly Type ViewComponentTagHelperType = typeof(ViewComponentTagHelper);

        private readonly IViewComponentDescriptorProvider _viewComponentDescriptorProvider;
        private IEnumerable<TagHelperDescriptor> _viewComponentTagHelperDescriptors;

        public ViewComponentTagHelperDescriptorResolver(
            [NotNull] TagHelperTypeResolver typeResolver,
            [NotNull] IViewComponentDescriptorProvider viewComponentDescriptorProvider)
            : base(typeResolver)
        {
            _viewComponentDescriptorProvider = viewComponentDescriptorProvider;
        }

        IEnumerable<TagHelperDescriptor> ITagHelperDescriptorResolver.Resolve(TagHelperDescriptorResolutionContext resolutionContext)
        {
            var descriptors = base.Resolve(resolutionContext);

            if (_viewComponentTagHelperDescriptors == null)
            {
                _viewComponentTagHelperDescriptors = ResolveViewComponentTagHelperDescriptors(descriptors.FirstOrDefault()?.Prefix ?? string.Empty);
            }

            return descriptors.Concat(_viewComponentTagHelperDescriptors);
        }

        private IEnumerable<TagHelperDescriptor> ResolveViewComponentTagHelperDescriptors(string prefix)
        {
            var viewComponentDescriptors = _viewComponentDescriptorProvider.GetViewComponents();
            var resolvedDescriptors = new List<TagHelperDescriptor>();

            foreach (var viewComponentDescriptor in viewComponentDescriptors)
            {
                IEnumerable<TagHelperAttributeDescriptor> attributeDescriptors;
                if (TryGetViewComponentAttributeDescriptors(viewComponentDescriptor.Type, out attributeDescriptors))
                {
                    resolvedDescriptors.Add(
                        new TagHelperDescriptor(
                            prefix,
                            viewComponentDescriptor.ShortName,
                            ViewComponentTagHelperType.FullName,
                            ViewComponentTagHelperType.GetTypeInfo().Assembly.GetName().Name,
                            attributeDescriptors,
                            requiredAttributes: attributeDescriptors.Select(descriptor => descriptor.Name)));
                }
            }

            return resolvedDescriptors;
        }

        private bool TryGetViewComponentAttributeDescriptors(Type type, out IEnumerable<TagHelperAttributeDescriptor> attributeDescriptors)
        {
            var methods = type.GetMethods();
            var invocableMethod = methods.Where(info => info.Name.StartsWith("Invoke", StringComparison.Ordinal)).FirstOrDefault();

            if (invocableMethod == null)
            {
                attributeDescriptors = null;
                return false;
            }

            var methodParameters = invocableMethod.GetParameters();
            var descriptors = new List<TagHelperAttributeDescriptor>();

            for (var i = 0; i < methodParameters.Length; i++)
            {
                var parameter = methodParameters[i];

                descriptors.Add(
                    new TagHelperAttributeDescriptor(
                        parameter.Name,
                        "Parameter" + i,
                        parameter.ParameterType.FullName));
            }

            attributeDescriptors = descriptors;

            return true;
        }
    }
}