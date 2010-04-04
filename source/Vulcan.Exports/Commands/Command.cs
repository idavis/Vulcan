#region Using Directives

using System;
using Vulcan.Exports.Interfaces;

#endregion

namespace Vulcan.Exports.Commands
{
    [Serializable]
    public abstract class Command : ICommand, ICloneable
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

        #region Implementation of ICloneable

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}