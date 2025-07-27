using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetroleraManager.Logic;
using PetroleraManager.Entities;

namespace PetroleraManager.Web.UserControls
{
    public partial class BuscarConcepto : System.Web.UI.UserControl
    {
        public event GridConceptosButtonEventHandler GridConceptosButtonClick;

        ProductosLogic lista = new ProductosLogic();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblInfo.Text = "";

                txtSearch.Attributes.Add("onkeypress", "if (event.keyCode == 13) return false;");
                
                fillSearchDDL();
            }
        }

        private void bindGrid()
        {
            var resultadoPorCodigo = lista.ReadConceptoByCodigo(txtSearch.Text.Trim()).ToList();

            var resultado = lista.ReadConceptosByDescripcion(txtSearch.Text.Trim()).ToList();


            if (resultadoPorCodigo.Count == 1)
            {
                txtSearch.Text = resultadoPorCodigo.FirstOrDefault().Descripcion;
                CargarSeleccionado(resultadoPorCodigo.FirstOrDefault().ID,
                                    resultadoPorCodigo.FirstOrDefault().ProductoLoteID,
                                    resultadoPorCodigo.FirstOrDefault().Descripcion);
                MPE.Hide();
            }
            else
            {
                if (resultado.Count > 0)
                {
                    dgConceptos.DataSource = resultado;
                    dgConceptos.DataBind();
                    MPE.Show();
                }
                else
                {
                    lblInfo.Text = "No existen Conceptos que coincidan con el criterio de búsqueda.";
                    MPE.Hide();
                }
            }
        }

        protected virtual void onGridConceptosButtonClick(GridConceptosButtonEventArgs e)
        {
            if (GridConceptosButtonClick != null)
            {
                GridConceptosButtonClick(this, e);
            }
        }

        private void fillSearchDDL()
        {
            if (ddlColumnList.Items.Count == 0)
            {
                ddlColumnList.Items.Add(new ListItem("Código", "ID"));
                ddlColumnList.Items.Add(new ListItem("Descripción", "Descripcion"));
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

        private void CargarSeleccionado(Guid ConceptoID, Guid ConceptoLoteID, String descripcion)
        {
            GridConceptosButtonEventArgs ge;

            ge = new GridConceptosButtonEventArgs(ConceptoID, ConceptoLoteID, descripcion);
            onGridConceptosButtonClick(ge);
        }

        public string FilterValue
        {
            get { return ddlColumnList.SelectedValue; }
        }

        public string SearchValue
        {
            get { return txtSearch.Text; }
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

        public void LimpiarTexto()
        {
            txtSearch.Text = "";
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
            else
            {
                MPE.Show();
                bindGrid();
            }
        }
        protected void dgConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = dgConceptos.SelectedRow.Cells[0].Text.Trim();

            CargarSeleccionado(new Guid(dgConceptos.DataKeys[dgConceptos.SelectedIndex].Values["ID"].ToString()),
                                new Guid(dgConceptos.DataKeys[dgConceptos.SelectedIndex].Values["ProductoLoteID"].ToString()), 
                                dgConceptos.SelectedRow.Cells[0].Text.Trim());
        }

    }

    public partial class GridConceptosButtonEventArgs
    {
        private Guid _IDConcepto;
        private Guid _IDConceptoLote;
        private String _Descripcion;

        public Guid ConceptoID
        {
            get { return _IDConcepto; }
        }

        public Guid ConceptoLoteID
        {
            get { return _IDConceptoLote; }
        }

        public String Descripcion
        {
            get { return _Descripcion; }
        }

        public GridConceptosButtonEventArgs(Guid ConceptoID, Guid ConceptoLoteID ,String Descripcion)
        {
            _IDConcepto = ConceptoID;
            _IDConceptoLote = ConceptoLoteID;
            _Descripcion = Descripcion;
        }
    }
    public delegate void GridConceptosButtonEventHandler(object sender, GridConceptosButtonEventArgs e);
   
}