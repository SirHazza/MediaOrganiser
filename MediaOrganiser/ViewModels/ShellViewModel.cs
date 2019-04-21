using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganiser.ViewModels
{

    public class ShellViewModel : Conductor<object>
    {
        
        public ShellViewModel()
        {
            OpenMenuView();
        }

        public void OpenMenuView()
        {
            ActivateItem(new MenuViewModel());
        }

    }
}
