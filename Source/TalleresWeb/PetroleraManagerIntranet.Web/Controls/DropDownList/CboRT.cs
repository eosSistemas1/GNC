using System;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboRT : DropDownList
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
            RTLogic logic = new RTLogic();          
            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";
            this.DataSource = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();
            this.DataBind();           

        }


    }
}