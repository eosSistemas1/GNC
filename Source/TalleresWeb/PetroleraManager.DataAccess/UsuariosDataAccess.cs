using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;
using PL.Fwk.Entities;

namespace PetroleraManager.DataAccess
{

    public class UsuariosDataAccess : EntityManagerDataAccess<USUARIOS, UsuariosExtendedView, UsuariosParameters, DataModelContext>
    {
        public USUARIOS Login(UsuariosParameters parameter)
        {
            using (var context = this.GetEntityContext())
            {
                // fijarse como haccer para incluir a los servicios
                var queryUser = from u in context.CreateQuery<USUARIOS>(this.EntityName)
                                //.Include("ROLES")
                                //.Include("ROLES.T_SERVICIOS")
                                //.Where(u => u.Descripcion == parameter.UserName
                                //            && u.Contrasenia == parameter.Password
                                //            && u.Activo == true)

                                select u;
                if (queryUser.Count() == 0)
                {
                    throw new Exception("El usuario ingresado o la contraseña no son correctos");
                }

                return queryUser.FirstOrDefault();
            }
        }

        public USUARIOS ReadByNombre(String nombre)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<USUARIOS>(this.EntityName)
                    .Where(x => x.Descripcion.Equals(nombre))
                            select t;

                return query.FirstOrDefault();
            }
        }
    }
}