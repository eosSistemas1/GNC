using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;
using System.Transactions;
using DatosDiscretos;
using PetroleraManager.Web.UserControls;

namespace PetroleraManager.Web.Tramites
{
    public partial class PHIngresar : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uscCargarCilindrosValvulasPH1.PermiteAgregar = true;
                if (Request.QueryString["idPH"] != null)
                {
                    // si viene un idPH es porque viene de PHConsultar o PHRecepcionCilindro.
                    // cargo los datos de la ph
                    try
                    {
                        Guid idPH = new Guid(Request.QueryString["idPH"].ToString());
                        CargarDatosPH(idPH);

                        btnBuscarOblea.Visible = true;
                        //txtNroObleaAnterior.ReadOnlyTxt = true;

                        if (Request.QueryString["e"] != null)
                        {
                            lnkModificar.Visible = true;
                            Guid estadoID = new Guid(Request.QueryString["e"].ToString());

                            if (estadoID.Equals(DatosDiscretos.ESTADOSPH.EnEsperaCilindros))
                            {
                                //vienen de espera de cilindros y va a estado en proceso.
                                estadoID = DatosDiscretos.ESTADOSPH.EnProceso;
                                //asigno nro de operacion ph
                                PHCilindrosLogic phCilLogic = new PHCilindrosLogic();
                                ViewState["NROOPERACIONCRPC"] = phCilLogic.ReadLastNroOperacionCrpc() + 1;
                                btnBuscarOblea.Visible = false;
                            }
                            if (estadoID.Equals(DatosDiscretos.ESTADOSPH.Ingresada))
                            {
                                //viene de la carga inicial y va a estado en espera de cilindros
                                estadoID = DatosDiscretos.ESTADOSPH.EnEsperaCilindros;
                                btnBuscarOblea.Visible = false;
                            }
                            ViewState["ESTADOPHID"] = estadoID;
                        }
                        else
                        {
                            txtNroObleaAnterior.ReadOnlyTxt = false;
                            txtNroObleaAnterior.Focus();
                            MessageBoxCtrl1.MessageBox(null, "El ID no tiene formato correcto.", MessageBoxCtrl.TipoWarning.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        txtNroObleaAnterior.ReadOnlyTxt = false;
                        txtNroObleaAnterior.Focus();
                        MessageBoxCtrl1.MessageBox(null, "El ID no tiene formato correcto. <br /> " + ex.Message, MessageBoxCtrl.TipoWarning.Error);
                    }
                }
                else
                {
                    // si no viene ID limpio los campos y le permito buscar
                    // por nro de oblea??? o por cilindro ???
                    txtNroObleaAnterior.ReadOnlyTxt = false;
                    lnkModificar.Visible = false;
                }

                //si viene el ID != null viene de la ficha tecnica este ID es el idOblea
                if (Request.QueryString["id"] != null)
                {  
                    var obleaID = new Guid(Request.QueryString["id"].ToString());
                    CargarDatosFicha(obleaID);
                    lnkModificar.Visible = false;
                }

                txtFecha.Text = DateTime.Now.ToString();
                lnkAceptar.Visible = !lnkModificar.Visible;
            }

            cboCRPC.SelectedValue = DatosDiscretos.CRPCs.PEAR;

            txtNroObleaAnterior.Attributes.Add("onkeypress", "if (event.keyCode == 13) return WebForm_FireDefaultButton(event, '" + btnBuscarOblea.ClientID + "');");
        }

        protected void Talleres_Click(object sender, GridTalleresButtonEventArgs e)
        {
            ViewState["TALLERID"] = e.ID;
        }

