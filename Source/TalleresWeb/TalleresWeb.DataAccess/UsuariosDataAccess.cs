using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using TalleresWeb.Entities;
using PL.Fwk.Entities;

namespace TalleresWeb.DataAccess
{

    public class UsuariosDataAccess : EntityManagerDataAccess<Usuario, UsuariosExtendedView, UsuariosParameters, TalleresWebEntities>
    {
        public Usuario ReadByNombre(String nombre)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Usuario>(this.EntityName)
                            .Include("Talleres") 
                    .Where(x => x.Descripcion.Equals(nombre))
                            select t;

                return query.ToList().FirstOrDefault();
            }
        }

        public List<Usuario> ReadByUsuario(String User)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Usuario>(this.EntityName)
                    .Where(x => x.Descripcion.Contains(User))
                            select t;

                return query.ToList();
             
            }
        }
        public Usuario ReadByNombre(string usuario, string encryptedPass, Guid idTipoDoc, string nroDoc)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Usuario>(this.EntityName)
                    .Where(x => x.Descripcion.Equals(usuario)
                             && x.Contrasenia.Equals(encryptedPass)
                             && x.IdTipoDoc.Value.Equals(idTipoDoc)
                             && x.NroDocumento.Equals(nroDoc))
                            select t;

                return query.ToList().FirstOrDefault();
            }
        }

        public Usuario Login(UsuariosParameters user)
        {
            using (var context = this.GetEntityContext())
            {                
                var queryUser = from u in context.CreateQuery<Usuario>(this.EntityName)
                                    .Include("Talleres")
                                    //.Include("ROLES")
                                    //.Include("ROLES.T_SERVICIOS")
                                    .Where(u => u.Descripcion == user.UserName
                                                && u.Contrasenia == user.Password
                                                && u.Activo == true)
                                select u;

                if (queryUser.Count() == 0)
                {
                    throw new Exception("El usuario ingresado o la contraseña no son correctos");
                }

                return queryUser.FirstOrDefault();
            }
        }

        public UsuarioBasicView LoginExterno(UsuariosParameters user)
        {
            using (var context = this.GetEntityContext())
            {
                try
                {
                    var queryUser = from u in context.CreateQuery<Usuario>(this.EntityName)
                                        .Where(u => u.Descripcion == user.UserName
                                                    && u.Contrasenia == user.Password
                                                    && u.Activo == true)
                                    select new UsuarioBasicView
                                    {
                                        ID = u.ID,
                                        Descripcion = u.Descripcion,
                                        IDTaller = u.IdTaller,
                                        RazonSocialTaller = u.Talleres.RazonSocialTaller,
                                        MatriculaTaller = u.Talleres.Descripcion
                                    };

                    if (queryUser.Count() == 0)
                    {
                        throw new Exception("El usuario ingresado o la contraseña no son correctos");
                    }

                    if (!queryUser.First().IDTaller.HasValue)
                    {
                        throw new InvalidOperationException();
                    }


                    return queryUser.FirstOrDefault();
                }
                catch (InvalidOperationException)
                {
                    throw new Exception("El usuario ingresado no pertenece a un taller de nuestra red");
                }
                catch (Exception)
                {
                    throw new Exception("Ha ocurrido un error al ingresar a la aplicación. \n Intente nuevamente más tarde");
                }
            }
        }
    }
}