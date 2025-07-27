using System;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboTiposDocumentos : DropDownList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (string.IsNullOrWhiteSpace(CssClass))
                this.Attributes.Add("class", "form-control nn");

            LoadData();
        }

        public void LoadData()
        {
            DocumentosLogic logic = new DocumentosLogic();
            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";
            this.DataSource = logic.ReadListView();
            this.DataBind();
        }
    }
}