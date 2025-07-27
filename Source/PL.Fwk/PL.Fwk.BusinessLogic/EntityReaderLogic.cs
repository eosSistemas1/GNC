using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using PL.Fwk.DataAccess;
using PL.Fwk.Entities;

namespace PL.Fwk.BusinessLogic
{
    public class EntityReaderLogic<E, DA>:IEntityReaderLogic<E>
      where  E: EntityObject, IIdentifiable
        where DA: IEntityReaderDataAccess<E>, new()
    {
        private DA _dataAccess;
        protected DA EntityDataAccess 
        { 
            get
            {
                if (_dataAccess == null)
	            {
                    _dataAccess = this.CreateDataAccess();
	            }
                return _dataAccess;
            }
            set
            {
                _dataAccess = value;
            }
        }

        protected virtual DA CreateDataAccess()
        {
            return new DA();
        }


        public virtual E Read(Guid id)
        {
          return  this.EntityDataAccess.Read(id);
        }



        #region IEntityReaderLogic<E> Members


        public ViewEntity ReadView(Guid id)
        {
            return this.EntityDataAccess.ReadView(id);
        }

        public List<E> ReadAll()
        {
            return this.EntityDataAccess.ReadAll();
        }

        public List<ViewEntity> ReadListView()
        {
            return this.EntityDataAccess.ReadListView();
        }

        #endregion
    }
}
