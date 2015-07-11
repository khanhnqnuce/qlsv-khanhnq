using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FDI.Common
{
    public class IParameter
    {
        public Dictionary<string, string> ToListParamater()
        {
            var properties = GetType().GetProperties();

            return properties.ToDictionary(pro => pro.Name, pro => pro.GetValue(this, null).ToString());
        }
    }


}
