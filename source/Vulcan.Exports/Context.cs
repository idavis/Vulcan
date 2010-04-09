#region Using Directives

using System;
using Vulcan.Exports.Interfaces;

#endregion

namespace Vulcan.Exports
{
    public class Context : IContext
    {
        #region Implementation of IContext

        public T Resolve<T>( string value )
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}