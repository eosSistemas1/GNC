using PL.Fwk.Presentation.Web.Controls;
using System.Linq;
using TalleresWeb.Logic;

namespace TalleresWeb.Controls
{
    public class CboUso : PLComboBox
    {
        #region Methods

        public override void LoadData()
        {
            if (!Page.IsPostBack)
            {
                UsoLogic logic = new UsoLogic();
                this.DataSource = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();
                //if (!Page.IsPostBack) this.SelectedValue = CrossCutting.DatosDiscretos.TipoVehiculo.Particular;
            }
        }

        #endregion
    }
}