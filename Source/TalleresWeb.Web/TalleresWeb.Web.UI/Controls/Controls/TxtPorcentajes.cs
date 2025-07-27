using System;

namespace TalleresWeb.Web.UI.Controls
{
    public class TxtPorcentajes : NumericControl
    {
        #region Methods

        protected override void OnInit(EventArgs e)
        {
            this.Formato = Formatos.DosDecimales;
            base.OnInit(e);
        }

        #endregion
    }
}