using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using TalleresWeb.Entities;
using TalleresWeb.DataAccess;

namespace TalleresWeb.Logic
{
    public class RTLogic : EntityManagerLogic<RT, RTExtendedView, RTParameters, RTDataAccess>
    {
        #region Methods

        public List<RT> ReadDetalladoByID(Guid ID)
        {
            RTDataAccess oa = new RTDataAccess();
            return oa.ReadDetalladoByID(ID);
        }
        public List<RT> ReadByRT(String RT)
        {
            return this.EntityDataAccess.ReadByRT(RT);
        }

        public void AddRT(RT RT)
        {
            var existeRT = this.Read(RT.ID) != null;

            //add o update 
            if (!existeRT)
            {
                this.Add(RT);
            }
            else
            {
                this.Update(RT);
            }
        }
        public List<String> ReadListRT(String rt)
        {
            return this.EntityDataAccess.ReadListRT(rt);
        }

        #endregion
    }
}