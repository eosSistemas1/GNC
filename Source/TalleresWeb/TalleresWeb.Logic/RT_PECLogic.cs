using PL.Fwk.BusinessLogic;
using System;
using System.Collections.Generic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class RT_PECLogic : EntityManagerLogic<RT_PEC, RT_PECExtendedView, RT_PECParameters, RT_PECDataAccess>
    {
        #region Methods

        public List<RT> ReadDetalladoByID(Guid ID)
        {
            RTDataAccess oa = new RTDataAccess();
            return oa.ReadDetalladoByID(ID);
        }

        public void AddRT(RT_PEC rT_PEC)
        {
            var existeRT = this.Read(rT_PEC.ID) != null;

            //add o update 
            if (!existeRT)
            {
                this.Add(rT_PEC);
            }
            else
            {
                this.Update(rT_PEC);
            }
        }

        public List<RT_PECExtendedView> ReadByPEC(Guid pecID)
        {
            return this.EntityDataAccess.ReadByPEC(pecID);
        }


        //public List<String> ReadListRT(String rt)
        //{
        //    return this.EntityDataAccess.ReadListRT(rt);
        //}

        #endregion
    }
}