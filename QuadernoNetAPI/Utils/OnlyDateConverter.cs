using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadernoNetAPI.Utils
{
    class OnlyDateConverter : IsoDateTimeConverter
    {
        public OnlyDateConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
