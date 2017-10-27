using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailRegisterApp.Helper
{
    public class Helpers
    {
        public static string getPropertyName<T>(System.Linq.Expressions.Expression<Func<T>> propertyExp)
        {
            return (propertyExp.Body as System.Linq.Expressions.MemberExpression).Member.Name;
        }
    }
}