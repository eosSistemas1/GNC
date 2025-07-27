using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalleresWeb.Entities
{
    public class ClientesView
    {
        public Guid ClienteTipoDocumentoID { get; set; }
        public string ClienteNombreApellido { get; set; }
        public string ClienteTipoDocumento { get; set; }
        public string ClienteNumeroDocumento { get; set; }
        public string ClienteDomicilio { get; set; }
        public Guid ClienteLocalidadID { get; set; }
        public string ClienteLocalidad { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteCelular { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteCP { get; set; }
        public string ClienteProvincia { get; set; }

        public static ClientesView ClientesToClientesView(Clientes cliente)
        {
            if (cliente != null)
            {
                ClientesView clientesView = new ClientesView();
                clientesView.ClienteTipoDocumentoID = cliente.IdTipoDniCliente;
                clientesView.ClienteTipoDocumento = cliente.DocumentosClientes.Descripcion;
                clientesView.ClienteNombreApellido = cliente.Descripcion;
                clientesView.ClienteNumeroDocumento = cliente.NroDniCliente;
                clientesView.ClienteDomicilio = cliente.CalleCliente;
                clientesView.ClienteLocalidadID = cliente.IdLocalidad;
                clientesView.ClienteLocalidad = cliente.Localidades.Descripcion;
                clientesView.ClienteTelefono = cliente.TelefonoCliente;
                clientesView.ClienteCelular = cliente.CelularCliente;
                clientesView.ClienteEmail = cliente.MailCliente;
                clientesView.ClienteCP = cliente.Localidades.CodigoPostal.Trim();
                clientesView.ClienteProvincia = cliente.Localidades.Provincias.Descripcion;
                return clientesView;
            }
            return default(ClientesView);
        }
    }
}
