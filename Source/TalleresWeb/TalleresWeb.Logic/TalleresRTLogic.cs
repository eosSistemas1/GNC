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
    public class TalleresRTLogic : EntityManagerLogic<TalleresRT, TalleresRTExtendedView, TalleresRTParameters, TalleresRTDataAccess>
    {
        #region Methods

        public override ViewEntity Add(TalleresRT entity)
        {
            if (entity.EsRTPrincipal)
            {
                this.EntityDataAccess.UncheckEsPrincipalByTallerID(entity.TalleresID);
            }

            return this.EntityDataAccess.Add(entity);
        }

        public override void Update(TalleresRT entity)
        {
            if (entity.EsRTPrincipal)
            {
                this.EntityDataAccess.UncheckEsPrincipalByTallerID(entity.TalleresID);
            }

            this.EntityDataAccess.Update(entity);
        }

        public void AddTallerRT(TalleresRT TalleresRT)
        {
            var existeTallerRT = this.Read(TalleresRT.ID) != null;

            //add o update 
            if (!existeTallerRT)
            {
                this.Add(TalleresRT);
            }
            else
            {
                this.Update(TalleresRT);
            }
        }

        /// <summary>
        /// Recupera los RT con contrato vigente de un taller
        /// </summary>
        /// <param name="tallerID"></param>
        /// <returns></returns>
        public List<TalleresRTExtendedView> ReadByTallerID(Guid tallerID)
        {
            return EntityDataAccess.ReadByTallerID(tallerID);
        }

        /// <summary>
        /// Recupera todos los RT de un taller
        /// </summary>
        /// <param name="tallerID"></param>
        /// <returns></returns>
        public List<TalleresRTExtendedView> ReadAllByTallerID(Guid tallerID)
        {
            return EntityDataAccess.ReadAllByTallerID(tallerID);
        }
        #endregion
    }
}
