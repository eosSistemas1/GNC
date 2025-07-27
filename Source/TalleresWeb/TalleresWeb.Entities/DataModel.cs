using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public partial class Operaciones : IIdentifiable
    {
        public Operaciones()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class Talleres : IIdentifiable
    {
        public Talleres()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class TalleresRT : IIdentifiable
    {
        public TalleresRT()
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

    public partial class Obleas : IIdentifiable
    {
        public Obleas()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class ObleaErrorDetalle : IIdentifiable
    {
        public ObleaErrorDetalle()
        {
            _ID = Guid.NewGuid();
        }
        public String Descripcion
        {
            get
            {
                return this.Correccion;
            }
            set
            {
                this.Correccion = value;
            }
        }
    }

    public partial class ObleaErrorTipo : IIdentifiable
    {
        public ObleaErrorTipo()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class Vehiculos : IIdentifiable
    {
        public Vehiculos()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class Clientes : IIdentifiable
    {
        public Clientes()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class Localidades : IIdentifiable
    {
        public Localidades()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class EstadosFichas : IIdentifiable
    {
        public EstadosFichas()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class CRPC : IIdentifiable
    {
        public CRPC()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class Uso : IIdentifiable
    {
        public Uso()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class PEC : IIdentifiable
    {
        public PEC()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class RT : IIdentifiable
    {
        public RT()
        {
            _ID = Guid.NewGuid();
        }

        public String Descripcion
        {
            get
            {
                return NombreApellidoRT;
            }
        }
    }

    public partial class RT_PEC : IIdentifiable
    {
        public RT_PEC()
        {
            _ID = Guid.NewGuid();
        }

        public String Descripcion
        {
            get
            {
                return string.Empty;
            }
        }
    }

    public partial class ObleasCilindros : IIdentifiable
    {
        public ObleasCilindros()
        {
            _ID = Guid.NewGuid();
        }

        public String Descripcion
        {
            get
            {
                return IdCilindroUnidad.ToString();
            }
        }
    }

    public partial class ObleasReguladores : IIdentifiable
    {
        public ObleasReguladores()
        {
            _ID = Guid.NewGuid();
        }

        public String Descripcion
        {
            get
            {
                return IdReguladorUnidad.ToString();
            }
        }
    }

    public partial class ObleasValvulas : IIdentifiable
    {
        public ObleasValvulas()
        {
            _ID = Guid.NewGuid();
        }

        public String Descripcion
        {
            get
            {
                return IdValvulaUnidad.ToString();
            }
        }
    }

    public partial class Reguladores : IIdentifiable
    {
        public Reguladores()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class Cilindros : IIdentifiable
    {
        public Cilindros()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class Valvula : IIdentifiable
    {
        public Valvula()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class ReguladoresUnidad : IIdentifiable
    {
        public ReguladoresUnidad()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class CilindrosUnidad : IIdentifiable
    {
        public CilindrosUnidad()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class Valvula_Unidad : IIdentifiable
    {
        public Valvula_Unidad()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class MarcasRegulador : IIdentifiable
    {
        public MarcasRegulador()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class MarcasCilindros : IIdentifiable
    {
        public MarcasCilindros()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class MarcasValvulas : IIdentifiable
    {
        public MarcasValvulas()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class PH : IIdentifiable
    {
        public PH()
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

    public partial class S_ROLES : IIdentifiable
    {
        public S_ROLES()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class S_CASOSDEUSO : IIdentifiable
    {
        public S_CASOSDEUSO()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class S_ACCESOSCU : IIdentifiable
    {
        public S_ACCESOSCU()
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

    public partial class DocumentosClientes : IIdentifiable
    {
        public DocumentosClientes()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class Usuario : IIdentifiable
    {
        public Usuario()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class Provincias : IIdentifiable
    {
        public Provincias()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class FLETE : IIdentifiable
    {
        public FLETE()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class Conductores : IIdentifiable
    {
        public Conductores()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class PHCilindros : IIdentifiable
    {
        public PHCilindros()
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

    public partial class LOTES : IIdentifiable
    {
        public LOTES()
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

    public partial class INFORME : IIdentifiable
    {
        public INFORME()
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

    public partial class INFORMEOBLEAS : IIdentifiable
    {
        public INFORMEOBLEAS()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class DESPACHO : IIdentifiable
    {
        public DESPACHO()
        {
            _ID = Guid.NewGuid();
        }
        public String Descripcion
        {
            get
            {
                return this.Numero.ToString();
            }
        }
    }

    public partial class DESPACHODETALLE : IIdentifiable
    {
        public DESPACHODETALLE()
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

    public partial class PhCilindroHistoricoEstado : IIdentifiable
    {        
        public PhCilindroHistoricoEstado()
        {
            _ID = Guid.NewGuid();
        }        
    }
    

    public partial class Inspecciones : IIdentifiable
    {
        public Inspecciones()
        {
            _ID = Guid.NewGuid();
        }
    }

    public partial class InspeccionesPH : IIdentifiable
    {
        public InspeccionesPH()
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

}
