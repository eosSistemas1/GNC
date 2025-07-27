using System;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboTalleres : DropDownList
    {        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (string.IsNullOrWhiteSpace(CssClass))
                this.Attributes.Add("class", "form-control");

            LoadData();
        }

        public void LoadData()
        {
            TalleresLogic logic = new TalleresLogic();
            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";
            this.DataSource = logic.ReadListView();
            this.DataBind();
        }        
    }
}