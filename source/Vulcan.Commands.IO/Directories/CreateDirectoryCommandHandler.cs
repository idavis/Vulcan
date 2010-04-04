#region Using Directives

using System.IO;
using System.Threading;
using Vulcan.Commands.IO.Directories.Commands;
using Vulcan.Exports.Commands;
using Vulcan.Exports.Handlers;
using Vulcan.Exports.Interfaces;

#endregion

namespace Vulcan.Commands.IO.Directories.Handlers
{
    public class CreateDirectoryCommandHandler : CommandHandler<CreateDirectory, IResponse<CreateDirectory>>
    {
        #region Overrides of CommandHandler<FolderCommand,IResponse>

        public override IResponse<CreateDirectory> Execute(IContext context, CreateDirectory command, CancellationToken token)
        {
            string target = context.Resolve( command.Directory );
            if ( Directory.Exists( target ) )
            {
                // return nothing to do
            }

            DirectoryInfo directoryInfo = Directory.CreateDirectory( target );
            //return success;
            return null;
        }

        #endregion
    }
}