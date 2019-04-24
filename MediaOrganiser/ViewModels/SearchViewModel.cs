using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MediaOrganiser.ViewModels
{
    class SearchViewModel : Screen
    {
        // Search type property
        public string _searchTypeName
        {
            get { return Search.searchTypeName; }
            set { }
        }

        //public List<string> picture
        //{
        //    get { return tablePicture(); }
        //    set { }
        //}

        //public List<string> title
        //{
        //    get { return tableTitle(); }
        //    set { }
        //}
        //public List<string> ext
        //{
        //    get { return tableExt(); }
        //    set { }
        //}

        public List<string> picture
        {
            get { return Main.ReturnConfigSetList(5); }
            set { }
        }

        public List<string> title
        {
            get { return Main.ReturnConfigSetList(1); }
            set { }
        }
        public List<string> ext
        {
            get { return Main.ReturnConfigSetList(2); }
            set { }
        }




        public SearchViewModel()
        {

        }

        //public List<string> tablePicture()
        //{
        //    string[] _picture = Main.ReturnConfigSet(5);
        //    List<string> picture = _picture.OfType<string>().ToList();
        //    return picture;
        //}

        //public List<string> tableTitle()
        //{
        //    string[] _title = Main.ReturnConfigSet(1);
        //    List<string> title = _title.OfType<string>().ToList();
        //    return title;
        //}

        //public List<string> tableExt()
        //{
        //    string[] _ext = Main.ReturnConfigSet(2);
        //    List<string> ext = _ext.OfType<string>().ToList();
        //    return ext;
        //}

    }
}
