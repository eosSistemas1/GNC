using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.UserControls
{
    public partial class BuscarTipoOperacion : UserControl
    {
        private TiposOperacionesLogic _logic;
        private TiposOperacionesLogic logic
        {
            get
            {
                if (_logic == null) _logic = new TiposOperacionesLogic();
                return _logic;
            }
        }

        public void SetFocus()
        {
            this.comboboxTipoOperaciones.Focus();
        }

        public ViewEntity SelectedValue
        {
            get
            {
                if (this.comboboxTipoOperaciones.SelectedIndex == -1) return null;

                int idx = this.comboboxTipoOperaciones.SelectedIndex;
                return new ViewEntity(new Guid(this.comboboxTipoOperaciones.Items[idx].Value),
                                      this.comboboxTipoOperaciones.Items[idx].Text);
            }
            set
            {
                ListItem li = new ListItem(value.Descripcion, value.ID.ToString());

                if (this.comboboxTipoOperaciones.Items.Count == 0) LoadData();

                if (this.comboboxTipoOperaciones.Items.Contains(li))
                    this.comboboxTipoOperaciones.SelectedIndex = this.comboboxTipoOperaciones.Items.IndexOf(li);
                else
                    this.comboboxTipoOperaciones.SelectedIndex = -1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LoadData();
            }
        }

        private void LoadData()
        {
            List<ViewEntity> TipoOperaciones = this.logic.ReadListView()
                                                  .Where(t => !String.IsNullOrEmpty(t.Descripcion))                                                  
                                                  .ToList();

            TipoOperaciones.Insert(0, new ViewEntity(Guid.Empty, ""));

            foreach (var TipoOperacion in TipoOperaciones)
            {
                ListItem item = new ListItem(TipoOperacion.Descripcion, TipoOperacion.ID.ToString());
                this.comboboxTipoOperaciones.Items.Add(item);
            }
        }
    }
}