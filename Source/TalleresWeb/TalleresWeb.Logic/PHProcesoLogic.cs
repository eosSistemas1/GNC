using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class PHProcesoLogic
    {
        #region Properties
        private PHCilindrosLogic phCilindrosLogic;
        private PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (phCilindrosLogic == null) phCilindrosLogic = new PHCilindrosLogic();
                return phCilindrosLogic;
            }
        }
        #endregion

        #region Methods

        public PasosProcesoPH? ObtenerProximoPasoPendiente(PHCilindros phCilindros)
        {
            if (phCilindros.IdEstadoPH == EstadosPH.Observar) return PasosProcesoPH.CilindrosObservados;

            if (EsInspeccionVisualPendiente(phCilindros)) return PasosProcesoPH.InspeccionVisual;

            if (EsPasoInspeccionPendiente(phCilindros, INSPECCIONTIPO.ROSCA)) return PasosProcesoPH.InspeccionRoscas;

            if (EsPasoInspeccionPendiente(phCilindros, INSPECCIONTIPO.EXTERIOR)) return PasosProcesoPH.InspeccionExterior;

            if (EsPasoMedicionEspesoresPendiente(phCilindros)) return PasosProcesoPH.MedicionEspesores;

            if (EsPasoRegistroPesoPendiente(phCilindros)) return PasosProcesoPH.RegistroPeso;
           
            if (EsPasoPruebaHidraulicaPendiente(phCilindros)) return PasosProcesoPH.PruebaHidraulica;

            if (EsPasoInspeccionPendiente(phCilindros, INSPECCIONTIPO.INTERIOR)) return PasosProcesoPH.InspeccionInterior;

            return null;
        }        

        public List<PasosProcesoPH> ObtenerPasosPendientesPH(Guid phCilindroID)
        {
            List<PasosProcesoPH> pasosPendientes = new List<PasosProcesoPH>();

            PHCilindros datosPH = this.PHCilindrosLogic.ReadPhCilindroDetallado(phCilindroID);

            if (EsInspeccionVisualPendiente(datosPH)) pasosPendientes.Add(PasosProcesoPH.InspeccionVisual);

            if (EsPasoInspeccionPendiente(datosPH, INSPECCIONTIPO.ROSCA)) pasosPendientes.Add(PasosProcesoPH.InspeccionRoscas);

            if (EsPasoInspeccionPendiente(datosPH, INSPECCIONTIPO.EXTERIOR)) pasosPendientes.Add(PasosProcesoPH.InspeccionExterior);

            if (EsPasoMedicionEspesoresPendiente(datosPH)) pasosPendientes.Add(PasosProcesoPH.MedicionEspesores);

            if (EsPasoRegistroPesoPendiente(datosPH)) pasosPendientes.Add(PasosProcesoPH.RegistroPeso);            

            if (EsPasoPruebaHidraulicaPendiente(datosPH)) pasosPendientes.Add(PasosProcesoPH.PruebaHidraulica);
           
            if (EsPasoInspeccionPendiente(datosPH, INSPECCIONTIPO.INTERIOR)) pasosPendientes.Add(PasosProcesoPH.InspeccionInterior);

            return pasosPendientes;
        }

        private bool EsInspeccionVisualPendiente(PHCilindros ph)
        {
            return !ph.InspeccionVisualCorrecta.HasValue;
        }

        private bool EsPasoRegistroPesoPendiente(PHCilindros ph)
        {
            if (!ph.PesoVacioCilindro.HasValue
                || !ph.PesoMarcadoCilindro.HasValue)
                return true;

            return false;
        }

        private bool EsPasoMedicionEspesoresPendiente(PHCilindros ph)
        {
            if (!ph.LecturaAParedCiindrol.HasValue ||
               !ph.LecturaBParedCilindro.HasValue ||
               !ph.LecturaAFondoCilindro.HasValue ||
                string.IsNullOrWhiteSpace(ph.TipoFondoCilindro)) return true;

            return false;
        }

        private bool EsPasoPruebaHidraulicaPendiente(PHCilindros ph)
        {
            if (!ph.Rechazado.HasValue) return true;

            return false;            
        }        

        private bool EsPasoInspeccionPendiente(PHCilindros ph, Guid inspeccionTipo)
        {
            var inspecciones = new InspeccionesPHLogic().ReadAllInspeccionesByIDPhCil(ph.ID);

            if (inspecciones == null || !inspecciones.Any(x => x.Inspecciones.IdInspeccionTipo == inspeccionTipo)) return true;

            return false;
        }
        #endregion
    }
}
