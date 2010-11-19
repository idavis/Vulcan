#region Using Directives

using System.IO;
using System.Threading;
using Vulcan.Exports.Commands;
using Vulcan.Exports.Handlers;
using Vulcan.Exports.Interfaces;

#endregion

namespace Vulcan.Commands.IO.Directories
{
    public class CreateDirectoryCommandHandler : CommandHandler<CreateDirectory, IResponse<CreateDirectory>>
    {
        #region Overrides of CommandHandler<FolderCommand,IResponse>

        public override IResponse<CreateDirectory> Execute( IContext context,
                                                            CreateDirectory command,
                                                            CancellationToken token )
        {
            var target = context.Resolve<string>( command.Directory );
            if ( Directory.Exists( target ) )
            {
                // return nothing to do
                return new Response<CreateDirectory>( command, CommandState.NothingToDo );
            }

            DirectoryInfo directoryInfo = Directory.CreateDirectory( target );
            CommandState result = directoryInfo.Exists ? CommandState.OK : CommandState.CommandFailed;
            return new Response<CreateDirectory>( command, result );
        }

        #endregion
    }
}