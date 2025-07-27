using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;
using PL.Fwk.Entities;

namespace PetroleraManager.DataAccess
{

    public class ContactosDataAccess : EntityManagerDataAccess<CONTACTOS, ContactosExtendedView, ContactosParameters, DataModelContext>
    {
        public List<CONTACTOS> ReadAllByIdProveedor(Guid IdProveedor)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<CONTACTOS>(this.EntityName)
                             .Where(x => x.ProveedoresID == IdProveedor)
                             .OrderBy (x=> x.Nombre)
                            select t;
                return query.ToList();
            }
        }
    }
}