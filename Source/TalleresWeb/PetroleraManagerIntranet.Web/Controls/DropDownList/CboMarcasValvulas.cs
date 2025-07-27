using System;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboMarcasValvulas : DropDownList
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
            MarcasValvulasLogic logic = new MarcasValvulasLogic();
            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";
            var list = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();
            list.Insert(0, new PL.Fwk.Entities.ViewEntity(Guid.Empty, "Seleccione"));
            this.DataSource = list;
            this.DataBind();
        }
    }
}