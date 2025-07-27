using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;

namespace PetroleraManager.Web.UserControls
{
    public partial class uscCargarCilindrosValvulas : System.Web.UI.UserControl
    {

        #region Properties
        public Guid TipoperacionID
        {
            get
            {
                if (ViewState["TIPOOPERACIONID"] == null) return Guid.Empty;

                return new Guid(ViewState["TIPOOPERACIONID"].ToString());
            }
            set
            {
                ViewState["TIPOOPERACIONID"] = value;                
            }
        }
        public List<ObleasCilindrosExtendedView> CilindrosCargados
        {
            get {
                return this.ObtenerCilindroCargados();
            }
        }     
        public List<ObleasValvulasExtendedView> ValvulasCargadas
        {
            get
            {
                return this.ObtenerValvulasCargadas();
            }
        }

        private List<DetalleCilindrosValvulasView> detalle
        {
            get
            {
                if (ViewState["DETALLECILVAL"] == null) ViewState["DETALLECILVAL"] = new List<DetalleCilindrosValvulasView>();

                return (List<DetalleCilindrosValvulasView>)ViewState["DETALLECILVAL"];
            }
            set
            {
                ViewState["DETALLECILVAL"] = value;

                this.BindearGrilla();
            }
        }

        //private ObleasCilindrosLogic obleasCilindrosLogic;
        //private ObleasCilindrosLogic ObleasCilindrosLogic
        //{
        //    get
        //    {
        //        if (obleasCilindrosLogic == null) obleasCilindrosLogic = new ObleasCilindrosLogic();
        //        return obleasCilindrosLogic;
        //    }
        //}

        //private ObleasValvulasLogic obleasValvulasLogic;
        //private ObleasValvulasLogic ObleasValvulasLogic
        //{
        //    get
        //    {
        //        if (obleasValvulasLogic == null) obleasValvulasLogic = new ObleasValvulasLogic();
        //        return obleasValvulasLogic;
        //    }
        //}
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                this.LimpiarDatos();
                this.BindearGrilla();

