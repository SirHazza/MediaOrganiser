using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace MediaOrganiser
{

    public class Lists
    {
        public static string[] availableListTypes = new string[2] { "Playlist", "Categories" };

        public static void SaveEdits(BindableCollection<ListModel> entryData)
        {
            //string[] listFile;

            //// Open list file
            //switch (Main.listType)
            //{
            //    //Playlists
            //    case 1:
            //        listFile = File.ReadAllLines(Main.playlistsFile);
            //        break;

            //    //Categories
            //    case 2:
            //        listFile = File.ReadAllLines(Main.categoriesFile);
            //        break;
            //}

            // For each list entry, set config to match
            int counter = 0;
            string[] newLine = new string[1];

            foreach (var entry in entryData)
            {
                newLine[0] = string.Format(newLine[0] + string.Format("{0},", entry.entry));
                counter++;
            }

            newLine[0] = newLine[0].Remove(newLine[0].Length - 1);

            switch (Main.listType)
            {
                //Playlists
                case 1:
                    File.WriteAllLines(Main.playlistsFile, newLine);
                    break;

                //Categories
                case 2:
                    File.WriteAllLines(Main.categoriesFile, newLine);
                    break;
            }

            MessageBox.Show("Successfully saved any edits");

        }
    }
}
