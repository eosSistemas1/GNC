using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI.WebControls;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI.Controls
{
    public class CboTipoDocumento : ComboBase
    {
        #region Properties
        public TipoDocumentoLogic tipoDocumentoLogic;
        public TipoDocumentoLogic TipoDocumentoLogic
        {
            get
            {
                if (this.tipoDocumentoLogic == null) this.tipoDocumentoLogic = new TipoDocumentoLogic();
                return this.tipoDocumentoLogic;
            }
        }
        #endregion

        #region Methods
        public override void LoadData()
        {
            var resultado = this.TipoDocumentoLogic.ReadListTiposDocumentos();
            this.DataSource = resultado;
        }
        #endregion
    }
}