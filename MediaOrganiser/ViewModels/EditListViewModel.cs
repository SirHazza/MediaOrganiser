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

        /// Data Object for datagrid
        public BindableCollection<ListModel> EntryData { get; set; }


        // View method
        public EditListViewModel()
        {
            // Create a new list object
            ListData NewList = new ListData();
            // Get lists and apply to datagrid object
            EntryData = new BindableCollection<ListModel>(NewList.GetList());
        }

        // Save edits button (Pass datagrid object)
        public void buttonSave()
        {
            Lists.SaveEdits(EntryData);
        }
    }
}
