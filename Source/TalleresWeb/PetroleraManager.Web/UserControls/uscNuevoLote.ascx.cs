using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls;
using PetroleraManager.Logic;
using ET = PetroleraManager.Entities;

namespace PetroleraManager.Web.UserControls
{
    public partial class uscNuevoLote : System.Web.UI.UserControl
    {
        LotesLogic logic = new LotesLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        public Boolean Grabar()
        {
            String msjValida = String.Empty;

            Boolean valido = logic.LoteValido(Decimal.Parse(txtNroObleaDesde.Text), Decimal.Parse(txtNroObleaHasta.Text));
            if (!valido) msjValida += "- El lote ya fue ingresado. <br/>";

            Decimal dif = Decimal.Parse(txtNroObleaHasta.Text) - Decimal.Parse(txtNroObleaDesde.Text);
            if(dif != Decimal.Parse(txtCantidadObleas.Text)) msjValida += "- La cantidad de obleas no coincide. <br/>";

            if(Decimal.Parse(txtNroObleaDesde.Text) > Decimal.Parse(txtNroObleaHasta.Text)) msjValida += "- La cantidad desde no puede ser mayor a a cantidad hasta. <br/>";

            if (msjValida.Equals(String.Empty))
            {
                ET.LOTES lotesEntity = new ET.LOTES();
                if (txtID.Text == String.Empty)
                {
                    //NUEVO
                    lotesEntity.FechaAlta = DateTime.Parse(calFechaAlta.Text);
                    lotesEntity.NroObleaDesde = Decimal.Parse(txtNroObleaDesde.Text);
                    lotesEntity.NroObleaHasta = Decimal.Parse(txtNroObleaHasta.Text);
                    lotesEntity.NroUltimaObleaUsada = Decimal.Parse(txtNroObleaDesde.Text);
                    lotesEntity.LoteActivo = chkActivo.Checked;
                    lotesEntity.AnioLote = Decimal.Parse(txtAnioLote.Text);
                    lotesEntity.CantObleasLote = Decimal.Parse(txtCantidadObleas.Text);
                    logic.Add(lotesEntity);
                }
                else
                {
                    //MODIFICAR 
                    lotesEntity.ID = new Guid(txtID.Text);
                    lotesEntity.FechaAlta = DateTime.Parse(calFechaAlta.Text);
                    lotesEntity.NroObleaDesde = Decimal.Parse(txtNroObleaDesde.Text);
                    lotesEntity.NroObleaHasta = Decimal.Parse(txtNroObleaHasta.Text);
                    lotesEntity.NroUltimaObleaUsada = 0;
                    lotesEntity.LoteActivo = chkActivo.Checked;
                    lotesEntity.AnioLote = Decimal.Parse(txtAnioLote.Text);
                    lotesEntity.CantObleasLote = Decimal.Parse(txtCantidadObleas.Text);
                    logic.Update(lotesEntity);
                }

                return true;
            }
            else 
            {
                MessageBoxCtrl1.MessageBox(null, msjValida, MessageBoxCtrl.TipoWarning.Warning);
                return false;
            }
        }


        public void LimpiarCampos()
        {
            txtID.Text = String.Empty;
            calFechaAlta.Text = DateTime.Now.ToShortDateString();
            txtNroObleaDesde.Text = String.Empty;
            txtNroObleaHasta.Text = String.Empty;
            txtAnioLote.Text = String.Empty;
            txtCantidadObleas.Text = String.Empty;
            chkActivo.Checked = false;
        }


        public void CargarDatos(Guid idProducto)
        {
            var lote = logic.Read(idProducto);

            txtID.Text = lote.ID.ToString();
            calFechaAlta.Text = lote.FechaAlta.Value.ToShortDateString();
            txtNroObleaDesde.Text= lote.NroObleaDesde.ToString();
            txtNroObleaHasta.Text = lote.NroObleaHasta.ToString();
            txtAnioLote.Text = lote.AnioLote.ToString();
            txtCantidadObleas.Text = lote.CantObleasLote.ToString();
            chkActivo.Checked = lote.LoteActivo;
        }
    }
}