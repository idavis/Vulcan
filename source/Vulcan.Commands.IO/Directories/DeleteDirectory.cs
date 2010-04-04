#region Using Directives

using System;

#endregion

namespace Vulcan.Commands.IO.Directories.Commands
{
    [Serializable]
    public class DeleteDirectory : DirectoryCommand
    {
        public bool IncludeSubFolders { get; set; }
    }
}