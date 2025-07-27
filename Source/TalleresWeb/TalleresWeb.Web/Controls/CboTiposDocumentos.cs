using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;

namespace TalleresWeb.Controls
{
    public class CboTiposDocumentos : PLComboBox
    {
        #region Methods

        public override void LoadData()
        {
            DocumentosLogic logic = new DocumentosLogic();
            this.DataSource = logic.ReadListView();
        }

        #endregion
    }
}