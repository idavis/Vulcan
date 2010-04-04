#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace Vulcan.Exports.Commands
{
    public abstract class Response : IResponse
    {
        public Response()
        {
            Errors = new Exception[0];
        }

        public Response( IEnumerable<Exception> errors, CommandState state )
        {
            Errors = errors;
            State = state;
        }

        #region Implementation of IResponse

        public IEnumerable<Exception> Errors { get; set; }
        public CommandState State { get; set; }

        #endregion
    }

    public class Response<T> : Response where T : ICommand
    {
        public Response()
        {
            Command = default( T );
        }

        public Response( T command )
        {
            Command = command;
        }

        public Response( IEnumerable<Exception> errors, CommandState state, T command ) : base( errors, state )
        {
            Command = command;
        }

        private T Command { get; set; }
    }
}