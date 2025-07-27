using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Web.UserControls;
using Common.Web.UserControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using System.Transactions;
using CrossCutting.DatosDiscretos;

namespace TalleresWeb.Web
{
    public partial class FichaTecnica : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            calFecha.Text = DateTime.Now.Date.ToShortDateString();
        }

        protected void btnBuscarOblea_Click(object sender, EventArgs e)
        {
            if (!Buscaroblea())
                MessageBoxCtrl1.MessageBox(null, "No se encontró oblea para el número ingresado.", MessageBoxCtrl.TipoWarning.Warning);
            else
                MyAccordion.SelectedIndex = 1;
        }

        protected void changeTipoOP(object sender, EventArgs e)
        {
            txtNroObleaAnterior.Visible = (cboTipoOperacion.SelectedValue != CrossCutting.DatosDiscretos.TipoOperacion.Conversion);
            btnBuscarOblea.Visible = (cboTipoOperacion.SelectedValue != CrossCutting.DatosDiscretos.TipoOperacion.Conversion);

            chkRealizaPH.Checked = (cboTipoOperacion.SelectedValue == CrossCutting.DatosDiscretos.TipoOperacion.RevisionCRPC);
            chkRealizaPH.Enabled = !(cboTipoOperacion.SelectedValue == CrossCutting.DatosDiscretos.TipoOperacion.RevisionCRPC);

            if (cboTipoOperacion.SelectedValue == CrossCutting.DatosDiscretos.TipoOperacion.Conversion)
            {
                MyAccordion.SelectedIndex = 1;
                uscCargarCliente1.Focus();
            }
            else
            {
                txtNroObleaAnterior.Focus();
            }

        }

        private Boolean Buscaroblea()
        {
            Boolean valor = true;

            ObleasLogic oLogic = new ObleasLogic();

            var oblea = oLogic.ReadDetalladoByObleaAnterior(txtNroObleaAnterior.Text);

            if (oblea != null)
            {

                BuscarTaller1.SearchValue = oblea.Talleres.RazonSocialTaller;
                ViewState["TALLERID"] = oblea.IdTaller.ToString();

                uscCargarCliente1.ClienteCargado = oblea.Clientes;

                VehiculosExtendedView vxv = new VehiculosExtendedView();
                vxv.ID = oblea.Vehiculos.ID;
                vxv.Descripcion = oblea.Vehiculos.Descripcion;
                vxv.MarcaVehiculo = oblea.Vehiculos.MarcaVehiculo;
                vxv.ModeloVehiculo = oblea.Vehiculos.ModeloVehiculo;
                vxv.AnioVehiculo = oblea.Vehiculos.AnioVehiculo.Value;
                vxv.EsInyeccionVehiculo = oblea.Vehiculos.EsInyeccionVehiculo.Value;
                vxv.IdUso = oblea.IdUso;
                uscCargarVehiculo1.VehiculoCargado = vxv;

                #region Reguladores
                List<ObleasReguladoresExtendedView> lstObleasRegExView = new List<ObleasReguladoresExtendedView>();
                foreach (ObleasReguladores or in oblea.ObleasReguladores)
                {
                    ObleasReguladoresExtendedView orx = new ObleasReguladoresExtendedView();
                    orx.ID = Guid.NewGuid();//or.ID;
                    orx.NroSerieReg = or.ReguladoresUnidad.Descripcion.ToUpper().Trim();
                    orx.CodigoReg = or.ReguladoresUnidad.Reguladores.Descripcion.ToUpper().Trim();
                    orx.MSDBRegID = or.IdOperacion;
                    orx.MSDBReg = or.Operaciones.Descripcion.ToUpper().Trim();
                    orx.IDReg = or.ReguladoresUnidad.Reguladores.ID;
                    orx.IDRegUni = or.ReguladoresUnidad.ID;
                    lstObleasRegExView.Add(orx);
                }
                uscCargarReguladores1.ReguladoresCargados = lstObleasRegExView;
                #endregion

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

                uscCargarCilindrosValvulas1.CilindrosCargados = lstObleasCilExView;
                uscCargarCilindrosValvulas1.ValvulasCargadas = lstObleasValExView;


                txtObservaciones.Text = oblea.ObservacionesFicha;
            }
            else
            {
                valor = false;
            }

            return valor;



            #region comnetado
            //Boolean valida = true;

            //if (txtNroObleaAnterior.Text.Trim() == String.Empty) valida = false;
            //if (txtNroObleaAnterior.Text == "0") valida = false;

            //if (valida)
            //{
            //    Nucleo.BFL.Obleas objOblea = new Nucleo.BFL.Obleas();
            //    if (objOblea.LoadObleaByNroObleaAnterior(txtNroObleaAnterior.Text))
            //    {
            //        valor = true;//devuelvo verdadero si la encontro.

            //        txtFechaHabilitacion.Text = DateTime.Now.ToShortDateString();
            //        //txtFechaVencimiento.Text = DateTime.Now.AddYears(1).ToShortDateString();
            //        CalcularFechaVencimiento();

            //        String msj = String.Empty;
            //        #region Cargo el auto
            //        if (objOblea.Vehiculos.RowCount > 0)
            //        {
            //            txtDominioVehiculo.Text = objOblea.Vehiculos.DominioVehiculo;

            //            //si encuentro el auto muestro los datos y el boton siguiente para permitir modificar
            //            txtMarcaAuto.Text = objOblea.Vehiculos.MarcaVehiculo;
            //            txtModeloAuto.Text = objOblea.Vehiculos.ModeloVehiculo;
            //            txtAnioAuto.Text = objOblea.Vehiculos.s_AnioVehiculo;
            //            chkBoxEsInyeccion.Checked = objOblea.Vehiculos.EsInyeccionVehiculo;

            //            btnAceptarVehiculo.Visible = true;
            //            btnBuscarOtroAuto.Visible = false;

            //            HabilitarAuto(true);
            //            imgValVehiculo.ImageUrl = Nucleo.DatosDiscretos.Imagenes.Accept;

            //            msj = " Dominio: " + txtDominioVehiculo.Text.ToUpper() + "  -  Vehículo: " + txtMarcaAuto.Text + " " + txtModeloAuto.Text;
            //            lblTituloVehiculo.Text = msj.ToUpper();
            //            lblTituloVehiculo.ForeColor = System.Drawing.Color.Green;
            //        }
            //        #endregion

            //        //SI LA OBLEA ANTERIOR NO ES DE ESE TALLER 
            //        //NO LE PONGO LOS DATOS DEL CLIENTE Y LOS DEBE CARGAR
            //        if (MasterTalleres.IdTaller == objOblea.Obleas.IdTaller)
            //        {
            //            #region Cargo el cliente
            //            cboDocCliente.SelectedValue = objOblea.Clientes.s_IdTipoDniCliente;
            //            txtNroDocCliente.Text = objOblea.Clientes.NroDniCliente;
            //            txtNom.Text = objOblea.Clientes.NombreApellidoCliente;
            //            txtCalle.Text = objOblea.Clientes.CalleCliente;
            //            txtTelefono.Text = objOblea.Clientes.TelefonoCliente;
            //            cboCiudades.SelectedValue = objOblea.Clientes.s_IdLocalidad;

            //            imgValCliente.ImageUrl = Nucleo.DatosDiscretos.Imagenes.Accept;
            //            msj = "Cliente: " + txtNom.Text;
            //            lblTituloCliente.Text = msj.ToUpper();
            //            lblTituloCliente.ForeColor = System.Drawing.Color.Green;

            //            #endregion
            //        }

            //        #region Cargo el titular si es diferente al cliente
            //        if (objOblea.Clientes.IdCliente != objOblea.Titular.IdCliente)
            //        {
            //            cboDocTitular.SelectedValue = objOblea.Titular.s_IdTipoDniCliente;
            //            txtNroDocTitular.Text = objOblea.Titular.NroDniCliente;
            //            txtNomTitular.Text = objOblea.Titular.NombreApellidoCliente;
            //            txtCalleTitular.Text = objOblea.Titular.CalleCliente;
            //            txtCalleNroTitular.Text = objOblea.Titular.NroCalleCliente;
            //            txtPisoDptoTitular.Text = objOblea.Titular.PisoDptoCliente;
            //            txtTelefonoTitular.Text = objOblea.Titular.TelefonoCliente;
            //            cboCiudadesTitular.SelectedValue = objOblea.Titular.s_IdLocalidad;

            //            chkBoxEsPropietario.Checked = false;
            //        }
            //        else
            //        {
            //            chkBoxEsPropietario.Checked = true;
            //        }
            //        #endregion

            //        #region cargo el tipo de uso    NOTA:por defecto le pongo Particular
            //        if (objOblea.Obleas.IdUso != null)
            //        {
            //            if (objOblea.Obleas.IdUso == new Guid(Nucleo.DatosDiscretos.TipoVehiculo.Taxi)) chkTipoVTaxi.Checked = true;
            //            if (objOblea.Obleas.IdUso == new Guid(Nucleo.DatosDiscretos.TipoVehiculo.PickUp)) chkTipoVPickUp.Checked = true;
            //            if (objOblea.Obleas.IdUso == new Guid(Nucleo.DatosDiscretos.TipoVehiculo.Particular)) chkTipoVPart.Checked = true;
            //            if (objOblea.Obleas.IdUso == new Guid(Nucleo.DatosDiscretos.TipoVehiculo.Bus)) chkTipoVBus.Checked = true;
            //            if (objOblea.Obleas.IdUso == new Guid(Nucleo.DatosDiscretos.TipoVehiculo.Oficial)) chkTipoVOficial.Checked = true;
            //            if (objOblea.Obleas.IdUso == new Guid(Nucleo.DatosDiscretos.TipoVehiculo.Otros)) chkTipoVOtros.Checked = true;
            //            if (objOblea.Obleas.IdUso == new Guid(Nucleo.DatosDiscretos.TipoVehiculo.Moto)) chkTipoVMoto.Checked = true;
            //            if (objOblea.Obleas.IdUso == new Guid(Nucleo.DatosDiscretos.TipoVehiculo.Autoelevadores)) chkTipoVAutoelevadores.Checked = true;
            //        }
            //        else
            //        {
            //            chkTipoVPart.Checked = true;
            //        }
            //        #endregion

            //        #region Cargo los reguladores
            //        //pongo en estado Sigue o Baja segun tipo operacion seleccionada
            //        String estadoReg = chkTipoBaja.Checked ? Nucleo.DatosDiscretos.MSDB.Baja : Nucleo.DatosDiscretos.MSDB.Sigue;

            //        List<ListReguladores> dsRegulador = new List<ListReguladores>();
            //        do
            //        {
            //            //filtro que ponga en la grilla solo los elementos que no fueron dados de baja
            //            if ((objOblea.ObleasReguladores.s_IdOperacion != Nucleo.DatosDiscretos.MSDB.Baja) &&
            //                (objOblea.ObleasReguladores.s_IdOperacion != Nucleo.DatosDiscretos.MSDB.Desmontaje))
            //            {
            //                Nucleo.BLL.ReguladoresUnidad objRegUni = new Nucleo.BLL.ReguladoresUnidad();
            //                Nucleo.BLL.Reguladores objReg = new Nucleo.BLL.Reguladores();

            //                objRegUni.LoadByPrimaryKey(objOblea.ObleasReguladores.IdReguladorUnidad);
            //                objReg.LoadByPrimaryKey(objRegUni.IdRegulador);

            //                ListReguladores dsRow = new ListReguladores();
            //                dsRow.CodigoReg = objReg.CodigoHomologacionRegulador;
            //                dsRow.NroSerieReg = objRegUni.s_NroSerieRegulador;
            //                dsRow.MSDBReg = estadoReg; //objOblea.ObleasReguladores.s_IdOperacion;
            //                dsRegulador.Add(dsRow);
            //            }
            //        } while (objOblea.ObleasReguladores.MoveNext());

            //        grdRegulador.DataSource = dsRegulador;
            //        grdRegulador.DataBind();

            //        imgValRegulador.ImageUrl = Nucleo.DatosDiscretos.Imagenes.Accept;
            //        msj = "Regulador(es): Ok";
            //        lblTituloRegulador.Text = msj.ToUpper();
            //        lblTituloRegulador.ForeColor = System.Drawing.Color.Green;

            //        #endregion

            //        #region cargo los cilindros
            //        //pongo en estado Sigue o Baja segun tipo operacion seleccionada
            //        String estadoCil = chkTipoBaja.Checked ? Nucleo.DatosDiscretos.MSDB.Baja : Nucleo.DatosDiscretos.MSDB.Sigue;

            //        List<ListCilindros> dsCilindro = new List<ListCilindros>();
            //        //do
            //        //{
            //        objOblea.ObleasCilindros.Rewind();
            //        for (int i = 0; i < objOblea.ObleasCilindros.RowCount; i++)
            //        {
            //            //filtro que ponga en la grilla solo los elementos que no fueron dados de baja
            //            if ((objOblea.ObleasCilindros.s_IdOperacion != Nucleo.DatosDiscretos.MSDB.Baja) &&
            //               (objOblea.ObleasCilindros.s_IdOperacion != Nucleo.DatosDiscretos.MSDB.Desmontaje))
            //            {
            //                Nucleo.BLL.CilindrosUnidad objCilUni = new Nucleo.BLL.CilindrosUnidad();
            //                Nucleo.BLL.Cilindros objCil = new Nucleo.BLL.Cilindros();

            //                objCilUni.LoadByPrimaryKey(objOblea.ObleasCilindros.IdCilindroUnidad);
            //                objCil.LoadByPrimaryKey(objCilUni.IdCilindro);

            //                ListCilindros dsCilRow = new ListCilindros();
            //                dsCilRow.CODIGO = objCil.CodHomologacinCil;
            //                dsCilRow.SERIE = objCilUni.s_NroSerieCilindro;
            //                dsCilRow.FABANIO = objCilUni.s_AnioFabCilindro;
            //                dsCilRow.FABMES = objCilUni.s_MesFabCilindro;
            //                dsCilRow.REVANIO = objOblea.ObleasCilindros.s_AnioUltimaRevisionCil;
            //                dsCilRow.REVMES = objOblea.ObleasCilindros.s_MesUltimaRevisionCil;
            //                dsCilRow.MSDB = estadoCil;
            //                dsCilRow.CRPC = objOblea.ObleasCilindros.s_IdCRPC;
            //                dsCilindro.Add(dsCilRow);
            //            }
            //            objOblea.ObleasCilindros.MoveNext();
            //        }
            //        //} while (objOblea.ObleasCilindros.MoveNext());

            //        grdCilindros.DataSource = dsCilindro;
            //        grdCilindros.DataBind();

            //        imgValCilindros.ImageUrl = Nucleo.DatosDiscretos.Imagenes.Accept;
            //        msj = "Cilindros: " + objOblea.ObleasCilindros.RowCount;
            //        lblTituloCilindros.Text = msj.ToUpper();
            //        lblTituloCilindros.ForeColor = System.Drawing.Color.Green;

            //        #endregion

            //        #region cargo las valvulas
            //        //pongo en estado Sigue o Baja segun tipo operacion seleccionada
            //        String estadoVal = chkTipoBaja.Checked ? Nucleo.DatosDiscretos.MSDB.Baja : Nucleo.DatosDiscretos.MSDB.Sigue;

            //        List<ListValvulas> dsValvula = new List<ListValvulas>();
            //        objOblea.ObleasValvulas.Rewind();
            //        do
            //        {
            //            //filtro que ponga en la grilla solo los elementos que no fueron dados de baja
            //            if ((objOblea.ObleasCilindros.s_IdOperacion != Nucleo.DatosDiscretos.MSDB.Baja) &&
            //               (objOblea.ObleasCilindros.s_IdOperacion != Nucleo.DatosDiscretos.MSDB.Desmontaje))
            //            {
            //                Nucleo.BLL.Valvula_Unidad objValUni = new Nucleo.BLL.Valvula_Unidad();
            //                Nucleo.BLL.Valvula objVal = new Nucleo.BLL.Valvula();

            //                objValUni.LoadByPrimaryKey(objOblea.ObleasValvulas.IdValvulaUnidad);
            //                objVal.LoadByPrimaryKey(objValUni.IdValvula);

            //                ListValvulas dsValRow = new ListValvulas();
            //                dsValRow.CodigoVal = objVal.CodHomologacionValvula;
            //                dsValRow.NroSerieVal = objValUni.s_IdSerieValvula;
            //                dsValRow.MSDBVal = estadoVal;//objOblea.ObleasValvulas.s_IdOperacion;
            //                dsValvula.Add(dsValRow);
            //            }

            //        } while (objOblea.ObleasValvulas.MoveNext());

            //        grdValvulasCil.DataSource = dsValvula;
            //        grdValvulasCil.DataBind();

            //        imgValvulas.ImageUrl = Nucleo.DatosDiscretos.Imagenes.Accept;
            //        msj = "Válvulas: " + grdCilindros.Rows.Count;
            //        lblTituloValvulas.Text = msj.ToUpper();
            //        lblTituloValvulas.ForeColor = System.Drawing.Color.Green;

            //        #endregion

            //        msj = "FECHA HAB.: " + DateTime.Parse(txtFechaHabilitacion.Text).ToString("dd/MM/yyyy") +
            //                    " - FECHA VENC.: " + DateTime.Parse(txtFechaVencimiento.Text).ToString("dd/MM/yyyy");
            //        lblTituloObleaAnt.Text = msj.ToUpper();
            //        lblTituloObleaAnt.ForeColor = System.Drawing.Color.Green;

            //        txtObservaciones.Text = objOblea.Obleas.ObservacionesFicha;

            //        #region habilito los paneles para que cargue los datos
            //        pane0TipoOperacion.Visible = true;
            //        pane1NroOblea.Visible = true;
            //        pane2Vehiculo.Visible = true;
            //        pane3Cliente.Visible = true;
            //        pane4Regulador.Visible = true;
            //        pane5Cilindros.Visible = true;
            //        pane6Valulas.Visible = true;
            //        pane7Observaciones.Visible = true;

            //        //habilito el boton enviar/imprimir
            //        lnkAceptar.Enabled = true;
            //        #endregion

            //        if (MasterTalleres.IdTaller == objOblea.Obleas.IdTaller)
            //        {
            //            MyAccordion.SelectedIndex = 7;
            //        }
            //        else
            //        {
            //            MyAccordion.SelectedIndex = 3;
            //            txtNroDocCliente.Focus();
            //        }
            //    }
            //}
            #endregion
        }

        protected void Talleres_Click(object sender, GridTalleresButtonEventArgs e)
        {
            ViewState["TALLERID"] = e.ID;
        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            String msjValida = String.Empty;

            if (cboTipoOperacion.SelectedValue == Guid.Empty) msjValida += "- Seleccione tipo de operacion <br>";

            if (uscCargarCliente1.ClienteCargado.ID == Guid.Empty) msjValida += "- Debe cargar el cliente <br>";
            if (uscCargarVehiculo1.VehiculoCargado.ID == null) msjValida += "- Debe cargar el vehículo <br>";

            if (uscCargarReguladores1.ReguladoresCargados.Count == 0) msjValida += "- Debe cargar un regulador <br>";
            if (uscCargarCilindrosValvulas1.CilindrosCargados.Count == 0) msjValida += "- Debe cargar un cilindro <br>";
            if (uscCargarCilindrosValvulas1.ValvulasCargadas.Count == 0) msjValida += "- Debe cargar una válvula <br>";

            if (msjValida == String.Empty)
            {
                using (TransactionScope ss = new TransactionScope())
                {
                    try
                    {
                        Guid idOblea = Guid.NewGuid();
                        Guid idOperacion = cboTipoOperacion.SelectedValue;

                        TalleresLogic tallerLogic = new TalleresLogic();
                        var taller = tallerLogic.Read(MasterTalleres.IdTaller);//cboTaller.SelectedValue);
                        int ultNroOP = taller.UltimoNroIntOperacion + 1;

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

                        #region creo ficha
                        //grabo ficha tecnica
                        ObleasLogic obleaLogic = new ObleasLogic();
                        Obleas oblea = new Obleas();
                        oblea.ID = idOblea;
                        oblea.FechaHabilitacion = new DateTime(int.Parse(calFecha.Text.Substring(6, 4)), int.Parse(calFecha.Text.Substring(3, 2)), int.Parse(calFecha.Text.Substring(0, 2)));
                        oblea.Descripcion = txtNroObleaAnterior.Text;
                        oblea.IdVehiculo = vehiculo.ID;
                        oblea.IdUso = vehiculo.IdUso;
                        oblea.IdOperacion = idOperacion;
                        oblea.IdEstadoFicha = CrossCutting.DatosDiscretos.EstadosFicha.PendienteRevision;
                        oblea.IdPEC = CrossCutting.DatosDiscretos.IdPec.idPEAR;
                        oblea.IdTaller = taller.ID;
                        oblea.NroIntOperacTP = ultNroOP;
                        oblea.IdCliente = cliente.ID;
                        oblea.IdTitular = cliente.ID;
                        oblea.ObservacionesFicha += txtObservaciones.Text.Trim();
                        oblea.IdUsuarioAlta = MasterTalleres.IdUsuarioLogueado;
                        oblea.FechaRealAlta = DateTime.Now;
                        obleaLogic.Add(oblea);
                        #endregion

                        #region reguladores
                        //grbar reguladores
                        GrabarRegulador(idOblea);

                        #endregion

                        #region cilindros
                        //grabarClindros y valvulas
                        GrabarCilindros(idOblea);
                        #endregion

                        //actualizo el puntero del ultimo nro de operacion
                        taller.UltimoNroIntOperacion = ultNroOP;
                        tallerLogic.Update(taller);
                        ss.Complete();

                        String urlRetorno = chkRealizaPH.Checked ? "PruebaHidraulica.aspx?id=" + oblea.ID : "FichaTecnica.aspx";
                        String urlImprimirOblea = MasterTalleres.URLBase + @"TalleresWeb/Tramites/ImprimirOblea.aspx?id=" + oblea.ID;
                        MessageBoxCtrl1.MessageBox(null, "La Ficha Técnica se creó correctamente.", urlRetorno, MessageBoxCtrl.TipoWarning.Success, urlImprimirOblea, "Imprmir");

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
                MessageBoxCtrl1.MessageBox(null, msjValida, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private void GrabarRegulador(Guid idOblea)
        {
            var reguladores = uscCargarReguladores1.ReguladoresCargados;

            foreach (ObleasReguladoresExtendedView gr in reguladores)
            {
                ObleasReguladores obleaReg = new ObleasReguladores();

                String CodigoREG = gr.CodigoReg.ToUpper().Trim();
                String SerieREG = gr.NroSerieReg.ToUpper().Trim();
                Guid MSDBREG = gr.MSDBRegID;

                Guid idReg = gr.IDReg;
                if (idReg == Guid.Empty)
                {
                    //Si el IdRegulador es vacio es porque no existe
                    //creo uno y guado el id en idReg
                    ReguladoresLogic objREG = new ReguladoresLogic();
                    Reguladores reg = new Reguladores();
                    reg.ID = Guid.NewGuid();
                    reg.IdMarcaRegulador = MarcasInexistentes.Reguladores;
                    reg.Descripcion = CodigoREG;
                    objREG.Add(reg);
                    idReg = reg.ID;
                }


                Guid idRegUni = gr.IDRegUni;
                if (idRegUni == Guid.Empty)
                {
                    //Si el id de unidad es vacio es porque no existe 
                    //creo la unidad y guardo el id para usarlo mas abajo
                    ReguladoresUnidadLogic objREGUNI = new ReguladoresUnidadLogic();
                    ReguladoresUnidad regUni = new ReguladoresUnidad();
                    regUni.ID = Guid.NewGuid();
                    regUni.IdRegulador = idReg;
                    regUni.Descripcion = SerieREG;
                    objREGUNI.Add(regUni);
                    idRegUni = regUni.ID;
                }
               

                ObleasReguladoresLogic objObleaREG = new ObleasReguladoresLogic();
                ObleasReguladores oR = new ObleasReguladores();
                oR.ID = gr.ID;
                oR.IdOblea = idOblea;
                oR.IdReguladorUnidad = idRegUni;
                oR.IdOperacion = MSDBREG;
                objObleaREG.Add(oR);
            }

        }

        private void GrabarCilindros(Guid idOblea)
        {
            ObleasValvulasLogic ovl = new ObleasValvulasLogic();
            ObleasCilindrosLogic ocl = new ObleasCilindrosLogic();
            var o = ocl.ReadByIDOblea(idOblea);

            var cilindros = uscCargarCilindrosValvulas1.CilindrosCargados;

            foreach (ObleasCilindrosExtendedView gr in cilindros)
            {
                #region Grabo el Cilindro
                ObleasCilindros obleaReg = new ObleasCilindros();

                String CodigoCIL = gr.CodigoCil;
                String SerieCIL = gr.NroSerieCil;
                int FabMes = int.Parse(gr.CilFabMes);
                int FabAnio = int.Parse(gr.CilFabAnio);
                int RevMes = int.Parse(gr.CilRevMes);
                int RevAnio = int.Parse(gr.CilRevAnio);
                Guid CRPC = gr.CRPCCilID;
                Guid MSDB = gr.MSDBCilID;
                String NroCertifPH = gr.NroCertificadoPH;

                Guid idCil = gr.IDCil;
                if (idCil == Guid.Empty)
                {
                    //si viene el ID Cilindro = guid.empty es porque no existe , 
                    //lo creo y guardo el valor del ID en idCil
                    CilindrosLogic objCIL = new CilindrosLogic();
                    Cilindros cil = new Cilindros();
                    cil.ID = Guid.NewGuid();
                    cil.IdMarcaCilindro = MarcasInexistentes.Reguladores;
                    cil.Descripcion = CodigoCIL;
                    objCIL.Add(cil);
                    idCil = cil.ID;
                }

                
                Guid idCilUni = gr.IDCilUni;
                if (idCilUni == Guid.Empty)
                {
                    //si viene el ID Cil unidad = guid.empty es porque no existe , 
                    //creo la unidad y guardo el valor del ID en idCilUni
                    CilindrosUnidadLogic objCILUNI = new CilindrosUnidadLogic();
                    CilindrosUnidad cilUni = new CilindrosUnidad();
                    cilUni.ID = Guid.NewGuid();
                    cilUni.IdCilindro = idCil;
                    cilUni.Descripcion = SerieCIL;
                    cilUni.MesFabCilindro = FabMes;
                    cilUni.AnioFabCilindro = FabAnio;
                    objCILUNI.Add(cilUni);
                    idCilUni = cilUni.ID;
                }

                ObleasCilindrosLogic objObleaCIL = new ObleasCilindrosLogic();
                ObleasCilindros oC = new ObleasCilindros();
                oC.ID = gr.ID;
                oC.IdOblea = idOblea;
                oC.IdCilindroUnidad = idCilUni;
                oC.MesUltimaRevisionCil = RevMes;
                oC.AnioUltimaRevisionCil = RevAnio;
                oC.IdCRPC = CRPC;
                oC.IdOperacion = MSDB;
                oC.NroCertificadoPH = NroCertifPH;
                objObleaCIL.Add(oC);
                #endregion
            }

            var valvulas = uscCargarCilindrosValvulas1.ValvulasCargadas;
            foreach (ObleasValvulasExtendedView grVal in valvulas)
            {
                #region Grabo la Valvula
                ObleasValvulas obleaVal = new ObleasValvulas();
                String CodigoVAL = grVal.CodigoVal.ToUpper().Trim();
                String SerieVAL = grVal.NroSerieVal.ToUpper().Trim();
                Guid IDObleaCil = grVal.IdObleaCil;
                Guid MSDBVAL = grVal.MSDBValID;

                
                Guid idCilVal = grVal.IDVal;
                if (idCilVal == Guid.Empty)
                {
                    //Si el id viene vacio es porque no existe la valvula
                    //entonces creo una y guardo el Id
                    ValvulaLogic objCILVAL = new ValvulaLogic();
                    Valvula val = new Valvula();
                    val.ID = Guid.NewGuid();
                    val.IdMarcaValvula = MarcasInexistentes.Valvulas;
                    val.Descripcion = CodigoVAL;
                    objCILVAL.Add(val);
                    idCilVal = val.ID;
                }

                Guid idValUni = grVal.IDValUni;
                if (idValUni == Guid.Empty)
                {
                    //Si el id de la unidad es vacio entonces creo la unidad
                    // y guardo el id para su uso posterior
                    ValvulaUnidadLogic objVALUNI = new ValvulaUnidadLogic();
                    Valvula_Unidad valUni = new Valvula_Unidad();
                    valUni.ID = Guid.NewGuid();
                    valUni.IdValvula = idCilVal;
                    valUni.Descripcion = SerieVAL;
                    objVALUNI.Add(valUni);
                    idValUni = valUni.ID;
                }

                ObleasValvulasLogic objObleaVAL = new ObleasValvulasLogic();
                ObleasValvulas oV = new ObleasValvulas();
                oV.ID = grVal.ID;
                oV.IdObleaCilindro = grVal.IdObleaCil;
                oV.IdValvulaUnidad = idValUni;
                oV.IdOperacion = MSDBVAL;
                objObleaVAL.Add(oV);

                #endregion
            }


        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request.Url.AbsoluteUri, false);
        }
        
    }
}