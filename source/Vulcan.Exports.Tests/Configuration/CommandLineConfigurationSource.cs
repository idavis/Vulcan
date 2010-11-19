#region Using Directives

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vulcan.Exports.Configuration;

#endregion

namespace Vulcan.Exports.Tests.Configuration
{
    [TestClass]
    public class CommandLineConfigurationSourceTests
    {
        [TestMethod]
        public void Do()
        {
            var arguments = new[] { "/?", "--help", "-h", "/platform:x86" };
            var source = new CommandLineConfigurationSource( arguments );
            source.AddSwitch( "Default", "/?", "help", "h", "platform" );
            IConfigurationSection section = source.Sections["Default"];
            Console.WriteLine( source );
        }
    }
}