using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web
{
    public partial class DetalleConsulta : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            //    Guid idoblea = new Guid(Request.QueryString["idoblea"].ToString());

            //    //oblea
            //    Nucleo.BFL.Obleas objOblea = new Nucleo.BFL.Obleas();
            //    objOblea.LoadObleaByIdOblea(idoblea);
            //    lblOblea.Text = objOblea.Obleas.s_NroObleaNueva;

            //    lnkSolicitarOblea.Attributes.Add("onclick", "opener.location=('../Tramites/FichaTecnica.aspx?idOblea=" + objOblea.Obleas.NroObleaNueva + "'); self.close();");

            //    lblHabilitacion.Text = objOblea.Obleas.FechaHabilitacion.ToString("dd/MM/yyyy");
            //    lblVencimiento.Text = objOblea.Obleas.FechaVencimiento.ToString("dd/MM/yyyy");

            //    //Taller
            //    lblTaller.Text = objOblea.Talleres.s_RazonSocialTaller.ToUpper();
            //    lblTallerMatricula.Text = objOblea.Talleres.s_MatriculaTaller.ToUpper();

            //    //Vehiculo
            //    lblDominio.Text = objOblea.Vehiculos.DominioVehiculo.ToUpper();
            //    lblMarca.Text = objOblea.Vehiculos.MarcaVehiculo.ToUpper();
            //    lblModelo.Text = objOblea.Vehiculos.ModeloVehiculo.ToUpper();
            //    lblAnio.Text = objOblea.Vehiculos.s_AnioVehiculo.ToUpper();

            //    //si busco por nro doc cliente le muestra los datos del cliente.
            //    Boolean mostrarCliente = false;
            //    if (Session["MUESTRACLIENTE"] != null) mostrarCliente = Boolean.Parse(Session["MUESTRACLIENTE"].ToString());
            //    //Cliente
            //    if ((PopUpMaster.IdTaller.ToString() == objOblea.Obleas.s_IdTaller) || (mostrarCliente))
            //    {
            //        Nucleo.BLL.DocumentosClientes objDoc = new Nucleo.BLL.DocumentosClientes();
            //        objDoc.LoadByPrimaryKey(objOblea.Clientes.IdTipoDniCliente);
            //        lblDocumento.Text = objDoc.s_DocumentosClientes.ToUpper() + "-" + objOblea.Clientes.NroDniCliente;
            //        lblNomApe.Text = objOblea.Clientes.NombreApellidoCliente.ToUpper();
            //        lblDomicilio.Text = objOblea.Clientes.CalleCliente.ToUpper() + " " + objOblea.Clientes.NroCalleCliente;
            //        lblTelefono.Text = objOblea.Clientes.TelefonoCliente == "0.00" ? "" : objOblea.Clientes.TelefonoCliente.ToUpper();
            //    }


            //    Session.Remove("MUESTRACLIENTE");

            //    //Regulador
            //    grdReguladores.DataSource = objOblea.ObleasReguladores.DefaultView;
            //    grdReguladores.DataBind();

            //    //Cilindros
            //    grdCilindros.DataSource = objOblea.ObleasCilindros.DefaultView;
            //    grdCilindros.DataBind();

            //    //Valvulas
            //    grdValvulas.DataSource = objOblea.ObleasValvulas.DefaultView;
            //    grdValvulas.DataBind();

            }
        }

        protected void grdRegulador_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
                //Guid id = new Guid(e.Row.Cells[0].Text);
                //Guid idOperacion = new Guid(e.Row.Cells[2].Text);

                //Nucleo.BLL.ReguladoresUnidad objRegUni = new Nucleo.BLL.ReguladoresUnidad();
                //objRegUni.LoadByPrimaryKey(id);
                //Nucleo.BLL.Reguladores objReg = new Nucleo.BLL.Reguladores();
                //objReg.LoadByPrimaryKey(objRegUni.IdRegulador);

                //e.Row.Cells[0].Text = objReg.CodigoHomologacionRegulador;
                //e.Row.Cells[1].Text = objRegUni.s_NroSerieRegulador;

                //Nucleo.BLL.Operaciones objOp = new Nucleo.BLL.Operaciones();
                //objOp.LoadByPrimaryKey(idOperacion);
                //e.Row.Cells[2].Text = objOp.s_Operacion;
            }
        }

        protected void grdCilindros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
                //Guid id = new Guid(e.Row.Cells[0].Text);
                //Guid idOperacion = new Guid(e.Row.Cells[7].Text);

                //Nucleo.BLL.CilindrosUnidad objCilUni = new Nucleo.BLL.CilindrosUnidad();
                //objCilUni.LoadByPrimaryKey(id);
                //Nucleo.BLL.Cilindros objCil = new Nucleo.BLL.Cilindros();
                //objCil.LoadByPrimaryKey(objCilUni.IdCilindro);

                //e.Row.Cells[0].Text = objCil.CodHomologacinCil;
                //e.Row.Cells[1].Text = objCilUni.s_NroSerieCilindro;
                //e.Row.Cells[2].Text = objCilUni.s_MesFabCilindro;
                //e.Row.Cells[3].Text = objCilUni.s_AnioFabCilindro;

                //if (e.Row.Cells[6].Text != String.Empty)
                //{
                //    try
                //    {
                //        Guid idCRPC = new Guid(e.Row.Cells[6].Text);
                //        Nucleo.BLL.CRPC objCRPC = new Nucleo.BLL.CRPC();
                //        objCRPC.LoadByPrimaryKey(idCRPC);
                //        e.Row.Cells[6].Text = objCRPC.s_RazonSocialCRPC;
                //    }
                //    catch
                //    {
                //        e.Row.Cells[6].Text = String.Empty;
                //    }
                //}

                //Nucleo.BLL.Operaciones objOp = new Nucleo.BLL.Operaciones();
                //objOp.LoadByPrimaryKey(idOperacion);
                //e.Row.Cells[7].Text = objOp.s_Operacion;
            }
        }

        protected void grdValvulas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
                //Guid id = new Guid(e.Row.Cells[0].Text);
                //Guid idOperacion = new Guid(e.Row.Cells[2].Text);

                //Nucleo.BLL.Valvula_Unidad objValUni = new Nucleo.BLL.Valvula_Unidad();
                //objValUni.LoadByPrimaryKey(id);
                //Nucleo.BLL.Valvula objVal = new Nucleo.BLL.Valvula();
                //objVal.LoadByPrimaryKey(objValUni.IdValvula);

                //e.Row.Cells[0].Text = objVal.CodHomologacionValvula;
                //e.Row.Cells[1].Text = objValUni.s_IdSerieValvula;

                //Nucleo.BLL.Operaciones objOp = new Nucleo.BLL.Operaciones();
                //objOp.LoadByPrimaryKey(idOperacion);
                //e.Row.Cells[2].Text = objOp.s_Operacion;
            }
        }
    }
}