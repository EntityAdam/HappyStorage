using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HappyStorage.BlazorWeb.Helpers
{
    public static class BlazorHelper
    {
        public static string GetDisplayName(PropertyInfo type)
        {
            try
            {
                var attribute = type.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().Single();
                return attribute.ShortName ?? attribute.Name ?? type.Name;
            }
            catch (Exception)
            {
                //Not critical, fail silently
                return string.Empty;
            }
        }
    }
}
