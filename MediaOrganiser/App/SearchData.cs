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

            output.picturePath = Main.config[id, 5];
            output.titleName = Main.config[id, 1];
            output.extName = Main.config[id, 2];

            return output;
        }


    }

    public class FilesModel
    {
        public string picturePath { get; set; }
        public string titleName { get; set; }
        public string extName { get; set; }
    }
}
