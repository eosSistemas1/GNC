using CrossCutting.DatosDiscretos;
using PL.Fwk.Presentation.Web.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.Tramites
{
    public partial class CargarResultadosEnte : PageBase
    {
        #region Members

        private ObleasLogic obleasLogic;
        private InformeLogic informeLogic;
        private InformeDetalleLogic informeDetalleLogic;

        private Guid InformeSeleccionadoID
        {
            get { return ViewState["IDINFORME"] != null ? new Guid(ViewState["IDINFORME"].ToString()) : Guid.Empty; }
            set { ViewState["IDINFORME"] = value; }
        }

        #endregion

        #region Properties
        public ObleasLogic ObleasLogic
        {
            get
            {
                if (this.obleasLogic == null)
                    this.obleasLogic = new ObleasLogic();

                return this.obleasLogic;
            }
        }
        public InformeLogic InformeLogic
        {
            get
            {
                if (this.informeLogic == null)
                    this.informeLogic = new InformeLogic();

                return this.informeLogic;
            }
        }
        public InformeDetalleLogic InformeDetalleLogic
        {
            get
            {
                if (this.informeDetalleLogic == null)
                    this.informeDetalleLogic = new InformeDetalleLogic();

                return this.informeDetalleLogic;
            }
        }
        #endregion

        #region Methods

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            if (InformeSeleccionadoID != Guid.Empty)
            {

                if (this.fuArchivoOK.HasFile || this.fuArchivoErrores.HasFile)
                {
                    //genero el informe
                    Guid informeID = InformeSeleccionadoID;

                    //Proceso Archivo OK
                    Int32 fichasOK = this.ProcesarArchivoOK(informeID);

                    //Proceso Archivo Errores
                    Int32 fichasError = this.ProcesarArchivoErrores(informeID);

                    //TODO: Copiar los archivos del ente a la carpeta correspondiente

                    this.InformeLogic.ActualizarInforme(informeID, fichasOK, fichasError);

                    String mensaje = String.Format("Se actualizaron {0} obleas OK. <br>  Se actualizaron {1} obleas Con Error.", fichasOK, fichasError);
                    this.MessageBoxCtrl.MessageBox(null, mensaje, UserControls.MessageBoxCtrl.TipoWarning.Success);

                    this.CargarInformes();
                }
                else
                {
                    this.MessageBoxCtrl.MessageBox(null, "Debe ingresar al menos un archivo", PetroleraManager.Web.UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            else
            {
                this.MessageBoxCtrl.MessageBox(null, "Debe seleccionar un informe", PetroleraManager.Web.UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTitulo.Text = String.Empty;
            }

            this.CargarInformes();

        }

        private void CargarInformes()
        {
            var obleasInformes = InformeLogic.ReadAllInformePendiente().ToList();
            grdInformes.DataSource = obleasInformes;
            grdInformes.DataBind();
        }

        private Int32 ParsearYActualizarObleaErronea(String textLine, Guid informeID)
        {
            String[] fila = textLine.Split(';');

            if (fila.Length != 4) return 0;

            ObleaCargaResultadosView oblea = new ObleaCargaResultadosView();
            oblea.PEC = fila[0].ToString();
            oblea.CUIT = fila[1].ToString();
            oblea.NroInternoObleaTaller = fila[2].ToString();
            oblea.CodigoTaller = fila[3].Substring(0, 7).ToUpper().Trim();

            oblea.DescripcionError = fila[3].Remove(0, 7).Trim().ToString();

            return this.ObleasLogic.ActualizarObleaErrorAsignada(oblea, informeID, SiteMaster.IdUsuarioLogueado);
        }

        private Int32 ParsearYActualizarObleaOK(String textLine, Guid informeID)
        {
            String[] fila = textLine.Split(';');

            if (fila.Length != 6) return 0;

            ObleaCargaResultadosView oblea = new ObleaCargaResultadosView();
            oblea.PEC = fila[0].ToString();
            oblea.CUIT = fila[1].ToString();
            oblea.CodigoTaller = fila[2].ToString();
            oblea.NroInternoObleaTaller = fila[3].ToString();
            oblea.NroObleaAsignada = fila[4].ToString();
            oblea.Dominio = fila[5].ToString();

            return this.ObleasLogic.ActualizarObleaAsignada(oblea, informeID, SiteMaster.IdUsuarioLogueado);
        }

        private Int32 ProcesarArchivoErrores(Guid informeID)
        {
            Int32 cantidadFichasActualizadas = 0;

            if (this.fuArchivoErrores.HasFile)
            {
                StreamReader reader = new StreamReader(this.fuArchivoErrores.FileContent);
                do
                {
                    String textLine = reader.ReadLine();

                    Int32 ficha = this.ParsearYActualizarObleaErronea(textLine, informeID);
                    cantidadFichasActualizadas += ficha;

                } while (reader.Peek() != -1);
                reader.Close();
            }

            return cantidadFichasActualizadas;
        }

        private Int32 ProcesarArchivoOK(Guid informeID)
        {
            Int32 cantidadFichasActualizadas = 0;

            if (this.fuArchivoOK.HasFile)
            {
                StreamReader reader = new StreamReader(this.fuArchivoOK.FileContent);
                Boolean primeraLinea = true;
                do
                {
                    if (primeraLinea)
                    {
                        reader.ReadLine();
                        primeraLinea = false;
                        continue;
                    }

                    String textLine = reader.ReadLine();

                    if (!String.IsNullOrWhiteSpace(textLine))
                    {
                        Int32 ficha = this.ParsearYActualizarObleaOK(textLine, informeID);
                        cantidadFichasActualizadas += ficha;
                    }
                } while (reader.Peek() != -1);
                reader.Close();
            }

            return cantidadFichasActualizadas;
        }

        protected void grdInformes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblTitulo.Text = String.Empty;

            GridView grd = (GridView)sender;
            Guid informeID = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            GridViewRow fila = grd.Rows[int.Parse(e.CommandArgument.ToString())];

            if (e.CommandName == "seleccionar")
            {
                lblTitulo.Text = String.Format("CARGAR ARCHIVOS PARA EL INFORME NUMERRO: {0} - {1}", fila.Cells[0].Text, DateTime.Parse(fila.Cells[1].Text).ToString("dd/MM/yyyy"));
                panelArchivos.Visible = true;
                InformeSeleccionadoID = informeID;
            }

            List<InformeDetalleBasicView> detalleObleas = null;
            String titulo = String.Empty;
            if (e.CommandName == "enviadas")
            {
                detalleObleas = this.InformeDetalleLogic.ReadDetalleNumerosObleasEnviadas(informeID);
                titulo = "Enviadas: ";
            }

            if (e.CommandName == "asignadas")
            {
                detalleObleas = this.InformeDetalleLogic.ReadDetalleNumerosObleasAsignadas(informeID);
                titulo = "Asignadas: ";
            }

            if (e.CommandName == "rechazadas")
            {
                detalleObleas = this.InformeDetalleLogic.ReadDetalleNumerosObleasRechazadas(informeID);
                titulo = "Rechazadas: ";
            }


            titulo = "Obleas " + titulo + DateTime.Parse(fila.Cells[1].Text).ToString("dd/MM/yyyy") + " - Informe Nro: " + fila.Cells[0].Text;

            if (e.CommandName == "enviadas" || e.CommandName == "asignadas" || e.CommandName == "rechazadas")
            {
                String mensaje = this.ArmarMensajeObleas(titulo, detalleObleas);

                if (!String.IsNullOrEmpty(mensaje))
                {
                    MessageBoxCtrl.MessageBox(null, mensaje, UserControls.MessageBoxCtrl.TipoWarning.Info);
                }
                else
                {
                    MessageBoxCtrl.MessageBox(null, "No hay información para mostrar.", UserControls.MessageBoxCtrl.TipoWarning.Info);
                }
            }

            if (e.CommandName == "eliminar")
            {
                pnlEliminar.Visible = true;
                hddInformeID.Value = informeID.ToString();
            }

            if (e.CommandName == "cerrar")
            {
                this.CerrarInforme(informeID);
            }
        }

        private void CerrarInforme(Guid informeID)
        {
            hddInformeBajasID.Value = informeID.ToString();

            if (String.IsNullOrWhiteSpace(hddInformeBajasID.Value))
            {
                MessageBoxCtrl.MessageBox(null, "Debe seleccionar un informe.", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                mpeBajas.Hide();
            }
            else
            {          
                var bajas = this.InformeDetalleLogic.ReadBajasByInformeID(informeID);

                if (bajas.Any())
                {
                    grdFichasBajas.DataSource = bajas;
                    grdFichasBajas.DataBind();

                    mpeBajas.Show();
                }
                else
                {
                    MessageBoxCtrl.MessageBox(null, "El informe seleccionado no contiene Bajas.", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }
        }

        private string ArmarMensajeObleas(String titulo, List<InformeDetalleBasicView> detalleObleas)
        {
            if (!detalleObleas.Any()) return String.Empty;

            String mensaje = "<table border='1' colspan='2'>";
            mensaje += "<tr><td colspan='4'>" + titulo + "</td></tr>";
            mensaje += "<tr><td style='width: 15%'>Dominio</td><td style='width: 15%'>Oblea Anterior</td><td>Taller</td><td style='width: 15%'>Operación</td></tr>";

            foreach (InformeDetalleBasicView item in detalleObleas)
            {
                mensaje += "<tr>";
                mensaje += String.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>", item.Dominio,
                                                                                            item.NumeroObleaAnterior,
                                                                                            item.Taller,
                                                                                            item.Operacion);
                mensaje += "</tr>";
            }

            mensaje += "</table>";

            return mensaje;
        }

        protected void grdInformes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int enviadas = int.Parse(((LinkButton)e.Row.Cells[2].Controls[0]).Text);
                int asignadas = int.Parse(((LinkButton)e.Row.Cells[3].Controls[0]).Text);
                int rechazadas = int.Parse(((LinkButton)e.Row.Cells[4].Controls[0]).Text);

                if (enviadas == (asignadas + rechazadas)) e.Row.Cells[5].Controls[0].Visible = false;
                if (asignadas != 0 || rechazadas != 0) e.Row.Cells[6].Controls[0].Visible = false;

                Guid informeID = new Guid(grdInformes.DataKeys[e.Row.RowIndex].Values["ID"].ToString());
                var tieneBajas = this.InformeDetalleLogic.ReadBajasByInformeID(informeID).Any();

                e.Row.Cells[7].Controls[0].Visible = tieneBajas;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(hddInformeID.Value))
                {
                    Guid informeID = new Guid(hddInformeID.Value);

                    if (txtPass.Text.ToUpper() != GetDinamyc.PasswordEliminacion.ToUpper())
                    {
                        MessageBoxCtrl.MessageBox(null, "- La contraseña de autorización es incorrecta.", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                    }
                    else
                    {
                        this.InformeLogic.EliminarInforme(informeID, SiteMaster.IdUsuarioLogueado);

                        this.CargarInformes();

                        MessageBoxCtrl.MessageBox(null, "El informe se eliminó correctamente.", UserControls.MessageBoxCtrl.TipoWarning.Success);

                        btnCancelarEliminar_Click(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBoxCtrl.MessageBox(null, "- Debe seleccionar un informe para eliminar.", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            catch (Exception ex)
            {
                String mensaje = String.Format("No se pudo eliminar el informe </br> {0}", ex.Message);
                MessageBoxCtrl.MessageBox(null, mensaje, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        protected void btnCancelarEliminar_Click(object sender, EventArgs e)
        {
            txtPass.Text = String.Empty;
            pnlEliminar.Visible = false;
            hddInformeID.Value = String.Empty;
        }
        #endregion

        protected void BtnAceptarBajas_Click(object sender, EventArgs e)
        {
            List<Guid> fichasSeleccionadas = new List<Guid>();
            foreach (GridViewRow gr in grdFichasBajas.Rows)
            {
                Guid idFicha = new Guid(grdFichasBajas.DataKeys[gr.RowIndex].Values["ID"].ToString());
                PLCheckBox chk = (PLCheckBox)gr.Cells[4].Controls[1];
                if(chk.Checked) fichasSeleccionadas.Add(idFicha);
            }

            if (!fichasSeleccionadas.Any())
            {                
                mpeBajas.Show();
            }

            try
            {
                Guid informeID = new Guid(hddInformeBajasID.Value);

                int cantidad =  this.InformeLogic.CerrarInforme(informeID, fichasSeleccionadas, SiteMaster.IdUsuarioLogueado);
                String mensajeOk = String.Format("Se cerraron {0} obleas correctamente.", cantidad);

                MessageBoxCtrl.MessageBox(null, mensajeOk, UserControls.MessageBoxCtrl.TipoWarning.Success);

                hddInformeBajasID.Value = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox(null, ex.Message, UserControls.MessageBoxCtrl.TipoWarning.Error);
            }

            this.CargarInformes();
        }
    }
}