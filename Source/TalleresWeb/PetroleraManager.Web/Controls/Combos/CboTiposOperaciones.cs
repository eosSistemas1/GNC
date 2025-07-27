using CrossCutting.DatosDiscretos;
using PL.Fwk.Presentation.Web.Controls;
using System;
using System.Collections.Generic;
using TalleresWeb.Entities;

namespace PetroleraManager.Web.Controls
{
    public class CboTiposOperaciones : PLComboBox
    {
        public override void LoadData()
        {

            List<PL.Fwk.Entities.ViewEntity> dt = new List<PL.Fwk.Entities.ViewEntity>();

            EstadosFichasExtendedView dr = new EstadosFichasExtendedView();
            dr.ID = Guid.Empty;
            dr.Descripcion = "SELECCIONE";
            dt.Add(dr);

            EstadosFichasExtendedView dr0 = new EstadosFichasExtendedView();
            dr0.ID = TIPOOPERACION.Conversion;
            dr0.Descripcion = "CONVERSION";
            dt.Add(dr0);

            EstadosFichasExtendedView dr1 = new EstadosFichasExtendedView();
            dr1.ID = TIPOOPERACION.RevisionAnual;
            dr1.Descripcion = "REVISION";
            dt.Add(dr1);

            EstadosFichasExtendedView dr2 = new EstadosFichasExtendedView();
            dr2.ID = TIPOOPERACION.RevisionCRPC;
            dr2.Descripcion = "REVISION CRPC";
            dt.Add(dr2);

            EstadosFichasExtendedView dr3 = new EstadosFichasExtendedView();
            dr3.ID = TIPOOPERACION.Modificacion;
            dr3.Descripcion = "MODIFICACION";
            dt.Add(dr3);

            EstadosFichasExtendedView dr4 = new EstadosFichasExtendedView();
            dr4.ID = TIPOOPERACION.Baja;
            dr4.Descripcion = "DESMONTAJE/BAJA";
            dt.Add(dr4);

            EstadosFichasExtendedView dr5 = new EstadosFichasExtendedView();
            dr5.ID = TIPOOPERACION.Reemplazo;
            dr5.Descripcion = "REEMPLAZO";
            dt.Add(dr5);

            this.DataSource = dt;

        }
    }
}