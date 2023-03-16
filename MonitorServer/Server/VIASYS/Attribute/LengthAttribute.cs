using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Server.VIASYS
{
    public class LengthAttribute : Attribute
    {
        private int _length;
        public LengthAttribute(int length)
        {
            _length = length;
        }

        public  int Length
        {
            get => _length;
            set => _length = value;
        }
    }
}
