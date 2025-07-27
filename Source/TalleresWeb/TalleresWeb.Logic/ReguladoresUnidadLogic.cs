using PL.Fwk.BusinessLogic;
using System;
using System.Collections.Generic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class ReguladoresUnidadLogic : EntityManagerLogic<ReguladoresUnidad, ReguladoresUnidadExtendedView, ReguladoresUnidadParameters, ReguladoresUnidadDataAccess>
    {
        #region Methods

        public List<ReguladoresUnidad> ReadReguladorUnidad(Guid idRegulador, String nroSerie)
        {
            ReguladoresUnidadDataAccess oa = new ReguladoresUnidadDataAccess();

            return oa.ReadReguladorUnidad(idRegulador, nroSerie);
        }

        #endregion
    }
}