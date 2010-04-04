#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace Vulcan.Commands.IO.Files.Commands
{
    [Serializable]
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