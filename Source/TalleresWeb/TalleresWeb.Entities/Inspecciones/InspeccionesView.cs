using PL.Fwk.Entities;
using System;
using System.Collections.Generic;

namespace TalleresWeb.Entities
{
    public class InspeccionesView : ViewEntity
    {        
        public Boolean ValorInspeccion { get; set; }
        public String Observacion { get; set; }

        public static List<InspeccionesView> GetFromViewEntity(List<ViewEntity> listaVE)
        {
            List<InspeccionesView> value = new List<InspeccionesView>();

            foreach (var item in listaVE)
            {
                value.Add(new InspeccionesView() { 
                                                   ID = item.ID,
                                                   Descripcion = item.Descripcion, 
                                                   ValorInspeccion=false, 
                                                   Observacion=String.Empty });
            }

            return value;
        }
    }
}
