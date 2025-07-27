using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using System.Drawing;

namespace PetroleraManager.Web.Tramites
{
    public partial class PhverResumen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PHCilindros datos = (PHCilindros)Session["PH_RESUMEN"];

                PHCilindrosLogic logic = new PHCilindrosLogic();
                var cilindro = logic.ReadDetallado(datos.ID);

                txtDatosNroOperacion.Text = "falta";
                txtDatosFecha.Text = cilindro.FechaHabilitacion.ToString("dd/MM/yyyy");
                txtDatosSerieCilindro.Text = cilindro.SerieCil;
                txtDatosCodHomCilindro.Text = cilindro.CodigoCil;
                txtDatosSerieValvula.Text = cilindro.SerieVal;
                txtDatosCodHomValvula.Text = cilindro.CodigoVal;
                txtDatosCliente.Text = cilindro.NombreyApellido;
                txtDatosDominio.Text = cilindro.Dominio;
                txtDatosTaller.Text = cilindro.Taller;
                txtDatosObservacion.Text = cilindro.Observacion;

                lblPesoVacioMarcado.Text = datos.PesoMarcadoCilindro.HasValue ? datos.PesoMarcadoCilindro.Value.ToString() : "0";
                lblPesoVacioActual.Text = datos.PesoVacioCilindro.HasValue ? datos.PesoVacioCilindro.Value.ToString() : "0";
                lblPesoConAgua.Text = datos.PesoAguaCilindro.HasValue ? datos.PesoAguaCilindro.Value.ToString() : "0";
                lblCapacidadMarcado.Text = datos.CapacidadMarcada.ToString();

                lblLectParedMin.Text = datos.LecturaAParedCilindro.HasValue ? datos.LecturaAParedCilindro.Value.ToString() : "0";
                lblLectParedMax.Text = datos.LecturaBParedCilindro.HasValue ? datos.LecturaBParedCilindro.Value.ToString() : "0";
                lblLectFondo.Text = datos.LecturaAFondoCilindro.HasValue ? datos.LecturaAFondoCilindro.Value.ToString() : "0"; 
                lblFondo.Text = datos.TipoFondoCilindro.ToString();

                lblLecturaBuretaMin.Text = datos.LecturaMinBureta.HasValue ? datos.LecturaMinBureta.Value.ToString() : "0";
                lblLecturaBuretaMAX.Text = datos.LecturaMaxBureta.HasValue ? datos.LecturaMaxBureta.Value.ToString() : "0";
                lblNroBureta.Text = datos.NroBureta.HasValue ? datos.NroBureta.Value.ToString() : "0";
                lblPresionPruebaCilindro.Text = datos.PresionPruebaCilindro.HasValue ? datos.PresionPruebaCilindro.Value.ToString() : "0";

                lblEstado.Text = datos.Rechazado.Value ? "RECHAZADO" : "APROBADO";
                lblEstado.ForeColor = lblEstado.Text == "APROBADO" ? Color.Green : Color.Red;  
            }
        }
    }
}