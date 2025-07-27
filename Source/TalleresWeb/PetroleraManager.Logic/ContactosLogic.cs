using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class ContactosLogic : EntityManagerLogic<CONTACTOS, ContactosExtendedView, ContactosParameters, ContactosDataAccess>
    {
        public List<CONTACTOS> ReadAllByIdProveedor(Guid idProveedor)
        {
            ContactosDataAccess oa = new ContactosDataAccess();
            return oa.ReadAllByIdProveedor(idProveedor);
        }
    }
}
