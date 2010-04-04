#region Using Directives

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Vulcan.Commands.IO.Files.Commands;
using Vulcan.Exports.Commands;

#endregion

namespace Vulcan.Commands.IO.Files.Handlers
{
    public class CopyFilesFilesCommandHandler : CommandHandler<CopyFilesCommand, IResponse<CopyFilesCommand>>
    {
        #region Overrides of CommandHandler<CopyFilesCommand,IResponse>

        public override IResponse<CopyFilesCommand> Execute( IContext context, CopyFilesCommand command,
                                                             CancellationToken token )
        {
            string source = context.Resolve( command.Directory );
            string destination = context.Resolve( command.Destination );
            IEnumerable<string> files = command.IncludePatterns.SelectMany(
                pattern =>
                Directory.GetFiles( source, pattern,
                                    command.IncludeSubDirectories
                                        ? SearchOption.AllDirectories
                                        : SearchOption.TopDirectoryOnly ) );
            foreach ( var file in files )
            {
                AbortIfCancellationIsRequested( command, token );
                File.Copy(file, "some target");
            }
        }

        #endregion
    }
}