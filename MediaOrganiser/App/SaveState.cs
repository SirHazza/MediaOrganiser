using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaOrganiser
{
    public class SaveState
    {

        public static void RunSaveState(string stateFileName)
        {
            // Check file name contains characters
            if (!string.IsNullOrWhiteSpace(stateFileName))
            {
                // Get intended folder location
                string stateFolderPath = Search.OpenFolder("Select folder to save State file");

                // Create text file
                string stateFilePath = string.Format(@"{0}\{1}.txt", stateFolderPath, stateFileName);
                Main.CreateFile(stateFilePath);

                // Write data to State file
                using (StreamWriter writetext = new StreamWriter(stateFilePath))
                {
                    writetext.WriteLine("{0};{1};{2};{3}",
                        Search.searchFolder,
                        Search.searchPlaylist,
                        Search.searchTitle,
                        Search.searchExt
                        );

                    writetext.Close();
                }

                // Confirm completion
                MessageBox.Show(string.Format("Successfully saved search state to file '{0}.txt'", stateFileName));
            }
            else
            {
                MessageBox.Show("Error, no name entered");
            }
        }
        
        

    }
}
