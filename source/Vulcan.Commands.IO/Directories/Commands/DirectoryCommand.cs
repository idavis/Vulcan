using Vulcan.Exports.Commands;

namespace Vulcan.Commands.IO.Directories.Commands
{
    public abstract class DirectoryCommand : Command
    {
        public string Directory { get; set; }
    }
}