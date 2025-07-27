using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using TalleresWeb.Entities;
using TalleresWeb.DataAccess;

namespace TalleresWeb.Logic
{
    public class UsuariosLogic : EntityManagerLogic<Usuario, UsuariosExtendedView, UsuariosParameters, UsuariosDataAccess>
    {
        public Usuario ReadByNombre(String nombre)
        {
            UsuariosDataAccess oa = new UsuariosDataAccess();
            return oa.ReadByNombre(nombre);
        }

     /*   public Usuarios ReadByUsuario(String usuario)
        {
            UsuariosDataAccess oa = new UsuariosDataAccess();
            return oa.ReadByUsuario(usuario);
        }*/
        public List<Usuario> ReadByUsuario(String Usuario)
        {
            return this.EntityDataAccess.ReadByUsuario(Usuario);
        }

        public Usuario LoadAllLogin(String usuario, String encryptedPass, Guid idTipoDoc, String nroDoc)
        {
            UsuariosDataAccess oa = new UsuariosDataAccess();
            return oa.ReadByNombre(usuario, encryptedPass,idTipoDoc, nroDoc);
        }

        public Usuario Login(UsuariosParameters user)
        {
            return this.EntityDataAccess.Login(user);
        }

        public UsuarioBasicView LoginExterno(UsuariosParameters user)
        {
            return this.EntityDataAccess.LoginExterno(user);
        }

        public void AddUsuario(Usuario Usuario)
        {
            var existeUsuario = this.Read(Usuario.ID) != null;

            //add o update 
            if (!existeUsuario)
            {
                this.Add(Usuario);
            }
            else
            {
                this.Update(Usuario);
            }
        }

    }
}
