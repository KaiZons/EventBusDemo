using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class NamevalidateAttribute : AbstractValidateAttribute
    {
        private int m_minLength;

        public NamevalidateAttribute(int minLength)
        {
            m_minLength = minLength;
        }

        public override bool Validate(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (value.ToString().Length < m_minLength)
            {
                return false;
            }
            return true;
        }
    }
}
