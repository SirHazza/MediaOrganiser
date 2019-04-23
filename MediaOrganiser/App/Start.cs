using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganiser
{
    public class Start
    {
        // Define variables

        // Folders
        static string rootFolder = Directory.GetCurrentDirectory();
        static string containerFolder = rootFolder + @"\data";
        static string pictureFolder = containerFolder + @"\pictures";

        // Files
        static string configFile = containerFolder + @"\config.txt";
        static string categoriesFile = containerFolder + @"\categories.txt";
        static string playlistsFile = containerFolder + @"\playlists.txt";

        // Run method on start
        public static void RunSetup()
        {
            CreateFolders();
            CreateFile(configFile);
            CreateFile(categoriesFile);
            CreateFile(playlistsFile);
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
            }
        }





    }
}
