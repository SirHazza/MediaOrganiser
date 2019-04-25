using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MediaOrganiser
{
    public class SearchData
    {
        public List<FilesModel> GetFiles()
        {
            List<FilesModel> output = new List<FilesModel>();

            int configLength = Main.config.GetLength(0);

            for (int i = 0; i < configLength; i++)
            {
                output.Add(GetOneFile(i));
            }

            return output;
        }

        private FilesModel GetOneFile(int id)
        {
            FilesModel output = new FilesModel();
            string _picturePath = @"C:\Users\harrywigman\source\repos\MediaOrganiser\MediaOrganiser\bin\Debug\data\pictures\" + Main.config[id, 5];

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
            return output;
        }


    }

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

    }
}
