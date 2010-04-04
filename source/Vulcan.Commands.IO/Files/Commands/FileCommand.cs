#region Using Directives

using Vulcan.Exports.Commands;

#endregion

namespace Vulcan.Commands.IO.Files.Commands
{
    public abstract class FileCommand : Command
    {
        public string Directory { get; set; }
    }
}