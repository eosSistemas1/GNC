using PL.Fwk.BusinessLogic;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class S_RolesLogic : EntityManagerLogic<S_ROLES, S_RolesExtendedView, S_RolesParameters, S_RolesDataAccess>
    {
        #region Methods

        public S_ROLES ReadByIdRol(int idRol)
        {
            S_RolesDataAccess oa = new S_RolesDataAccess();
            return oa.ReadByIdRol(idRol);
        }

        public void AddRol(S_ROLES rol)
        {
            var existeRol = this.Read(rol.ID) != null;

            //add o update 
            if (!existeRol)
            {
                this.Add(rol);
            }
            else
            {
                this.Update(rol);
            }
        }

        #endregion
    }
}