using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;

namespace TalleresWeb.Controls
{
    public class CboMSDB : PLComboBox
    {
        #region Methods

        public override void LoadData()
        {
            TiposOperacionesLogic logic = new TiposOperacionesLogic();
            this.DataSource = logic.ReadEVMSDB();
        }

        #endregion
    }
}