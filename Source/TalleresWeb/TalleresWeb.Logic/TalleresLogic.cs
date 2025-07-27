using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using TalleresWeb.Entities;
using TalleresWeb.DataAccess;

namespace TalleresWeb.Logic
{
    public class TalleresLogic : EntityManagerLogic<Talleres, TalleresExtendedView, TalleresParameters, TalleresDataAccess>
    {
        public List<Talleres> ReadDetalladoByID(Guid ID)
        {
            TalleresDataAccess oa = new TalleresDataAccess();
            return oa.ReadDetalladoByID(ID);
        }


        public List<Talleres> ReadByTalleres(String Talleres)
        {
            return this.EntityDataAccess.ReadByTalleres(Talleres);
        }

        public void AddTaller(Talleres Talleres)
        {
            var existeTaller = this.Read(Talleres.ID) != null;

            //add o update 
            if (!existeTaller)
            {
                this.Add(Talleres);
            }
            else
            {
                this.Update(Talleres);
            }
        }


        /* public List<TalleresExtendedView> ReadExtendedViewByMatricula(TalleresParameters param)
         {
             TalleresDataAccess oa = new TalleresDataAccess();
             return oa.ReadExtendedViewByMatricula(param);
         }
         */

        public List<String> ReadListTalleres(String taller)
        {
            return this.EntityDataAccess.ReadListTalleres(taller);
        }

        /// <summary>
        /// Obtiene el proximo numero interno de operacion para el taller
        /// </summary>
        /// <param name="taller"></param>
        /// <returns></returns>
        public static int GetProximoNumeroInternoOperacion(Guid idTaller)
        {
            var taller = new TalleresLogic().Read(idTaller);
            return taller.UltimoNroIntOperacion + 1;
        }
        /// <summary>
        /// Actualiza el ultimo numero de operacion usado
        /// </summary>
        /// <param name="idTaller"></param>
        /// <param name="nroIntOperacTP"></param>
        public static void SetUltimoNumeroOperacionTaller(Guid idTaller, int nroIntOperacTP)
        {
            var tallerLogic = new TalleresLogic();
            var taller = tallerLogic.Read(idTaller);
            taller.UltimoNroIntOperacion = nroIntOperacTP;
            tallerLogic.Update(taller);
        }
    }
}
