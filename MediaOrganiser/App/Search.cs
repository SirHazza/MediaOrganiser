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

        //searchtype
        //searchfolder
        //searchplaylist
        //searchfile

        public static void RunSearch(int searchType)
        {
            // Search Types: 1 (Folder), 2 (File), 3 (Playlist)

            // Set search window title
            searchTypeName = availableSearchTypes[searchType - 1];

            // Folder
            if (searchType == 1)
            {
                string selectedPath = OpenFolder();

                if (!string.IsNullOrWhiteSpace(selectedPath))
                {
                    ImportFiles(selectedPath);
                }
                else
                {
                    // Show error if path invalid
                    MessageBox.Show("Error, invalid folder");
                }
                
            }


            Main.LoadConfig();
            
        }


        public static string OpenFolder()
        {
            // Open file explorer
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                return fbd.SelectedPath;

                //if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                //{
                    // string[] files = Directory.GetFiles(fbd.SelectedPath);

                    // Number of files found : files.Length
                    // Selected path : fbd.SelectedPath

                //}
            }
        }


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

            //RunSearch();

            //when first searching you will need to store the type of search and where/what it is searching in a public var (see above)
            //you will also need to change RunSearch so that it can either take new input, or use the last search values
            //in this case we want to use the last search values are we are only reloading the same search

        }
        





    }

    
}
