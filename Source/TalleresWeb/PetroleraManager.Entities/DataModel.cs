using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    #region Inventario
    public partial class DEPOSITOS : IIdentifiable
    {
        public DEPOSITOS()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class PRODUCTOS : IIdentifiable
    {
        public PRODUCTOS()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class PRODUCTOLOTE : IIdentifiable
    {
        public PRODUCTOLOTE()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class PRODUCTOSCOMPONENTES : IIdentifiable
    {
        public PRODUCTOSCOMPONENTES()
        {
            _ID = Guid.NewGuid();
        }
        public String Descripcion
        {
            get
            {
                return String.Empty;
            }
        }
    }
    public partial class INVENTARIO : IIdentifiable
    {
        public INVENTARIO()
        {
            _ID = Guid.NewGuid();
        }
        public String Descripcion
        {
            get
            {
                return String.Empty;
            }
        }
    }
    public partial class INVENTARIODETALLE : IIdentifiable
    {
        public INVENTARIODETALLE()
        {
            _ID = Guid.NewGuid();
        }
        public String Descripcion
        {
            get
            {
                return String.Empty;
            }
        }
    }
    #endregion

    #region Compras
    public partial class PROVEEDORES : IIdentifiable
    {
        public PROVEEDORES()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class COMPROBANTESCOMPRAS : IIdentifiable
    {
        public COMPROBANTESCOMPRAS()
        {
            _ID = Guid.NewGuid();
        }
        public String Descripcion
        {
            get
            {
                return this.NroComprobante;
            }
            set
            {
                this.NroComprobante = value.ToString().Trim();
            }
        }
    }
    public partial class COMPROBANTESCOMPRASDETALLE : IIdentifiable
    {
        public COMPROBANTESCOMPRASDETALLE()
        {
            _ID = Guid.NewGuid();
        }
        public String Descripcion
        {
            get
            {
                return String.Empty;
            }
        }
    }

    #endregion

    #region Ventas
    public partial class CLIENTES : IIdentifiable
    {
        public CLIENTES()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class VENDEDORES : IIdentifiable
    {
        public VENDEDORES()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class COMPROBANTESVENTAS : IIdentifiable
    {
        public COMPROBANTESVENTAS()
        {
            _ID = Guid.NewGuid();
        }
        public String Descripcion
        {
            get
            {
                return this.NroComprobante;
            }
            set
            {
                this.NroComprobante = value.ToString().Trim();
            }
        }
    }
    public partial class COMPROBANTESVENTASDETALLE : IIdentifiable
    {
        public COMPROBANTESVENTASDETALLE()
        {
            _ID = Guid.NewGuid();
        }
        public String Descripcion
        {
            get
            {
                return String.Empty;
            }
        }
    }

    #endregion

    public partial class COMPROBANTESCUMPLIDO : IIdentifiable
    {
        public COMPROBANTESCUMPLIDO()
        {
            _ID = Guid.NewGuid();
        }
        public String Descripcion
        {
            get
            {
                return String.Empty;
            }
        }
    }

    #region Tesoreria ( comentado )
    //public partial class BANCOS : IIdentifiable
    //{
    //    public BANCOS()
    //    {
    //        _ID = Guid.NewGuid();
    //    }
    //}
    //public partial class CUENTAS : IIdentifiable
    //{
    //    public CUENTAS()
    //    {
    //        _ID = Guid.NewGuid();
    //    }
    //}
    //public partial class TESORERIA : IIdentifiable
    //{
    //    public TESORERIA()
    //    {
    //        _ID = Guid.NewGuid();
    //    }
    //    public String Descripcion
    //    {
    //        get
    //        {
    //            return String.Empty;
    //        }
    //    }
    //}
    //public partial class TESORERIADET : IIdentifiable
    //{
    //    public TESORERIADET()
    //    {
    //        _ID = Guid.NewGuid();
    //    }
    //    public String Descripcion
    //    {
    //        get
    //        {
    //            return String.Empty;
    //        }
    //    }
    //}
    //public partial class TESORERIAIMPUTACIONES : IIdentifiable
    //{
    //    public TESORERIAIMPUTACIONES()
    //    {
    //        _ID = Guid.NewGuid();
    //    }
    //    public String Descripcion
    //    {
    //        get
    //        {
    //            return String.Empty;
    //        }
    //    }
    //}
    //public partial class VALORES : IIdentifiable
    //{
    //    public VALORES()
    //    {
    //        _ID = Guid.NewGuid();
    //    }
    //}
    //public partial class VALORESTIPOS : IIdentifiable
    //{
    //    public VALORESTIPOS()
    //    {
    //        _ID = Guid.NewGuid();
    //    }
    //}

    #endregion

    #region Sistema
    public partial class BASEIMPONIBLE : IIdentifiable
    {
        public BASEIMPONIBLE()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class CONDICIONES : IIdentifiable
    {
        public CONDICIONES()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class CONTACTOS : IIdentifiable
    {
        public CONTACTOS()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class FABRICANTES : IIdentifiable
    {
        public FABRICANTES()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class IMPUESTOINTERNO : IIdentifiable
    {
        public IMPUESTOINTERNO()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class LOCALIDADES : IIdentifiable
    {
        public LOCALIDADES()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class MARCAS : IIdentifiable
    {
        public MARCAS()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class PROVINCIAS : IIdentifiable
    {
        public PROVINCIAS()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class REGIVA : IIdentifiable
    {
        public REGIVA()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class RUBROS : IIdentifiable
    {
        public RUBROS()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class TIPOSCOMPROBANTES : IIdentifiable
    {
        public TIPOSCOMPROBANTES()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class TRANSPORTES : IIdentifiable
    {
        public TRANSPORTES()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class UNIDADES : IIdentifiable
    {
        public UNIDADES()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class USUARIOS : IIdentifiable
    {
        public USUARIOS()
        {
            _ID = Guid.NewGuid();
        }
    }
    public partial class ZONAS : IIdentifiable
    {
        public ZONAS()
        {
            _ID = Guid.NewGuid();
        }
    }
    #endregion

    #region Informes
    //public partial class VALORIZACIONEXISTENCIAS : IIdentifiable
    //{
    //    public VALORIZACIONEXISTENCIAS()
    //    {
    //        Guid ID =  Guid.NewGuid();
    //    }
    //}
    #endregion

    public partial class DOCUMENTOSCLIENTES : IIdentifiable
    {
        public DOCUMENTOSCLIENTES()
        {
            _ID = Guid.NewGuid();
        }
    }  

    public partial class OBLEASLIBRES : IIdentifiable
    {
        public OBLEASLIBRES()
        {
            _ID = Guid.NewGuid();
        }
    }

}
