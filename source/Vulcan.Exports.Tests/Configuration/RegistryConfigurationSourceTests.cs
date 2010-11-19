#region Using Directives

using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Vulcan.Exports.Configuration;

#endregion

namespace Vulcan.Exports.Tests.Configuration
{
    /// <summary>
    /// The root test key must be created prior to running these tests
    /// or an authorization exception will be thrown.
    /// </summary>
    [TestClass]
    [Ignore]
    public class RegistryConfigurationSourceTests
    {
        private const int dWordValue = 42;
        private const string expandedStringValue = "The path is %PATH%";
        private const string KeyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Management";
        private const string LocalMachineRoot = @"HKEY_LOCAL_MACHINE";
        private const long quadWordValue = 42;
        private const string stringValue = "The path is %PATH%";
        private static readonly string TestKey = @"SOFTWARE\Vulcan.Exports.Tests.Configuration";
        private readonly byte[] binaryValue = new byte[] { 10, 43, 44, 45, 14, 255 };
        private readonly string[] multipleStringValue = new[] { "One", "Two", "Three" };

        private static string SectionName
        {
            get
            {
                var stackTrace = new StackTrace( 1 );
                string currentMethod = stackTrace.GetFrame( 0 ).GetMethod().Name;
                return currentMethod;
            }
        }

        private static string TestKeyName
        {
            get
            {
                var stackTrace = new StackTrace( 1 );
                string currentMethod = stackTrace.GetFrame( 0 ).GetMethod().Name;
                string key = string.Format( "{0}\\{1}", TestKey, currentMethod );
                return key;
            }
        }

        [TestMethod]
        public void CanReadRegistryGivenKey()
        {
            using ( RegistryKey key =
                    Registry.LocalMachine.OpenSubKey( KeyName ) )
            {
                using ( var source = new RegistryConfigurationSource( key ) )
                {
                }
            }
        }

        [TestMethod]
        public void CanReadRegistryGivenKeyName()
        {
            using (
                    var source = new RegistryConfigurationSource( string.Format( "{0}\\{1}", LocalMachineRoot, KeyName ) )
                    )
            {
            }
        }

        [TestMethod]
        public void CanAddNewKeys()
        {
            try
            {
                using ( var source = new RegistryConfigurationSource( FullTestKeyName( TestKeyName ) ) )
                {
                    source.Sections[SectionName].Set( "key", "value" );
                    source.Add( new ConfigurationSection( "NewSettings" ) );
                    source.Sections["NewSettings"].Set( "count", 5 );
                    source.Save();
                }

                string root = TestKeyName;
                Assert.IsTrue( KeyExists( root ) );
                Assert.IsFalse( KeyExists( string.Format( "{0}\\{1}", root, SectionName ) ) );
                Assert.IsTrue( KeyExists( root + "\\NewSettings" ) );
            }
            finally
            {
                DeleteKey( TestKeyName );
            }
        }

        [TestMethod]
        public void CanLoadKeys()
        {
            try
            {
                using ( var source = new RegistryConfigurationSource( FullTestKeyName( TestKeyName ) ) )
                {
                    source.Sections[SectionName].Set( "key", "value" );
                    source.Add( new ConfigurationSection( "NewSettings" ) );
                    source.Sections["NewSettings"].Set( "count", 5 );
                    source.Save();
                }
                using ( var source = new RegistryConfigurationSource( FullTestKeyName( TestKeyName ) ) )
                {
                    Assert.AreEqual( 2, source.Sections.Count );

                    Assert.AreEqual( "NewSettings", source.Sections.ToList()[0].Value.Name );
                    Assert.AreEqual( 5, source.Sections["NewSettings"].Get<int>( "count" ) );

                    Assert.AreEqual( SectionName, source.Sections.ToList()[1].Value.Name );
                    Assert.AreEqual( "value", source.Sections[SectionName].Get<string>( "key" ) );
                }
            }
            finally
            {
                DeleteKey( TestKeyName );
            }
        }

