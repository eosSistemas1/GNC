using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TalleresWeb.Entities;

namespace TalleresWeb.Web.UI.Controls
{
    public class CboMSDB : ComboBase
    {
        public Guid TipoOperacionID
        {
            get
            {
                if (ViewState["CBOMSDBTIPOOPERACIONID"] == null) return Guid.Empty;
                return new Guid(ViewState["CBOMSDBTIPOOPERACIONID"].ToString());
            }
            set
            {
                ViewState["CBOMSDBTIPOOPERACIONID"] = value;
            }
        }

        public override void LoadData()
        {
            this.DataSource = null;
            if (TipoOperacionID == TIPOOPERACION.RevisionAnual)
            {
                this.DataSource = Valores(false, true, false, false);
            }
            else if (TipoOperacionID == TIPOOPERACION.Conversion)
            {
                this.DataSource = Valores(true, false, false, false);
            }
            else if (TipoOperacionID == TIPOOPERACION.Conversion)
            {
                this.DataSource = Valores(false, false, true, false);
            }
            else
            {
                this.DataSource = Valores();
            }
        }

        private List<ViewEntity> Valores()
        {
            return this.Valores(true, true, true, true);
        }

        private List<ViewEntity> Valores(Boolean M, Boolean S, Boolean D, Boolean B)
        {
            List<ViewEntity> dt = new List<ViewEntity>();

            if (M)
            {
                EstadosFichasExtendedView dr = new EstadosFichasExtendedView();
                dr.ID = MSDB.Montaje;
                dr.Descripcion = "MONTAJE";
                dt.Add(dr);
            }

            if (S)
            {
                EstadosFichasExtendedView dr1 = new EstadosFichasExtendedView();
                dr1.ID = MSDB.Sigue;
                dr1.Descripcion = "SIGUE";
                dt.Add(dr1);
            }

            if (D)
            {
                EstadosFichasExtendedView dr2 = new EstadosFichasExtendedView();
                dr2.ID = MSDB.Desmontaje;
                dr2.Descripcion = "DESMONTAJE";
                dt.Add(dr2);
            }

            if (B)
            {
                EstadosFichasExtendedView dr3 = new EstadosFichasExtendedView();
                dr3.ID = MSDB.Baja;
                dr3.Descripcion = "BAJA";
                dt.Add(dr3);
            }

            return dt;
        }
    }
}