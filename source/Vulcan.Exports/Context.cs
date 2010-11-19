#region Using Directives

using System.Collections.Generic;
using Vulcan.Exports.Interfaces;

#endregion

namespace Vulcan.Exports
{
    public class Context : IContext
    {
        public IEnumerable<IVariable<object>> Variables { get; protected set; }

        #region Implementation of IContext

        public T Resolve<T>( string value )
        {
            // use configuration variables in order to resolve information.
            return default( T );
        }

        #endregion
    }
}