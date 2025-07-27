using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalleresWeb.Web.UI.Controls
{
    public class CboMes : ComboBase
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
                MesExtendedView drSeleccione = new MesExtendedView();
                drSeleccione.ID = "";
                drSeleccione.Descripcion = "-- MES -- ";
                dt.Add(drSeleccione);
            }

            MesExtendedView drMes = new MesExtendedView();
            drMes.ID = "01";
            drMes.Descripcion = "ENERO";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "02";
            drMes.Descripcion = "FEBRERO";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "03";
            drMes.Descripcion = "MARZO";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "04";
            drMes.Descripcion = "ABRIL";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "05";
            drMes.Descripcion = "MAYO";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "06";
            drMes.Descripcion = "JUNIO";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "07";
            drMes.Descripcion = "JULIO";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "08";
            drMes.Descripcion = "AGOSTO";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "09";
            drMes.Descripcion = "SETIEMBRE";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "10";
            drMes.Descripcion = "OCTUBRE";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "11";
            drMes.Descripcion = "NOVIEMBRE";
            dt.Add(drMes);
            drMes = new MesExtendedView();
            drMes.ID = "12";
            drMes.Descripcion = "DICIEMBRE";
            dt.Add(drMes);

            this.DataSource = dt;
        }

        #endregion

        private class MesExtendedView : ViewEntity
        {
            public String ID { get; set; }
        }
    }
}