        protected void btnBuscarOblea_Click(object sender, EventArgs e)
        {
            ObleasLogic ol = new ObleasLogic();
            var oblea = ol.ReadDetalladoByObleaAnterior(txtNroObleaAnterior.Text);
            if (oblea != null)
            {
                CargarDatosFicha(oblea.ID);
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, "No se encontró oblea para el número ingresado.", MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private void CargarDatosFicha(Guid obleaID) 
        {
            ObleasLogic oLogic = new ObleasLogic();
            var oblea = oLogic.ReadDetalladoByID(obleaID);
            if (oblea != null)
            {
                //ViewState["TALLERID"] = oblea.IdTaller;
                //BuscarTaller1.SearchValue = oblea.Talleres.RazonSocialTaller;

                //cargo cliente
                uscCargarCliente1.ClienteCargado = oblea.Clientes;

                //Cargo vehiculo
                VehiculosExtendedView vxv = new VehiculosExtendedView();
                vxv.ID = oblea.Vehiculos.ID;
                vxv.Descripcion = oblea.Vehiculos.Descripcion;
                vxv.MarcaVehiculo = oblea.Vehiculos.MarcaVehiculo;
                vxv.ModeloVehiculo = oblea.Vehiculos.ModeloVehiculo;
                vxv.AnioVehiculo = oblea.Vehiculos.AnioVehiculo.HasValue ? oblea.Vehiculos.AnioVehiculo.Value : 0;
                vxv.EsInyeccionVehiculo = oblea.Vehiculos.EsInyeccionVehiculo.HasValue ? oblea.Vehiculos.EsInyeccionVehiculo.Value : false;
                uscCargarVehiculo1.VehiculoCargado = vxv;

                //cargo cilindros
                List<PHCILINDROSExtendedView> lstCilindrosExView = new List<PHCILINDROSExtendedView>();
                foreach (ObleasCilindros oc in oblea.ObleasCilindros)
                {
                    #region cargo cilindros
                    PHCILINDROSExtendedView ocx = new PHCILINDROSExtendedView();
                    ocx.ID = Guid.NewGuid(); // oc.ID;
                    ocx.IDCilUni = oc.CilindrosUnidad.ID;
                    ocx.SerieCil = oc.CilindrosUnidad.Descripcion.ToUpper().Trim();
                    ocx.CodigoCil = oc.CilindrosUnidad.Cilindros.Descripcion.ToUpper().Trim();
                    ocx.Capacidad = oc.CilindrosUnidad.Cilindros.CapacidadCil.Value;
                    ocx.CilFabMes = oc.CilindrosUnidad.MesFabCilindro.Value.ToString();
                    ocx.CilFabAnio = Genericos.Genericos.FormatearAnio(oc.CilindrosUnidad.AnioFabCilindro.Value.ToString());

                    var ov = oc.ObleasValvulas.Where(x => x.IdOperacion != DatosDiscretos.MSDB.Baja
                                                        && x.IdOperacion != DatosDiscretos.MSDB.Desmontaje
                                                        && x.IdOperacion != Guid.Empty).FirstOrDefault();
                    if (ov != null)
                    {
                        ocx.IDValUni = ov.Valvula_Unidad.ID;
                        ocx.SerieVal = ov.Valvula_Unidad.Descripcion.ToUpper().Trim();
                        ocx.CodigoVal = ov.Valvula_Unidad.Valvula.Descripcion.ToUpper().Trim();
                    }
                    lstCilindrosExView.Add(ocx);
                    #endregion
                }

                uscCargarCilindrosValvulasPH1.CilindrosValvulasCargados = lstCilindrosExView;
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, "El nro. de oblea es inexistente.", MessageBoxCtrl.TipoWarning.Warning);
                txtNroObleaAnterior.Focus();
            }
        }

        private void CargarDatosPH(Guid idPH)
        {

            PHLogic phLogic = new PHLogic();
            var ph = phLogic.ReadDetalladoByID(idPH);

            if (ph != null)
            {
                Guid idCilindroPH = Request.QueryString["idCP"] != null? new Guid(Request.QueryString["idCP"].ToString()) : Guid.Empty;

                txtNroObleaAnterior.Text = ph.NroObleaHabilitante != null ? ph.NroObleaHabilitante.Trim() : String.Empty;

                txtFecha.Text = ph.FechaOperacion.ToString();

                BuscarTaller1.SearchValue = ph.Talleres.RazonSocialTaller;
                ViewState["TALLERID"] = ph.IdTaller;

                //cargo cliente
                uscCargarCliente1.ClienteCargado = ph.Clientes;

                //Cargo vehiculo
                VehiculosExtendedView vxv = new VehiculosExtendedView();
                vxv.ID = ph.Vehiculos.ID;
                vxv.Descripcion = ph.Vehiculos.Descripcion;
                vxv.MarcaVehiculo = ph.Vehiculos.MarcaVehiculo;
                vxv.ModeloVehiculo = ph.Vehiculos.ModeloVehiculo;
                vxv.AnioVehiculo = ph.Vehiculos.AnioVehiculo.HasValue ? ph.Vehiculos.AnioVehiculo.Value : 0;
                vxv.EsInyeccionVehiculo = ph.Vehiculos.EsInyeccionVehiculo.HasValue ? ph.Vehiculos.EsInyeccionVehiculo.Value : false;
                uscCargarVehiculo1.VehiculoCargado = vxv;

                List<PHCILINDROSExtendedView> lstCilValExView = new List<PHCILINDROSExtendedView>();
                if (idCilindroPH == Guid.Empty)
                {
                    foreach (PHCilindros oc in ph.PHCilindros)
                    {
                        #region cargo cilindros
                        PHCILINDROSExtendedView ocx = new PHCILINDROSExtendedView();
                        ocx.ID = oc.ID;
                        ocx.SerieCil = oc.CilindrosUnidad.Descripcion.ToUpper().Trim();
                        ocx.CodigoCil = oc.CilindrosUnidad.Cilindros.Descripcion.ToUpper().Trim();
                        ocx.Capacidad = oc.CilindrosUnidad.Cilindros.CapacidadCil.Value;
                        ocx.CilFabMes = oc.CilindrosUnidad.MesFabCilindro.Value.ToString();
                        ocx.CilFabAnio = oc.CilindrosUnidad.AnioFabCilindro.Value.ToString();

                        ocx.SerieVal = oc.Valvula_Unidad.Descripcion.ToUpper().Trim();
                        ocx.CodigoVal = oc.Valvula_Unidad.Valvula.Descripcion.ToUpper().Trim();

                        ocx.IDValUni = oc.Valvula_Unidad.ID;
                        ocx.IDCilUni = oc.CilindrosUnidad.ID;

                        lstCilValExView.Add(ocx);
                        #endregion
                    }
                }
                else
                {
                    //si viene el id del cilindro cargo un solo cilindro porque es modificacion del estado de la ph
                    var oc = ph.PHCilindros.Where(x => x.ID.Equals(idCilindroPH)).FirstOrDefault();
                    #region cargo cilindros
                    PHCILINDROSExtendedView ocx = new PHCILINDROSExtendedView();
                    ocx.ID = oc.ID;
                    ocx.SerieCil = oc.CilindrosUnidad.Descripcion.ToUpper().Trim();
                    ocx.CodigoCil = oc.CilindrosUnidad.Cilindros.Descripcion.ToUpper().Trim();
                    ocx.Capacidad = oc.CilindrosUnidad.Cilindros.CapacidadCil.Value;
                    ocx.CilFabMes = oc.CilindrosUnidad.MesFabCilindro.Value.ToString();
                    ocx.CilFabAnio = oc.CilindrosUnidad.AnioFabCilindro.Value.ToString();

                    ocx.SerieVal = oc.Valvula_Unidad.Descripcion.ToUpper().Trim();
                    ocx.CodigoVal = oc.Valvula_Unidad.Valvula.Descripcion.ToUpper().Trim();

                    ocx.IDValUni = oc.Valvula_Unidad.ID;
                    ocx.IDCilUni = oc.CilindrosUnidad.ID;

                    lstCilValExView.Add(ocx);
                    #endregion

                    uscCargarCilindrosValvulasPH1.PermiteAgregar = false;
                }

                uscCargarCilindrosValvulasPH1.CilindrosValvulasCargados = lstCilValExView;

            }
            else
            {
                txtNroObleaAnterior.ReadOnlyTxt = false;
                btnBuscarOblea.Enabled = true;
                MessageBoxCtrl1.MessageBox(null, "No se encontro la PH.", MessageBoxCtrl.TipoWarning.Error);
            }
        }
        
        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            String isValid = String.Empty;

            if (uscCargarCliente1.ClienteCargado.ID == null) isValid += "- Debe cargar el cliente <br>";
            if (uscCargarVehiculo1.VehiculoCargado.ID == null) isValid += "- Debe cargar el vehículo <br>";
            if (ViewState["TALLERID"] == null) isValid += "- Debe cargar el taller <br>";

            if (uscCargarCilindrosValvulasPH1.CilindrosValvulasCargados.Count == 0) isValid += "- Debe seleccionar un cilindro <br>";

            if (isValid == String.Empty)
            {
                using (TransactionScope ss = new TransactionScope())
                {
                    try
                    {
                        PHLogic phLogic = new PHLogic();
                        PH ph = new PH();
                        //grab oencabezado de la PH
                        ph.ID = Guid.NewGuid();
                        ph.IdTaller = new Guid(ViewState["TALLERID"].ToString());
                        ph.IdPEC = SiteMaster.Pec;
                        ph.NroObleaHabilitante = txtNroObleaAnterior.Text.Trim();
                        ph.FechaOperacion = DateTime.Parse(txtFecha.Text);
                        ph.IdCRPC = cboCRPC.SelectedValue;

                        #region grabo cliente
                        //grbar nuevo cliente o actualizar el que ingreso
                        ClientesLogic clienteLogic = new ClientesLogic();
                        var cliente = uscCargarCliente1.ClienteCargado;
                        clienteLogic.AddCliente(cliente);
                        #endregion

                        #region grabo vehiculo
                        //grbar nuevo vehiculo o actualizar el que ingreso
                        VehiculosLogic vehiculoLogic = new VehiculosLogic();
                        var vehiculo = uscCargarVehiculo1.VehiculoCargado;
                        vehiculo.IdDuenioVehiculo = cliente.ID;
                        vehiculoLogic.Add(vehiculo);
                        #endregion

                        ph.IDCliente = cliente.ID;
                        ph.IDVehiculo = vehiculo.ID;

                        phLogic.Add(ph);

                        PHCilindrosLogic phCilLogic = new PHCilindrosLogic();
                        foreach (PHCILINDROSExtendedView cilindro in uscCargarCilindrosValvulasPH1.CilindrosValvulasCargados)
                        {
                            PHCilindros phCil = new PHCilindros();
                            phCil.ID = Guid.NewGuid();
                            phCil.IdPH = ph.ID;
                            phCil.IdCilindroUnidad = cilindro.IDCilUni;
                            phCil.IdValvulaUnidad = cilindro.IDValUni;
                            phCil.ObservacionPH = cilindro.Observacion;
                            phCil.IdEstadoPH = DatosDiscretos.ESTADOSPH.Ingresada;
                            phCilLogic.Add(phCil);
                        }

                        ss.Complete();

                        String urlRetorno = Page.Request.Url.AbsoluteUri;
                        String urlImprimir = SiteMaster.UrlBase + @"02.Tramites/PHImprimirCarta.aspx?id=" + ph.ID;
                        MessageBoxCtrl1.MessageBox(null, "La carta de compromiso se creó correctamente.", urlRetorno, MessageBoxCtrl.TipoWarning.Success, urlImprimir, "Imprmir");


                    }
                    catch (Exception ex)
                    {
                        MessageBoxCtrl1.MessageBox(null, ex.Message, MessageBoxCtrl.TipoWarning.Error);
                    }
                    finally
                    {
                        ss.Dispose();
                    }
                }
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        protected void lnkModificar_Click(object sender, EventArgs e)
        {
            String isValid = String.Empty;

            if (uscCargarCliente1.ClienteCargado.ID == null) isValid += "- Debe cargar el cliente <br>";
            if (uscCargarVehiculo1.VehiculoCargado.ID == null) isValid += "- Debe cargar el vehículo <br>";
            if (ViewState["TALLERID"] == null) isValid += "- Debe cargar el taller <br>";
            if (uscCargarCilindrosValvulasPH1.CilindrosValvulasCargados.Count > 1) isValid += "- Debe haber solamente un cilindro <br>";
            if (uscCargarCilindrosValvulasPH1.CilindrosValvulasCargados.Count == 0) isValid += "- Debe seleccionar un cilindro <br>";

            if (isValid == String.Empty)
            {
                using (TransactionScope ss = new TransactionScope())
                {
                    try
                    {
                        PHLogic phLogic = new PHLogic();
                        PH ph = new PH();
                        //grab oencabezado de la PH
                        ph.ID = new Guid(Request.QueryString["idPH"].ToString());
                        ph.IdTaller = new Guid(ViewState["TALLERID"].ToString());
                        ph.IdPEC = SiteMaster.Pec;
                        ph.NroObleaHabilitante = txtNroObleaAnterior.Text.Trim();
                        ph.FechaOperacion = DateTime.Parse(txtFecha.Text);
                        ph.IdCRPC = cboCRPC.SelectedValue;

                        #region grabo cliente
                        //grbar nuevo cliente o actualizar el que ingreso
                        ClientesLogic clienteLogic = new ClientesLogic();
                        var cliente = uscCargarCliente1.ClienteCargado;
                        clienteLogic.AddCliente(cliente);
                        #endregion

                        #region grabo vehiculo
                        //grbar nuevo vehiculo o actualizar el que ingreso
                        VehiculosLogic vehiculoLogic = new VehiculosLogic();
                        var vehiculo = uscCargarVehiculo1.VehiculoCargado;
                        vehiculo.IdDuenioVehiculo = cliente.ID;
                        vehiculoLogic.Add(vehiculo);
                        #endregion

                        ph.IDCliente = cliente.ID;
                        ph.IDVehiculo = vehiculo.ID;

                        phLogic.Update(ph);

                        Guid phCilID = Guid.Empty;
                        PHCilindrosLogic phCilLogic = new PHCilindrosLogic();
                        foreach (PHCILINDROSExtendedView cilindro in uscCargarCilindrosValvulasPH1.CilindrosValvulasCargados)
                        {
                            PHCilindros phCil = new PHCilindros();
                            phCil.ID = cilindro.ID;
                            phCil.IdPH = ph.ID;
                            phCil.IdCilindroUnidad = cilindro.IDCilUni;
                            phCil.IdValvulaUnidad = cilindro.IDValUni;
                            phCil.ObservacionPH = cilindro.Observacion;
                            phCil.IdEstadoPH = new Guid(ViewState["ESTADOPHID"].ToString());
                            if (ViewState["NROOPERACIONCRPC"] != null) phCil.NroOperacionCRPC = int.Parse(ViewState["NROOPERACIONCRPC"].ToString());
                            phCilLogic.Update(phCil);

                            phCilID = cilindro.ID;
                        }

                        ss.Complete();
                        String urlRetorno = "PHConsultar.aspx";
                        if (new Guid(ViewState["ESTADOPHID"].ToString()).Equals(DatosDiscretos.ESTADOSPH.EnProceso))
                        {
                            urlRetorno = "PHRecepcionCilindros.aspx";
                            //Le digo que tiene que imprimir los papelitos y el encabezado de la hoja de ruta
                            ViewState["urlImprimir"] = SiteMaster.UrlBase + @"02.Tramites/PHImprimirHojaRuta.aspx?id=" + phCilID.ToString();
                        }
                        if (ViewState["urlImprimir"] != null)
                        {
                            String urlImprimir = ViewState["urlImprimir"].ToString();
                            MessageBoxCtrl1.MessageBox(null, "La carta de compromiso se modificó correctamente.", urlRetorno, MessageBoxCtrl.TipoWarning.Success, urlImprimir, "Imprmir");
                        }
                        else
                        {
                            MessageBoxCtrl1.MessageBox(null, "La carta de compromiso se modificó correctamente.", urlRetorno, MessageBoxCtrl.TipoWarning.Success);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBoxCtrl1.MessageBox(null, ex.Message, MessageBoxCtrl.TipoWarning.Error);
                    }
                    finally
                    {
                        ss.Dispose();
                    }
                }
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, isValid, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request.Url.AbsoluteUri, false);
        }
    }
}