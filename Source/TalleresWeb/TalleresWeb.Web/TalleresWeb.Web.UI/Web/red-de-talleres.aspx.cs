using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web
{
    public partial class red_de_talleres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarProvincias();
                CargarCboZonas();
                cboLocalidad.Enabled = false;
            }
        }

        private void CargarProvincias()
        {
            ProvinciasLogic provinciasLogic = new ProvinciasLogic();
            List<ViewEntity> provincias = provinciasLogic.ReadListProvincias();
            cboProvincias.DataTextField = "Descripcion";
            cboProvincias.DataValueField = "ID";
            cboProvincias.DataSource = provincias;
            cboProvincias.DataBind();
            cboProvincias.Items.Insert(0, new ListItem("--Seleccionar--", "-1"));
        }

        private void CargarCboLocalidad(Guid idProvincia)
        {
            //cboLocalidad.Items.Clear();
            //Nucleo.BLL.Localidades obj = new Nucleo.BLL.Localidades();
            //if (obj.LoadAllByProvincia(idProvincia))
            //{
            //    obj.Sort = "Localidad";
            //    cboLocalidad.DataTextField = "Localidad";
            //    cboLocalidad.DataValueField = "IdLocalidad";
            //    cboLocalidad.DataSource = obj.DefaultView;
            //    cboLocalidad.DataBind();
            //    cboLocalidad.Enabled = true;
            //    cboLocalidad.Items.Insert(0, new ListItem("--Seleccionar--", "-1"));
            //}
            //else
            //{
            //    cboLocalidad.Enabled = false;
            //}
        }

        private void CargarCboZonas()
        {
            //cboZonas.Items.Clear();
            //cboZonas.Items.Add(new ListItem("--Todas--", "-1"));
            //cboZonas.Items.Add(new ListItem(Nucleo.DatosDiscretos.Zonas.Norte, Nucleo.DatosDiscretos.Zonas.Norte));
            //cboZonas.Items.Add(new ListItem(Nucleo.DatosDiscretos.Zonas.Sur, Nucleo.DatosDiscretos.Zonas.Sur));
            //cboZonas.Items.Add(new ListItem(Nucleo.DatosDiscretos.Zonas.Este, Nucleo.DatosDiscretos.Zonas.Este));
            //cboZonas.Items.Add(new ListItem(Nucleo.DatosDiscretos.Zonas.Oeste, Nucleo.DatosDiscretos.Zonas.Norte));
            //cboZonas.Items.Add(new ListItem(Nucleo.DatosDiscretos.Zonas.Centro, Nucleo.DatosDiscretos.Zonas.Centro));
            //cboZonas.SelectedIndex = 0;
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Nucleo.BLL.Talleres objTalleres = new Nucleo.BLL.Talleres();
            //if (objTalleres.LoadByCiudadZona(new Guid(cboLocalidad.SelectedValue.ToString()), cboZonas.SelectedValue.ToString()))
            //{
            //    grdResultados.DataSource = objTalleres.DefaultView;
            //    grdResultados.DataBind();

            //    grdResultados.Visible = true;
            //}
            //else
            //{
            //    grdResultados.Visible = false;
            //}

            //String mensaje = objTalleres.RowCount.ToString() + " resultados para " + cboLocalidad.SelectedItem.Text;
            //if (cboZonas.SelectedValue != "-1")
            //{ mensaje += " / " + cboZonas.SelectedItem.Text + ":"; }
            //else
            //{ mensaje += ":"; }
            //lblResultados.Text = mensaje;
        }
        protected void cboProvincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid idProvincia = new Guid(cboProvincias.SelectedValue);
            CargarCboLocalidad(idProvincia);
        }

        protected void grdResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
                String idtaller = grdResultados.DataKeys[e.Row.RowIndex].Values[0].ToString();
                String mail = grdResultados.DataKeys[e.Row.RowIndex].Values[1].ToString();
                ImageButton btn = (ImageButton)e.Row.Cells[4].FindControl("btnMail");
                if (mail != String.Empty)
                {
                    btn.OnClientClick = "javascript:popup('PopUp/enviarMail.aspx?taller=" + idtaller + "');";
                }
                else
                {
                    btn.Visible = false;
                }
            }
        }
    }
}