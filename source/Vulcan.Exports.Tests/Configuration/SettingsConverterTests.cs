#region Using Directives

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vulcan.Exports.Configuration;
using Vulcan.Exports.Tests.Classes;

#endregion

namespace Vulcan.Exports.Tests.Configuration
{
    [TestClass]
    public class SettingsConverterTests
    {
        [TestMethod]
        public void CanGetEnumValueByName()
        {
            const string none = "None";
            var value = SettingConverter.GetTFromString<OSEnum>( none );
            Assert.AreEqual( OSEnum.None, value );
        }

        [TestMethod]
        public void CanGetEnumValueByValue()
        {
            const string none = "0";
            var value = SettingConverter.GetTFromString<OSEnum>( none );
            Assert.AreEqual( OSEnum.None, value );
        }

        [TestMethod]
        public void CanGetEnumValueByFlagValue()
        {
            const OptionsEnum all = ( OptionsEnum.A | OptionsEnum.B | OptionsEnum.C );
            string allString = ( (int) all ).ToString();
            var value = SettingConverter.GetTFromString<OptionsEnum>( allString );
            Assert.AreEqual( all, value );
        }

        [TestMethod]
        public void CanGetEnumValueByFlagString()
        {
            const OptionsEnum all = ( OptionsEnum.A | OptionsEnum.B | OptionsEnum.C );
            string allString = all.ToString();
            var value = SettingConverter.GetTFromString<OptionsEnum>( allString );
            Assert.AreEqual( all, value );
        }

        [TestMethod]
        public void CanGetBoolFromTrueString()
        {
            string boolString = bool.TrueString;
            var boolValue = SettingConverter.GetTFromString<bool>( boolString );
            Assert.IsTrue( boolValue );
        }

        [TestMethod]
        public void CanGetBoolFromFalseString()
        {
            string boolString = bool.FalseString;
            var boolValue = SettingConverter.GetTFromString<bool>( boolString );
            Assert.IsFalse( boolValue );
        }

        [TestMethod]
        public void CanGetBoolFromTrueStringLowerCase()
        {
            string boolString = bool.TrueString.ToLower();
            var boolValue = SettingConverter.GetTFromString<bool>( boolString );
            Assert.IsTrue( boolValue );
        }

        [TestMethod]
        public void CanGetBoolFromFalseStringLowerCase()
        {
            string boolString = bool.FalseString.ToLower();
            var boolValue = SettingConverter.GetTFromString<bool>( boolString );
            Assert.IsFalse( boolValue );
        }

        [TestMethod]
        public void CanGetBoolFromIntToTrue()
        {
            string boolString = 1.ToString();
            var boolValue = SettingConverter.GetTFromString<bool>( boolString );
            Assert.IsTrue( boolValue );
        }

        [TestMethod]
        public void CanGetBoolFromIntToFalse()
        {
            string boolString = 0.ToString();
            var boolValue = SettingConverter.GetTFromString<bool>( boolString );
            Assert.IsFalse( boolValue );
        }

        [TestMethod]
        public void CanGetInt()
        {
            string now = 5.ToString();
            var value = SettingConverter.GetTFromString<int>( now );
            Assert.AreEqual( now, value.ToString() );
        }

        [TestMethod]
        public void CanGetStringInt()
        {
            string now = 5.ToString();
            string value = SettingConverter.GetStringFromT( 5 );
            Assert.AreEqual( now, value );
        }

        [TestMethod]
        public void CanGetDateTime()
        {
            string now = DateTime.Now.ToString();
            var value = SettingConverter.GetTFromString<DateTime>( now );
            Assert.AreEqual( now, value.ToString() );
        }

        [TestMethod]
        public void CanGetUri()
        {
            const string url = "http://mydomain.com/";
            var value = SettingConverter.GetTFromString<Uri>( url );
            Assert.AreEqual( url, value.ToString() );
        }
    }
}