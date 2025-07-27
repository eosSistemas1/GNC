using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using TalleresWeb.Entities;
using TalleresWeb.DataAccess;


namespace TalleresWeb.Logic
{
    public class ValvulasLogic : EntityManagerLogic<Valvula, ValvulaExtendedView, ValvulaParameters, ValvulaDataAccess>
    {
        #region Methods

        public List<Valvula> ReadDetalladoByID(Guid ID)
        {
            ValvulaDataAccess oa = new ValvulaDataAccess();
            return oa.ReadDetalladoByID(ID);
        }
        

        public void AddValvula(Valvula Valvula)
        {
            var existeValvula = this.Read(Valvula.ID) != null;

            //add o update 
            if (!existeValvula)
            {
                this.Add(Valvula);
            }
            else
            {
                this.Update(Valvula);
            }
        }


        public List<Valvula> ReadByCodigoHomologacion(String codHomologacion)
        {
            return this.EntityDataAccess.ReadByCodigoHomologacion(codHomologacion);
        }

        public List<String> ReadListCodigosHomologacion(String codigoHomologacion)
        {
            return this.EntityDataAccess.ReadListCodigosHomologacion(codigoHomologacion);
        }
        #endregion
        public List<String> ReadValvulaYMarca(String codigoHomologacion)
        {
            return this.EntityDataAccess.ReadValvulaYMarca(codigoHomologacion);
        }

    }
}