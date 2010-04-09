#region Using Directives

using System;

#endregion

namespace Vulcan.Commands.IO.Directories
{
    [Serializable]
    public class DeleteDirectory : DirectoryCommand
    {
        public bool IncludeSubFolders { get; set; }
    }
}