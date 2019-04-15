using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class AttributeExtend
    {
        public static bool Valicate<T>(this T t)
        {
            Type type = t.GetType();
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(AbstractValidateAttribute), true))
                {
                    object value = prop.GetValue(t);
                    foreach (AbstractValidateAttribute attribute in prop.GetCustomAttributes(typeof(AbstractValidateAttribute), true))
                    {
                        if (!attribute.Validate(value))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
