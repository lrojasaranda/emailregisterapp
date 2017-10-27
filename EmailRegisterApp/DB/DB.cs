using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailRegisterApp.DB
{
    public class DB
    {
        public class StoreProcedure
        {
            public static string UserInsert = "SP_INS_USER";
            public static string EmailInsert = "SP_INS_USER_EMAIL";
            public static string EmailExists ="SP_SEL_EXISTE_EMAIL";

        }
        public class Parameter
        {
            /// <summary>
            /// @ovMensajeError
            /// </summary>
            public static string MensajeError = "@ovMensajeError";

            /// <summary>
            /// @oiNumeroError
            /// </summary>
            public static string NumeroError = "@oiNumeroError";
        }
    }
}