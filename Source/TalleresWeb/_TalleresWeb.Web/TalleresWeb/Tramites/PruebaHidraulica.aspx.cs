using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;
using Common.Web.UserControls;
using System.Transactions;
using DatosDiscretos;

namespace TalleresWeb.Web
{
    public partial class PruebaHidraulica : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    // si viene un ID es porque viene de la Ficha tecnica.
                    // cargo los datos de cilindros para que seleccione
                    // a cual le hace PH y cargo los datos de la Ficha
                    try
                    {
                        Guid idOblea = new Guid(Request.QueryString["id"].ToString());
                        CargarDatosFicha(new Guid(Request.QueryString["id"].ToString()));
                        txtNroObleaAnterior.ReadOnlyTxt = true;
                    }
                    catch (Exception ex)
                    {
                        txtNroObleaAnterior.ReadOnlyTxt = false;
                        MessageBoxCtrl1.MessageBox(null, "El ID de oblea no tiene formato correcto.", MessageBoxCtrl.TipoWarning.Error);
                    }
                }
                else
                { 
                    // si no viene ID limpio los campos y le permito buscar
                    // por nro de oblea??? o por cilindro ???
                    txtNroObleaAnterior.ReadOnlyTxt = false;
                }

                btnBuscarOblea.Enabled = !txtNroObleaAnterior.ReadOnlyTxt;
                cboCRPC.SelectedValue = IdCRPC.idPEAR;
            }
        }

        private void CargarDatosFicha(Guid idOblea)
        {
            ObleasLogic oLogic = new ObleasLogic();
            var oblea = oLogic.ReadDetalladoByID(idOblea);

            if (oblea != null)
            {
                txtNroObleaAnterior.Text = oblea.NroObleaNueva != null ? oblea.NroObleaNueva.Trim() : String.Empty; ;//.Descripcion.Trim();

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
                vxv.IdUso = oblea.IdUso;
                uscCargarVehiculo1.VehiculoCargado = vxv;

                List<ObleasValvulasExtendedView> lstObleasValExView = new List<ObleasValvulasExtendedView>();
                List<ObleasCilindrosExtendedView> lstObleasCilExView = new List<ObleasCilindrosExtendedView>();
                foreach (ObleasCilindros oc in oblea.ObleasCilindros)
                {
                    #region cargo cilindros
                    ObleasCilindrosExtendedView ocx = new ObleasCilindrosExtendedView();
                    ocx.ID = Guid.NewGuid(); // oc.ID;
                    ocx.NroSerieCil = oc.CilindrosUnidad.Descripcion.ToUpper().Trim();
                    ocx.CodigoCil = oc.CilindrosUnidad.Cilindros.Descripcion.ToUpper().Trim();

                    ocx.CilFabMes = oc.CilindrosUnidad.MesFabCilindro.Value.ToString();
                    ocx.CilFabAnio = oc.CilindrosUnidad.AnioFabCilindro.Value.ToString();
                    ocx.CilRevMes = oc.MesUltimaRevisionCil.ToString();
                    ocx.CilRevAnio = oc.AnioUltimaRevisionCil.ToString();

                    ocx.CRPCCilID = oc.IdCRPC.HasValue ? oc.IdCRPC.Value : Guid.Empty;
                    ocx.CRPCCil = oc.CRPC != null ? oc.CRPC.Descripcion.ToUpper().Trim() : String.Empty;
                    ocx.MSDBCilID = oc.IdOperacion;
                    ocx.MSDBCil = oc.Operaciones.Descripcion[0].ToString().ToUpper();
                    ocx.IDCil = oc.CilindrosUnidad.Cilindros.ID;
                    ocx.IDCilUni = oc.CilindrosUnidad.ID;
                    lstObleasCilExView.Add(ocx);
                    #endregion

                    //cargo la valvula del cil
                    foreach (ObleasValvulas ov in oc.ObleasValvulas)
                    {
                        ObleasValvulasExtendedView ovx = new ObleasValvulasExtendedView();
                        ovx.ID = Guid.NewGuid(); //ov.ID;
                        ovx.NroSerieVal = ov.Valvula_Unidad.Descripcion.ToUpper().Trim();
                        ovx.CodigoVal = ov.Valvula_Unidad.Valvula.Descripcion.ToUpper().Trim();
                        ovx.MSDBValID = ov.IdOperacion;
                        ovx.MSDBVal = ov.Operaciones.Descripcion[0].ToString().ToUpper();
                        ovx.IDVal = ov.Valvula_Unidad.Valvula.ID;
                        ovx.IDValUni = ov.Valvula_Unidad.ID;
                        ovx.IdObleaCil = ocx.ID;
                        lstObleasValExView.Add(ovx);
                    }

                }

                uscCargarCilindrosValvulas1.CilindrosCargados = lstObleasCilExView.Where(x => x.MSDBCilID != TipoOperacion.Baja).ToList();
                uscCargarCilindrosValvulas1.ValvulasCargadas = lstObleasValExView.Where(x => x.MSDBValID != TipoOperacion.Baja).ToList();
            }
            else
            {
                txtNroObleaAnterior.ReadOnlyTxt = false;
                btnBuscarOblea.Enabled = true;
                MessageBoxCtrl1.MessageBox(null, "El no se encontro oblea para el nro ingresado.", MessageBoxCtrl.TipoWarning.Error);
            }
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

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
             String isValid = String.Empty;

             if (uscCargarCliente1.ClienteCargado.ID == null) isValid += "- Debe cargar el cliente <br>";
             if (uscCargarVehiculo1.VehiculoCargado.ID == null) isValid += "- Debe cargar el vehículo <br>";

             if (uscCargarCilindrosValvulas1.CilindrosSeleccionados.Count == 0) isValid += "- Debe seleccionar un cilindro <br>";
             if (uscCargarCilindrosValvulas1.ValvulasCargadas.Count == 0) isValid += "- Debe cargar una válvula <br>";

            if (isValid == String.Empty)
            {
                using (TransactionScope ss = new TransactionScope())
                {
                    try
                    {
                        PHLogic phLogic = new PHLogic();
                        PH ph = new PH();
                        //agregar para que grabe
                        ph.ID = Guid.NewGuid();
                        ph.IdTaller = MasterTalleres.IdTaller;
                        ph.IdPEC = IdPec.idPEAR;
                        ph.NroObleaHabilitante = txtNroObleaAnterior.Text.Trim();
                        ph.FechaOperacion = DateTime.Now;
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

                        int idx = 0;
                        PHCilindrosLogic phCilLogic = new PHCilindrosLogic();
                        foreach (ObleasCilindrosExtendedView cilindro in uscCargarCilindrosValvulas1.CilindrosSeleccionados)
                        {
                            PHCilindros phCil = new PHCilindros();
                            phCil.ID = Guid.NewGuid();
                            phCil.IdPH = ph.ID;
                            phCil.IdCilindroUnidad = cilindro.IDCilUni;
                            phCil.IdValvulaUnidad = uscCargarCilindrosValvulas1.ValvulasCargadas[idx].IDValUni;
                            phCil.ObservacionPH = "";  //falta poner observacion
                            phCil.IdEstadoPH = DatosDiscretos.EstadosPH.EnEsperaCilindros;
                            phCilLogic.Add(phCil);

                            idx++;
                        }

                        ss.Complete();

                        String urlRetorno = "../default.aspx";
                        String urlImprimir = MasterTalleres.URLBase + @"TalleresWeb/Tramites/ImprimirCartaPH.aspx?id=" + ph.ID;
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

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request.Url.AbsoluteUri, false);
        }
    }
}