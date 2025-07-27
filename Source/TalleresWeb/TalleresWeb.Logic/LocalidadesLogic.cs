using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using TalleresWeb.Entities;
using TalleresWeb.DataAccess;

namespace TalleresWeb.Logic
{
    public class LocalidadesLogic : EntityManagerLogic<Localidades, LocalidadesExtendedView, LocalidadesParameters, LocalidadesDataAccess>
    {
		
		 public void AddLocalidad(Localidades localidad)
        {
            var existeLocalidad = this.Read(localidad.ID) != null;

            //add o update 
            if (!existeLocalidad)
            {
                this.Add(localidad);
            }
            else
            {
                this.Update(localidad);
            }
        }

        public List<Localidades> ReadByLocalidad(String localidad)
        {
            return this.EntityDataAccess.ReadByLocalidad(localidad);
        }
       

        public List<String> ReadListLocalidades(String localidad)
        {
            return this.EntityDataAccess.ReadListLocalidades(localidad);
        }


    }
}
