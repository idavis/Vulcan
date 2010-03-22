#region Using Directives

using System;

#endregion

namespace Vulcan.Exports
{
    public interface IDisposableObject : IDisposable
    {
        bool IsDisposed { get; }
    }
}