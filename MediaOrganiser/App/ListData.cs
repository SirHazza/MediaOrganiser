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
        public List<ListModel> GetList()
        {
            List<ListModel> output = new List<ListModel>();
            ListModel newEntry; // New list entry
            
            string listPath = null;
            string listLine;
            string[] splitLists;

            // Get List file path
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
            StreamReader reader = new StreamReader(listPath);
            listLine = reader.ReadLine();
            splitLists = listLine.Split(',');
            reader.Close();

            // Removes complier warning
            newEntry = new ListModel();

            foreach (string entry in splitLists)
            {
                ListModel oneEntry = new ListModel(); // New object for new list entry
                oneEntry.entry = entry;

                newEntry = oneEntry;
                output.Add(newEntry);
            }

            return output;
        }
    }

    public class ListModel
    {
        public string entry { get; set; }
    }

}
