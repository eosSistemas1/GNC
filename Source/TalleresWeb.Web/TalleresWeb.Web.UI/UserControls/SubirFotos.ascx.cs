using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI.UserControls
{
    public partial class SubirFotos : System.Web.UI.UserControl
    {
        #region Properties
        private ObleasLogic obleasLogic;
        public ObleasLogic ObleasLogic
        {
            get
            {
                if (this.obleasLogic == null) this.obleasLogic = new ObleasLogic();
                return this.obleasLogic;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            hdnTallerID.Value = ((ViewEntity)HttpContext.Current.Session["TALLERID"]).ID.ToString();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            String path = Server.MapPath("~/Captures/");

            this.Guardar(fileDniFrente, labelDniFrente, path, "DNIFRENTE");
            this.Guardar(fileDniDorso, labelDniDorso, path, "DNIDORSO");
            this.Guardar(fileTjFrente, labelTjFrente, path, "TJFRENTE");
            this.Guardar(fileTjDorso, labelTjDorso, path, "TJDORSO");
        }

        private bool Guardar(FileUpload fileUpload, Label label, string path, string tipoFoto)
        {
            Boolean fileOK = ValidarFormato(fileUpload);

            if (fileOK)
            {
                this.SubirArchivo(fileUpload, label, path, tipoFoto);
            }
            else
            {
                if (fileUpload.HasFile)
                    label.Text = "Formato de archivo incorrecto.";
            }

            return fileOK;
        }        

        private void SubirArchivo(FileUpload fileUpload, Label label, string path, string tipoFoto)
        {
            try
            {
                String tallerID = hdnTallerID.Value;
                String dominio = hdnDominio.Value;
                String nombreImagen = GetNombreImagen(dominio, tipoFoto, tallerID);
                fileUpload.PostedFile.SaveAs(path + nombreImagen + ".png");
                label.Text = "Imágen subida!";                

                this.ObleasLogic.GuardarImagen(new ImagenModel()
                {
                    ImageContent = fileUpload.FileBytes,
                    ImageName = nombreImagen
                });
            }
            catch (Exception ex)
            {
                label.Text = $"La imágen no se pudo subir. \n {ex.InnerException}";
            }
        }

        private static bool ValidarFormato(FileUpload fileUpload)
        {
            if (fileUpload.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                String[] allowedExtensions = { ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private String GetNombreImagen(string dominio, string tipoFoto, string tallerID)
        {
            String imageName = String.Empty;
            return String.Format("{0}_{1}_{2}", tallerID, dominio, tipoFoto);
        }
        #endregion
    }
}