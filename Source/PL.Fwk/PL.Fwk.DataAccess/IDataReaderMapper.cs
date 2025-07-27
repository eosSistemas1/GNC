using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace PL.Fwk.DataAccess
{
    public interface IDataReaderMapper<T>
    {
        void MaterializeReader(DbDataReader dr, T objectToMap);
    }
}
