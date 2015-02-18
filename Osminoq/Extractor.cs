using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public abstract class Extractor : IExtractor
    {
        public IExtractorSettings Settings { get; private set; }

        protected Extractor(IExtractorSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            this.Settings = settings;
        }


        ~Extractor()
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


        public abstract IDataItem ExtractDataItem();
    }
}
