using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class ProveedoresLogic : EntityManagerLogic<PROVEEDORES, ProveedoresExtendedView, ProveedoresParameters, ProveedoresDataAccess>
    {
        public List<ProveedoresExtendedView> ReadExtendedViewByCodigo(ProveedoresParameters param)
        {
            ProveedoresDataAccess oa = new ProveedoresDataAccess();
            return oa.ReadExtendedViewByCodigo(param);
        }

        public List<ProveedoresExtendedView> ReadNominaProveedores(String razonSocial)
        {
            ProveedoresDataAccess oa = new ProveedoresDataAccess();
            return oa.ReadNominaProveedores(razonSocial);
        }
    }
}
