using System;

namespace TalleresWeb.Web.UI.Controls
{
    public class TxtImportes : NumericControl
    {
        #region Methods

        protected override void OnInit(EventArgs e)
        {
            this.Formato = Formatos.CuatroDecimales;
            base.OnInit(e);
        }

        #endregion
    }
}