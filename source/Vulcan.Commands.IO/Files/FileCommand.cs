#region Using Directives

using System;
using Vulcan.Exports.Commands;

#endregion

namespace Vulcan.Commands.IO.Files
{
    [Serializable]
    public abstract class FileCommand : Command
    {
        public string Directory { get; set; }
    }
}