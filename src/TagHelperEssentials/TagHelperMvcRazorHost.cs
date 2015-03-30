using System;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Mvc.Razor.Directives;
using Microsoft.AspNet.Mvc.ViewComponents;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.Framework.Internal;

namespace TagHelperEssentials
{
    public class TagHelperMvcRazorHost : MvcRazorHost
    {
        public TagHelperMvcRazorHost(
            [NotNull] ICodeTreeCache codeTreeCache,
            [NotNull] IViewComponentDescriptorProvider viewComponentDescriptorProvider)
            : base(codeTreeCache)
        {
            TagHelperDescriptorResolver = new ViewComponentTagHelperDescriptorResolver(
                new TagHelperTypeResolver(),
                viewComponentDescriptorProvider);
        }
    }
}