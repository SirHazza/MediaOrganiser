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
        // On shell start up
        public ShellViewModel()
        {
            ActivateMenuView();
        }

        //View activations
        public void ActivateMenuView()
        {
            ActivateItem(new MenuViewModel());
        }
        public void ActivateSearchView()
        {
            ActivateItem(new SearchViewModel());
        }
        public void ActivateEditListView()
        {
            ActivateItem(new EditListViewModel());
        }

        // Navigation buttons
        public void buttonHome()
        {
            ActivateMenuView();
        }
        public void buttonSearchFolder()
        {
            ActivateSearchView();
        }
        public void buttonSearchFile()
        {
            ActivateSearchView();
        }
        public void buttonSearchPlaylist()
        {
            ActivateSearchView();
        }
        public void buttonEditPlaylists()
        {
            ActivateEditListView();
        }
        public void buttonEditCategories()
        {
            ActivateEditListView();
        }
        

    }
}
