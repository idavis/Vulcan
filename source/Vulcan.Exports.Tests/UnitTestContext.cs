#region Using Directives

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Vulcan.Exports.Tests
{
    /// <summary>
    /// Summary description for UnitTestContext
    /// </summary>
    [TestClass]
    public class UnitTestContext
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        //You can use the following additional attributes as you write your tests:

        /*
        /// <summary>
        /// Use ClassInitialize to run code before running the first test in the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize]
        public static void OnClassInitialize( TestContext testContext )
        {
        }

        /// <summary>
        /// Use ClassCleanup to run code after all tests in a class have run
        /// </summary>
        [ClassCleanup]
        public static void OnClassCleanup()
        {
        }
        */

        /// <summary>
        /// Use TestInitialize to run code before running each test 
        /// </summary>
        [TestInitialize]
        public virtual void OnTestInitialize()
        {
        }

        /// <summary>
        /// Use TestCleanup to run code after each test has run
        /// </summary>
        [TestCleanup]
        public virtual void OnTestCleanup()
        {
        }

        #endregion

        /*
        [TestMethod]
        public void TestMethod()
        {
        }
        */
    }
}