namespace Vulcan.Exports.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        #region Implementation of ICommand

        public ICommandBehavior Behavior { get; private set; }
        public ICommandIdentity Identity { get; private set; }

        #endregion
    }
}