        [TestMethod]
        public void CanLoadMultiLevelKeys()
        {
            try
            {
                using ( var source = new RegistryConfigurationSource( FullTestKeyName( TestKeyName ) ) )
                {
                    source.Sections[SectionName].Set( "key", "value" );
                    source.Add( new ConfigurationSection( "NewSettings" ) );
                    source.Sections["NewSettings"].Set( "count", 5 );
                    source.Add( new ConfigurationSection( "NewSettings\\Legacy" ) );
                    source.Sections["NewSettings\\Legacy"].Set( "count", 15 );
                    source.Save();
                }
                using ( var source = new RegistryConfigurationSource( FullTestKeyName( TestKeyName ) ) )
                {
                    Assert.AreEqual( 3, source.Sections.Count );

                    Assert.AreEqual( "NewSettings\\Legacy", source.Sections.ToList()[0].Value.Name );
                    Assert.AreEqual( 15, source.Sections["NewSettings\\Legacy"].Get<int>( "count" ) );

                    Assert.AreEqual( "NewSettings", source.Sections.ToList()[1].Value.Name );
                    Assert.AreEqual( 5, source.Sections["NewSettings"].Get<int>( "count" ) );

                    Assert.AreEqual( SectionName, source.Sections.ToList()[2].Value.Name );
                    Assert.AreEqual( "value", source.Sections[SectionName].Get<string>( "key" ) );
                }
            }
            finally
            {
                DeleteKey( TestKeyName );
            }
        }

        [TestMethod]
        public void CanAddMultiLevelKeys()
        {
            try
            {
                using ( var source = new RegistryConfigurationSource( FullTestKeyName( TestKeyName ) ) )
                {
                    source.Sections[SectionName].Set( "key", "value" );
                    source.Add( new ConfigurationSection( "NewSettings" ) );
                    source.Sections["NewSettings"].Set( "count", 5 );
                    source.Add( new ConfigurationSection( "NewSettings\\Legacy" ) );
                    source.Sections["NewSettings\\Legacy"].Set( "count", 5 );
                    source.Save();
                }

                string root = TestKeyName;
                Assert.IsTrue( KeyExists( root ) );
                Assert.IsFalse( KeyExists( string.Format( "{0}\\{1}", root, SectionName ) ) );
                Assert.IsTrue( KeyExists( root + "\\NewSettings" ) );
                Assert.IsTrue( KeyExists( root + "\\NewSettings\\Legacy" ) );
            }
            finally
            {
                DeleteKey( TestKeyName );
            }
        }

        [TestMethod]
        public void CanReadRegistryValueKinds()
        {
            try
            {
                CreateRegistryValueKindSamples( TestKey, SectionName );

                using ( var source = new RegistryConfigurationSource( FullTestKeyName( TestKeyName ) ) )
                {
                    IConfigurationSection section = source.Sections[SectionName];

                    var quadWord = section.Get<long>( "QuadWordValue" );
                    Assert.AreEqual( quadWordValue, quadWord );

                    var dWord = section.Get<int>( "DWordValue" );
                    Assert.AreEqual( quadWordValue, dWord );

                    var strings = section.Get<string[]>( "MultipleStringValue" );
                    Assert.AreEqual( multipleStringValue, strings );

                    var newStringValue = section.Get<string>( "StringValue" );
                    Assert.AreEqual( stringValue, newStringValue );

                    var newExpandedStringValue = section.Get<string>( "ExpandedStringValue" );
                    Assert.AreNotEqual( expandedStringValue, newExpandedStringValue );
                    string realExpandedValue = expandedStringValue.Replace( "%PATH%",
                                                                            Environment.GetEnvironmentVariable( "PATH" ) );
                    Assert.AreEqual( realExpandedValue, newExpandedStringValue );

                    var data = section.Get<byte[]>( "BinaryValue" );
                    Assert.AreEqual( binaryValue, data );
                }
            }
            finally
            {
                DeleteKey( TestKeyName );
            }
        }

