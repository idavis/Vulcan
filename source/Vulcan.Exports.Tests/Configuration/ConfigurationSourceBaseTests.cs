#region Using Directives

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vulcan.Exports.Configuration;
using Vulcan.Exports.Tests.Classes;
using Vulcan.Exports.Tests.Util;

#endregion

namespace Vulcan.Exports.Tests.Configuration
{
    [TestClass]
    public class ConfigurationSourceBaseTests
    {
        private const string Key = "key";
        private const string SectionName = "Default";
        private const string Value = "value";

        protected static ConfigurationSourceBaseImpl GetConfigurationSource()
        {
            var section = new ConfigurationSection( SectionName );
            var source = new ConfigurationSourceBaseImpl { section };
            section.Set( Key, Value );
            return source;
        }

        [TestMethod]
        public void MergingWithNullCollectionThrows()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            Assert.IsTrue( UnitExt.Throws<ArgumentNullException>( () => source.Merge( null ) ) );
        }

        [TestMethod]
        public void MergingWithCollectionAddsConfigurationSection()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            const string sectionName = SectionName + Key;
            var section = new ConfigurationSection( sectionName );
            section.Set( Key, Key );
            var source2 = new ConfigurationSourceBaseImpl { section };
            source.Merge( new[] { source2 } );
            Assert.IsTrue( UnitExt.Contains( section, source ) );
        }

        [TestMethod]
        public void MergingWithCollectionOverwritesExistingKeys()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, Key );
            var source2 = new ConfigurationSourceBaseImpl { section };
            Assert.AreEqual( Value, source.Sections[SectionName].Get<string>( Key ) );
            source.Merge( new[] { source2 } );
            Assert.AreEqual( Key, source.Sections[SectionName].Get<string>( Key ) );
        }

        [TestMethod]
        public void MergingWithAleadyMergedSourceDoesNothing()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            ConfigurationSourceBaseImpl newSource = GetConfigurationSource();

            Assert.AreEqual( 0, source.ConfigurationSources.Count );

            source.Merge( new[] { newSource } );
            Assert.AreEqual( 1, source.ConfigurationSources.Count );
            Assert.AreEqual( newSource, source.ConfigurationSources[0] );

            source.Merge( new[] { newSource } );
            Assert.AreEqual( 1, source.ConfigurationSources.Count );
            Assert.AreEqual( newSource, source.ConfigurationSources[0] );
        }

        [TestMethod]
        public void AddingNewSectionOverridesKeys()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, Key );

            Assert.AreEqual( Value, source.Sections[SectionName].Get<string>( Key ) );
            source.Add( section );
            Assert.AreEqual( Key, source.Sections[SectionName].Get<string>( Key ) );
        }

        [TestMethod]
        public void AddingNullSectionThrows()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            Assert.IsTrue( UnitExt.Throws<ArgumentNullException>( () => source.Add( null ) ) );
        }

        [TestMethod]
        public void ExpandSimpleWorks()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            source.Sections[SectionName].Set( Key, "${value}" );
            source.Sections[SectionName].Set( Value, Key );

            Assert.AreEqual( "${value}", source.Sections[SectionName].Get<string>( Key ) );
            source.ExpandKeyValues();
            Assert.AreEqual( Key, source.Sections[SectionName].Get<string>( Key ) );
        }

        [TestMethod]
        public void ExpandBackToBackWorks()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            source.Sections[SectionName].Set( Key, "${value}${value}" );
            source.Sections[SectionName].Set( Value, Key );

            Assert.AreEqual( "${value}${value}", source.Sections[SectionName].Get<string>( Key ) );
            source.ExpandKeyValues();
            Assert.AreEqual( Key + Key, source.Sections[SectionName].Get<string>( Key ) );
        }

        [TestMethod]
        public void ExpandBackToBackWithSpaceWorks()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            source.Sections[SectionName].Set( Key, "${value} ${value}" );
            source.Sections[SectionName].Set( Value, Key );

            Assert.AreEqual( "${value} ${value}", source.Sections[SectionName].Get<string>( Key ) );
            source.ExpandKeyValues();
            Assert.AreEqual( Key + " " + Key, source.Sections[SectionName].Get<string>( Key ) );
        }

        [TestMethod]
        public void ExpandFromExternalSectionWorks()
        {
            const string newSectionName = SectionName + "New";
            var section = new ConfigurationSection( newSectionName );
            section.Set( Value, Value );

            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            const string varKeyValue = "${" + newSectionName + "|value}";
            source.Sections[SectionName].Set( Key, varKeyValue );

            source.Add( section );

            Assert.AreEqual( varKeyValue, source.Sections[SectionName].Get<string>( Key ) );
            source.ExpandKeyValues();
            Assert.AreEqual( Value, source.Sections[SectionName].Get<string>( Key ) );
        }

        [TestMethod]
        public void ExpandFromDoubleExternalSectionWorks()
        {
            // old -> new -> dev -> key : value
            const string devSectionName = SectionName + "Dev";
            const string newSectionName = SectionName + "New";

            var devSection = new ConfigurationSection( devSectionName );
            var newSection = new ConfigurationSection( newSectionName );
            const string varKeyValue = "${" + newSectionName + "|key}";

            devSection.Set( Key, Value );
            newSection.Set( Key, "${" + devSectionName + "|key}" );

            ConfigurationSourceBaseImpl source = GetConfigurationSource();

            source.Sections[SectionName].Set( Key, varKeyValue );

            source.Add( newSection );
            source.Add( devSection );

            Assert.AreEqual( varKeyValue, source.Sections[SectionName].Get<string>( Key ) );
            source.ExpandKeyValues();
            Assert.AreEqual( Value, source.Sections[SectionName].Get<string>( Key ) );
        }

        [TestMethod]
        public void SaveIsCalledOnPropertyChangeWithAutoSaveEnabled()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            source.AutoSave = true;
            Assert.IsTrue( UnitExt.Throws<NotSupportedException>( () => source.Sections[SectionName].Set( Key, Key ) ) );
            Assert.AreEqual( source.Sections[SectionName].Get<string>( Key ), Key );
        }

        [TestMethod]
        public void SaveIsNotCalledOnPropertyChangeWithAutoSaveDisabled()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            source.AutoSave = false;
            source.Sections[SectionName].Set( Key, Key );
            Assert.AreEqual( source.Sections[SectionName].Get<string>( Key ), Key );
        }

        [TestMethod]
        public void ClearRemovesAllSectionsAndClearsAllSettingsInSections()
        {
            ConfigurationSourceBaseImpl source = GetConfigurationSource();
            source.Merge( new[] { GetConfigurationSource() } );
            Assert.AreEqual( 1, source.ConfigurationSources.Count );
            IConfigurationSource configSource = source.ConfigurationSources[0];
            Assert.AreEqual( 1, configSource.Sections.Count );
            Assert.AreEqual( 1, source.Sections.Count );
            IConfigurationSection configSection = source.Sections[SectionName];
            Assert.AreEqual( 1, configSection.Count );
            source.Clear();
            Assert.AreEqual( 0, source.ConfigurationSources.Count );
            Assert.AreEqual( 0, configSource.Sections.Count );
            Assert.AreEqual( 0, source.Sections.Count );
            Assert.AreEqual( 0, configSection.Count );
        }
    }
}