using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class ObleasReguladoresDataAccess : EntityManagerDataAccess<ObleasReguladores, ObleasReguladoresExtendedView, ObleasReguladoresParameters, TalleresWebEntities>
    {
        #region Methods

        public List<ObleasExtendedView> ReadAllConsultaEnBase(ObleasParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<ObleasReguladores>(this.EntityName)
                            .Where(x => (x.ReguladoresUnidad.Reguladores.IdMarcaRegulador.Value.Equals(param.MarcaRegID) || param.MarcaRegID.Equals(Guid.Empty))
                                     && (x.ReguladoresUnidad.Descripcion.Equals(param.SerieReg) || String.IsNullOrEmpty(param.SerieReg))
                                     )
                            select new ObleasExtendedView
                            {
                                ID = t.Obleas.ID,
                                FechaHabilitacion = t.Obleas.FechaHabilitacion.HasValue ? t.Obleas.FechaHabilitacion.Value : CrossCutting.DatosDiscretos.GetDinamyc.MinDatetime,
                                Dominio = t.Obleas.Vehiculos.Descripcion,
                                NombreyApellido = t.Obleas.Clientes.Descripcion,
                                NroObleaAnterior = t.Obleas.Descripcion,
                                NroObleaNueva = t.Obleas.NroObleaNueva,
                            };

                return query.ToList();
            }
        }

        public List<ObleasReguladores> ReadByIDOblea(Guid idOblea)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<ObleasReguladores>(this.EntityName)
                    .Where(x => x.IdOblea.Equals(idOblea))
                            select t;

                return query.ToList();
            }
        }

        #endregion
    }
}