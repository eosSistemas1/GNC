using PL.Fwk.Presentation.Web.Controls;
using System.Linq;

namespace PetroleraManager.Web.Controls
{
    public class CboMarcas : PLComboBox
    {

        public override void LoadData()
        {
            //MarcasLogic logic = new MarcasLogic();
            //this.DataSource = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();
        }
    }
}