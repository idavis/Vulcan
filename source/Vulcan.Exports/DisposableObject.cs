#region Using Directives

using System;

#endregion

namespace Vulcan.Exports
{
    public abstract class DisposableObject : IDisposableObject
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
            if ( !IsDisposed )
            {
                if ( disposing )
                {
                    DisposeManagedResources();
                }
                DisposeUnmanagedResources();
                IsDisposed = true;
            }
        }

        protected virtual void DisposeManagedResources()
        {
        }

        protected virtual void DisposeUnmanagedResources()
        {
        }
    }
}