using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace TagHelperEssentials
{
    [TargetElement("__")]
    public class ViewComponentTagHelper : ITagHelper
    {
        private readonly object[] _values = new object[10];
        private int _parametersProvided = 0;

        int ITagHelper.Order { get; } = 0;

        [Activate]
        protected IViewComponentHelper Component { get; set; }

        public object Parameter0
        {
            get
            {
                return _values[0];
            }
            set
            {
                _parametersProvided = 1;
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
                _parametersProvided = 2;
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
                _parametersProvided = 3;
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
                _parametersProvided = 4;
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
                _parametersProvided = 5;
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
                _parametersProvided = 6;
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
                _parametersProvided = 7;
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
                _parametersProvided = 8;
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
                _parametersProvided = 9;
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
                _parametersProvided = 10;
                _values[9] = value;
            }
        }

        public async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var parameters = new object[_parametersProvided];

            for (var i = 0; i < parameters.Length; i++)
            {
                parameters[i] = _values[i];
            }

            string componentResult = null;

            componentResult = (await Component.InvokeAsync(output.TagName, parameters)).ToString();

            output.SuppressOutput();

            output.Content.SetContent(componentResult);
        }
    }
}