        [TestMethod]
        public void CanWriteRegistryValueKinds()
        {
            try
            {
                CreateRegistryValueKindSamples( TestKey, SectionName );

                using ( var source = new RegistryConfigurationSource( FullTestKeyName( TestKeyName ) ) )
                {
                    IConfigurationSection section = source.Sections[SectionName];
                    section.Set( "QuadWordValue", 13 );
                    section.Set( "DWordValue", 13 );
                    section.Set( "StringValue", 13.ToString() );
                    section.Set( "ExpandedStringValue", "13 %PATH%" );
                    section.Set( "MultipleStringValue", new[] { 13.ToString(), 13.ToString() } );
                    section.Set( "BinaryValue", new byte[] { 13, 13 } );
                    source.Save();
                }
                using ( var source = new RegistryConfigurationSource( FullTestKeyName( TestKeyName ) ) )
                {
                    IConfigurationSection section = source.Sections[SectionName];
                    var quadWord = section.Get<long>( "QuadWordValue" );
                    Assert.AreEqual( 13, quadWord );

                    var dWord = section.Get<int>( "DWordValue" );
                    Assert.AreEqual( 13, dWord );

                    var strings = section.Get<string[]>( "MultipleStringValue" );
                    Assert.AreEqual( new[] { 13.ToString(), 13.ToString() }, strings );

                    var newStringValue = section.Get<string>( "StringValue" );
                    Assert.AreEqual( 13.ToString(), newStringValue );

                    var newExpandedStringValue = section.Get<string>( "ExpandedStringValue" );
                    Assert.AreNotEqual( expandedStringValue, newExpandedStringValue );
                    string realExpandedValue = "13 %PATH%".Replace( "%PATH%",
                                                                    Environment.GetEnvironmentVariable( "PATH" ) );
                    Assert.AreEqual( realExpandedValue, newExpandedStringValue );

                    var data = section.Get<byte[]>( "BinaryValue" );
                    Assert.AreEqual( new byte[] { 13, 13 }, data );
                }
            }
            finally
            {
                DeleteKey( TestKeyName );
            }
        }

        private void CreateRegistryValueKindSamples( string testKey, string sectionName )
        {
            using ( RegistryKey key = Registry.LocalMachine.OpenSubKey( testKey, true ) )
            {
                using (
                        RegistryKey sectionKey = key.CreateSubKey( sectionName,
                                                                   RegistryKeyPermissionCheck.ReadWriteSubTree ) )
                {
                    // This overload supports QWord (long) values. 
                    sectionKey.SetValue( "QuadWordValue", quadWordValue, RegistryValueKind.QWord );

                    // The following SetValue calls have the same effect as using the
                    // SetValue overload that does not specify RegistryValueKind.
                    //
                    sectionKey.SetValue( "DWordValue", dWordValue, RegistryValueKind.DWord );
                    sectionKey.SetValue( "MultipleStringValue", multipleStringValue, RegistryValueKind.MultiString );
                    sectionKey.SetValue( "BinaryValue", binaryValue, RegistryValueKind.Binary );
                    sectionKey.SetValue( "StringValue", stringValue, RegistryValueKind.String );

                    // This overload supports setting expandable string values. Compare
                    // the output from this value with the previous string value.
                    sectionKey.SetValue( "ExpandedStringValue", expandedStringValue, RegistryValueKind.ExpandString );
                }
            }
        }

        private static string FullTestKeyName( string testKeyName )
        {
            string key = string.Format( "{0}\\{1}", LocalMachineRoot, testKeyName );
            return key;
        }

        private static void DeleteKey( string keyName )
        {
            if ( !KeyExists( keyName ) )
            {
                return;
            }

            using ( RegistryKey key = RegistryConfigurationSource.OpenRoot( LocalMachineRoot ) )
            {
                key.DeleteSubKeyTree( keyName );
                Assert.IsNull( key.OpenSubKey( keyName ) );
            }
        }

        private static bool KeyExists( string keyName )
        {
            using ( RegistryKey key = RegistryConfigurationSource.OpenRoot( LocalMachineRoot ) )
            {
                using ( RegistryKey target = key.OpenSubKey( keyName ) )
                {
                    return target != null;
                }
            }
        }
    }
}