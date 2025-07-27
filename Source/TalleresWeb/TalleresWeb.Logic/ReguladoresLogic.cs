using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using TalleresWeb.Entities;
using TalleresWeb.DataAccess;

namespace TalleresWeb.Logic
{
    public class ReguladoresLogic : EntityManagerLogic<Reguladores, ReguladoresExtendedView, ReguladoresParameters, ReguladoresDataAccess>
    {
        #region Methods
        public List<Reguladores> ReadDetalladoByID(Guid ID)
        {
            ReguladoresDataAccess oa = new ReguladoresDataAccess();
            return oa.ReadDetalladoByID(ID);
        }

        public List<Reguladores> ReadByCodigoHomologacion(String codHomologacion)
        {
            return this.EntityDataAccess.ReadByCodigoHomologacion(codHomologacion);
        }

        public void AddRegulador(Reguladores Reguladores)
        {
            var existeRegulador = this.Read(Reguladores.ID) != null;

            //add o update 
            if (!existeRegulador)
            {
                this.Add(Reguladores);
            }
            else
            {
                this.Update(Reguladores);
            }
        }

        public List<String> ReadListReguladores(String reguladores)
        {
            return this.EntityDataAccess.ReadListReguladores(reguladores);
        }

        public List<String> ReadReguladorYMarca(String codigoHomologacion)
        {
            return this.EntityDataAccess.ReadReguladorYMarca(codigoHomologacion);
        }

        public List<String> ReadListCodigosHomologacion(String codigoHomologacion)
        {
            return this.EntityDataAccess.ReadListCodigosHomologacion(codigoHomologacion);
        }
        #endregion
    }
}