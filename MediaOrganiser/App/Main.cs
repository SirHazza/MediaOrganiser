using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaOrganiser
{
    public class Main
    {
        // Config data object
        public static string[,] config;

        // Folders paths
        static string rootFolder = Directory.GetCurrentDirectory();
        static string containerFolder = rootFolder + @"\data";
        public static string pictureFolder = containerFolder + @"\pictures";

        // Files paths
        public static string configFile = containerFolder + @"\config.txt";
        public static string categoriesFile = containerFolder + @"\categories.txt";
        public static string playlistsFile = containerFolder + @"\playlists.txt";

        // Supported extensions array
        public static string[] allExt = new string[11]
        {
            "", ".flac", ".ogg", ".aac", ".mp3", ".wav", ".avi", ".mp4", ".mov", ".wmv", ".mkv"
        };

        // Avaiable playlists array
        public static string[] allPlaylists
        {
            get { return ReturnSplitList(playlistsFile); }
            set { }
        }

        // List type
        public static int listType;


        // Application start up method
        public static void RunSetup()
        {
            // Create new folders
            CreateFolders();
            // Create new files
            CreateFile(configFile);
            CreateFile(categoriesFile);
            CreateFile(playlistsFile);
            // Load config file
            LoadConfig();
        }

        // Create folders
        public static void CreateFolders()
        {
            // If directory does not exist, create it. 
            if (!Directory.Exists(containerFolder))
            {
                Directory.CreateDirectory(containerFolder);
            }

            // If directory does not exist, create sub directory for pictures
            if (!Directory.Exists(pictureFolder))
            {
                Directory.CreateDirectory(pictureFolder);
            }
        }

        //Create files
        public static void CreateFile(string fileName)
        {
            // Check if file already exists. If no, create it.     
            if (!File.Exists(fileName))
            {
                // Create a new file     
                FileStream fs = File.Create(fileName);
                fs.Close();
            }
        }

        // Get folder path
        public static string OpenFolder(string description)
        {
            // Use Folder Browser Dialog
            using (var fbd = new FolderBrowserDialog())
            {
                // Dialog description
                fbd.Description = description;
                // Show dialog to user and return slected path
                DialogResult result = fbd.ShowDialog();
                return fbd.SelectedPath;
            }
        }

        // Get file path
        public static string OpenFile()
        {
            // Use Open File Dialog
            using (var fbd = new OpenFileDialog())
            {
                // Set dialog properties
                fbd.Multiselect = false;
                fbd.DefaultExt = "txt";
                // Show dialog to user and return slected path
                DialogResult result = fbd.ShowDialog();
                return fbd.FileName;
            }
        }

        // Load config file into application
        public static void LoadConfig()
        {
            string line;
            int fileLines = File.ReadLines(configFile).Count();
            config = new string[fileLines, 9];
            string[] parts;
            int counter = 0;

            // Read cinfig file
            StreamReader reader = new StreamReader(configFile);

            // While line present, split line by ';' into main config array
            while ((line = reader.ReadLine()) != null)
            {
                parts = line.Split(';');

                for (int i = 0; i < 9; i++)
                {
                    config[counter, i] = parts[i];
                }

                counter++;
            }

            // Close reader
            reader.Close();
        }

        // Get a set of data in the config file
        public static string[] ReturnConfigSet(int set)
        {
            int configLength = config.GetLength(0);
            string[] dataSet = new string[configLength];
            
            // Gets category of data from config array
            for (int i = 0; i < configLength; i++)
            {
                dataSet[i] = config[i, set];
            }

            return dataSet;
        }

        //Get list as split array
        public static string[] ReturnSplitList(string listPath)
        {
            // Create new folders
            CreateFolders();
            // Create new files
            CreateFile(categoriesFile);
            CreateFile(playlistsFile);

            string listLine;
            string[] splitLists;

            // Read list file and add to array
            StreamReader reader = new StreamReader(listPath);
            listLine = reader.ReadLine();
            reader.Close();

            // Split list if content present
            if (!string.IsNullOrWhiteSpace(listLine))
            {
                splitLists = listLine.Split(',');
                return splitLists;
            }
            else
            {
                // If content empty, return 1 sized array with blank string
                string[] splitOne = new string[1] { "" };
                return splitOne;
            }
        }

    }
}
