#region Using Directives

using System;
using Vulcan.Exports.Interfaces;

#endregion

namespace Vulcan.Exports
{
    public abstract class Disposable : IDisposableObject
    {
        #region IDisposableObject Members

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        public bool IsDisposed { get; private set; }

        #endregion

        protected virtual void Dispose( bool disposing )
        {
            if ( IsDisposed )
            {
                return;
            }

            if ( disposing )
            {
                DisposeManagedResources();
            }

            DisposeUnmanagedResources();
            IsDisposed = true;
        }

        protected virtual void DisposeManagedResources()
        {
        }

        protected virtual void DisposeUnmanagedResources()
        {
        }
    }
}