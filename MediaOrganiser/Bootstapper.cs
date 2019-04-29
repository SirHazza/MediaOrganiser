using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using System.Windows;
using MediaOrganiser.ViewModels;

namespace MediaOrganiser
{
    public class Bootstapper : BootstrapperBase
    {
        public Bootstapper()
        {
            // Runs Caliburn.Micro Bootstrapper to start application
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            // Overrides default start-up method to display shellview through Caliburn.Micro
            DisplayRootViewFor<ShellViewModel>();
            // Runs application setup method
            Main.RunSetup();
        }
    }
}
