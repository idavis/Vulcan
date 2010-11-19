#region Using Directives

using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Policy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Vulcan.Exports.Tests.Properties;

#endregion

namespace Vulcan.Exports.Tests.Configuration
{
    public class MediumTrustContext
    {
        protected const string LocalMachineRoot = @"HKEY_LOCAL_MACHINE";

        protected const string MediumTrustConfigFile =
                @"C:\Windows\Microsoft.NET\Framework\v2.0.50727\CONFIG\web_mediumtrust.config.default";

        protected static readonly string TestKey = @"SOFTWARE\Innovatian.Configuration.Tests";

        protected static string TestKeyName
        {
            get
            {
                var stackTrace = new StackTrace( 1 );
                string currentMethod = stackTrace.GetFrame( 0 ).GetMethod().Name;
                string key = string.Format( "{0}\\{1}", TestKey, currentMethod );
                return key;
            }
        }

        protected static string FullTestKeyName( string testKeyName )
        {
            string key = string.Format( "{0}\\{1}", LocalMachineRoot, testKeyName );
            return key;
        }

        public PolicyLevel CreateMediumTrustPolicy()
        {
            PolicyLevel policyLevel = PolicyLevel.CreateAppDomainLevel();
            string contents;
            using ( StreamReader file = File.OpenText( MediumTrustConfigFile ) )
            {
                contents = file.ReadToEnd();
            }
            SecurityElement securityElement = SecurityElement.FromString( Resources.MediumTrustConfig );
            policyLevel.FromXml( securityElement );
            return policyLevel;
        }

        public AppDomain CreateAppDomain( PolicyLevel policyLevel )
        {
            AppDomain domain = AppDomain.CreateDomain( "medium", AppDomain.CurrentDomain.Evidence );

            domain.SetAppDomainPolicy( policyLevel );
            return domain;
        }
    }

    // TODO: Figure out how to test this.
    [TestClass]
    internal class RegistryTestContext : MediumTrustContext
    {
        private const string KeyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Management";
        private const string LocalMachineRoot = @"HKEY_LOCAL_MACHINE";

        [TestMethod]
        public void CanReadRegistryGivenKeyName()
        {
            AppDomain domain = CreateAppDomain( CreateMediumTrustPolicy() );
            domain.DoCallBack(
                    () =>
                    OpenKey( string.Format( "{0}\\{1}", LocalMachineRoot, KeyName ),
                             RegistryKeyPermissionCheck.ReadWriteSubTree ) );
        }

        internal static RegistryKey OpenKey( string key, RegistryKeyPermissionCheck permissionCheck )
        {
            RegistryKey root = OpenRoot( key );

            string path = key.Replace( root.Name, string.Empty ).Trim( '\\' );
            if ( string.IsNullOrEmpty( path ) )
            {
                return root;
            }

            using ( root )
            {
                return root.CreateSubKey( path, permissionCheck );
            }
        }

        internal static RegistryKey OpenRoot( string key )
        {
            string[] pathParts = key.Split( new[] { @"\" }, StringSplitOptions.None );
            RegistryKey currentKey = null;

            switch ( pathParts[0].ToUpper() )
            {
                case "HKEY_CLASSES_ROOT":
                    currentKey = Registry.ClassesRoot;
                    break;
                case "HKEY_CURRENT_CONFIG":
                    currentKey = Registry.CurrentConfig;
                    break;
                case "HKEY_CURRENT_USER":
                    currentKey = Registry.CurrentUser;
                    break;
                case "HKEY_DYN_DATA":
                    currentKey = Registry.DynData;
                    break;
                case "HKEY_LOCAL_MACHINE":
                    currentKey = Registry.LocalMachine;
                    break;
                case "HKEY_PERFORMANCE_DATA":
                    currentKey = Registry.PerformanceData;
                    break;
                case "HKEY_USERS":
                    currentKey = Registry.Users;
                    break;
            }
            return currentKey;
        }
    }
}