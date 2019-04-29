using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganiser
{
    public class ListData
    {
        // Get all list entries needed
        public List<ListModel> GetList()
        {
            // Create list data object
            List<ListModel> output = new List<ListModel>();
            // New list entry
            ListModel newEntry; 
            
            string listPath = null;
            string[] splitLists;

            // Get List file path from type
            switch (Main.listType)
            {
                //Playlists
                case 1:
                    listPath = Main.playlistsFile;
                    break;

                //Categories
                case 2:
                    listPath = Main.categoriesFile;
                    break;
            }

            // Read list file and add to array
            splitLists = Main.ReturnSplitList(listPath);

            // Removes complier warning
            newEntry = new ListModel();

            // For each list entry from file
            foreach (string entry in splitLists)
            {
                // New object for new list entry
                ListModel oneEntry = new ListModel(); 
                oneEntry.entry = entry;

                // Adds new entry to list data object
                newEntry = oneEntry;
                output.Add(newEntry);
            }

            return output;
        }
    }

    // List data model
    public class ListModel
    {
        public string entry { get; set; }
    }
}
