using System;

namespace TagHelperEssentials.Web.Components
{
    public class CustomViewComponent
    {
        public string Invoke(int count, string extraValue)
        {
            return $"Value: {count} : {extraValue}";
        }
    }
}