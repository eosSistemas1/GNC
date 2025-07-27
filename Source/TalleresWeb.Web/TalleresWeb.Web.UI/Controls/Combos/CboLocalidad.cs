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
    public class CboLocalidad : ComboBase
    {
        #region Properties
        public LocalidadesLogic localidadesLogic;
        public LocalidadesLogic LocalidadesLogic
        {
            get
            {
                if (this.localidadesLogic == null) this.localidadesLogic = new LocalidadesLogic();
                return this.localidadesLogic;
            }
        }
        #endregion

        #region Methods
        public override void LoadData()
        {
            var resultado = this.LocalidadesLogic.ReadListLocalidades();
            this.DataSource = resultado;
        }
        #endregion
    }
}