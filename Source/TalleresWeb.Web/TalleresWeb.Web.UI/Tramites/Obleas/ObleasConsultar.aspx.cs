using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI.Tramites.Obleas
{
    public partial class ObleasConsultar : PageBase
    {
        #region Properties
        private ObleasLogic obleasLogic;
        private ObleasLogic ObleasLogic
        {
            get
            {
                if (obleasLogic == null) obleasLogic = new ObleasLogic();
                return obleasLogic;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnBuscar_ServerClick(object sender, EventArgs e)
        {
            var mensaje = Validar();
            defaultItem.Visible = false;
            if (!mensaje.Any())
            {
                var fechaDesde = !string.IsNullOrWhiteSpace(calFechaD.Text) ? DateTime.Parse(calFechaD.Text) : GetDinamyc.MinDatetime;
                var fechaHasta = !string.IsNullOrWhiteSpace(calFechaH.Text) ? DateTime.Parse(calFechaH.Text) : GetDinamyc.MaxDatetime;
                var Dominio = dominio.Value.ToString();
                var numeroOblea = nroOblea.Text;
                var tallerID = SiteMaster.Taller.ID;

                var obleas = ObleasLogic.ReadObleasConsulta(fechaDesde, fechaHasta, Dominio, numeroOblea, tallerID);

                repeaterTaller.DataSource = obleas;
                repeaterTaller.DataBind();

                defaultItem.Text = $"({obleas.Count}) trámites encontrados.";
                defaultItem.Visible = true;
            }
            else
            {
                Master.MessageBox(null, mensaje, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private List<string> Validar()
        {
            var mensjes = new List<string>();

            if (string.IsNullOrWhiteSpace(calFechaD.Text) && string.IsNullOrWhiteSpace(calFechaH.Text) &&
                string.IsNullOrWhiteSpace(dominio.Value) && string.IsNullOrWhiteSpace(nroOblea.Text))
            {
                mensjes.Add("- Debe ingresar al menos un filtro.");
            }

            if (string.IsNullOrWhiteSpace(calFechaD.Text) && !string.IsNullOrWhiteSpace(calFechaH.Text))
            {
                mensjes.Add("- Debe ingresar fecha desde.");
            }

            if (!string.IsNullOrWhiteSpace(calFechaD.Text) && string.IsNullOrWhiteSpace(calFechaH.Text))
            {
                mensjes.Add("- Debe ingresar fecha hasta.");
            }

            if (!string.IsNullOrWhiteSpace(calFechaD.Text) && !string.IsNullOrWhiteSpace(calFechaH.Text))
            {
                try
                {
                    var fechaDesde = DateTime.Parse(calFechaD.Text);
                    var fechaHasta = DateTime.Parse(calFechaH.Text);
                   
                    if (fechaHasta.Date < fechaDesde.Date)
                    {
                        mensjes.Add("- La Fecha Hasta debe ser mayor a la Fecha Desde.");
                    }

                    TimeSpan ts = fechaHasta - fechaDesde;
                    int diasDiferencia = ts.Days;
                    if (diasDiferencia > 30)
                    {
                        mensjes.Add("- La diferencia entre las fechas no puede ser mayor a 30 dias ");
                    }
                }
                catch (Exception)
                {
                    mensjes.Add("- Alguna de las fechas ingresadas no tiene formato correcto.");
                }                
            }

            return mensjes;
        }
    }
}