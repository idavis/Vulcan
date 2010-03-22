#region Using Directives

using System.Threading;

#endregion

namespace Vulcan.Exports.Commands
{
    public interface ICommandHandler<in TCommand, out TResponse>
        where TCommand : ICommand
        where TResponse : IResponse
    {
        TResponse Execute( IContext context, TCommand command, CancellationToken token );
    }
}