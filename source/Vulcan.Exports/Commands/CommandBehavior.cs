using System;

namespace Vulcan.Exports.Commands
{
    public class CommandBehavior : ICommandBehavior
    {
        #region Implementation of ICommandBehavior

        public bool Enabled { get; set; }
        public bool IgnoreFailue { get; set; }
        public int NumberOfRetries { get; set; }
        public int DelayBetweenRetries { get; set; }
        public bool SupportsCancelation { get; set; }

        #endregion
    }
}