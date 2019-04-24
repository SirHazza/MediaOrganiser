using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganiser
{
    public class Main
    {
        // Define variables

        // Data Objects
        public static string[,] config;
        //public static string[][] config;
        //public static string[] playlists;
        //public static string[] categories;

        // Folders
        static string rootFolder = Directory.GetCurrentDirectory();
        static string containerFolder = rootFolder + @"\data";
        public static string pictureFolder = containerFolder + @"\pictures";
        // Files
        public static string configFile = containerFolder + @"\config.txt";
        public static string categoriesFile = containerFolder + @"\categories.txt";
        public static string playlistsFile = containerFolder + @"\playlists.txt";
        // Supported extensions
        public static string[] allExt = new string[10]
        {
            ".flac", ".ogg", ".aac", ".mp3", ".wav", ".avi", ".mp4", ".mov", ".wmv", ".mkv"
        };

        // Run method on start
        public static void RunSetup()
        {
            CreateFolders();
            CreateFile(configFile);
            CreateFile(categoriesFile);
            CreateFile(playlistsFile);
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

            // Create sub directory
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

        // Load config file into application
        public static void LoadConfig()
        {
            string line;
            int fileLines = File.ReadLines(configFile).Count();
            config = new string[fileLines, 9];
            string[] parts;
            int counter = 0;

            // Clear any previous data
            //if (!(config == null))
            //{
            //    Array.Clear(config, 0, config.GetLength(0) * config.GetLength(1));
            //}

            // Open stream reader
            StreamReader reader = new StreamReader(configFile);

            while ((line = reader.ReadLine()) != null)
            {

                parts = line.Split(';');

                for (int i = 0; i < 9; i++)
                {
                    config[counter, i] = parts[i];
                }

                counter++;
            }

            reader.Close();

        }


        // Get a set of data in the config file
        public static string[] ReturnConfigSet(int set)
        {
            int configLength = config.GetLength(0);
            string[] dataSet = new string[configLength];
            
            for (int i = 0; i < configLength; i++)
            {
                dataSet[i] = config[i, set];
            }

            return dataSet;
        }


        // Get a set of data in the config file as list
        public static List<string> ReturnConfigSetList(int set)
        {
            int configLength = config.GetLength(0);
            List<string> dataSet = new List<string>();

            for (int i = 0; i < configLength; i++)
            {
                dataSet.Add(config[i, set]);
            }

            return dataSet;
        }

    }
}
