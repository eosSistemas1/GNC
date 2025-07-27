using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using PL.ServicesWeb.Entities;
using PL.ServicesWeb.DataAccess;

namespace PL.ServicesWeb.Logic
{
    public class MenuLogic: EntityManagerLogic<T_MENU, MenuExtendedView, MenuParameters, MenuDataAccess>
    {
    }
}
