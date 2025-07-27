using PL.Fwk.BusinessLogic;
using System;
using System.Collections.Generic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class ObleasReguladoresLogic : EntityManagerLogic<ObleasReguladores, ObleasReguladoresExtendedView, ObleasReguladoresParameters, ObleasReguladoresDataAccess>
    {
        #region Methods

        public List<ObleasExtendedView> ReadAllConsultaEnBase(ObleasParameters param)
        {
            return EntityDataAccess.ReadAllConsultaEnBase(param);
        }

        public List<ObleasReguladores> ReadByIDOblea(Guid idOblea)
        {
            ObleasReguladoresDataAccess oa = new ObleasReguladoresDataAccess();
            return oa.ReadByIDOblea(idOblea);
        }

        #endregion
    }
}