using System;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Internal;

namespace TagHelperEssentials
{
    public static class TagHelperEssentialsServiceCollectionExtensions
    {
        public static IServiceCollection AddTagHelperEssentials([NotNull] this IServiceCollection services)
        {
            services.AddTransient<IMvcRazorHost, TagHelperMvcRazorHost>();

            return services;
        }
    }
}