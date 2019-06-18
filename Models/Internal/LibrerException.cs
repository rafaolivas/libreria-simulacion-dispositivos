using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreR.Models
{
    public class LibrerException : Exception
    {
        public LibrerException(string message = "An exception was thrown in LibreR") : base(message) { }
    }
}

