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

        // Ext list properties
        public string[] _allExt
        {
            get { return Main.allExt; }
            set { }
        }

        // On shell start up
        public ShellViewModel()
        {
            //Show menu view first
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

        //HOME
        public void buttonHome()
        {
            ActivateMenuView();
        }

        //SEARCH
        public void buttonSearchFolder(string titleSearch, string extSearch)
        {
            Search.RunSearch(1, titleSearch, extSearch);
            ActivateSearchView();
        }
        public void buttonSearchFile(string titleSearch, string extSearch)
        {
            Search.RunSearch(2, titleSearch, extSearch);
            ActivateSearchView();
        }
        public void buttonSearchPlaylist(string titleSearch, string extSearch)
        {
            Search.RunSearch(3, titleSearch, extSearch);
            ActivateSearchView();
        }

        //EDIT
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
