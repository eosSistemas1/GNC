using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PL.ServicesWeb.Entities;
using PL.Fwk.Exceptions;

namespace PL.ServicesWeb.DataAccess
{
    public class UsuariosDataAccess : EntityManagerDataAccess<USUARIOS, UsuariosExtendedView, UsuariosParameters, ServicesContext>
    {

        public USUARIOS Login(UsuariosParameters parameter)
        {
            using (var context = this.GetEntityContext())
            {
                // fijarse como haccer para incluir a los servicios
                var queryUser = from u in context.CreateQuery<USUARIOS>(this.EntityName)
                                .Include("ROLES")
                                .Include("ROLES.T_SERVICIOS")
                                .Where(u => u.Descripcion == parameter.UserName 
                                            && u.Password == parameter.Password
                                            && u.Activo == true)

                                select u;
                if (queryUser.Count() == 0)
                {
                    throw new LoginException("El usuario ingresado o la contraseña no son correctos");
                }

                return queryUser.FirstOrDefault();
            }
        }
    }
}
