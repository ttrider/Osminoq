using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public abstract class DataItemReader : IDataItemReader
    {
        public IDataPackageModel Settings { get; private set; }

        protected DataItemReader(IDataPackageModel settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            this.Settings = settings;
        }

        #region Dispose

        ~DataItemReader()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.IsDisposed)
            {
                this.IsDisposed = true;
                if (Disposed != null)
                {
                    Disposed(this, EventArgs.Empty);
                }
            }
        }

        protected void CheckDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(this.ToString());
            }
        }

        public event EventHandler Disposed;
        public bool IsDisposed { get; private set; }
        #endregion Dispose

        #region IDataItemReader
        public abstract IDataItem ReadDataItem();
        public abstract bool NextDataSet();

        #endregion IDataItemReader
    }
}
