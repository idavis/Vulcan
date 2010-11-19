#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using Vulcan.Exports.Interfaces;

#endregion

namespace Vulcan.Exports.Commands
{
    [Serializable]
    public abstract class Response : IResponse
    {
        protected Response()
                : this( CommandState.OK )
        {
        }

        protected Response( CommandState state )
                : this( state, Enumerable.Empty<Exception>() )
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

    [Serializable]
    public class Response<TCommand> : Response, IResponse<TCommand>
            where TCommand : ICommand
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

        #region IResponse<TCommand> Members

        public TCommand Command { get; set; }

        #endregion
    }
}