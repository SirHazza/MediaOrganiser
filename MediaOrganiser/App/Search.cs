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
    class Search
    {
        static string[] availableSearchTypes = new string[3] {"Folder", "File", "Playlist"};
        public static string searchTypeName = "null";

        public static int searchType;       //Search Types: 1(Folder) 2(File) 3(Playlist)
        public static string searchFolder;  //Search folder path
        public static string searchPlaylist;
        public static string searchFile;
        public static string searchTitle;   //Current title search preference
        public static string searchExt;     //Current ext search preference

        public static int searchStateType;          //Search type from state file: 1/2(Folder/Playlist)
        public static string searchStateValues;     //Search values from state file

        public static void RunSearch(int _searchType, string _searchTitle, string _searchExt, string _searchPlaylist)
        {
            searchType = _searchType;

            // Set search window title
            searchTypeName = availableSearchTypes[_searchType - 1];

            // Search from preference
            switch (_searchType)
            {
                // Folder
                case 1:

                    searchPlaylist = "";
                    searchTitle = _searchTitle;
                    searchExt = _searchExt;
                    string folderPath = Main.OpenFolder("Select folder to search");
                    searchStateValues = folderPath;

                    if (!string.IsNullOrWhiteSpace(folderPath))
                    {
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
                    string filePath = Main.OpenFile();
                    if (!string.IsNullOrWhiteSpace(filePath))
                    {
                        searchFile = filePath;
                        ImportState(filePath);
                    }
                    else
                    {
                        // Show error if path invalid
                        MessageBox.Show("Error, invalid file");
                    }

                    break;

                // Playlist
                case 3:
                    searchStateValues = _searchPlaylist;
                    searchPlaylist = _searchPlaylist;
                    searchFolder = "";
                    searchTitle = _searchTitle;
                    searchExt = _searchExt;

                    if (string.IsNullOrWhiteSpace(searchPlaylist))
                    {
                        // Show error if path invalid
                        MessageBox.Show("Error, no playlist selected");
                    }

                    break;
            }

            Main.LoadConfig();
            
        }


        // Import files from system to config file
        public static void ImportFiles(string path)
        {
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
                    using (StreamWriter writetext = new StreamWriter(Main.configFile, append: true))
                    {
                        fileID = Guid.NewGuid();

                        writetext.WriteLine("{0};{1};{2};{3};{4};;;;",
                            fileID,
                            Path.GetFileNameWithoutExtension(file),
                            Path.GetExtension(file),
                            file,
                            path
                            );


                        //Path.GetExtension(null); // returns .exe
                        //Path.GetFileNameWithoutExtension(null); // returns File
                        //Path.GetFileName(null); // returns File.exe

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
            foreach (var entry in searchDataObject)
            {
                // Config Line from search entry
                _configLine = searchDataObject[counter].configLine;

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

                // Config file []
                configFile[_configLine] = newLine;

                counter++;
            }

            File.WriteAllLines(Main.configFile, configFile);
            Main.LoadConfig();
            MessageBox.Show("Successfully saved any edits");

            //RunSearch();

            //when first searching you will need to store the type of search and where/what it is searching in a public var (see above)
            //you will also need to change RunSearch so that it can either take new input, or use the last search values
            //in this case we want to use the last search values are we are only reloading the same search

        }
        

        // Import State file
        public static void ImportState(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);

            string line;
            string[] parts;

            line = reader.ReadLine();
            reader.Close();
            parts = line.Split(';');

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

            searchTitle = parts[2];
            searchExt = parts[3];
        }




    }

    
}
