using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailRegisterApp.Helper.Exceptions
{
    public class SaveException:Exception
    {
        public SaveException(string message) : base(message)
        {

        }
    }
}