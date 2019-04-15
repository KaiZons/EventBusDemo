using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class AgeValidateAttribute : AbstractValidateAttribute
    {
        private int m_min = 0;
        private int m_max = 0;

        public AgeValidateAttribute(int min, int max)
        {
            m_min = min;
            m_max = max;
        }

        public override bool Validate(object value)
        {
            if (value == null)
            {
                return false;
            }
            int age = 0;
            if (int.TryParse(value.ToString(), out age))
            {
                return age >= m_min
                && age <= m_max;
            }
            return false;
        }
    }
}
