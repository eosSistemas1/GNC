using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using TalleresWeb.Entities;
using TalleresWeb.DataAccess;
using PL.Fwk.Entities;

namespace TalleresWeb.Logic
{
    public class TiposOperacionesLogic : EntityManagerLogic<Operaciones, TiposOperacionesExtendedView, TiposOperacionesParameters, TiposOperacionesDataAccess>
    {

        public List<ViewEntity> ReadEVOperaciones()
        {
            return EntityDataAccess.ReadEVOperaciones();
        }

        public List<ViewEntity> ReadEVMSDB()
        {
            return EntityDataAccess.ReadEVMSDB();
        }
    }
}
