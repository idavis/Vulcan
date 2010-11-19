#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Vulcan.Exports.Tests.Util
{
    internal class UnitExt
    {
        public static bool Throws<T>( Action action ) where T : Exception
        {
            bool throws = false;
            try
            {
                action();
            }
            catch ( Exception )
            {
                throws = true;
            }
            return throws;
        }

        public static bool Contains<T>( T value, IEnumerable<T> collection )
        {
            return collection.Contains( value );
        }
    }
}