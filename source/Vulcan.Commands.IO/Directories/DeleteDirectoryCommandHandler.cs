#region Using Directives

using System.IO;
using System.Threading;
using Vulcan.Exports.Commands;
using Vulcan.Exports.Handlers;
using Vulcan.Exports.Interfaces;

#endregion

namespace Vulcan.Commands.IO.Directories
{
    public class DeleteDirectoryCommandHandler : CommandHandler<DeleteDirectory, IResponse<DeleteDirectory>>
    {
        #region Overrides of CommandHandler<FolderCommand,IResponse>

        public override IResponse<DeleteDirectory> Execute( IContext context, DeleteDirectory command,
                                                            CancellationToken token )
        {
            var target = context.Resolve<string>( command.Directory );
            if ( !Directory.Exists( target ) )
            {
                return new Response<DeleteDirectory>( command, CommandState.NothingToDo );
            }
            var directoryInfo = new DirectoryInfo( target );
            directoryInfo.Delete( command.IncludeSubFolders );
            CommandState result = directoryInfo.Exists ? CommandState.CommandFailed : CommandState.OK;
            return new Response<DeleteDirectory>( command, result );
        }

        #endregion
    }
}