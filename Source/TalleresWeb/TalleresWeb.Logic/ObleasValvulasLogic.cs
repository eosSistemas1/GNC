using PL.Fwk.BusinessLogic;
using System.Collections.Generic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class ObleasValvulasLogic : EntityManagerLogic<ObleasValvulas, ObleasValvulasExtendedView, ObleasValvulasParameters, ObleasValvulasDataAccess>
    {
        #region Methods

        public List<ObleasExtendedView> ReadAllConsultaEnBase(ObleasParameters param)
        {
            return EntityDataAccess.ReadAllConsultasEnBase(param);
        }

        #endregion
    }
}