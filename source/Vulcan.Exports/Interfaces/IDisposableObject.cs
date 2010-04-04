#region Using Directives

using System;

#endregion

namespace Vulcan.Exports.Interfaces
{
    public interface IDisposableObject : IDisposable
    {
        bool IsDisposed { get; }
    }
}