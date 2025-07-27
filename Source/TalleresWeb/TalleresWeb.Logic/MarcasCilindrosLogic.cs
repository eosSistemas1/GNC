using System;
using PL.Fwk.BusinessLogic;
using PL.Fwk.Entities;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class MarcasCilindrosLogic : EntityManagerLogic<MarcasCilindros, MarcasCilindrosExtendedView, MarcasCilindrosParameters, MarcasCilindrosDataAccess>
    {
        public ViewEntity ReadByDescripcion(string marcaCilindro)
        {
            return this.EntityDataAccess.ReadByDescripcion(marcaCilindro);
        }
    }
}