namespace Vulcan.Exports.Commands
{
    public abstract class Command : ICommand
    {
        protected Command()
        {
        }

        protected Command( ICommandBehavior behavior, ICommandIdentity identity )
        {
            Behavior = behavior;
            Identity = identity;
        }

        #region Implementation of ICommand

        public ICommandBehavior Behavior { get; protected set; }
        public ICommandIdentity Identity { get; protected set; }

        #endregion
    }
}