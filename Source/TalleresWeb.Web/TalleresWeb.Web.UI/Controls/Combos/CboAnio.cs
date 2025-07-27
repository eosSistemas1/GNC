using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalleresWeb.Web.UI.Controls
{
    public class CboAnio : ComboBase
    {
        #region Members

        private Boolean _esFiltro;

        #endregion

        #region Properties

        public Boolean esFiltro
        {
            get { return _esFiltro; }
            set { _esFiltro = value; }
        }

        #endregion

        #region Methods

        public override void LoadData()
        {
            List<ViewEntity> dt = new List<ViewEntity>();

            if (_esFiltro)
            {
                AnioExtendedView drSeleccione = new AnioExtendedView();
                drSeleccione.ID = "";
                drSeleccione.Descripcion = "-- MES -- ";
                dt.Add(drSeleccione);
            }

            for (int i = DateTime.Now.Year; i >= 1990; i--)
            {
                AnioExtendedView drAnio = new AnioExtendedView();
                drAnio.ID = i.ToString("0000");
                drAnio.Descripcion = i.ToString("0000");
                dt.Add(drAnio);
            }

            this.DataSource = dt;
        }

        #endregion

        private class AnioExtendedView : ViewEntity
        {
            public String ID { get; set; }
        }
    }
}