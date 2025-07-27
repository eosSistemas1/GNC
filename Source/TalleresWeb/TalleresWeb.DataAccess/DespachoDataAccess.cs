using CrossCutting.DatosDiscretos;
using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class DespachoDataAccess : EntityManagerDataAccess<DESPACHO, DespachoExtendedView, DespachoParameters, TalleresWebEntities>
    {
        
        #region Methods
        public override DESPACHO Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<DESPACHO>(this.EntityName)
                             .Include("FLETE")
                             .Include("DESPACHODETALLE")
                             .Include("DESPACHODETALLE.OBLEAS")
                             .Include("DESPACHODETALLE.OBLEAS.OPERACIONES")
                             .Include("DESPACHODETALLE.OBLEAS.VEHICULOS")
                             .Include("DESPACHODETALLE.OBLEAS.CLIENTES")
                             .Include("DESPACHODETALLE.OBLEAS.TALLERES")
                             .Include("DESPACHODETALLE.PHCilindros.PH")                             
                             .Include("DESPACHODETALLE.PHCilindros.PH.VEHICULOS")
                             .Include("DESPACHODETALLE.PHCilindros.PH.TALLERES")
                             .Include("DESPACHODETALLE.PHCilindros.PH.CLIENTES")                             
                             //.Include("DESPACHODETALLE.PEDIDOS")
                             .Where(x => x.ID == id && x.Activo)

                            select t;

                return query.FirstOrDefault();
            }
        }

        public List<DespachoExtendedView> ReadTramitesPendientesByZona(string zona)
        {
            using (var context = this.GetEntityContext())
            {
                SqlParameter pZona = new SqlParameter("@pZona", zona);
                //Norte
                var parameters = new object[] { pZona };
                //parameters[0] = Norte

                context.CommandTimeout = 200;


                List<DespachoExtendedView> datos =
                    context.ExecuteStoreQuery<DespachoExtendedView>("dbo.spReadTramitesParaDespacho @pZona", parameters).ToList();


                List<DespachoExtendedView> tramites = new List<DespachoExtendedView>();
                foreach (var item in datos)
                {
                    if ((item.IdEstado == ESTADOSFICHAS.Bloqueada || item.IdEstado == ESTADOSFICHAS.Eliminada))
                    {
                        int dias = (DateTime.Now - item.FechaTramite).Days;
                        if (dias > 1)
                            continue;
                    }

                    tramites.Add(item);
                }

                return tramites;
            }
        }

        public DESPACHO ReadDespachoByNumero(string numeroDespacho)
        {
            using (var context = this.GetEntityContext())
            {
                int nro = int.Parse(numeroDespacho);
                var query = from t in context.CreateQuery<DESPACHO>(this.EntityName)
                             .Include("FLETE")
                             .Include("DESPACHODETALLE")
                             .Include("DESPACHODETALLE.OBLEAS")
                             .Include("DESPACHODETALLE.OBLEAS.VEHICULOS")
                             .Include("DESPACHODETALLE.OBLEAS.CLIENTES")
                             .Include("DESPACHODETALLE.PHCILINDROS")
                             .Include("DESPACHODETALLE.PHCILINDROS.PH")
                             //.Include("DESPACHODETALLE.PEDIDOS")
                             .Where(x => x.Numero == nro && x.Activo)

                            select t;

                return query.FirstOrDefault();
            }
        }

        public List<DespachoEnCursoView> ReadDespachosEnCurso()
        {
            using (var context = this.GetEntityContext())
            {               
                var query = from t in context.CreateQuery<DESPACHO>(this.EntityName)                                            
                                            .Where(x => !x.FechaHoraLlegada.HasValue && x.Activo)
                                            .OrderBy(x => x.Numero)
                            select new DespachoEnCursoView()
                            {
                                ID = t.ID,
                                Numero = t.Numero,
                                Flete = t.FLETE.Descripcion,
                                Fecha = t.Fecha,
                                FechaHoraSalida = t.FechaHoraSalida,                                
                                Zona = t.DESPACHODETALLE.FirstOrDefault(x => x.Talleres.Zona != String.Empty).Talleres.Zona
                          
                            };

                return query.ToList();
            }
        }
       
        public List<DespachoEnCursoView> ReadDespachosEnCursoEntreFechas(DateTime fechaDesde, DateTime fechaHasta)
        {      
            using (var context = this.GetEntityContext())
            {
                
                fechaHasta = fechaHasta.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                var query = from t in context.CreateQuery<DESPACHO>(this.EntityName)
                                            .Where(x => !x.FechaHoraLlegada.HasValue && x.Activo && (x.Fecha >= fechaDesde && x.Fecha <= fechaHasta))
                                            .OrderBy(x => x.Numero)

                            select new DespachoEnCursoView()
                            {
                                ID = t.ID,
                                Numero = t.Numero,
                                Flete = t.FLETE.Descripcion,
                                Fecha = t.Fecha,
                                FechaHoraSalida = t.FechaHoraSalida,
                                Zona = t.DESPACHODETALLE.FirstOrDefault(x => x.Talleres.Zona != String.Empty).Talleres.Zona
                          };

                return query.ToList();
            }
        }

        #endregion
    }
}
