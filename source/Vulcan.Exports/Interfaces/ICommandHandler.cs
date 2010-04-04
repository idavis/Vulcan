#region Using Directives

using System.Threading;
using Vulcan.Exports.Commands;

#endregion

namespace Vulcan.Exports.Interfaces
{
    public interface ICommandHandler<in TCommand, out TResponse>
        where TCommand : ICommand
        where TResponse : IResponse
    {
        TResponse Execute( IContext context, TCommand command, CancellationToken token );
    }
}