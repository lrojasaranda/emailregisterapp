using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EmailRegisterApp.Helper
{
    public class Keys
    {
        public static string GetSqlConexion()
        {
            return ConfigurationManager.ConnectionStrings["SqlCn"].ConnectionString;
        }
    }
}