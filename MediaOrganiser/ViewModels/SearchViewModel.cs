using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MediaOrganiser.ViewModels
{
    public class SearchViewModel : Screen
    {
        // Search type property
        public string _searchTypeName
        {
            get { return Search.searchTypeName; }
            set { }
        }

        // Search state title property
        public string _searchStateValues
        {
            get { return Search.searchStateValues; }
            set { }
        }

        // Data Object for datagrid
        public BindableCollection<FilesModel> FilesData { get; set; }


        // View method
        public SearchViewModel()
        {
            // Create a new search object
            SearchData NewSearch = new SearchData();
            // Get files and apply to datagrid object
            FilesData = new BindableCollection<FilesModel>(NewSearch.GetFiles());
        }


        // Buttons

        // Exports current search state to file (Pass export title name)
        public void buttonExport(string boxStateName)
        {
            SaveState.RunSaveState(boxStateName);
        }

        // Saves any edits (Pass datagrid object)
        public void buttonSave()
        {
            Search.SaveEdits(FilesData);
        }

        // Play selected file (Pass selected file path)
        public void buttonOpen(string filePath)
        {
            Process.Start(filePath);
        }

        // Open pictures directory
        public void buttonAddImage()
        {
            Process.Start("explorer.exe", Main.pictureFolder);
        }
    }
}
