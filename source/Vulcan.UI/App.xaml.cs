#region Using Directives

using System.Windows;
using Caliburn.Core;
using Caliburn.PresentationFramework;

#endregion

namespace Vulcan.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CaliburnFramework
                .ConfigureCore()
                .WithPresentationFramework()
                .Start();
        }
    }
}