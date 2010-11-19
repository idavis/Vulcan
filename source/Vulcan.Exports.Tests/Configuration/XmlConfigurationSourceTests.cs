#region Using Directives

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vulcan.Exports.Configuration;
using Vulcan.Exports.Tests.Classes;

#endregion

namespace Vulcan.Exports.Tests.Configuration
{
    [TestClass]
    public class XmlConfigurationSourceTests
    {
        [TestMethod]
        public void CanParseSingleSection()
        {
            IConfigurationSection section = SectionGenerator.GetSingleSection();

            string xml = XmlConfigurationSource.ToXml( new[] { section } );

            var source = new XmlConfigurationSource( xml );
            Assert.AreEqual( 1, source.Sections.Count );
            Assert.IsNotNull( source.Sections["Default"] );
            int count = 0;
            foreach ( IConfigurationSection configurationSection in source )
            {
                foreach ( KeyValuePair<string, string> pair in configurationSection )
                {
                    Assert.AreEqual( pair.Value, section.Get<string>( pair.Key ) );
                    count++;
                }

                foreach ( KeyValuePair<string, string> pair in section )
                {
                    Assert.AreEqual( pair.Value, configurationSection.Get<string>( pair.Key ) );
                    count++;
                }
            }
            Assert.AreEqual( 10, count );
        }

        [TestMethod]
        public void CanParseMultipleSections()
        {
            string xml = XmlConfigurationSource.ToXml( SectionGenerator.GetThreeSections() );

            var source = new XmlConfigurationSource( xml );
            Assert.AreEqual( 3, source.Sections.Count );
            Assert.IsNotNull( source.Sections["Default"] );
            Assert.IsNotNull( source.Sections["Default2"] );
            Assert.IsNotNull( source.Sections["Default3"] );

            int count = 0;
            foreach ( IConfigurationSection configurationSection in source )
            {
                foreach ( KeyValuePair<string, string> pair in configurationSection )
                {
                    Assert.AreEqual( pair.Value,
                                     source.Sections[configurationSection.Name].Get<string>( pair.Key ) );
                    count++;
                }

                foreach ( KeyValuePair<string, string> pair in source.Sections[configurationSection.Name] )
                {
                    Assert.AreEqual( pair.Value, configurationSection.Get<string>( pair.Key ) );
                    count++;
                }
            }
            Assert.AreEqual( 10, count );
        }

        [TestMethod]
        public void CanLoadFromFile()
        {
            string xml = XmlConfigurationSource.ToXml( SectionGenerator.GetThreeSections() );

            var source = new XmlConfigurationSource( xml ) { FileName = "CanLoadFromFile.xml" };
            source.Save();

            IConfigurationSource sourceFromFile = XmlConfigurationSource.FromFile( "CanLoadFromFile.xml" );
            string sourceString = source.ToString();
            string sourceFromFileString = sourceFromFile.ToString();
            Assert.AreEqual( sourceString, sourceFromFileString );
        }
    }
}