using System;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboFletes : DropDownList
    {
        public Boolean? EsFletePropio { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (string.IsNullOrWhiteSpace(CssClass))
                this.Attributes.Add("class", "form-control");

            LoadData();
        }

        public void LoadData()
        {
            FleteLogic logic = new FleteLogic();
            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";

            if(!this.EsFletePropio.HasValue)
                this.DataSource = logic.ReadListView();
            else
                this.DataSource = logic.ReadAll().Where(x => x.EsFletePropio == this.EsFletePropio);
            this.DataBind();
        }

        public void LoadData(Boolean esFletePropio)
        {
            this.EsFletePropio = esFletePropio;

            this.LoadData();
        }
    }
}