using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls;
using PetroleraManager.Web.Controls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using PL.Fwk.Entities;
using CrossCutting.DatosDiscretos;

namespace PetroleraManager.Web.UserControls
{
    public partial class uscCargarCilindrosValvulasPH : System.Web.UI.UserControl
    {

        #region Propiedades
        public Boolean _PermiteSeleccionar;
        public Boolean PermiteSeleccionar
        {
            get
            {
                return PermiteSeleccionar = _PermiteSeleccionar == null ? false : _PermiteSeleccionar;
            }
            set
            {
                _PermiteSeleccionar = value == null ? false : value;
            }
        }

       public Boolean? PermiteAgregar
        {
            get
            {
                return gdv.FooterRow.Visible;
            }
            set
            {
                gdv.ShowFooter = value.HasValue ? value.Value : true;
            }
        }

        public List<PHCILINDROSExtendedView> CilindrosValvulasCargados
        {
            get
            {
                UpdatePanel1.Update();
                List<PHCILINDROSExtendedView> tabla2 = new List<PHCILINDROSExtendedView>();
                foreach (GridViewRow gr in gdv.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        PHCILINDROSExtendedView ocxv = new PHCILINDROSExtendedView();
                        ocxv.OrdenCil = ((PLLabel)gr.FindControl("lblItem")).Text;
                        ocxv.ID = new Guid(gdv.DataKeys[gr.RowIndex].Values["ID"].ToString());
                        ocxv.IDCilUni = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDCilUni"].ToString());
                        ocxv.IDValUni = new Guid(gdv.DataKeys[gr.RowIndex].Values["IDValUni"].ToString());
                        ocxv.CodigoCil = ((PLLabel)gr.FindControl("lblCodigo")).Text;
                        ocxv.SerieCil = ((PLLabel)gr.FindControl("lblSerieCil")).Text;
                        ocxv.CilFabMes = ((PLLabel)gr.FindControl("lblCilFabMes")).Text;
                        ocxv.CilFabAnio = ((PLLabel)gr.FindControl("lblCilFabAnio")).Text;
                        ocxv.CodigoVal = ((PLLabel)gr.FindControl("lblCodigoVal")).Text;
                        ocxv.SerieVal = ((PLLabel)gr.FindControl("lblSerieVal")).Text;
                        ocxv.Observacion = ((PLTextBox)gr.FindControl("txtObservaciones")).Text;
                        tabla2.Add(ocxv);
                    }
                }

                return tabla2;
            }
            set
            {
                tabla = value;
                ViewState["lstDetalleCil"] = tabla;

                gdv.DataSource = tabla;
                gdv.DataBind();
                UpdatePanel1.Update();
            }
        }
        #endregion

