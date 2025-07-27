using System;

namespace PL.Fwk.Entities
{
    public class DataEventArgs<T> : EventArgs
    {
        #region Constructors

        public DataEventArgs(T value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public T Value
        {
            get;
            private set;
        }

        #endregion
    }
}