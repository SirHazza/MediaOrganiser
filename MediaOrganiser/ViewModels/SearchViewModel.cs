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
        // Search type properties
        public string _searchTypeName
        {
            get { return Search.searchTypeName; }
            set { }
        }

        public string _searchStateValues
        {
            get { return Search.searchStateValues; }
            set { }
        }

        public BindableCollection<FilesModel> FilesData { get; set; }


        public SearchViewModel()
        {
            SearchData NewSearch = new SearchData();
            FilesData = new BindableCollection<FilesModel>(NewSearch.GetFiles());
        }

        // Exports current search state to file
        public void buttonExport(string boxStateName)
        {
            SaveState.RunSaveState(boxStateName);
        }

        // Saves any edits
        public void buttonSave()
        {
            Search.SaveEdits(FilesData);
        }

        // Play selected file
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
