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
            bool cleanString = true;
            foreach (var entry in entryData)
            {
                // Check string doesn't contain ';'
                if (entry.entry.Contains(';'))
                {
                    cleanString = false;
                }

                // Merge strings
                newLine[0] = string.Format(newLine[0] + string.Format("{0},", entry.entry));
                counter++;
            }

            // Remove last added comma
            newLine[0] = newLine[0].Remove(newLine[0].Length - 1);

            switch (cleanString)
            {
                case true:

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

                    MessageBox.Show("Successfully saved any edits, re-start to filter any new lists");
                    break;

                case false:
                    MessageBox.Show("Error! Edits contained semicolon ';'");
                    break;
            }
        }
    }
}
