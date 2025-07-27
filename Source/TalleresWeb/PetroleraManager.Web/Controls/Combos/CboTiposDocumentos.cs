using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.Controls
{
    public class CboTiposDocumentos : PLComboBox
    {

        public override void LoadData()
        {
            DocumentosLogic logic = new DocumentosLogic();
            this.DataSource = logic.ReadListView();
        }
    }
}