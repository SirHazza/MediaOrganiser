using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganiser.ViewModels
{
    public class EditListViewModel : Screen
    {
        // List type property
        public string _listTypeName
        {
            get { return Lists.availableListTypes[Main.listType - 1]; }
            set { }
        }

        public BindableCollection<ListModel> EntryData { get; set; }


        public EditListViewModel()
        {
            ListData NewList = new ListData();
            EntryData = new BindableCollection<ListModel>(NewList.GetList());
        }

        public void buttonSave()
        {
            Lists.SaveEdits(EntryData);
        }
    }
}
