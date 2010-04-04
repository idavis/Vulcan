#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace Vulcan.Exports.Commands
{
    public abstract class Response : IResponse
    {
        protected Response()
            : this( CommandState.OK )
        {
        }

        protected Response( CommandState state )
            : this( state, new Exception[0] )
        {
        }

        protected Response( CommandState state, IEnumerable<Exception> errors )
        {
            State = state;
            Errors = errors;
        }

        #region Implementation of IResponse

        public IEnumerable<Exception> Errors { get; set; }
        public CommandState State { get; set; }

        #endregion
    }

    public class Response<TCommand> : Response, IResponse<TCommand> where TCommand : ICommand
    {
        public Response()
            : this( default( TCommand ) )
        {
        }

        public Response( TCommand command )
            : this( command, CommandState.OK )
        {
        }

        public Response( TCommand command, CommandState state )
            : base( state )
        {
            Command = command;
        }

        public Response( TCommand command, CommandState state, IEnumerable<Exception> errors )
            : base( state, errors )
        {
            Command = command;
        }

        public TCommand Command { get; set; }
    }
}