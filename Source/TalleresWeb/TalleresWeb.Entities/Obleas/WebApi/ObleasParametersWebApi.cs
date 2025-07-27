using PL.Fwk.Entities;
using System;

namespace TalleresWeb.Entities
{
    public class ObleasParametersWebApi : ParametersEntity
    {
        #region Properties

        public String Dominio { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroOblea { get; set; }
        public Guid TallerID { get; set; }
        public Guid TipoDocumento { get; set; }
        public Guid TipoOperacionID { get; set; }

        #endregion
    }
}