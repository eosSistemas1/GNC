using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using TalleresWeb.Entities;
using CrossCutting.DatosDiscretos;

namespace TalleresWeb.DataAccess
{

    public class PHDataAccess : EntityManagerDataAccess<PH, PHExtendedView, PHParameters, TalleresWebEntities>
    {
        public PH ReadDetalladoByID(Guid idPH)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PH>(this.EntityName)
                            .Include("Talleres")
                            .Include("PHCilindros")
                            .Include("PHCilindros.CilindrosUnidad")
                            .Include("PHCilindros.CilindrosUnidad.Cilindros")
                            .Include("PHCilindros.CilindrosUnidad.Cilindros.MarcasCilindros")
                            .Include("PHCilindros.Valvula_Unidad")
                            .Include("PHCilindros.Valvula_Unidad.Valvula")
                            .Include("PHCilindros.Valvula_Unidad.Valvula.MarcasValvulas")
                            .Include("CRPC")
                            .Include("PEC")
                            .Include("Clientes")
                            .Include("Clientes.DocumentosClientes")
                            .Include("Clientes.Localidades")
                            .Include("Clientes.Localidades.Provincias")
                            .Include("Vehiculos")

                    .Where(x => x.ID == idPH)
                            select t;

                return query.FirstOrDefault();
            }
        }

        public PH ReadDetalladoByphCilindroID(Guid idPH)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PH>(this.EntityName)
                            .Include("Talleres")
                            .Include("PHCilindros")
                            .Include("PHCilindros.CilindrosUnidad")
                            .Include("PHCilindros.CilindrosUnidad.Cilindros")
                            .Include("PHCilindros.CilindrosUnidad.Cilindros.MarcasCilindros")
                            .Include("PHCilindros.Valvula_Unidad")
                            .Include("PHCilindros.Valvula_Unidad.Valvula")
                            .Include("PHCilindros.Valvula_Unidad.Valvula.MarcasValvulas")
                            .Include("CRPC")
                            .Include("PEC")
                            .Include("Clientes")
                            .Include("Clientes.DocumentosClientes")
                            .Include("Clientes.Localidades")
                            .Include("Clientes.Localidades.Provincias")
                            .Include("Vehiculos")

                    .Where(x => x.PHCilindros.Any( p => p.ID == idPH))
                            select t;

                return query.FirstOrDefault();
            }
        }

        public PH ReadDetalladoByIDParaConsulta(Guid idPH)
        {
            using (var context = this.GetEntityContext())
            { 
                var query = from t in context.CreateQuery<PH>(this.EntityName)
                            .Include("Talleres")
                            .Include("PHCilindros")
                            .Include("PHCilindros.CilindrosUnidad")
                            .Include("PHCilindros.CilindrosUnidad.Cilindros")
                            .Include("PHCilindros.CilindrosUnidad.Cilindros.MarcasCilindros")
                            .Include("PHCilindros.Valvula_Unidad")
                            .Include("PHCilindros.Valvula_Unidad.Valvula")
                            .Include("PHCilindros.Valvula_Unidad.Valvula.MarcasValvulas")
                            .Include("CRPC")
                            .Include("PEC")
                            .Include("Clientes")
                            .Include("Clientes.DocumentosClientes")
                            .Include("Clientes.Localidades")
                            .Include("Clientes.Localidades.Provincias")
                            .Include("Vehiculos")

                    .Where(x => x.ID == idPH 
                             && x.PHCilindros.Any(c => c.IdEstadoPH == EstadosPH.EnEsperaCilindros
                                                    || c.IdEstadoPH == EstadosPH.Ingresada
                                                    || c.IdEstadoPH == EstadosPH.Bloqueada))
                            select t;

                return query.FirstOrDefault();
            }
        }

    }
}