using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MediaOrganiser
{
    public class SearchData
    {
        // Get all media files needed
        public List<FilesModel> GetFiles()
        {
            // Create search data object
            List<FilesModel> output = new List<FilesModel>();

            // Gets current config length
            int configLength = Main.config.GetLength(0);
            FilesModel newEntry;

            // Steps through config
            for (int i = 0; i < configLength; i++)
            {
                // Gets each config entry
                newEntry = GetOneFile(i);
                // Adds entry if it matches search preferences
                if (SearchPreferences(newEntry) == true)
                {
                    output.Add(newEntry);
                }
                
            }

            return output;
        }

        // Get one file entry
        private FilesModel GetOneFile(int id)
        {
            // Create search data object
            FilesModel output = new FilesModel();
            // Creates picture path
            string _picturePath = Main.pictureFolder + @"\" + Main.config[id, 5];

            // Outputs config data from entry
            output.guid = Main.config[id, 0];
            output.title = Main.config[id, 1];
            output.ext = Main.config[id, 2];
            output.filePath = Main.config[id, 3];
            output.folderPath = Main.config[id, 4];
            output.picture = Main.config[id, 5];
            output.categories = Main.config[id, 6];
            output.playlists = Main.config[id, 7];
            output.comment = Main.config[id, 8];
            output.picturePath = _picturePath;
            output.configLine = id;

            return output;
        }

        private bool SearchPreferences(FilesModel entry)
        {
            // Preference exit flags
            bool[] includeEntry = new bool[4] { false, false, false, false }; 

            // 1 (Check agaisnt search type parameter)
            switch (Search.searchType)
            {
                //Folder
                case 1:
                    if (entry.folderPath == Search.searchFolder)
                    {
                        includeEntry[0] = true;
                    }
                    break;

                //State File
                case 2:
                    //Folder or playlist from state file
                    switch (Search.searchStateType)
                    {
                        case 1:
                            if (entry.folderPath == Search.searchFolder)
                            {
                                includeEntry[0] = true;
                            }
                            break;

                        case 2:
                            if (entry.playlists.Contains(Search.searchPlaylist))
                            {
                                includeEntry[0] = true;
                            }
                            break;
                    }
                    break;

                //Playlist
                case 3:
                    if (string.IsNullOrWhiteSpace(Search.searchPlaylist))
                    {
                        //doesn't include entry
                    }
                    else if (entry.playlists.Contains(Search.searchPlaylist))
                    {
                        includeEntry[0] = true;
                    }
                    break;
            }

            // 2 (Check agaisnt ext)
            if (string.IsNullOrWhiteSpace(Search.searchExt) == true)
            {
                includeEntry[1] = true;
            }
            else if (entry.ext == Search.searchExt)
            {
                includeEntry[1] = true;
            }

            // 3 (Check agaisnt title)
            if (string.IsNullOrWhiteSpace(Search.searchTitle) == true)
            {
                includeEntry[2] = true;
            }
            else if ((entry.title).Contains(Search.searchTitle) == true)
            {
                includeEntry[2] = true;
            }

            // 4 (Check file exists)
            if (File.Exists(entry.filePath))
            {
                includeEntry[3] = true;
            }

            // If all checks pass include entry in search
            if (includeEntry[0] == true & includeEntry[1] == true & includeEntry[2] == true & includeEntry[3] == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    // Search object model
    public class FilesModel
    {
        public string guid { get; set; }
        public string title { get; set; }
        public string ext { get; set; }
        public string filePath { get; set; }
        public string folderPath { get; set; }
        public string picture { get; set; }
        public string categories { get; set; }
        public string playlists { get; set; }
        public string comment { get; set; }
        public string picturePath { get; set; }
        public int configLine { get; set; }
    }
}
