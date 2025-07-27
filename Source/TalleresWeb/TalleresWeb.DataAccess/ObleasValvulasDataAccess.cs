using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class ObleasValvulasDataAccess : EntityManagerDataAccess<ObleasValvulas, ObleasValvulasExtendedView, ObleasValvulasParameters, TalleresWebEntities>
    {
        #region Methods

        public List<ObleasExtendedView> ReadAllConsultasEnBase(ObleasParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<ObleasValvulas>(this.EntityName)
                            .Where(x => (x.Valvula_Unidad.Valvula.IdMarcaValvula.Value.Equals(param.MarcaValID) || param.MarcaValID.Equals(Guid.Empty))
                                     && (x.Valvula_Unidad.Descripcion.Equals(param.SerieVal) || param.SerieVal.Equals(String.Empty))
                                     )
                            select new ObleasExtendedView
                            {
                                ID = t.ObleasCilindros.Obleas.ID,
                                FechaHabilitacion = t.ObleasCilindros.Obleas.FechaHabilitacion.HasValue ? t.ObleasCilindros.Obleas.FechaHabilitacion.Value : CrossCutting.DatosDiscretos.GetDinamyc.MinDatetime,
                                Dominio = t.ObleasCilindros.Obleas.Vehiculos.Descripcion,
                                NombreyApellido = t.ObleasCilindros.Obleas.Clientes.Descripcion,
                                NroObleaAnterior = t.ObleasCilindros.Obleas.Descripcion,
                                NroObleaNueva = t.ObleasCilindros.Obleas.NroObleaNueva,
                            };

                return query.ToList();
            }
        }

        #endregion
    }
}