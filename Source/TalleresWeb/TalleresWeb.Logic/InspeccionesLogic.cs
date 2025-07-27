using PL.Fwk.BusinessLogic;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class InspeccionesLogic : EntityManagerLogic<Inspecciones, InspeccionesExtendedView, InspeccionesParameters, InspeccionesDataAccess>
    {
        public List<ViewEntity> ReadInspeccionesPorTipo(Guid tipoInspeccionID)
        {
            return this.EntityDataAccess.ReadInspeccionesPorTipo(tipoInspeccionID);
        }
    }
}