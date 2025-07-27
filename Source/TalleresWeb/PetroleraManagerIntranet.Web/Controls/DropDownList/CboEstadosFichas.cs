using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboEstadosFichas : DropDownList
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
            EstadosFichasLogic logic = new EstadosFichasLogic();
            List<ViewEntity> estados = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();

            estados.Insert(0, new ViewEntity { ID = Guid.Empty, Descripcion = "Todos" });           
            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";
            this.DataSource = estados;
            this.DataBind();
        }
    }
}