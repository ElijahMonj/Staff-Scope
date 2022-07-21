using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loginTutorial
{
    class Class1
    {
        static string Uname;
        public static string uname
        {
            get
            {
                return Uname;
            }
            set
            {
                Uname = value;
            }
        }

        static string Datepicker;
        public static string datepicker
        {
            get
            {
                return Datepicker;
            }
            set
            {
                Datepicker = value;
            }
        }
    }
}
