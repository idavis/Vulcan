#region Using Directives

using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Vulcan.Exports.Commands
{
    public abstract class CommandHandler<TCommand, TResponse>
        : DisposableObject, ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand
        where TResponse : IResponse<TCommand>
    {
        protected readonly object SyncRoot = new object();

        #region ICommandHandler<TCommand,TResponse> Members

        public abstract TResponse Execute( IContext context, TCommand command, CancellationToken token );

        #endregion

        public event EventHandler Canceled;

        public virtual TResponse Execute( IContext context, ICommand command )
        {
            var typedCommand = (TCommand) command;
            BeforeExecute( context, typedCommand );
            TResponse response = ExecuteAsync( context, typedCommand );
            AfterExecute( context, typedCommand );
            return response;
        }

        public virtual TResponse ExecuteAsync( IContext context, TCommand command )
        {
            using ( var cancellationTokenSource = new CancellationTokenSource() )
            {
                CancellationToken token = cancellationTokenSource.Token;
                EventHandler handler = (s, e) => cancellationTokenSource.Cancel();
                Canceled += handler;
                var action = new Func<TResponse>( () => Execute( context, command, token ) );
                Task<TResponse> task = Task<TResponse>.Factory.StartNew( action, token );
                try
                {
                    task.Wait( token );
                }
                catch ( AggregateException ae )
                {
                    foreach ( Exception innerException in ae.InnerExceptions )
                    {
                        if ( innerException is TaskCanceledException )
                        {
                            // set state to canceled.
                        }
                    }
                }
                finally
                {
                    Canceled -= handler;
                }
                return task.Result;
            }
        }

        public virtual void BeforeExecute( IContext context, TCommand command )
        {
        }

        public virtual void AfterExecute( IContext context, TCommand command )
        {
        }

        public virtual void Cancel()
        {
            EventHandler handler = Canceled;
            if ( handler != null )
            {
                handler( this, EventArgs.Empty );
            }
        }

        protected virtual void AbortIfCancellationIsRequested(ICommand command, CancellationToken token)
        {
            if ( !command.Behavior.SupportsCancelation )
            {
                return;
            }

            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
        }
    }
}