using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Vulcan.Exports.Commands;

namespace Vulcan.Commands.IO.Files.Commands
{
    public class DeleteFilesCommand : FileCommand
    {
        public string Pattern { get; set; }
    }

    public class DeleteFilesCommandHandler : CommandHandler<CopyFilesCommand, IResponse<CopyFilesCommand>>
    {
        #region Overrides of CommandHandler<CopyFilesCommand,IResponse>

        public override IResponse<CopyFilesCommand> Execute(IContext context, CopyFilesCommand command, CancellationToken token)
        {
            string source = context.Resolve( command.Directory );
            string destination = context.Resolve( command.Destination );
            IEnumerable<string> files = command.IncludePatterns.SelectMany<string, string>(
                pattern =>
                Directory.GetFiles( source, pattern,
                                    command.IncludeSubDirectories
                                        ? SearchOption.AllDirectories
                                        : SearchOption.TopDirectoryOnly ) );
            
            
        }

        #endregion
    }
    public class CopyFilesCommand : FileCommand
    {
        public string Destination { get; set; }
        public IEnumerable<string> IncludePatterns { get; set; }
        public IEnumerable<string> ExcludeFiles { get; set; }
        public bool IncludeSubDirectories { get; set; }
        public bool IncludeEmptySubDirectories { get; set; }
        public bool CopyOnlyFilesThatHaveChanged { get; set; }
        public bool SkipFilesOlderThanDestination { get; set; }
    }
}