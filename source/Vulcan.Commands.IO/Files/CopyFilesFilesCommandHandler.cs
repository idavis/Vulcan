#region Using Directives

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Vulcan.Exports.Commands;
using Vulcan.Exports.Handlers;
using Vulcan.Exports.Interfaces;

#endregion

namespace Vulcan.Commands.IO.Files
{
    public class CopyFilesFilesCommandHandler : CommandHandler<CopyFilesCommand, IResponse<CopyFilesCommand>>
    {
        #region Overrides of CommandHandler<CopyFilesCommand,IResponse>

        public override IResponse<CopyFilesCommand> Execute( IContext context,
                                                             CopyFilesCommand command,
                                                             CancellationToken token )
        {
            var source = context.Resolve<string>( command.Directory );
            var destination = context.Resolve<string>( command.Destination );
            IEnumerable<string> files = command.IncludePatterns.SelectMany(
                    pattern =>
                    Directory.GetFiles( source,
                                        pattern,
                                        command.IncludeSubDirectories
                                                ? SearchOption.AllDirectories
                                                : SearchOption.TopDirectoryOnly ) );
            foreach ( string file in files )
            {
                AbortIfCancellationIsRequested( command, token );
                File.Copy( file, "some target" );
            }
            return new Response<CopyFilesCommand>( command );
        }

        #endregion
    }
}