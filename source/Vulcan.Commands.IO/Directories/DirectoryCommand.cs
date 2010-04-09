#region Using Directives

using System;
using Vulcan.Exports.Commands;

#endregion

namespace Vulcan.Commands.IO.Directories
{
    [Serializable]
    public abstract class DirectoryCommand : Command
    {
        public string Directory { get; set; }
    }
}