#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace Vulcan.Exports.Commands
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