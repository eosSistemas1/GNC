using CrossCutting.DatosDiscretos;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboEstadosFichasAsignar : DropDownList
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Attributes.Add("class", "form-control");

            LoadData();
        }

        public void LoadData()
        {
        
            EstadosFichasLogic logic = new EstadosFichasLogic();
            var estados = logic.ReadListView()
                           .Where(f => f.ID == ESTADOSFICHAS.Asignada || f.ID == ESTADOSFICHAS.AsignadaConError || f.ID == ESTADOSFICHAS.RechazadaPorEnte)
                           .OrderBy(x => x.Descripcion).ToList();


            estados.Insert(0, new PL.Fwk.Entities.ViewEntity { ID = Guid.Empty, Descripcion = "-- TODOS --" });

            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";
            this.DataSource = estados;
            this.DataBind();
        }
    }
}