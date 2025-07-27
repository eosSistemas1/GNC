using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetroleraManager.DataAccess;
using PetroleraManager.Logic;
using PetroleraManager.Entities;

namespace PetroleraManager.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        TestLogic logic = new TestLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                cboTest.DataSource = logic.ReadListView();

                //var test = logic.Read(new Guid("d4bc1036-ce05-4802-9a0c-688f9fb3377e"));

                //jj.Text = test.Descripcion;

                //test.Descripcion = "nueva descripcion";
                //logic.Update(test);

                //test = logic.Read(new Guid("d4bc1036-ce05-4802-9a0c-688f9fb3377e"));
                //jj.Text = test.Descripcion;
            }
            
           

           // logic.Delete(new Guid("9f8f22eb-6f68-4092-948e-01ddf0d500b9"));
            //var test2 = new Test();
            //test2.Descripcion = "A ver si guarda o";
            //logic.Add(test2);
        }
    
        public void Add_Click(object seder, EventArgs e)
        {

            Test testEntity = new Test();
            testEntity.Descripcion = "Test agregado";
            logic.Add(testEntity);
        }


        public void Delete_Click(object seder, EventArgs e)
        {
            logic.Delete(new Guid(jj.Text));
        }


        public void Update_Click(object seder, EventArgs e)
        {            
            Test testEntity = new Test();
            testEntity.ID = new Guid("d4bc1036-ce05-4802-9a0c-688f9fb3377e");
            testEntity.Descripcion = jj.Text;
            logic.Update(testEntity);
        }

        public void BuscarExt_Click(object seder, EventArgs e)
        {            
            
            var param = new TestParameters();
            if (!String.IsNullOrEmpty(jj.Text))
            {
                param.Descripcion = jj.Text.Trim();
            }
            var testEXt = logic.ReadExtendedView(param);
            grd.DataSource = testEXt;
            grd.DataBind();
        }
    }

    


}
