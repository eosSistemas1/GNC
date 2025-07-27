using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class UsuariosLogic : EntityManagerLogic<UsuariosLogic, UsuariosExtendedView, UsuariosParameters, UsuariosDataAccess>
    {
        public USUARIOS Login(UsuariosParameters parameter)
        {
            UsuariosDataAccess oa = new UsuariosDataAccess();
            return oa.Login(parameter);
        }

        public USUARIOS ReadByNombre(String nombre)
        {
            return EntityDataAccess.ReadByNombre(nombre);
        }
    }
}
