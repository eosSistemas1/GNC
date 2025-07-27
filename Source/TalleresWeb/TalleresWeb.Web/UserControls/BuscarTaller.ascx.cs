using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace TalleresWeb.Web.UserControls
{
    public partial class BuscarTaller : System.Web.UI.UserControl
    {
        public event GridTalleresButtonEventHandler GridTalleresButtonClick;
        TalleresLogic lista = new TalleresLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblInfo.Text = "";

                //txtSearch.Attributes.Add("onkeypress", "if (event.keyCode == 13) __doPostBack('"+btnSearchP.ClientID+"');");
                txtSearch.Attributes.Add("onkeypress", "if (event.keyCode == 13) return false;");

                fillSearchDDL();
                txtSearch.Focus();
            }
        }

        private void bindGrid()
        {
            TalleresLogic lista = new TalleresLogic();

            //si busca por código
            if (FilterValue == "C")
            {
                TalleresParameters param = new TalleresParameters();
                param.Matricula = txtSearch.Text.Trim();
                List<TalleresExtendedView> resultado = lista.ReadExtendedViewByMatricula(param);

                if (resultado.Count > 1)
                {
                    dgTalleres.DataSource = resultado;
                    dgTalleres.DataBind();
                    MPE.Show();

                }
                else if (resultado.Count == 1)
                {
                    CargarSeleccionado(resultado.FirstOrDefault().ID, resultado.FirstOrDefault().Descripcion);
                    MPE.Hide();
                }
                else
                {
                    lblInfo.Text = "No existen Talleres para el texto ingresado.";
                    MPE.Hide();
                }
            }

            //si busca por Descripcion
            if (FilterValue == "D")
            {
                TalleresParameters param = new TalleresParameters();
                param.Descripcion = txtSearch.Text.Trim();
                List<TalleresExtendedView> resultado = lista.ReadExtendedView(param);

                if (resultado.Count > 1)
                {
                    dgTalleres.DataSource = resultado;
                    dgTalleres.DataBind();
                    MPE.Show();

                }
                else if (resultado.Count == 1)
                {
                    CargarSeleccionado(resultado.FirstOrDefault().ID, resultado.FirstOrDefault().Descripcion);
                    MPE.Hide();
                }
                else
                {
                    lblInfo.Text = "No existen Talleres para el texto ingresado.";
                    MPE.Hide();
                }
            }
        }

        protected virtual void onGridTalleresButtonClick(GridTalleresButtonEventArgs e)
        {
            if (GridTalleresButtonClick != null)
            {
                GridTalleresButtonClick(this, e);
            }
        }

        private void fillSearchDDL()
        {
            if (ddlColumnList.Items.Count == 0)
            {
                ddlColumnList.Items.Add(new ListItem("Razón Social", "D"));
                ddlColumnList.Items.Add(new ListItem("Matricula", "C"));
                //ddlColumnList.Items.Add(new ListItem("CUIT", "CUIT"));
                ddlColumnList.DataBind();
            }
        }

        public void Clear()
        {
            this.txtSearch.Text = "";
            this.ddlColumnList.SelectedIndex = 0;
            MPE.Hide();
        }

        private void CargarSeleccionado(Guid IDTaller, String razonSocial)
        {
            txtSearch.Text = razonSocial;
            GridTalleresButtonEventArgs ge;

            ge = new GridTalleresButtonEventArgs(IDTaller, razonSocial);
            onGridTalleresButtonClick(ge);
        }

        public string FilterValue
        {
            get { return ddlColumnList.SelectedValue; }
        }

        public string SearchValue
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }

        public bool ShowGrid
        {
            get { return this.Panel1.Visible; }
            set { this.Panel1.Visible = value; }
        }

        public void SetFocus()
        {
            txtSearch.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "";

            if (txtSearch.Text.Trim() == "")
            {
                lblInfo.Text = "Debe ingresar un texto para la búsqueda.";
                MPE.Hide();
                return;
            }

            bindGrid();
        }
        protected void dgTalleres_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSeleccionado(new Guid(dgTalleres.DataKeys[dgTalleres.SelectedIndex].Values["ID"].ToString()), dgTalleres.SelectedRow.Cells[1].Text.Trim());
        }
    }

    public partial class GridTalleresButtonEventArgs
    {
        private Guid _ID;
        private String _RazonSocial;

        public Guid ID
        {
            get { return _ID; }
        }

        public String RazonSocial
        {
            get { return _RazonSocial; }
        }

        public GridTalleresButtonEventArgs(Guid IDTaller, String razonSocial)
        {
            _ID = IDTaller;
            _RazonSocial = razonSocial;
        }
    }
    public delegate void GridTalleresButtonEventHandler(object sender, GridTalleresButtonEventArgs e);
}