#region Using Directives

using System;

#endregion

namespace Vulcan.Commands.IO.Files.Commands
{
    [Serializable]
    public class DeleteFilesCommand : FileCommand
    {
        public string Pattern { get; set; }
    }
}