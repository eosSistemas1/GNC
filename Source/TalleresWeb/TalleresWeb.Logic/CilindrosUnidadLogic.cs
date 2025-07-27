using System;
using System.Collections.Generic;
using PL.Fwk.BusinessLogic;
using TalleresWeb.Entities;
using TalleresWeb.DataAccess;
using System.Linq;
using CrossCutting.DatosDiscretos;

namespace TalleresWeb.Logic
{
    public class CilindrosUnidadLogic : EntityManagerLogic<CilindrosUnidad, CilindrosUnidadExtendedView, CilindrosUnidadParameters, CilindrosUnidadDataAccess>
    {
        #region Properties
        private CilindrosLogic cilindrosLogic;
        public CilindrosLogic CilindrosLogic
        {
            get
            {
                if (this.cilindrosLogic == null) this.cilindrosLogic = new CilindrosLogic();
                return this.cilindrosLogic;
            }
        }
        #endregion

        #region Methods
        public List<CilindrosUnidad> ReadCilindroUnidad(Guid idCilindro, String nroSerie)
        {
            return EntityDataAccess.ReadCilindroUnidad(idCilindro, nroSerie);        
        }

        /// <summary>
        /// Lee un cilindro unidad por codigo homologacion y serie y si no existe lo crea
        /// </summary>
        /// <param name="codigoHomologacionCilindro"></param>
        /// <param name="numeroSerieCilindro"></param>
        /// <param name="mesFabricacion"></param>
        /// <param name="anioFabricacion"></param>
        /// <returns></returns>
        public Guid LeerCrearCilindroUnidad(string codigoHomologacionCilindro,
                                            string numeroSerieCilindro,
                                            string mesFabricacion,
                                            string anioFabricacion,
                                            decimal? capacidad = default(decimal?))
        {
           return this.LeerCrearCilindroUnidad(codigoHomologacionCilindro, numeroSerieCilindro, mesFabricacion, anioFabricacion, null, null);
        }

        /// <summary>
        /// Lee un cilindro unidad por codigo homologacion y serie y si no existe lo crea
        /// </summary>
        /// <param name="codigoHomologacionCilindro"></param>
        /// <param name="numeroSerieCilindro"></param>
        /// <param name="mesFabricacion"></param>
        /// <param name="anioFabricacion"></param>
        /// <returns></returns>
        public Guid LeerCrearCilindroUnidad(string codigoHomologacionCilindro, 
                                            string numeroSerieCilindro,
                                            string mesFabricacion,
                                            string anioFabricacion,
                                            string marcaCilindro,
                                            decimal? capacidad = default(decimal?))
        {
            Cilindros cilindro = this.CilindrosLogic.ReadByCodigoHomologacion(codigoHomologacionCilindro).FirstOrDefault();
            if (cilindro == null)
            {
                var marcaCil = new MarcasCilindrosLogic().ReadByDescripcion(marcaCilindro);

                //si viene el ID Cilindro = guid.empty es porque no existe ,
                //lo creo y guardo el valor del ID en idCil                    
                cilindro = new Cilindros
                {
                    ID = Guid.NewGuid(),
                    IdMarcaCilindro = marcaCil != null ? marcaCil.ID : MARCASINEXISTENTES.Cilindros,
                    Descripcion = codigoHomologacionCilindro,
                    CapacidadCil = capacidad
                };
                this.CilindrosLogic.Add(cilindro);
            }

            CilindrosUnidad cilindroUnidad = this.ReadCilindroUnidad(cilindro.ID, numeroSerieCilindro).FirstOrDefault();
            if (cilindroUnidad == null)
            {
                //si viene el ID Cil unidad = guid.empty es porque no existe ,
                //creo la unidad y guardo el valor del ID en idCilUni                    
                cilindroUnidad = new CilindrosUnidad();
                cilindroUnidad.ID = Guid.NewGuid();
                cilindroUnidad.IdCilindro = cilindro.ID;
                cilindroUnidad.Descripcion = numeroSerieCilindro;
                cilindroUnidad.MesFabCilindro = !string.IsNullOrWhiteSpace(mesFabricacion) ? int.Parse(mesFabricacion) : default(int?);
                cilindroUnidad.AnioFabCilindro = !string.IsNullOrWhiteSpace(anioFabricacion) ? int.Parse(anioFabricacion) : default(int?);
                this.EntityDataAccess.Add(cilindroUnidad);
            }
            else
            {
                cilindroUnidad.MesFabCilindro = !string.IsNullOrWhiteSpace(mesFabricacion) ? int.Parse(mesFabricacion) : default(int?);
                cilindroUnidad.AnioFabCilindro = !string.IsNullOrWhiteSpace(anioFabricacion) ? int.Parse(anioFabricacion) : default(int?);
                this.EntityDataAccess.Update(cilindroUnidad);
            }

            return cilindroUnidad.ID;
        }
        #endregion
    }
}
