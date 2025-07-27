using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.ServicesWeb.Entities;
using PL.Fwk.Entities;
using PL.Fwk.DataAccess;

namespace PL.ServicesWeb.DataAccess
{
    public class MenuDataAccess : EntityManagerDataAccess<T_MENU, MenuExtendedView, MenuParameters, ServicesContext>
    {

        //public override List<T_MENU> ReadAll()
        //{
        //    using (var context = this.GetEntityContext())
        //    {



        //        var menues = from m in context.CreateQuery<PL.ServicesWeb.Entities.MenuEntity>(this.EntityName)
        //                      .Include("Services")
        //                     select m;

        //        return menues.ToList();
        //    }

        //}

    }


}
