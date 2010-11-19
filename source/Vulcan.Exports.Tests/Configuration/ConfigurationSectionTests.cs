#region Using Directives

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vulcan.Exports.Configuration;
using Vulcan.Exports.Tests.Classes;
using Vulcan.Exports.Tests.Util;

#endregion

namespace Vulcan.Exports.Tests.Configuration
{
    [TestClass]
    public class ConfigurationSectionTests
    {
        private const string Key = "key";
        private const string SectionName = "Default";
        private const string Value = "value";

        [TestMethod]
        public void CanAddValue()
        {
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, Value );
        }

        [TestMethod]
        public void CanReadAddedValue()
        {
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, Value );
            var value = section.Get<string>( "key" );
            Assert.AreEqual( Value, value );
        }

        [TestMethod]
        public void CanRemoveAddedValue()
        {
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, Value );
            bool success = section.Remove( Key );
            Assert.IsTrue( success );
        }

        [TestMethod]
        public void RemovingUnAddedKeyFails()
        {
            var section = new ConfigurationSection( SectionName );
            bool success = section.Remove( Key );
            Assert.IsFalse( success );
        }

        [TestMethod]
        public void CreatingSectionWithNullNameFails()
        {
            Assert.IsTrue( UnitExt.Throws<ArgumentNullException>( () => new ConfigurationSection( null ) ) );
            Assert.IsTrue( UnitExt.Throws<ArgumentNullException>( () => new ConfigurationSection( string.Empty ) ) );
        }

        [TestMethod]
        public void NamePassedDuringCreationIsSet()
        {
            var section = new ConfigurationSection( SectionName );
            Assert.AreEqual( SectionName, section.Name );
        }

        [TestMethod]
        public void EnumeratorGivesKeysAndValues()
        {
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, Value );
            int count = 0;
            foreach ( KeyValuePair<string, string> pair in section )
            {
                Assert.AreEqual( Key, pair.Key );
                Assert.AreEqual( Value, pair.Value );
                count++;
            }
            Assert.AreEqual( 1, count );
        }

        [TestMethod]
        public void SettingAValueForAnExistingKeyOverrides()
        {
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, Value );
            Assert.AreEqual( Value, section.Get<string>( Key ) );
            section.Set( Key, Key );
            Assert.AreEqual( Key, section.Get<string>( Key ) );
        }

        [TestMethod]
        public void CanGetAndSetEnumValues()
        {
            const OSEnum value = OSEnum.Win2k;
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, value );
            var fromSection = section.Get<OSEnum>( Key );
            Assert.AreEqual( value, fromSection );
        }

        [TestMethod]
        public void CanGetAndSetEnumStringValues()
        {
            const OSEnum value = OSEnum.Win2k;
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, value.ToString() );
            var fromSection = section.Get<OSEnum>( Key );
            Assert.AreEqual( value, fromSection );
        }

        [TestMethod]
        public void CanGetAndSetEnumInt32Values()
        {
            const OSEnum value = OSEnum.Win2k;
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, (int) value );
            var fromSection = section.Get<OSEnum>( Key );
            Assert.AreEqual( value, fromSection );
        }

        [TestMethod]
        public void CanGetAndSetEnumFlagValues()
        {
            const OptionsEnum all = ( OptionsEnum.A | OptionsEnum.B | OptionsEnum.C );
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, all );
            var fromSection = section.Get<OptionsEnum>( Key );
            Assert.AreEqual( all, fromSection );
        }

        [TestMethod]
        public void CanGetAndSetEnumFlagStringValues()
        {
            const OptionsEnum all = ( OptionsEnum.A | OptionsEnum.B | OptionsEnum.C );
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, all.ToString() );
            var fromSection = section.Get<OptionsEnum>( Key );
            Assert.AreEqual( all, fromSection );
        }

        [TestMethod]
        public void CanGetAndSetEnumFlagInt32Values()
        {
            const OptionsEnum all = ( OptionsEnum.A | OptionsEnum.B | OptionsEnum.C );
            var section = new ConfigurationSection( SectionName );
            section.Set( Key, (int) all );
            var fromSection = section.Get<OptionsEnum>( Key );
            Assert.AreEqual( all, fromSection );
        }

        [TestMethod]
        public void GettingNonExistingItemReturnsDefaultForType()
        {
            var section = new ConfigurationSection( SectionName );
            var optionsEnum = section.Get<OptionsEnum>( Key );
            Assert.AreEqual( default( OptionsEnum ), optionsEnum );

            var osEnum = section.Get<OSEnum>( Key );
            Assert.AreEqual( default( OSEnum ), osEnum );

            var stringValue = section.Get<string>( Key );
            Assert.AreEqual( default( string ), stringValue );

            var boolValue = section.Get<bool>( Key );
            Assert.AreEqual( default( bool ), boolValue );

            var dummySection = section.Get<ConfigurationSection>( Key );
            Assert.AreEqual( default( ConfigurationSection ), dummySection );
        }

        [TestMethod]
        public void DefaultParameterValueIsReturnedForNonExistingKey()
        {
            var section = new ConfigurationSection( SectionName );

            const OptionsEnum optionsEnumDefault = OptionsEnum.None;
            OptionsEnum optionsEnum = section.Get( Key, optionsEnumDefault );
            Assert.AreEqual( optionsEnumDefault, optionsEnum );

            const OSEnum osEnumDefault = OSEnum.WinXp;
            OSEnum osEnum = section.Get( Key, osEnumDefault );
            Assert.AreEqual( osEnumDefault, osEnum );

            const string stringDefault = Value;
            string stringValue = section.Get( Key, stringDefault );
            Assert.AreEqual( stringDefault, stringValue );

            bool boolValue = section.Get( Key, true );
            Assert.AreEqual( true, boolValue );

            boolValue = section.Get( Key, false );
            Assert.AreEqual( false, boolValue );

            var defaultSection = new ConfigurationSection( "MyDefault" );
            ConfigurationSection dummySection = section.Get( Key, defaultSection );
            Assert.AreEqual( defaultSection, dummySection );
        }

        [TestMethod]
        public void SectionsCanBeComparedForEquality()
        {
            IConfigurationSection section1 = SectionGenerator.GetSingleSection();
            IConfigurationSection section2 = SectionGenerator.GetSingleSection();
            Assert.AreEqual( section1, section2 );
        }
    }
}