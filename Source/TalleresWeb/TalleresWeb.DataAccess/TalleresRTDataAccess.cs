using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class TalleresRTDataAccess : EntityManagerDataAccess<TalleresRT, TalleresRTExtendedView, TalleresRTParameters, TalleresWebEntities>
    {
        #region Methods

         public override TalleresRT Read(Guid id)
       {
           using (var context = this.GetEntityContext())
           {
               var entity = from t in context.CreateQuery<TalleresRT>(this.EntityName)
                            .Include("Talleres")
                            .Where(x => x.ID == id)
                            select t;

               return entity.FirstOrDefault();
           }
       }
       



        /// <summary>
        /// desmarco a todos los RT el campo es principal por taller id
        /// </summary>
        /// <param name="tallerID"></param>
        public void UncheckEsPrincipalByTallerID(Guid tallerID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<TalleresRT>(this.EntityName)
                                             .Where(x => x.TalleresID == tallerID)
                                             select t;

                foreach (var item in query)
                {
                    item.EsRTPrincipal = false;                    
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Recupera los RT con contrato vigente de un taller
        /// </summary>
        /// <param name="tallerID"></param>
        /// <returns></returns>
        public List<TalleresRTExtendedView> ReadByTallerID(Guid tallerID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<TalleresRT>(this.EntityName)
                    .Where(x => x.TalleresID == tallerID
                             && !x.FechaHastaRTT.HasValue)

                            select new TalleresRTExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.RT.NombreApellidoRT,
                                EsRTPrincipal = t.EsRTPrincipal
                            };

                return query.ToList();
            }
        }

        /// <summary>
        /// Recupera todos los RT de un taller
        /// </summary>
        /// <param name="tallerID"></param>
        /// <returns></returns>
        public List<TalleresRTExtendedView> ReadAllByTallerID(Guid tallerID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<TalleresRT>(this.EntityName)
                    .Where(x => x.TalleresID == tallerID)

                            select new TalleresRTExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.RT.NombreApellidoRT,
                                TalleresID = t.TalleresID,
                                FechaDesdeRTT = t.FechaDesdeRTT,
                                FechaHastaRTT = t.FechaHastaRTT,
                                EsRTPrincipal = t.EsRTPrincipal
                            };

                return query.ToList();
            }
        }
        #endregion
    }
}