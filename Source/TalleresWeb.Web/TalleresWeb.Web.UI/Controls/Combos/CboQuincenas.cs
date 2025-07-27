using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalleresWeb.Web.UI.Controls
{
    public class CboQuincenas : ComboBase
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
        public override void LoadData()
        {
            List<ViewEntity> dt = new List<ViewEntity>();

            if (_esFiltro)
            {
                MesExtendedView drSeleccione = new MesExtendedView();
                drSeleccione.ID = "";
                drSeleccione.Descripcion = "-- SELECCIONAR -- ";
                dt.Add(drSeleccione);
            }

            MesExtendedView drMes = new MesExtendedView();
            drMes.ID = "0";
            drMes.Descripcion = "MES";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "1";
            drMes.Descripcion = "1ra. Quincena.";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "2";
            drMes.Descripcion = "2da. Quincena";
            dt.Add(drMes);

            this.DataSource = dt;
        }

        private class MesExtendedView : ViewEntity
        {
            public String ID { get; set; }
        }
    }
}