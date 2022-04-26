using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avd_MultiAddressBook
{
    public class ContactException : Exception
    {        
            ExceptionType type;
            public enum ExceptionType
            {
                Connection_Failed
            }
            public ContactException(ExceptionType type, string message) : base(message)
            {
                this.type = type;
            }
        
    }
}
