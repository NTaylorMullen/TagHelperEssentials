using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Mvc.ViewComponents;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;

namespace TagHelperEssentials
{
    public class ViewComponentTagHelperDescriptorResolver : TagHelperDescriptorResolver, ITagHelperDescriptorResolver
    {
        private static readonly Type ViewComponentTagHelperType = typeof(ViewComponentTagHelper);

        private readonly IViewComponentDescriptorProvider _viewComponentDescriptorProvider;
        private IEnumerable<TagHelperDescriptor> _viewComponentTagHelperDescriptors;

        public ViewComponentTagHelperDescriptorResolver(
            TagHelperTypeResolver typeResolver,
            IViewComponentDescriptorProvider viewComponentDescriptorProvider)
            : base(typeResolver, designTime: false)
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
                        new TagHelperDescriptor
                        {
                            Prefix = prefix,
                            TagName = viewComponentDescriptor.ShortName,
                            TypeName = ViewComponentTagHelperType.FullName,
                            AssemblyName = ViewComponentTagHelperType.GetTypeInfo().Assembly.GetName().Name,
                            Attributes = attributeDescriptors,
                            RequiredAttributes = attributeDescriptors.Select(descriptor => descriptor.Name)
                        });
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
                    new TagHelperAttributeDescriptor
                    {
                        Name = parameter.Name,
                        PropertyName = "Parameter" + i,
                        TypeName = parameter.ParameterType.FullName
                    });
            }

            attributeDescriptors = descriptors;

            return true;
        }
    }
}