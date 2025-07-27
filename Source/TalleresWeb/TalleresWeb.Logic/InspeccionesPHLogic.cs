using PL.Fwk.BusinessLogic;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class InspeccionesPHLogic : EntityManagerLogic<InspeccionesPH, InspeccionesPHExtendedView, InspeccionesPHParameters, InspeccionesPHDataAccess>
    {
        #region Methods
        /// <summary>
        /// Borra las inspecciones guardadas y graba las que fueron seleccionadas
        /// </summary>
        /// <param name="inspecciones"></param>
        /// <param name="phcilindrosID"></param>
        public void SaveInspecciones(List<InspeccionesView> inspecciones, Guid phcilindrosID)
        {
            if (inspecciones == null) return;

            this.EntityDataAccess.DeleteByPhCilindrosIDAndTipo(phcilindrosID, inspecciones.FirstOrDefault().ID);
                     
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {                    
                    foreach (var item in inspecciones)
                    {                        
                        var inspeccion = new InspeccionesPH()
                        {
                            ID = Guid.NewGuid(),
                            IdInspeccion = item.ID,
                            IdPHCilndro = phcilindrosID,
                            ObservacionesInspeccion = item.Observacion,
                            ValorInspeccion = item.ValorInspeccion
                        };
                        this.EntityDataAccess.Add(inspeccion);
                    }

                    ss.Complete();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    ss.Dispose();
                }
            }
        }

        public List<InspeccionesPH> ReadAllInspeccionesByIDPhCil(Guid phcilindrosID)
        {
            return this.EntityDataAccess.ReadAllInspeccionesByIDPhCil(phcilindrosID);
        }
        #endregion
    }
}