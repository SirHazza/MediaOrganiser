using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Caliburn.Micro;

namespace MediaOrganiser
{
    public class Search
    {
        static string[] availableSearchTypes = new string[3] {"Folder", "File", "Playlist"}; //Search type names
        public static string searchTypeName = "null";   //Title of search

        public static int searchType;               //Search Types: 1(Folder) 2(File) 3(Playlist)
        public static string searchFolder;          //Search folder path
        public static string searchPlaylist;        //Search Playlist selected
        public static string searchFile;            //Search file selected
        public static string searchTitle;           //Current title search preference
        public static string searchExt;             //Current ext search preference

        public static int searchStateType;          //Search type from state file: 1/2(Folder/Playlist)
        public static string searchStateValues;     //Search values from state file


        // Run search using prefferences
        public static void RunSearch(int _searchType, string _searchTitle, string _searchExt, string _searchPlaylist)
        {
            // Set new search type
            searchType = _searchType;

            // Set search window title
            searchTypeName = availableSearchTypes[_searchType - 1];

            // Search from preference type
            switch (_searchType)
            {
                // Folder
                case 1:
                    // Set values for search type
                    searchPlaylist = "";
                    searchTitle = _searchTitle;
                    searchExt = _searchExt;
                    string folderPath = Main.OpenFolder("Select folder to search");
                    searchStateValues = folderPath;

                    if (!string.IsNullOrWhiteSpace(folderPath))
                    {
                        // Import files from desired search folder
                        searchFolder = folderPath;
                        ImportFiles(folderPath);
                    }
                    else
                    {
                        // Show error if path invalid
                        MessageBox.Show("Error, invalid folder");
                    }
                    break;

                // File
                case 2:
                    // Open desired file
                    string filePath = Main.OpenFile();
                    if (!string.IsNullOrWhiteSpace(filePath))
                    {
                        // Run search type from file
                        searchFile = filePath;
                        ImportState(filePath);
                    }
                    else
                    {
                        // Show error if file invalid
                        MessageBox.Show("Error, invalid file");
                    }
                    break;

                // Playlist
                case 3:
                    // Set values for search type
                    searchStateValues = _searchPlaylist;
                    searchPlaylist = _searchPlaylist;
                    searchFolder = "";
                    searchTitle = _searchTitle;
                    searchExt = _searchExt;

                    // Report error if no selected playlist
                    if (string.IsNullOrWhiteSpace(searchPlaylist))
                    {
                        // Show error if path invalid
                        MessageBox.Show("Error, no playlist selected");
                    }
                    break;
            }

            // Load config to view media files in database
            Main.LoadConfig();
        }

        // Import files from system to config file
        public static void ImportFiles(string path)
        {
            // Creates array of all files contained within folder
            string[] files = Directory.GetFiles(path);

            bool exitFlag;
            Guid fileID;

            // Check import for each file in path folder
            foreach (var file in files)
            {
                exitFlag = true;

                // Check against supported extensions
                foreach (var ext in Main.allExt)
                {
                    // If ext is right and we haven't already passed
                    if (Path.GetExtension(file) == ext & exitFlag == true)
                    {
                        exitFlag = false;
                    }
                }

                // Check agaisnt duplicates
                int configLength = Main.config.GetLength(0);
                for (int i = 0; i < configLength; i++)
                {
                    // If file is duplicate
                    if (file == Main.config[i , 3])
                    {
                        exitFlag = true;
                    }
                }

                // If no import errors import
                if (exitFlag == false)
                {
                    // Write to config file with new imported file
                    using (StreamWriter writetext = new StreamWriter(Main.configFile, append: true))
                    {
                        fileID = Guid.NewGuid();

                        // New media file data entry
                        writetext.WriteLine("{0};{1};{2};{3};{4};;;;",
                            fileID,
                            Path.GetFileNameWithoutExtension(file),
                            Path.GetExtension(file),
                            file,
                            path
                            );

                        writetext.Close();
                    }
                }
            }
        }

        // Save any changes made during a search
        public static void SaveEdits(BindableCollection<FilesModel> searchDataObject)
        {
            // Open config.txt
            string[] configFile = File.ReadAllLines(Main.configFile);

            // For each search entry, set config to match
            int counter = 0;
            int _configLine;
            string newLine;
            bool cleanString = true;

            // For every object in the data object
            foreach (var entry in searchDataObject)
            {
                // Config Line from search entry
                _configLine = searchDataObject[counter].configLine;

                // Check strings don't contain ';'
                if (searchDataObject[counter].picture.Contains(';'))
                {
                    cleanString = false;
                }
                else if (searchDataObject[counter].categories.Contains(';'))
                {
                    cleanString = false;
                }
                else if (searchDataObject[counter].playlists.Contains(';'))
                {
                    cleanString = false;
                }
                else if (searchDataObject[counter].comment.Contains(';'))
                {
                    cleanString = false;
                }

                // Write new string with edits
                newLine = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}",
                Main.config[_configLine, 0],
                Main.config[_configLine, 1],
                Main.config[_configLine, 2],
                Main.config[_configLine, 3],
                Main.config[_configLine, 4],
                searchDataObject[counter].picture,
                searchDataObject[counter].categories,
                searchDataObject[counter].playlists,
                searchDataObject[counter].comment);

                // Add new line to Config file
                configFile[_configLine] = newLine;
                counter++;
            }

            switch (cleanString)
            {
                // If no errors occured
                case true:
                    // Write edits to config
                    File.WriteAllLines(Main.configFile, configFile);
                    // Reload config
                    Main.LoadConfig();
                    MessageBox.Show("Successfully saved any edits");
                    break;

                // If error
                case false:
                    MessageBox.Show("Error! Edits contained semicolon ';'");
                    break;
            }
        }
        
        // Import search state file
        public static void ImportState(string filePath)
        {
            // Read state file
            StreamReader reader = new StreamReader(filePath);

            string line;
            string[] parts;

            // Read state file line and split
            line = reader.ReadLine();
            reader.Close();
            parts = line.Split(';');

            // Checks if state file is for Folder or Playlist search
            if (string.IsNullOrWhiteSpace(parts[0]))
            {
                //playlist
                searchPlaylist = parts[1];
                searchStateType = 2;
                searchStateValues = string.Format("[Playlist({0}) Title({1}) Ext({2})]", parts[1], parts[2], parts[3]);
            }
            else
            {
                //folder
                searchFolder = parts[0];
                ImportFiles(parts[0]);
                searchStateType = 1;
                searchStateValues = string.Format("[Folder({0}) Title({1}) Ext({2})]", parts[0], parts[2], parts[3]);
            }

            // Sets title and ext search filters
            searchTitle = parts[2];
            searchExt = parts[3];
        }

    }
}
