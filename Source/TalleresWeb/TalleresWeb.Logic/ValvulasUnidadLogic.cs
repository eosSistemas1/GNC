using CrossCutting.DatosDiscretos;
using PL.Fwk.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class ValvulaUnidadLogic : EntityManagerLogic<Valvula_Unidad, ValvulaUnidadExtendedView, ValvulaUnidadParameters, ValvulaUnidadDataAccess>
    {
        #region Properties
        private ValvulasLogic valvulaLogic;
        public ValvulasLogic ValvulaLogic
        {
            get
            {
                if (this.valvulaLogic == null) this.valvulaLogic = new ValvulasLogic();
                return this.valvulaLogic;
            }
        }
        #endregion


        #region Methods

        public List<Valvula_Unidad> ReadValvulaUnidad(Guid idValvula, String nroSerie)
        {
            return EntityDataAccess.ReadValvulaUnidad(idValvula, nroSerie);
        }

        /// <summary>
        /// Lee un valvula unidad por codigo homologacion y serie y si no existe lo crea
        /// </summary>
        /// <param name="codigoHomologacionValvula"></param>
        /// <param name="numeroSerieValvula"></param>
        /// <returns></returns>
        public Guid LeerCrearValvulaUnidad(string codigoHomologacionValvula, 
                                           string numeroSerieValvula)
        {
            Valvula valvula = this.ValvulaLogic.ReadByCodigoHomologacion(codigoHomologacionValvula).FirstOrDefault();
            if (valvula == null)
            {
                valvula = new Valvula();
                valvula.ID = Guid.NewGuid();
                valvula.IdMarcaValvula = MARCASINEXISTENTES.Valvulas;
                valvula.Descripcion = codigoHomologacionValvula;
                this.ValvulaLogic.Add(valvula);
            }

            Valvula_Unidad valvulaUnidad = this.ReadValvulaUnidad(valvula.ID, numeroSerieValvula).FirstOrDefault();
            if (valvulaUnidad == null)
            {                           
                valvulaUnidad = new Valvula_Unidad();
                valvulaUnidad.ID = Guid.NewGuid();
                valvulaUnidad.IdValvula = valvula.ID;
                valvulaUnidad.Descripcion = numeroSerieValvula;                
                this.EntityDataAccess.Add(valvulaUnidad);
            }

            return valvulaUnidad.ID;
        }
        #endregion
    }
}