using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Mvc.ViewFeatures.Internal;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace TagHelperEssentials
{
    [HtmlTargetElement("__")]
    public class ViewComponentTagHelper : ITagHelper
    {
        private readonly object[] _values = new object[10];
        private readonly IViewComponentHelper _component;
        private int _parametersProvided;

        public ViewComponentTagHelper(IViewComponentHelper component)
        {
            _component = component;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        int ITagHelper.Order { get; } = 0;

        public object Parameter0
        {
            get
            {
                return _values[0];
            }
            set
            {
                _parametersProvided++;
                _values[0] = value;
            }
        }

        public object Parameter1
        {
            get
            {
                return _values[1];
            }
            set
            {
                _parametersProvided++;
                _values[1] = value;
            }
        }

        public object Parameter2
        {
            get
            {
                return _values[2];
            }
            set
            {
                _parametersProvided++;
                _values[2] = value;
            }
        }

        public object Parameter3
        {
            get
            {
                return _values[3];
            }
            set
            {
                _parametersProvided++;
                _values[3] = value;
            }
        }

        public object Parameter4
        {
            get
            {
                return _values[4];
            }
            set
            {
                _parametersProvided++;
                _values[4] = value;
            }
        }

        public object Parameter5
        {
            get
            {
                return _values[5];
            }
            set
            {
                _parametersProvided++;
                _values[5] = value;
            }
        }

        public object Parameter6
        {
            get
            {
                return _values[6];
            }
            set
            {
                _parametersProvided++;
                _values[6] = value;
            }
        }

        public object Parameter7
        {
            get
            {
                return _values[7];
            }
            set
            {
                _parametersProvided++;
                _values[7] = value;
            }
        }

        public object Parameter8
        {
            get
            {
                return _values[8];
            }
            set
            {
                _parametersProvided++;
                _values[8] = value;
            }
        }

        public object Parameter9
        {
            get
            {
                return _values[9];
            }
            set
            {
                _parametersProvided++;
                _values[9] = value;
            }
        }

        public void Process(TagHelperContext context, TagHelperOutput output)
        {
            ((ICanHasViewContext)_component).Contextualize(ViewContext);

            var parameters = new object[_parametersProvided];

            for (var i = 0; i < parameters.Length; i++)
            {
                parameters[i] = _values[i];
            }

            var componentResult = _component.Invoke(output.TagName, parameters);

            output.SuppressOutput();

            output.Content.SetContent(componentResult);
        }

#pragma warning disable 1998
        public virtual async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Process(context, output);
        }
#pragma warning restore 1998
    }
}