        List<PHCILINDROSExtendedView> tabla = new List<PHCILINDROSExtendedView>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["lstDetalleCil"] == null)
                ViewState["lstDetalleCil"] = tabla;
            else
                tabla = (List<PHCILINDROSExtendedView>)ViewState["lstDetalleCil"];

            if (!Page.IsPostBack)
            {
                gdv.DataSource = tabla;
                gdv.DataBind();

            }
        }

        #region CILINDROS
        protected void ibtAgregar_Click(object sender, ImageClickEventArgs e)
        {
            String isValid = String.Empty;
            PHCILINDROSExtendedView dfc = new PHCILINDROSExtendedView();

            if (gdv.Rows.Count == 0)
            {
                var filaGrilla = gdv.Controls[0].Controls[1].Controls[0];
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text)) isValid += "- Ingrese número de serie. </br>";

                if (((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Text == String.Empty)
                { isValid += "- Ingrese mes de fabricación. </br>"; }
                else
                {
                    if (Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Text) > 12)
                        isValid += "- El mes de fabricación debe estar entre 1 y 12. </br>";
                }

                if (((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Text == String.Empty) isValid += "- Ingrese año de fabricación. </br>";
                if (((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Text.Length != 4) isValid += "- El año de fabricación debe tener 4 números. Ej: 2010. </br>";

                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text)) isValid += "- Ingrese código válvula. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text)) isValid += "- Ingrese número de serie válvula. </br>";

                if (isValid == String.Empty)
                {

                    dfc.CodigoCil = ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text.ToUpper();
                    dfc.SerieCil = ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text.ToUpper();
                    dfc.Capacidad = Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("txtCapacidad")).Text);
                    dfc.CilFabMes = ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Text;
                    dfc.CilFabAnio = ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Text;
                    dfc.CodigoVal = ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text.ToUpper();
                    dfc.SerieVal = ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text.ToUpper();
                    dfc.Observacion = ((PLTextBox)filaGrilla.FindControl("txtObservaciones")).Text.ToUpper();

                    dfc.IDCilUni = CargarCilindroUnidad(dfc.CodigoCil, dfc.SerieCil, dfc.Capacidad, dfc.CilFabMes, dfc.CilFabAnio);
                    dfc.IDValUni = CargarValvulaUnidad(dfc.CodigoCil, dfc.SerieCil); 

                    dfc.ID = Guid.NewGuid();
                }
            }
            else
            {
                GridViewRow filaGrilla = gdv.FooterRow;
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text)) isValid += "- Ingrese código. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text)) isValid += "- Ingrese número de serie. </br>";

                if (((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Text == String.Empty)
                { 
                    isValid += "- Ingrese mes de fabricación. </br>"; 
                }
                else
                {
                    if (Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Text) > 12) isValid += "- El mes de fabricación debe estar entre 1 y 12. </br>";
                }

                if (((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Text == String.Empty) isValid += "- Ingrese año de fabricación. </br>";
                if (((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Text.Length != 4) isValid += "- El año de fabricación debe tener 4 números. Ej: 2010. </br>";

                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text)) isValid += "- Ingrese código válvula. </br>";
                if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text)) isValid += "- Ingrese número de serie válvula. </br>";

                if (isValid == String.Empty)
                {

                    dfc.CodigoCil = ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text.ToUpper();
                    dfc.SerieCil = ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text.ToUpper();
                    dfc.CilFabMes = ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Text;
                    dfc.CilFabAnio = ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Text;
                    dfc.Capacidad = Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("txtCapacidad")).Text);
                    dfc.CodigoVal = ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text.ToUpper();
                    dfc.SerieVal = ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text.ToUpper();
                    dfc.Observacion = ((PLTextBox)filaGrilla.FindControl("txtObservaciones")).Text.ToUpper();

                    dfc.IDCilUni = CargarCilindroUnidad(dfc.CodigoCil, dfc.SerieCil, dfc.Capacidad, dfc.CilFabMes, dfc.CilFabAnio);
                    dfc.IDValUni = CargarValvulaUnidad(dfc.CodigoCil, dfc.SerieCil); 

                    dfc.ID = Guid.NewGuid();
                }

            }

            if (isValid == String.Empty)
            {
                tabla.Add(dfc);

                gdv.DataSource = tabla;
                gdv.DataBind();

                GridViewRow filaGrilla = gdv.FooterRow;
                ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Focus();

            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        protected void CodigoCil_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            GridViewRow filaGrilla = (GridViewRow)(txt).NamingContainer;

            var txtcodigo = ((PLTextBox)filaGrilla.FindControl("txtCodigoCil"));
            CilindrosLogic cilLogic = new CilindrosLogic();
            var cil = cilLogic.ReadByCodigoHomologacion(txtcodigo.Text);

            var txtCapacidad = ((PLTextBoxMasked)filaGrilla.FindControl("txtCapacidad"));
            txtCapacidad.Text = String.Empty;

            if (cil.Count > 0)
            {
                ((PLTextBoxMasked)filaGrilla.FindControl("txtCapacidad")).Text = cil.FirstOrDefault().CapacidadCil.ToString();
                ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Focus();
            }
            else
            {
                ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Focus();
            }

            txt.Text = txt.Text.ToUpper();
            txtCapacidad.ReadOnlyTxt = (txtCapacidad.Text != String.Empty); // si esta vacio lo habilito para poder ingresar la capacidad
        }

        protected void SerieCil_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            GridViewRow filaGrilla = (GridViewRow)(txt).NamingContainer;

            var txtcodigo = ((PLTextBox)filaGrilla.FindControl("txtCodigoCil"));
            var txtserie = ((PLTextBox)filaGrilla.FindControl("txtSerieCil"));

            if (txtcodigo.Text != String.Empty)
            {
                CilindrosUnidadLogic cilLogic = new CilindrosUnidadLogic();
                var cil = cilLogic.ReadCilindroUnidad(txtcodigo.Text, txtserie.Text);

                if (cil != null)
                {
                    ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Text = cil.MesFabCilindro.ToString();
                    ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Text = Genericos.Genericos.FormatearAnio(cil.AnioFabCilindro.ToString());
                    ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Focus();
                }
                else
                {
                    ((PLTextBoxMasked)filaGrilla.FindControl("txtCapacidad")).Focus();
                }
            }
            else
            {
                txtcodigo.Focus();
            }
        }

        protected void AnioCil_TextChanged(object sender, EventArgs e) 
        {
            TextBox txt = (TextBox)sender;
            GridViewRow filaGrilla = (GridViewRow)(txt).NamingContainer;

            var txtanio = ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio"));
            txtanio.Text = Genericos.Genericos.FormatearAnio(txtanio.Text);

            ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Focus();
        }

        private Guid CargarCilindroUnidad(String codigoCil, String serieCil, Decimal capacidad, String mesFab, String anioFab)
        {
            CilindrosUnidadLogic cilindroLogic = new CilindrosUnidadLogic();
            var cilindro = cilindroLogic.ReadCilindroUnidad(codigoCil, serieCil);

            if (cilindro == null)
            {
                //si no existe grabo el cilindro y el cil unidad
                CilindrosLogic cilNuevoLogic = new CilindrosLogic();
                var cilNuevo = cilNuevoLogic.ReadByCodigoHomologacion(codigoCil).FirstOrDefault();
                if (cilNuevo == null)
                {
                    cilNuevo = new Cilindros();
                    cilNuevo.ID = Guid.NewGuid();
                    cilNuevo.Descripcion = codigoCil;
                    cilNuevo.CapacidadCil = capacidad;
                    cilNuevo.IdMarcaCilindro = MARCASINEXISTENTES.Cilindros;
                    cilNuevoLogic.Add(cilNuevo);
                }

                cilindro = new CilindrosUnidad();
                cilindro.ID = Guid.NewGuid();
                cilindro.IdCilindro = cilNuevo.ID;
                cilindro.Descripcion = serieCil;
                cilindro.MesFabCilindro = mesFab == String.Empty ? 0 : int.Parse(mesFab);
                cilindro.AnioFabCilindro = anioFab == String.Empty ? 0 : int.Parse(anioFab);
                cilindroLogic.Add(cilindro);

            }

            return cilindro.ID;
        }

        private Guid CargarValvulaUnidad(String codigoVal, String serieVal)
        {
            ValvulaUnidadLogic valvulaLogic = new ValvulaUnidadLogic();
            var valvula = valvulaLogic.ReadValvulaUnidad(codigoVal, serieVal);

            if (valvula == null)
            {
                //si no existe deberia grabar la valvula y la val unidad
                ValvulaLogic valNuevoLogic = new ValvulaLogic();
                var valNuevo = valNuevoLogic.ReadByCodigoHomologacion(codigoVal).FirstOrDefault();
                if (valNuevo == null)
                {
                    valNuevo = new Valvula();
                    valNuevo.ID = Guid.NewGuid();
                    valNuevo.Descripcion = codigoVal;
                    valNuevo.IdMarcaValvula = MARCASINEXISTENTES.Valvulas;
                    valNuevoLogic.Add(valNuevo);
                }

                valvula = new Valvula_Unidad();
                valvula.ID = Guid.NewGuid();
                valvula.IdValvula = valNuevo.ID;
                valvula.Descripcion = serieVal;
                valvulaLogic.Add(valvula);
            }

            return valvula.ID;
        }

        protected void gdv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int i = Convert.ToInt32(e.CommandArgument);

                #region MODIFICAR
                //if (e.CommandName == "modificar")
                //{
                //    Boolean lblVisible = false;
                //    Boolean txtVisible = true;
                //    var filaGrilla = gdv.Rows[i];
                //    ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text = ((PLLabel)filaGrilla.FindControl("lblCodigo")).Text;
                //    ((PLLabel)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                //    ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Visible = txtVisible;

                //    ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text = ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Text;
                //    ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Visible = lblVisible;
                //    ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Visible = txtVisible;

                //    ((PLTextBoxMasked)filaGrilla.FindControl("txtCapacidad")).Text = ((PLLabel)filaGrilla.FindControl("lblCilCapacidad")).Text;
                //    ((PLLabel)filaGrilla.FindControl("lblCilCapacidad")).Visible = lblVisible;
                //    ((PLTextBoxMasked)filaGrilla.FindControl("txtCapacidad")).Visible = txtVisible;

                //    ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Text = ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Text;
                //    ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Visible = lblVisible;
                //    ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Visible = txtVisible;

                //    ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Text = Genericos.Genericos.FormatearAnio(((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Text);
                //    ((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Visible = lblVisible;
                //    ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Visible = txtVisible;

                //    ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Text = ((PLLabel)filaGrilla.FindControl("lblCodigoVal")).Text;
                //    ((PLLabel)filaGrilla.FindControl("lblCodigoVal")).Visible = lblVisible;
                //    ((PLTextBox)filaGrilla.FindControl("txtCodigoVal")).Visible = txtVisible;

                //    ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Text = ((PLLabel)filaGrilla.FindControl("lblSerieVal")).Text;
                //    ((PLLabel)filaGrilla.FindControl("lblSerieVal")).Visible = lblVisible;
                //    ((PLTextBox)filaGrilla.FindControl("txtSerieVal")).Visible = txtVisible;

                //    ((PLTextBox)filaGrilla.FindControl("txtObservaciones")).Text = ((PLLabel)filaGrilla.FindControl("txtObservaciones")).Text;
                //    ((PLLabel)filaGrilla.FindControl("txtObservaciones")).Visible = lblVisible;
                //    ((PLTextBox)filaGrilla.FindControl("txtObservaciones")).Visible = txtVisible;

                //    ((PLImageButton)filaGrilla.FindControl("btnEliminar")).Visible = false;
                //    ((PLImageButton)filaGrilla.FindControl("btnModificar")).Visible = false;
                //    ((PLImageButton)filaGrilla.FindControl("btnAceptar")).Visible = true;

                //    ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Focus();
                //}
                #endregion

                #region ACEPTAR MODIFICACION
                //if (e.CommandName == "aceptar")
                //{
                //    var filaGrilla = gdv.Rows[i];
                //    String isValid = String.Empty;

                //    if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text)) isValid += "- Ingrese código. </br>";
                //    if (String.IsNullOrEmpty(((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text)) isValid += "- Ingrese número de serie. </br>";

                //    if (((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Text == String.Empty)
                //    { isValid += "- Ingrese mes de fabricación. </br>"; }
                //    else
                //    {
                //        if (Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Text) > 12)
                //            isValid += "- El mes de fabricación debe estar entre 1 y 12. </br>";
                //    }

                //    if (((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Text == String.Empty) isValid += "- Ingrese año de fabricación. </br>";
                //    if (((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Text.Length != 4) isValid += "- El año de fabricación debe tener 4 números. Ej: 2010. </br>";

                //    if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text == String.Empty)
                //    { isValid += "- Ingrese mes de fabricación. </br>"; }
                //    else
                //    {
                //        if (Decimal.Parse(((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevMes")).Text) > 12)
                //            isValid += "- El mes de fabricación debe estar entre 1 y 12. </br>";
                //    }

                //    if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text == String.Empty) isValid += "- Ingrese año última revisión. </br>";
                //    if (((PLTextBoxMasked)filaGrilla.FindControl("cboCilRevAnio")).Text.Length != 4) isValid += "- El año última revisión debe tener 4 números. Ej: 2010. </br>";

                //    if (((CboCRPC)filaGrilla.FindControl("cboCilCRPC")).SelectedValueString == "-1") isValid += "- Seleccione CRPC. </br>";

                //    if (isValid == String.Empty)
                //    {
                //        Boolean lblVisible = true;
                //        Boolean txtVisible = false;
                //        ((PLLabel)filaGrilla.FindControl("lblCodigo")).Text = ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Text;
                //        ((PLLabel)filaGrilla.FindControl("lblCodigo")).Visible = lblVisible;
                //        ((PLTextBox)filaGrilla.FindControl("txtCodigoCil")).Visible = txtVisible;

                //        ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Text = ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Text;
                //        ((PLLabel)filaGrilla.FindControl("lblSerieCil")).Visible = lblVisible;
                //        ((PLTextBox)filaGrilla.FindControl("txtSerieCil")).Visible = txtVisible;

                //        ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Text = ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Text;
                //        ((PLLabel)filaGrilla.FindControl("lblCilFabMes")).Visible = lblVisible;
                //        ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabMes")).Visible = txtVisible;

                //        ((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Text = ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Text;
                //        ((PLLabel)filaGrilla.FindControl("lblCilFabAnio")).Visible = lblVisible;
                //        ((PLTextBoxMasked)filaGrilla.FindControl("txtCilFabAnio")).Visible = txtVisible;

                //        ((PLLabel)filaGrilla.FindControl("lblCilCapacidad")).Text = ((PLTextBoxMasked)filaGrilla.FindControl("txtCapacidad")).Text;
                //        ((PLLabel)filaGrilla.FindControl("lblCilCapacidad")).Visible = lblVisible;
                //        ((PLTextBoxMasked)filaGrilla.FindControl("txtCapacidad")).Visible = txtVisible;


                //        ((PLImageButton)filaGrilla.FindControl("btnEliminar")).Visible = true;
                //        ((PLImageButton)filaGrilla.FindControl("btnModificar")).Visible = true;
                //        ((PLImageButton)filaGrilla.FindControl("btnAceptar")).Visible = false;

                //        ((PLTextBox)gdv.FooterRow.FindControl("txtCodigoCil")).Focus();
                //    }
                //    else
                //    {
                //        MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);
                //    }
                //}
                #endregion

                #region ELIMINAR
                if (e.CommandName == "eliminar")
                {

                    tabla.RemoveAt(i);

                    gdv.DataSource = tabla;
                    gdv.DataBind();

                }
                #endregion
            }
        }
        #endregion
    }
}