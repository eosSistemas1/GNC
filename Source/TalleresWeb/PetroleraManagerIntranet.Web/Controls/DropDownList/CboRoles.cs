using System;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboRoles : DropDownList
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
            S_RolesLogic logic = new S_RolesLogic();
            this.DataTextField = "Descripcion";
            this.DataValueField = "IdRol";
            this.DataSource = logic.ReadAll();
            this.DataBind();
        }
    }
}