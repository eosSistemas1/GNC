using System.Collections.Generic;
using System;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using PL.Fwk.Entities;
using CrossCutting.DatosDiscretos;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboEstadosPhConsulta : DropDownList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (string.IsNullOrWhiteSpace(CssClass))
                this.Attributes.Add("class", "form-control");

            LoadData();
        }

        public void LoadData()
        {
            List<ViewEntity> estados = new List<ViewEntity>();
            estados.Add(new ViewEntity(Guid.Empty, "Seleccione"));
            estados.Add(new ViewEntity(EstadosPH.VerificarCodigos, "Verifica Códigos"));
            estados.Add(new ViewEntity(EstadosPH.Bloqueada, "Bloquea"));

            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";
            this.DataSource = estados;
            this.DataBind();
        }
    }
}