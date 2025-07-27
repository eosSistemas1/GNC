using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PL.Fwk.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException():base("Usuario o Contraseña no valido")
        {

        }


        public LoginException(String message):base(message)
        {

        }
    }
}
