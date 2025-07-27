using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class DespachoExtendedView : ViewEntity
    {
        public Guid IdTramite { get; set; }
        public Guid IdTaller { get; set; }
        public String RazonSocialTaller { get; set; }
        public DateTime FechaTramite { get; set; }
        public String TipoTramite { get; set; }
        public Guid? IDOperacion { get; set; }
        public String Operacion { get; set; }
        public Guid? IdVehiculo { get; set; }
        public String Dominio { get; set; }
        public Guid IdEstado { get; set; }
        public String Estado { get; set; }

        public String ObleaAnterior { get; set; }
        public String ObleaAsignada { get; set; }

        public TramiteDespachoView MapToTramiteDespachoView()
        {
            TramiteDespachoView tramiteDespachoView = new TramiteDespachoView();
            tramiteDespachoView.ID = this.ID;
            tramiteDespachoView.Fecha = this.FechaTramite;
            tramiteDespachoView.TipoTramite = this.TipoTramite;
            tramiteDespachoView.IDOperacion = this.IDOperacion;
            tramiteDespachoView.Operacion = this.Operacion;
            tramiteDespachoView.IdVehiculo = this.IdVehiculo;
            tramiteDespachoView.Dominio = this.Dominio;
            tramiteDespachoView.IdEstado = this.IdEstado;
            tramiteDespachoView.Estado = this.Estado;
            tramiteDespachoView.ObleaAnterior = this.ObleaAnterior;
            tramiteDespachoView.ObleaAsignada = this.ObleaAsignada;

            return tramiteDespachoView;
        }
    }
}
