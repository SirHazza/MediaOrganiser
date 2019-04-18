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
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MenuViewModel>();
        }
    }
}
