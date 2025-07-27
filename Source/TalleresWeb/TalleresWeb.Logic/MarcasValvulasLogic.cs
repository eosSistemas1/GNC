using System;
using PL.Fwk.BusinessLogic;
using PL.Fwk.Entities;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;


namespace TalleresWeb.Logic
{
    public class MarcasValvulasLogic : EntityManagerLogic<MarcasValvulas, MarcasValvulasExtendedView, MarcasValvulasParameters, MarcasValvulasDataAccess>
    {
        public ViewEntity ReadByDescripcion(string marcaValvulas)
        {
            return this.EntityDataAccess.ReadByDescripcion(marcaValvulas);
        }
    }
}