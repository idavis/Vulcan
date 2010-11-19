#region Using Directives

using System;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vulcan.Exports.Configuration;
using Vulcan.Exports.Tests.Classes;
using Vulcan.Exports.Tests.Util;

#endregion

namespace Vulcan.Exports.Tests.Configuration
{
    // These tests are all integration tests.
    [TestClass]
    public class IsoStorageConfigurationSourceTests
    {
        private const IsolatedStorageScope Scope = IsolatedStorageScope.User | IsolatedStorageScope.Assembly;
        private readonly string _fileName = typeof (IsoStorageConfigurationSourceTests).Name + ".xml";

        [TestMethod]
        public void EmptyFileNameThrows()
        {
            Assert.IsTrue( UnitExt.Throws<ArgumentNullException>(
                    () => new IsoStorageConfigurationSource( Scope, string.Empty ) ) );
        }

        [TestMethod]
        public void NullFileNameThrows()
        {
            Assert.IsTrue( UnitExt.Throws<ArgumentNullException>(
                    () => new IsoStorageConfigurationSource( Scope, null ) ) );
        }

        /* 
         * System.IO.IsolatedStorage.IsolatedStorageException : Unable to determine application identity of the caller.
         * Need to find a way to test application scope IsoStorage in unit testing.
         * 
        [TestMethod]
        public void CanLoadAndSaveWithDefaultScope()
        {
            var source = new IsoStorageConfigurationSource( _fileName );
            RunCreationTest( source );
        }
        
        [TestMethod]
        public void CanLoadAndSaveInApplicationMachineScope()
        {
            var source = new IsoStorageConfigurationSource( IsolatedStorageScope.Application | IsolatedStorageScope.Machine, _fileName );
            RunCreationTest( source );
        }
        
        [TestMethod]
        public void CanLoadAndSaveInApplicationUserScope()
        {
            var source = new IsoStorageConfigurationSource(IsolatedStorageScope.Application | IsolatedStorageScope.User, _fileName);
            RunCreationTest(source);
        }
        */

        [TestMethod]
        public void CanLoadAndSaveInMachineAssemblyScope()
        {
            const IsolatedStorageScope scope = IsolatedStorageScope.Machine | IsolatedStorageScope.Assembly;
            var source = new IsoStorageConfigurationSource( scope, _fileName );
            RunCreationTest( source );
        }

        [TestMethod]
        public void CanLoadAndSaveInAssemblyUserScope()
        {
            const IsolatedStorageScope scope = IsolatedStorageScope.User | IsolatedStorageScope.Assembly;
            var source = new IsoStorageConfigurationSource( scope, _fileName );
            RunCreationTest( source );
        }

        [TestMethod]
        public void CanLoadAndSaveInMachineStorForDomainScope()
        {
            const IsolatedStorageScope scope =
                    IsolatedStorageScope.Machine | IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain;
            var source =
                    new IsoStorageConfigurationSource( scope, _fileName );
            RunCreationTest( source );
        }

        [TestMethod]
        public void CanLoadAndSaveInUserStorForDomainScope()
        {
            const IsolatedStorageScope scope =
                    IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain;
            var source =
                    new IsoStorageConfigurationSource( scope, _fileName );
            RunCreationTest( source );
        }

        [TestMethod]
        public void CreationWithNoneScopeThrows()
        {
            Assert.IsTrue( UnitExt.Throws<ArgumentException>(
                    () => new IsoStorageConfigurationSource( IsolatedStorageScope.None, _fileName ) ) );
        }

        private void RunCreationTest( IsoStorageConfigurationSource source )
        {
            string sourceFile = string.Empty;
            try
            {
                source.Add( SectionGenerator.GetSingleSection() );
                source.Save();
                sourceFile = source.FullPath;
                // we should now have a file on the hdd with the settings we want.
                string sourceAsXml = XmlConfigurationSource.ToXml( source );

                // Now create a new instance so it can load the data.
                var newSource = new IsoStorageConfigurationSource( source.Scope, _fileName );
                string newSourceAsXml = XmlConfigurationSource.ToXml( newSource );
                Assert.AreEqual( sourceAsXml, newSourceAsXml );
            }
            finally
            {
                if ( File.Exists( sourceFile ) )
                {
                    File.Delete( sourceFile );
                }
            }
        }
    }
}