using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public class ConnectionStrings
    {
        public static string Master()
        {
            return "Server=localhost; Database=master; Trusted_Connection=True;";
        }

        public static string Bzp()
        {
            return "Server=localhost; Database=Bzp; Trusted_Connection=True;";
        }
    }
}
