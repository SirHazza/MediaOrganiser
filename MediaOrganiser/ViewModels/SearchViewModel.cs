using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        // Ext list properties
        public string[] _allExt
        {
            get { return Main.allExt; }
            set { }
        }


        public BindableCollection<FilesModel> FilesData { get; set; }


        public SearchViewModel()
        {
            SearchData NewSearch = new SearchData();
            FilesData = new BindableCollection<FilesModel>(NewSearch.GetFiles());
        }

        public void buttonSearch(string titleSearch, string extSearch)
        {
            Console.WriteLine(titleSearch + extSearch);
            FilesData[0].comment = "IT WORKS";
        }

        public void buttonExport()
        {

        }

        public void buttonSave()
        {
            Search.SaveEdits(FilesData);
        }

    }
}
