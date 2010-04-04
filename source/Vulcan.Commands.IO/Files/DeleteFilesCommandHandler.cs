#region Using Directives

using System;
using System.IO;
using System.Linq;
using System.Threading;
using Vulcan.Commands.IO.Files.Commands;
using Vulcan.Exports.Commands;
using Vulcan.Exports.Handlers;
using Vulcan.Exports.Interfaces;

#endregion

namespace Vulcan.Commands.IO.Files.Handlers
{
    public class DeleteFilesCommandHandler : CommandHandler<DeleteFilesCommand, IResponse<DeleteFilesCommand>>
    {
        #region Overrides of CommandHandler<DeleteFilesCommand,IResponse>

        public override IResponse<DeleteFilesCommand> Execute(IContext context, DeleteFilesCommand command, CancellationToken token)
        {
            string target = context.Resolve( command.Directory );
            string[] files = Directory.GetFiles( target, command.Pattern );
            foreach ( var fileInfo in from file in files
                                      where File.Exists( file )
                                      select new FileInfo( file ) )
            {
                AbortIfCancellationIsRequested( command, token );
                fileInfo.Delete();
            }
            return new Response<DeleteFilesCommand>(command);
        }

        #endregion
    }
}