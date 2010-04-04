#region Using Directives

using System.IO;
using System.Threading;
using Vulcan.Commands.IO.Directories.Commands;
using Vulcan.Exports.Commands;

#endregion

namespace Vulcan.Commands.IO.Directories.Handlers
{
    public class DeleteDirectoryCommandHandler : CommandHandler<DeleteDirectory, IResponse<DeleteDirectory>>
    {
        #region Overrides of CommandHandler<FolderCommand,IResponse>

        public override IResponse<DeleteDirectory> Execute(IContext context, DeleteDirectory command, CancellationToken token)
        {
            string target = context.Resolve( command.Directory );
            if ( !Directory.Exists( target ) )
            {
                // return nothing to do
            }

            Directory.Delete( target, command.IncludeSubFolders );
            //return success;
            return null;
        }

        #endregion
    }
}