                cboMSDBCil.TipoOperacionID = this.TipoperacionID;
                cboMSDBVal1.TipoOperacionID = this.TipoperacionID;
                cboMSDBVal2.TipoOperacionID = this.TipoperacionID;
                cboMSDBCil.LoadData();
                cboMSDBVal1.LoadData();
                cboMSDBVal2.LoadData();
            }           
        }

        protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            DetalleCilindrosValvulasView item = this.CrearItem();

            List<String> mensajes = item.RegistroValido();
            this.ValidarTipoMSDB(mensajes, item);

            if (!mensajes.Any())
            {
                detalle.Add(item);

                this.BindearGrilla();

                this.LimpiarDatos();

                txtCodigoCil.Focus();
            }
            else
            {
                MessageBoxCtrl.MessageBox(null, mensajes, TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        /// <summary>
        /// Valido el tipo de operaciones y el MSDB
        /// </summary>        
        private void ValidarTipoMSDB(List<String> mensajes, DetalleCilindrosValvulasView item)
        {
            if (this.TipoperacionID == TIPOOPERACION.Conversion && 
                (
                    item.MSDBCilindroID != MSDB.Montaje ||
                    (!String.IsNullOrEmpty(item.CodigoValvula1) && !String.IsNullOrEmpty(item.NroSerieValvula1) && item.MSDBValvula1ID != MSDB.Montaje) ||
                    (!String.IsNullOrEmpty(item.CodigoValvula2) && !String.IsNullOrEmpty(item.NroSerieValvula2) && item.MSDBValvula2ID != MSDB.Montaje)
                )
               )
                mensajes.Add("- Solo se permite Montaje cuando el tipo de operación es conversión");

            if (this.TipoperacionID == TIPOOPERACION.RevisionAnual &&
                (
                    item.MSDBCilindroID != MSDB.Sigue ||
                    (!String.IsNullOrEmpty(item.CodigoValvula1) && !String.IsNullOrEmpty(item.NroSerieValvula1) && item.MSDBValvula1ID != MSDB.Sigue) ||
                    (!String.IsNullOrEmpty(item.CodigoValvula2) && !String.IsNullOrEmpty(item.NroSerieValvula2) && item.MSDBValvula2ID != MSDB.Sigue)
                )
               )
                mensajes.Add("- Solo se permite Sigue cuando el tipo de operación es Revisión Anual");

            if (this.TipoperacionID == TIPOOPERACION.Baja &&
                (
                    (item.MSDBCilindroID != MSDB.Baja && item.MSDBCilindroID != MSDB.Desmontaje) ||
                    (!String.IsNullOrEmpty(item.CodigoValvula1) && !String.IsNullOrEmpty(item.NroSerieValvula1) && item.MSDBValvula1ID != MSDB.Baja && item.MSDBValvula1ID != MSDB.Desmontaje) ||
                    (!String.IsNullOrEmpty(item.CodigoValvula2) && !String.IsNullOrEmpty(item.NroSerieValvula2) && item.MSDBValvula2ID != MSDB.Baja && item.MSDBValvula2ID != MSDB.Desmontaje)
                )
               )
                mensajes.Add("- Solo se permite Sigue cuando el tipo de operación es Revisión Anual");            
        }

        /// <summary>
        /// Crea el objeto de un item del detalle
        /// </summary>
        /// <returns></returns>
        private DetalleCilindrosValvulasView CrearItem()
        {
            updPanel.Update();
            DetalleCilindrosValvulasView item = new DetalleCilindrosValvulasView();

            item.IdObleaCilindro = !String.IsNullOrEmpty(hdnIdObleaCilindro.Value) ? new Guid(hdnIdObleaCilindro.Value) : Guid.NewGuid();
            item.IDCilindro = !String.IsNullOrEmpty(hdnIDCilindro.Value) ? new Guid(hdnIDCilindro.Value) : Guid.NewGuid();
            item.IDCilindroUnidad = !String.IsNullOrEmpty(hdnIDCilindroUnidad.Value) ? new Guid(hdnIDCilindroUnidad.Value) : Guid.NewGuid();

            item.IdObleaValvula1 = !String.IsNullOrEmpty(hdnIdObleaValvula1.Value) ? new Guid(hdnIdObleaValvula1.Value) : Guid.NewGuid();
            item.IDValvula1 = !String.IsNullOrEmpty(hdnIDValvula1.Value) ? new Guid(hdnIDValvula1.Value) : Guid.NewGuid();
            item.IDValvula1Unidad = !String.IsNullOrEmpty(hdnIDValvula1Unidad.Value) ? new Guid(hdnIDValvula1Unidad.Value) : Guid.NewGuid();

            item.IdObleaValvula2 = !String.IsNullOrEmpty(hdnIdObleaValvula2.Value) ? new Guid(hdnIdObleaValvula2.Value) : Guid.NewGuid();
            item.IDValvula2 = !String.IsNullOrEmpty(hdnIDValvula2.Value) ? new Guid(hdnIDValvula2.Value) : Guid.NewGuid();
            item.IDValvula2Unidad = !String.IsNullOrEmpty(hdnIDValvula2Unidad.Value) ? new Guid(hdnIDValvula2Unidad.Value) : Guid.NewGuid();

            item.CodigoCilindro = txtCodigoCil.Text.Trim().ToUpper();
            item.NroSerieCilindro = txtSerieCil.Text.Trim().ToUpper();
            item.CilindroFabMes = !String.IsNullOrEmpty(txtCilFabMes.Text)? int.Parse(txtCilFabMes.Text).ToString("00") : String.Empty;
            item.CilindroFabAnio = !String.IsNullOrEmpty(txtCilFabAnio.Text) ? int.Parse(txtCilFabAnio.Text).ToString("00") : String.Empty;
            item.CilindroRevMes = !String.IsNullOrEmpty(txtCilRevMes.Text) ? int.Parse(txtCilRevMes.Text).ToString("00") : String.Empty;
            item.CilindroRevAnio = !String.IsNullOrEmpty(txtCilRevAnio.Text) ? int.Parse(txtCilRevAnio.Text).ToString("00") : String.Empty;
            item.CRPCCilindroID = cboCilCRPC.SelectedValue;
            item.CRPCCilindro = cboCilCRPC.SelectedText.Trim().ToUpper();
            item.MSDBCilindroID = cboMSDBCil.SelectedValue;
            item.MSDBCilindro = cboMSDBCil.SelectedText.Trim().ToUpper();
            item.NroCertificadoPH = txtNroCertifPH.Text.Trim().ToUpper();

            item.CodigoValvula1 = txtCodigoVal1.Text.Trim().ToUpper();
            item.NroSerieValvula1 = txtSerieVal1.Text.Trim().ToUpper();
            item.MSDBValvula1 = cboMSDBVal1.SelectedText.Trim().ToUpper();
            item.MSDBValvula1ID = cboMSDBVal1.SelectedValue;

            item.CodigoValvula2 = txtCodigoVal2.Text.Trim().ToUpper();
            item.NroSerieValvula2 = txtSerieVal2.Text.Trim().ToUpper();
            item.MSDBValvula2 = cboMSDBVal2.SelectedText.Trim().ToUpper();
            item.MSDBValvula2ID = cboMSDBVal2.SelectedValue;

            item.RealizaPH = chkRealizaPH.Checked;
            return item;
        }

        /// <summary>
        /// Borra los datos del registro cargado
        /// </summary>
        private void LimpiarDatos()
        {
            hdnID.Value = String.Empty;

            hdnIdObleaCilindro.Value = String.Empty;
            hdnIDCilindro.Value = String.Empty;
            hdnIDCilindroUnidad.Value = String.Empty;

            hdnIdObleaValvula1.Value = String.Empty;
            hdnIDValvula1.Value = String.Empty;
            hdnIDValvula1Unidad.Value = String.Empty;

            hdnIdObleaValvula2.Value = String.Empty;
            hdnIDValvula2.Value = String.Empty;
            hdnIDValvula2Unidad.Value = String.Empty;

            txtCodigoCil.Text = String.Empty;
            txtSerieCil.Text = String.Empty;
            txtCilFabMes.Text = String.Empty;
            txtCilFabAnio.Text = String.Empty;
            txtCilRevMes.Text = String.Empty;
            txtCilRevAnio.Text = String.Empty;
            cboCilCRPC.SelectedIndex = -1;            
            cboMSDBCil.SelectedIndex = -1;            
            txtNroCertifPH.Text = String.Empty;

            txtCodigoVal1.Text = String.Empty;
            txtSerieVal1.Text = String.Empty;            
            cboMSDBVal1.SelectedIndex = -1;

            txtCodigoVal2.Text = String.Empty;
            txtSerieVal2.Text = String.Empty;
            cboMSDBVal2.SelectedIndex = -1;

            chkRealizaPH.Checked = false;

            updPanel.Update();
        }

        /// <summary>
        /// Bindea el detalle a la grilla
        /// </summary>
        private void BindearGrilla()
        {
            gdvCilindrosValvulas.DataSource = this.detalle;
            gdvCilindrosValvulas.DataBind();

            updPanel.Update();
        }

        protected void btnModificar_Click(object sender, ImageClickEventArgs e)
        {
            updPanel.Update();
            DetalleCilindrosValvulasView itemValidar = this.CrearItem();

            List<String> mensajes = itemValidar.RegistroValido();
            this.ValidarTipoMSDB(mensajes, itemValidar);

            if (!mensajes.Any())
            {
                Guid id = new Guid(hdnID.Value.ToString());
                DetalleCilindrosValvulasView item = this.detalle.First(d => d.ID == id);

                item.CodigoCilindro = txtCodigoCil.Text.Trim().ToUpper();
                item.NroSerieCilindro = txtSerieCil.Text.Trim().ToUpper();
                item.CilindroFabMes = !String.IsNullOrEmpty(txtCilFabMes.Text) ? int.Parse(txtCilFabMes.Text).ToString("00") : String.Empty;
                item.CilindroFabAnio = !String.IsNullOrEmpty(txtCilFabAnio.Text) ? int.Parse(txtCilFabAnio.Text).ToString("00") : String.Empty;
                item.CilindroRevMes = !String.IsNullOrEmpty(txtCilRevMes.Text) ? int.Parse(txtCilRevMes.Text).ToString("00") : String.Empty;
                item.CilindroRevAnio = !String.IsNullOrEmpty(txtCilRevAnio.Text) ? int.Parse(txtCilRevAnio.Text).ToString("00") : String.Empty;
                item.CRPCCilindroID = cboCilCRPC.SelectedValue;
                item.CRPCCilindro = cboCilCRPC.SelectedText.Trim().ToUpper();
                item.MSDBCilindroID = cboMSDBCil.SelectedValue;
                item.MSDBCilindro = cboMSDBCil.SelectedText.Trim().ToUpper();
                item.NroCertificadoPH = txtNroCertifPH.Text.Trim().ToUpper();

                item.CodigoValvula1 = txtCodigoVal1.Text.Trim().ToUpper();
                item.NroSerieValvula1 = txtSerieVal1.Text.Trim().ToUpper();
                item.MSDBValvula1 = cboMSDBVal1.SelectedText.Trim().ToUpper();
                item.MSDBValvula1ID = cboMSDBVal1.SelectedValue;

                item.CodigoValvula2 = txtCodigoVal2.Text.Trim().ToUpper();
                item.NroSerieValvula2 = txtSerieVal2.Text.Trim().ToUpper();
                item.MSDBValvula2 = cboMSDBVal2.SelectedText.Trim().ToUpper();
                item.MSDBValvula2ID = cboMSDBVal2.SelectedValue;

                item.RealizaPH = chkRealizaPH.Checked;               

                this.btnModificar.Visible = false;
                this.btnAgregar.Visible = true;

                this.BindearGrilla();

                this.LimpiarDatos();
                                
                txtCodigoCil.Focus();
            }
            else
            {
                MessageBoxCtrl.MessageBox(null, mensajes, TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            this.LimpiarDatos();

            this.BindearGrilla();
        }

        protected void gdvCilindrosValvulas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int i = Convert.ToInt32(e.CommandArgument);
                var filaGrilla = gdvCilindrosValvulas.Rows[i];

                #region MODIFICAR
                if (e.CommandName == "modificar")
                {                                       
                    this.CargarDatosAModificar(filaGrilla);

                    this.btnModificar.Visible = true;
                    this.btnAgregar.Visible = false;

                    this.txtCodigoCil.Focus();

                    return;
                }
                #endregion


                #region ELIMINAR
                if (e.CommandName == "eliminar")
                {
                    Guid idObleaCilindro = new Guid(gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IdObleaCilindro"].ToString());
                    Guid idObleaValvula1 = !String.IsNullOrEmpty(gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IdObleaValvula1"].ToString())?
                                            new Guid(gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IdObleaValvula1"].ToString()) : Guid.Empty;
                    Guid idObleaValvula2 = !String.IsNullOrEmpty(gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IdObleaValvula2"].ToString()) ?
                                            new Guid(gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IdObleaValvula2"].ToString()) : Guid.Empty;

                    this.detalle.RemoveAt(i);

                    this.BindearGrilla();
                }
                #endregion
                
            }
        }

        /// <summary>
        /// Carga el detalle en la grilla
        /// </summary>
        /// <param name="cilindros">cilindros</param>
        /// <param name="valvulas">valvulas</param>
        public void CargarDetalle(List<ObleasCilindrosExtendedView> cilindros, List<ObleasValvulasExtendedView> valvulas)
        {
            foreach (var cilindro in cilindros)
            {
                DetalleCilindrosValvulasView item = new DetalleCilindrosValvulasView();

                item.IdObleaCilindro = Guid.NewGuid();
                item.IDCilindro = cilindro.IDCil;
                item.IDCilindroUnidad = cilindro.IDCilUni;
                item.CodigoCilindro = cilindro.CodigoCil;
                item.NroSerieCilindro = cilindro.NroSerieCil;
                item.CilindroFabMes = int.Parse(cilindro.CilFabMes).ToString("00");
                item.CilindroFabAnio = int.Parse(cilindro.CilFabAnio).ToString("00");
                item.CilindroRevMes = int.Parse(cilindro.CilRevMes).ToString("00");
                item.CilindroRevAnio = int.Parse(cilindro.CilRevAnio).ToString("00");
                item.CRPCCilindroID = cilindro.CRPCCilID;
                item.CRPCCilindro = cilindro.CRPCCil;
                item.MSDBCilindroID = cilindro.MSDBCilID;
                item.MSDBCilindro = cilindro.MSDBCil;
                item.NroCertificadoPH = !String.IsNullOrWhiteSpace(cilindro.NroCertificadoPH)? cilindro.NroCertificadoPH.Trim().ToUpper() : String.Empty;                
                item.RealizaPH = false;

                var valvulasCargadas = valvulas.Where(v => v.IdObleaCil == cilindro.ID);

                var valvula1 = valvulasCargadas.FirstOrDefault();
                var valvula2 = valvulasCargadas.Count() > 1 ? valvulasCargadas.LastOrDefault() : null;

                if (valvula1 != null)
                {
                    item.IdObleaValvula1 = valvula1.ID;
                    item.IDValvula1 = valvula1.IDVal;
                    item.IDValvula1Unidad = valvula1.IDValUni;
                    item.CodigoValvula1 = valvula1.CodigoVal;
                    item.NroSerieValvula1 = valvula1.NroSerieVal;
                    item.MSDBValvula1 = valvula1.MSDBVal.ToUpper();
                    item.MSDBValvula1ID = valvula1.MSDBValID;
                }

                if (valvula2 != null)
                {
                    item.IdObleaValvula2 = valvula2.ID;
                    item.IDValvula2 = valvula2.IDVal;
                    item.IDValvula2Unidad = valvula2.IDValUni;
                    item.CodigoValvula2 = valvula2.CodigoVal;
                    item.NroSerieValvula2 = valvula2.NroSerieVal;
                    item.MSDBValvula2 = valvula2.MSDBVal;
                    item.MSDBValvula2ID = valvula2.MSDBValID;
                }

                this.detalle.Add(item);                
            }

            this.BindearGrilla();

            this.LimpiarDatos();
        }

        /// <summary>
        /// cargo los datos a modificar en la linea de edición
        /// </summary>
        /// <param name="filaGrilla">Fila de la grilla seleccionada</param>
        private void CargarDatosAModificar(GridViewRow filaGrilla)
        {
            hdnID.Value = gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["ID"].ToString();

            hdnIdObleaCilindro.Value = gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IdObleaCilindro"].ToString();
            hdnIDCilindro.Value = gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IDCilindro"].ToString();
            hdnIDCilindroUnidad.Value = gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IDCilindroUnidad"].ToString();

            hdnIdObleaValvula1.Value = gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IdObleaValvula1"].ToString();
            hdnIDValvula1.Value = gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IDValvula1"].ToString();
            hdnIDValvula1Unidad.Value = gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IDValvula1Unidad"].ToString();

            hdnIdObleaValvula2.Value = gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IdObleaValvula2"].ToString();
            hdnIDValvula2.Value = gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IDValvula2"].ToString();
            hdnIDValvula2Unidad.Value = gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["IDValvula2Unidad"].ToString();

            txtCodigoCil.Text = filaGrilla.Cells[0].Text;
            txtSerieCil.Text = filaGrilla.Cells[1].Text;
            txtCilFabMes.Text = filaGrilla.Cells[2].Text;
            txtCilFabAnio.Text = filaGrilla.Cells[3].Text;
            txtCilRevMes.Text = filaGrilla.Cells[4].Text;
            txtCilRevAnio.Text = filaGrilla.Cells[5].Text;
            cboCilCRPC.SelectedValue = new Guid(gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["CRPCCilindroID"].ToString());
            cboMSDBCil.SelectedValue = new Guid(gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["MSDBCilindroID"].ToString());
            txtNroCertifPH.Text = filaGrilla.Cells[8].Text;

            txtCodigoVal1.Text = filaGrilla.Cells[9].Text.Replace("&nbsp;", String.Empty);
            txtSerieVal1.Text = filaGrilla.Cells[10].Text.Replace("&nbsp;", String.Empty);
            cboMSDBVal1.SelectedValue = new Guid(gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["MSDBValvula1ID"].ToString());

            txtCodigoVal2.Text = filaGrilla.Cells[12].Text.Replace("&nbsp;", String.Empty);
            txtSerieVal2.Text = filaGrilla.Cells[13].Text.Replace("&nbsp;", String.Empty);
            cboMSDBVal2.SelectedValue = new Guid(gdvCilindrosValvulas.DataKeys[filaGrilla.RowIndex].Values["MSDBValvula2ID"].ToString());

            chkRealizaPH.Checked = ((CheckBox)filaGrilla.Cells[15].Controls[0]).Checked;

            updPanel.Update();
        }

        protected void gdvCilindrosValvulas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Text == "&nbsp;") cell.Text = String.Empty;
                }

                if (String.IsNullOrEmpty(e.Row.Cells[9].Text) &&
                    String.IsNullOrEmpty(e.Row.Cells[10].Text)) e.Row.Cells[11].Text = String.Empty;

                if (String.IsNullOrEmpty(e.Row.Cells[12].Text) &&
                    String.IsNullOrEmpty(e.Row.Cells[13].Text)) e.Row.Cells[14].Text = String.Empty;                
            }
        }

        /// <summary>
        /// devuelve una lista de cilindros extended view con los cilindros cargadas 
        /// </summary>
        private List<ObleasCilindrosExtendedView> ObtenerCilindroCargados()
        {
            List<ObleasCilindrosExtendedView> cilindros = new List<ObleasCilindrosExtendedView>();

            foreach (var item in this.detalle)
            {
                ObleasCilindrosExtendedView cilindro = new ObleasCilindrosExtendedView();
                cilindro.ID = item.IdObleaCilindro;
                cilindro.IDCil = item.IDCilindro;
                cilindro.IDCilUni = item.IDCilindroUnidad;
                cilindro.CodigoCil = item.CodigoCilindro;
                cilindro.NroSerieCil = item.NroSerieCilindro;
                cilindro.CilFabMes = item.CilindroFabMes;
                cilindro.CilFabAnio = item.CilindroFabAnio;
                cilindro.CilRevMes = item.CilindroRevMes;
                cilindro.CilRevAnio = item.CilindroRevAnio;
                cilindro.CRPCCilID = item.CRPCCilindroID;
                cilindro.CRPCCil = item.CRPCCilindro;
                cilindro.MSDBCilID = item.MSDBCilindroID;
                cilindro.MSDBCil = item.MSDBCilindro;
                cilindro.NroCertificadoPH = item.NroCertificadoPH.Trim().ToUpper();
                cilindro.RealizaPH = item.RealizaPH;                
                cilindros.Add(cilindro);
            }

            return cilindros;
        }

        /// <summary>
        /// devuelve una lista de obleas extended view con las valvulas cargadas 
        /// </summary>        
        private List<ObleasValvulasExtendedView> ObtenerValvulasCargadas()
        {
            List<ObleasValvulasExtendedView> valvulas = new List<ObleasValvulasExtendedView>();

            foreach (var item in this.detalle)
            {
                if (!String.IsNullOrEmpty(item.CodigoValvula1) && !String.IsNullOrEmpty(item.NroSerieValvula1) &&
                     !String.IsNullOrEmpty(item.CodigoValvula1.Replace("&nbsp;",String.Empty)) &&
                     !String.IsNullOrEmpty(item.NroSerieValvula1.Replace("&nbsp;", String.Empty)))
                {
                    ObleasValvulasExtendedView valvula1 = new ObleasValvulasExtendedView();
                    valvula1.ID = item.IdObleaValvula1;
                    valvula1.IdObleaCil = item.IdObleaCilindro;
                    valvula1.IDVal = item.IDValvula1;
                    valvula1.IDValUni = item.IDValvula1Unidad;
                    valvula1.CodigoVal = item.CodigoValvula1;
                    valvula1.NroSerieVal = item.NroSerieValvula1;
                    valvula1.MSDBVal = item.MSDBValvula1;
                    valvula1.MSDBValID = item.MSDBValvula1ID;
                    valvulas.Add(valvula1);
                }

                if (!String.IsNullOrEmpty(item.CodigoValvula2) && !String.IsNullOrEmpty(item.NroSerieValvula2) &&
                    !String.IsNullOrEmpty(item.CodigoValvula2.Replace("&nbsp;", String.Empty)) &&
                     !String.IsNullOrEmpty(item.NroSerieValvula2.Replace("&nbsp;", String.Empty)))
                {
                    ObleasValvulasExtendedView valvula2 = new ObleasValvulasExtendedView();
                    valvula2.ID = item.IdObleaValvula2;
                    valvula2.IdObleaCil = item.IdObleaCilindro;
                    valvula2.IDVal = item.IDValvula2;
                    valvula2.IDValUni = item.IDValvula2Unidad;
                    valvula2.CodigoVal = item.CodigoValvula2;
                    valvula2.NroSerieVal = item.NroSerieValvula2;
                    valvula2.MSDBVal = item.MSDBValvula2;
                    valvula2.MSDBValID = item.MSDBValvula2ID;
                    valvulas.Add(valvula2);
                }

            }

            return valvulas;
        }
        #endregion        
    }
}