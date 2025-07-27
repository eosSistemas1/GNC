using PL.Fwk.BusinessLogic;
using System.Collections.Generic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class S_AccesosCULogic : EntityManagerLogic<S_ACCESOSCU, S_AccesosCUExtendedView, S_AccesosCUParameters, S_AccesosCUDataAccess>
    {
        #region Methods

        public List<S_AccesosCUExtendedView> ReadPadresByRol(S_AccesosCUParameters param)
        {
            S_AccesosCUDataAccess oa = new S_AccesosCUDataAccess();
            return oa.ReadPadresByRol(param);
        }

        #endregion
    }
}