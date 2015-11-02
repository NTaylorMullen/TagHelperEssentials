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
            IChunkTreeCache chunkTreeCache,
            IViewComponentDescriptorProvider viewComponentDescriptorProvider)
            : base(chunkTreeCache)
        {
            TagHelperDescriptorResolver = new ViewComponentTagHelperDescriptorResolver(
                new TagHelperTypeResolver(),
                viewComponentDescriptorProvider);
        }
    }
}