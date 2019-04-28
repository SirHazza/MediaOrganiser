using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganiser.ViewModels
{
    public class MenuViewModel : Screen
    {
        public string textVersion
        {
            get { return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion; }
            set { }
        }

        // Exit button
        public void buttonExit()
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}
