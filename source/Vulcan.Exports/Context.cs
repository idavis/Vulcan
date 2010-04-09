using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vulcan.Exports.Interfaces;

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
