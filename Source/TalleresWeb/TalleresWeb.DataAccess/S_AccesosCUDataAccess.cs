using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class S_AccesosCUDataAccess : EntityManagerDataAccess<S_ACCESOSCU, S_AccesosCUExtendedView, S_AccesosCUParameters, TalleresWebEntities>
    {
        #region Methods

        public override S_ACCESOSCU Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<S_ACCESOSCU>(this.EntityName)
                             .Where(x => x.ID.Equals(id))//&& x.Activo == true)
                             select t;

                return entity.FirstOrDefault();
            }
        }

        public override List<S_ACCESOSCU> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<S_ACCESOSCU>(this.EntityName)
                             //.Where(x => x.Activo == true)
                             select t;

                return entity.ToList();
            }
        }

        public override List<S_AccesosCUExtendedView> ReadExtendedView(S_AccesosCUParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<S_ACCESOSCU>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion))
                            .OrderBy(o => o.Descripcion)
                            select new S_AccesosCUExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                            };

                return query.ToList();
            }
        }

        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<S_ACCESOSCU>(this.EntityName)
                            //.Where(x => x.Activo == true)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.ToList();
            }
        }

        public List<S_AccesosCUExtendedView> ReadPadresByRol(S_AccesosCUParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<S_ACCESOSCU>(this.EntityName)
                             .Include("S_ACCESOSCU")
                             .Include("S_ROLES")
                             .Include("S_ROLES.Usuario")
                             .Where(x => x.S_ROLES.IdRol.Equals(param.IdRol)
                                      && x.S_CASOSDEUSO.IdPadre.Value.Equals(param.IdPadre)
                                      && x.S_ROLES.Usuario.FirstOrDefault().Activo.Equals(true)
                                      && x.Permitido.Value.Equals(true)
                                      )
                             .OrderBy(x => x.IdCU)
                             select new S_AccesosCUExtendedView
                             {
                                 IdCU = t.S_CASOSDEUSO.IdCU,
                                 Descripcion = t.S_CASOSDEUSO.Descripcion,
                                 CodigoCU = t.S_CASOSDEUSO.CodigoCU,
                                 IdPadre = t.S_CASOSDEUSO.IdPadre.HasValue ? t.S_CASOSDEUSO.IdPadre.Value : 0,
                                 Url = t.S_CASOSDEUSO.Url,
                                 UrlImagen = t.S_CASOSDEUSO.UrlImagen,
                                 IdRol = t.IdRol.Value,
                                 Permitido = t.Permitido.Value,
                                 Activo = t.S_ROLES.Usuario.FirstOrDefault().Activo,
                                 Usuario = t.S_ROLES.Usuario.FirstOrDefault().Descripcion,
                                 AbrirEnVentanaNueva = t.S_CASOSDEUSO.AbrirEnVentanaNueva.HasValue ? t.S_CASOSDEUSO.AbrirEnVentanaNueva.Value : false
                             };

                return entity.ToList();
            }
        }

        #endregion
    }
}