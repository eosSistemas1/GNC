using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class ObleasLibresLogic : EntityManagerLogic<OBLEASLIBRES, ObleasLibresExtendedView, ObleasLibresParameters, ObleasLibresDataAccess>
    {
        public OBLEASLIBRES ReadNumeroLibre()
        {
            ObleasLibresDataAccess oa = new ObleasLibresDataAccess();

            return oa.ReadNumeroLibre();
        }
    }
}
