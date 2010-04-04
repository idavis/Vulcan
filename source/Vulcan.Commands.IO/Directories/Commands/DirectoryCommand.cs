using Vulcan.Exports.Commands;

namespace Vulcan.Commands.IO.Directories.Commands
{
    public abstract class DirectoryCommand : AbstractCommand
    {
        public string Directory { get; set; }
    }
}