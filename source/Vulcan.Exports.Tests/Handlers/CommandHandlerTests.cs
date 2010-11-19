#region Using Directives

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vulcan.Exports.Commands;
using Vulcan.Exports.Handlers;
using Vulcan.Exports.Interfaces;

#endregion

namespace Vulcan.Exports.Tests.Handlers
{
    public class SimpleCommand : Command
    {
        public SimpleCommand()
                : base( new CommandBehavior { SupportsCancelation = true }, null )
        {
        }
    }

    public class SimpleResponse : Response<SimpleCommand>
    {
    }

    public class SimpleCommandHandler : CommandHandler<SimpleCommand, SimpleResponse>
    {
        public override SimpleResponse Execute( IContext context, SimpleCommand command, CancellationToken token )
        {
            Thread.Sleep( 10000 );
            AbortIfCancellationIsRequested( command, token );
            return new SimpleResponse();
        }
    }

    [TestClass]
    public class CommandHandlerTests
    {
        [TestMethod]
        public void WhenATaskIsCanceledThenIsStateIsCancelled()
        {
            var handler = new SimpleCommandHandler();
            var command = new SimpleCommand();
            Func<SimpleResponse> func = () => handler.Execute( new Context(), command );
            Task<SimpleResponse> task = Task<SimpleResponse>.Factory.StartNew( func );
            handler.Cancel();
            task.Wait();
            Assert.AreEqual( CommandState.Cancelled, task.Result.State );
        }
    }
}