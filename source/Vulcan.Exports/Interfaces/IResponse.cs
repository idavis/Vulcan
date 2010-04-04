#region Using Directives

using System;
using System.Collections.Generic;
using Vulcan.Exports.Commands;

#endregion

namespace Vulcan.Exports.Interfaces
{
    public interface IResponse
    {
        IEnumerable<Exception> Errors { get; set; }
        CommandState State { get; set; }
    }

    public interface IResponse<T> : IResponse
    {
        T Command { get; set; }
    }
}