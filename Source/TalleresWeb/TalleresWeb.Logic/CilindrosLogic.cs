using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.BusinessLogic;
using TalleresWeb.Entities;
using TalleresWeb.DataAccess;

namespace TalleresWeb.Logic
{
    public class CilindrosLogic : EntityManagerLogic<Cilindros, CilindrosExtendedView, CilindrosParameters, CilindrosDataAccess>
    {
        public void AddCilindro(Cilindros cilindro, string nuevaMarca)
        {
            var existeCilindro = this.Read(cilindro.ID) != null;

            //si ingresa una nueva marca, la doy de alta
            if (!String.IsNullOrEmpty(nuevaMarca))
            {
                Guid marcaID = Guid.NewGuid();
                MarcasCilindrosLogic marcasCilindrosLogic = new MarcasCilindrosLogic();
                var marca = marcasCilindrosLogic.Add(new MarcasCilindros()
                {
                    ID = marcaID,
                    Descripcion = nuevaMarca                    
                });

                cilindro.IdMarcaCilindro = marcaID;
            }

            //add o update 
            if (!existeCilindro)
            {
                this.Add(cilindro);
            }
            else
            {
                this.Update(cilindro);
            }
        }

        public List<Cilindros> ReadByCodigoHomologacion(String codHomologacion)
        {
            return this.EntityDataAccess.ReadByCodigoHomologacion(codHomologacion);
        }

        public List<String> ReadListCodigosHomologacion(String codigoHomologacion)
        {
            return this.EntityDataAccess.ReadListCodigosHomologacion(codigoHomologacion);
        }

        public List<String> ReadCilindroMarcaYCapacidad(String codigoHomologacion)
        {
            return this.EntityDataAccess.ReadCilindroMarcaYCapacidad(codigoHomologacion);
        }

        public bool TieneDatosCompletos(string codHomologacion)
        {
            var cilindro = this.EntityDataAccess.ReadByCodigoHomologacion(codHomologacion).FirstOrDefault();

            return cilindro != null &&
                   cilindro.CapacidadCil.HasValue &&
                   cilindro.DiametroCilindro.HasValue &&
                   cilindro.EspesorAdmisibleCil.HasValue &&                                
                   cilindro.Activo.Value;
        }
    }
}
