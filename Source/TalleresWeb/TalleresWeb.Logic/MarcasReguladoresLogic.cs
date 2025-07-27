using System;
using PL.Fwk.BusinessLogic;
using PL.Fwk.Entities;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class MarcasReguladoresLogic : EntityManagerLogic<MarcasRegulador, MarcasReguladoresExtendedView, MarcasReguladoresParameters, MarcasReguladoresDataAccess>
    {

        public ViewEntity ReadByDescripcion(string marcaReguladores)
        {
            return this.EntityDataAccess.ReadByDescripcion(marcaReguladores);
        }
    }
}