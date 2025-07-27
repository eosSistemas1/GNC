using PL.Fwk.BusinessLogic;
using System;
using System.Collections.Generic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class PECLogic : EntityManagerLogic<PEC, PECExtendedView, PECParameters, PECDataAccess>
    {
        #region Methods

        public List<PEC> ReadDetalladoByID(Guid ID)
        {
            PECDataAccess oa = new PECDataAccess();
            return oa.ReadDetalladoByID(ID);
        }

        public void AddPEC(PEC pec)
        {
            var pecExistente = Read(pec.ID);
            if (pecExistente == null)
            {
                Add(pec);
            }
            else
            {
                Update(pec);
            }
        }

        #endregion
    }
}