using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using PL.ServicesWeb.Entities;
using PL.ServicesWeb.DataAccess;

namespace PL.ServicesWeb.Logic
{
    public class UsuariosLogic : EntityManagerLogic<USUARIOS, UsuariosExtendedView, UsuariosParameters, UsuariosDataAccess>
    {
        public USUARIOS Login(UsuariosParameters parameter)
        {
           return this.EntityDataAccess.Login(parameter);
        }
    }
}
