using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Obleas
{
    public partial class CargarResultadosEnte : PageBase
    {
        #region Properties
        private ObleasLogic obleasLogic;
        private ObleasLogic ObleasLogic
        {
            get
            {
                if (this.obleasLogic == null)
                    this.obleasLogic = new ObleasLogic();

                return this.obleasLogic;
            }
        }

        private InformeLogic informeLogic;
        private InformeLogic InformeLogic
        {
            get
            {
                if (this.informeLogic == null)
                    this.informeLogic = new InformeLogic();

                return this.informeLogic;
            }
        }

        private InformeDetalleLogic informeDetalleLogic;
        private InformeDetalleLogic InformeDetalleLogic
        {
            get
            {
                if (this.informeDetalleLogic == null)
                    this.informeDetalleLogic = new InformeDetalleLogic();

                return this.informeDetalleLogic;
            }
        }

        private Guid? InformeSeleccionadoID
        {
            get { return ViewState["IDINFORME"] != null ? new Guid(ViewState["IDINFORME"].ToString()) : default(Guid?); }
            set { ViewState["IDINFORME"] = value; }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BorrarInformeSeleccionado();

                this.CargarInformes();
            }
        }

        private void BorrarInformeSeleccionado()
        {
            lblTitulo.Text = String.Empty;
            this.InformeSeleccionadoID = default(Guid?);
        }

        private void CargarInformes()
        {
            List<InformesPendientesView> obleasInformes = InformeLogic.ReadAllInformePendiente();
            grdInformes.DataSource = obleasInformes;
            grdInformes.DataBind();
        }

        protected void grdInformes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;

            Guid informeID = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());
            this.InformeSeleccionadoID = informeID;

            GridViewRow fila = grd.Rows[int.Parse(e.CommandArgument.ToString())];

            panelArchivos.Visible = false;

            if (e.CommandName == "seleccionar")
            {
                lblTitulo.Text = String.Format("CARGAR ARCHIVOS PARA EL INFORME NUMERRO: {0} - {1}", fila.Cells[0].Text, DateTime.Parse(fila.Cells[1].Text).ToString("dd/MM/yyyy"));
                panelArchivos.Visible = true;
            }

            List<InformeDetalleBasicView> detalleObleas = null;
            String titulo = String.Empty;

            if (e.CommandName == "enviadas")
            {
                detalleObleas = this.InformeDetalleLogic.ReadDetalleNumerosObleasEnviadas(informeID);
                titulo = "Obleas Enviadas: ";
            }

            if (e.CommandName == "asignadas")
            {
                detalleObleas = this.InformeDetalleLogic.ReadDetalleNumerosObleasAsignadas(informeID);
                titulo = "Obleas Asignadas: ";
            }

            if (e.CommandName == "rechazadas")
            {
                detalleObleas = this.InformeDetalleLogic.ReadDetalleNumerosObleasRechazadas(informeID);
                titulo = "Obleas Rechazadas: ";
            }

            //titulo = "Obleas " + titulo + DateTime.Parse(fila.Cells[1].Text).ToString("dd/MM/yyyy") + " - Informe Nro: " + fila.Cells[0].Text;

            if (e.CommandName == "enviadas" || e.CommandName == "asignadas" || e.CommandName == "rechazadas")
            {
                lblDetalle.Text = titulo;
                grdDetalle.DataSource = detalleObleas;
                grdDetalle.DataBind();
                mpeDetalle.Show();
            }

            if (e.CommandName == "eliminar")
            {
                mpeEliminar.Show();
            }

            if (e.CommandName == "cerrar")
            {
                this.CerrarInforme();
            }
        }

        protected void grdInformes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int enviadas = int.Parse(((LinkButton)e.Row.Cells[2].Controls[0]).Text);
                int asignadas = int.Parse(((LinkButton)e.Row.Cells[3].Controls[0]).Text);
                int rechazadas = int.Parse(((LinkButton)e.Row.Cells[4].Controls[0]).Text);
                int bajas = int.Parse(grdInformes.DataKeys[e.Row.RowIndex].Values["CantidadObleasBaja"].ToString());

                e.Row.Cells[5].FindControl("btnSeleccionar").Visible = enviadas > (asignadas + rechazadas + bajas);
                e.Row.Cells[6].FindControl("btnEliminar").Visible = asignadas == 0 && rechazadas == 0 && enviadas > bajas;
                e.Row.Cells[7].FindControl("btnCorrecta").Visible = bajas > 0;
            }
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            if (!InformeSeleccionadoID.HasValue || InformeSeleccionadoID != Guid.Empty)
            {

                if (this.fuArchivoOK.HasFile || this.fuArchivoErrores.HasFile)
                {
                    //genero el informe
                    Guid? informeID = InformeSeleccionadoID;

                    //Proceso Archivo OK
                    Int32 fichasOK = this.ProcesarArchivoOK(informeID.Value);

                    //Proceso Archivo Errores
                    Int32 fichasError = this.ProcesarArchivoErrores(informeID.Value);

                    //TODO: Copiar los archivos del ente a la carpeta correspondiente

                    this.InformeLogic.ActualizarInforme(informeID.Value, fichasOK, fichasError);

                    String mensaje = String.Format("Se actualizaron {0} obleas OK. <br>  Se actualizaron {1} obleas Con Error.", fichasOK, fichasError);
                    this.MessageBoxCtrl.MessageBox(null, mensaje, UserControls.MessageBoxCtrl.TipoWarning.Success);

                    this.CargarInformes();
                }
                else
                {
                    this.MessageBoxCtrl.MessageBox(null, "Debe ingresar al menos un archivo", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            else
            {
                this.MessageBoxCtrl.MessageBox(null, "Debe seleccionar un informe", UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        protected void btnCancelar_ServerClick(object sender, EventArgs e)
        {
            InformeSeleccionadoID = default(Guid?);
            panelArchivos.Visible = false;
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

        private Int32 ProcesarArchivoErrores(Guid informeID)
        {
            Int32 cantidadFichasActualizadas = 0;

            if (this.fuArchivoErrores.HasFile)
            {
                try
                {
                    StreamReader reader = new StreamReader(this.fuArchivoErrores.FileContent);
                    do
                    {
                        String textLine = reader.ReadLine();

                        if (textLine.Contains("PEC NO TIENE OBLEAS DEL AÑO  PARA ASIGNAR"))
                        {
                            throw new Exception("EL PEC NO TIENE OBLEAS DEL AÑO  PARA ASIGNAR");
                        }

                        Int32 ficha = this.ParsearYActualizarObleaErronea(textLine, informeID);
                        cantidadFichasActualizadas += ficha;

                    } while (reader.Peek() != -1);
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBoxCtrl.MessageBox(null, $"No se puede procesar el archivo de errores : <br> {e.Message}", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }

            return cantidadFichasActualizadas;
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

            return this.ObleasLogic.ActualizarObleaAsignada(oblea, informeID, this.UsuarioID);
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

            return ObleasLogic.ActualizarObleaErrorAsignada(oblea, informeID, this.UsuarioID);
        }

        #endregion

        #region Cerrar Informe
        //Cuando el informe tiene bajas, se debe cerrar ya que el ente no devuelve el ok de las mismas
        private void CerrarInforme()
        {
            if (!InformeSeleccionadoID.HasValue || InformeSeleccionadoID.Value == Guid.Empty)
            {
                MessageBoxCtrl.MessageBox(null, "Debe seleccionar un informe.", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                mpeBajas.Hide();
            }
            else
            {
                var bajas = this.InformeDetalleLogic.ReadBajasByInformeID(InformeSeleccionadoID.Value);

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

        protected void BtnAceptarBajas_Click(object sender, EventArgs e)
        {
            List<Guid> fichasSeleccionadas = new List<Guid>();
            foreach (GridViewRow gr in grdFichasBajas.Rows)
            {
                Guid idFicha = new Guid(grdFichasBajas.DataKeys[gr.RowIndex].Values["ID"].ToString());
                CheckBox chk = (CheckBox)gr.Cells[4].Controls[1];
                if (chk.Checked) fichasSeleccionadas.Add(idFicha);
            }

            if (!fichasSeleccionadas.Any())
            {
                mpeBajas.Show();
            }
            else
            {
                try
                {
                    int cantidad = this.InformeLogic.CerrarInforme(InformeSeleccionadoID.Value, fichasSeleccionadas, this.UsuarioID);
                    String mensajeOk = String.Format("Se cerraron {0} obleas correctamente.", cantidad);

                    MessageBoxCtrl.MessageBox(null, mensajeOk, UserControls.MessageBoxCtrl.TipoWarning.Success);

                    InformeSeleccionadoID = default(Guid?);
                }
                catch (Exception ex)
                {
                    MessageBoxCtrl.MessageBox(null, ex.Message, UserControls.MessageBoxCtrl.TipoWarning.Error);
                }

                this.CargarInformes();
            }
        }
        #endregion

        #region Eliminar Informe
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (InformeSeleccionadoID.HasValue && InformeSeleccionadoID.Value != Guid.Empty)
                {
                    if (txtPass.Text.ToUpper() != CrossCutting.DatosDiscretos.GetDinamyc.PasswordEliminacion.ToUpper())
                    {
                        MessageBoxCtrl.MessageBox(null, "- La contraseña de autorización es incorrecta.", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                    }
                    else
                    {
                        this.InformeLogic.EliminarInforme(InformeSeleccionadoID.Value, this.UsuarioID);

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
                MessageBoxCtrl.MessageBox(null, $"No se pudo eliminar el informe </br> {ex.Message}", UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        protected void btnCancelarEliminar_Click(object sender, EventArgs e)
        {
            txtPass.Text = String.Empty;
            mpeEliminar.Hide();
            this.InformeSeleccionadoID = default(Guid?);
        }
        #endregion

    }
}