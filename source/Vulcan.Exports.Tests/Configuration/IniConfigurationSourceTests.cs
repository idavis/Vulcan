#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vulcan.Exports.Configuration;
using Vulcan.Exports.Tests.Properties;
using Vulcan.Exports.Tests.Util;

#endregion

namespace Vulcan.Exports.Tests.Configuration
{
    [TestClass]
    public class IniConfigurationSourceTests
    {
        [TestMethod]
        public void CanProcessValidIniFile()
        {
            var source = new IniConfigurationSource( Resources.IniTestCases );
            List<IConfigurationSection> sections = source.Sections.Values.ToList();

            Assert.AreEqual( 5, sections.Count );
            Assert.AreEqual( "owner", sections[0].Name );
            Assert.AreEqual( 2, sections[0].Count );
            Assert.AreEqual( sections[0].Get<string>( "name" ), "John Doe" );
            Assert.AreEqual( sections[0].Get<string>( "organization" ), "Acme Products" );

            Assert.AreEqual( "database", sections[1].Name );
            Assert.AreEqual( 3, sections[1].Count );
            Assert.AreEqual( sections[1].Get<string>( "server" ), "192.0.2.42" );
            Assert.AreEqual( sections[1].Get<string>( "port" ), "143" );
            Assert.AreEqual( sections[1].Get<string>( "file" ), "\"acme payroll.dat\"" );

            Assert.AreEqual( "Empty", sections[2].Name );
            Assert.AreEqual( 1, sections[2].Count );
            Assert.AreEqual( sections[2].Get<string>( "MyEmptyValue" ), "" );

            Assert.AreEqual( "Completely Empty Section", sections[3].Name );
            Assert.AreEqual( 0, sections[3].Count );

            Assert.AreEqual( "NonEmptyAfterCompletelyEmpty", sections[4].Name );
            Assert.AreEqual( 1, sections[4].Count );
            Assert.AreEqual( sections[4].Get<string>( "mykey" ), "myval  akdk" );

            foreach ( IConfigurationSection section in source.Sections.Values )
            {
                foreach ( KeyValuePair<string, string> pair in section )
                {
                    Console.WriteLine( pair.Key + ", " + pair.Value );
                }
            }
        }

        [TestMethod]
        public void ThrowsOnEmptySectionNames()
        {
            Assert.IsTrue( UnitExt.Throws<InvalidOperationException>(
                    () => new IniConfigurationSource( Resources.EmptySectionnameTest0 ) ) );
            Assert.IsTrue( UnitExt.Throws<InvalidOperationException>(
                    () => new IniConfigurationSource( Resources.EmptySectionnameTest1 ) ) );
            Assert.IsTrue( UnitExt.Throws<InvalidOperationException>(
                    () => new IniConfigurationSource( Resources.EmptySectionnameTest2 ) ) );
        }

        [TestMethod]
        public void SavingWithoutSettingFileNameFails()
        {
            Assert.IsTrue(
                    UnitExt.Throws<InvalidOperationException>(
                            () => new IniConfigurationSource( Resources.IniTestCases ).Save() ) );
        }

        [TestMethod]
        public void CanLoadFromFile()
        {
            var source = new IniConfigurationSource( Resources.IniTestCases ) { FileName = "CanLoadFromFile.ini" };
            source.Save();

            IConfigurationSource sourceFromFile = IniConfigurationSource.FromFile( "CanLoadFromFile.ini" );
            string sourceString = source.ToString();
            string sourceFromFileString = sourceFromFile.ToString();
            Assert.AreEqual( sourceString, sourceFromFileString );
        }
    }
}