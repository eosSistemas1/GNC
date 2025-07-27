using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.UserControls
{
    public partial class BuscarTaller : System.Web.UI.UserControl
    {
        private TalleresLogic _logic;
        private TalleresLogic logic
        {
            get
            {
                if (_logic == null) _logic = new TalleresLogic();
                return _logic;
            }
        }

        public void SetFocus()
        {
            this.comboboxTalleres.Focus();
        }

        public ViewEntity SelectedValue
        {
            get
            {
                if (this.comboboxTalleres.SelectedIndex == -1) return null;

                int idx = this.comboboxTalleres.SelectedIndex;
                return new ViewEntity(new Guid(this.comboboxTalleres.Items[idx].Value),
                                      this.comboboxTalleres.Items[idx].Text);
            }
            set
            {                               
                if (this.comboboxTalleres.Items.Count == 0) LoadData();

                this.comboboxTalleres.SelectedIndex = -1;
                foreach (ListItem taller in this.comboboxTalleres.Items)
                {
                    if (taller.Value.ToUpper() == value.ID.ToString().ToUpper())
                    {
                        this.comboboxTalleres.SelectedIndex = this.comboboxTalleres.Items.IndexOf(taller);
                    }
                }                                               
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
            List<ViewEntity> talleres = this.logic.ReadListView()
                                                  .Where(t => !String.IsNullOrEmpty(t.Descripcion))                                                  
                                                  .ToList();

            talleres.Insert(0, new ViewEntity(Guid.Empty, ""));

            foreach (var taller in talleres)
            {
                ListItem item = new ListItem(taller.Descripcion, taller.ID.ToString());
                this.comboboxTalleres.Items.Add(item);
            }
        }
    }
}