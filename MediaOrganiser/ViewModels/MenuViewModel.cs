using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganiser.ViewModels
{
    public class MenuViewModel : Screen
    {

        // Exit button
        public void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }

        // Search Playlist
        public void SearchPlaylist()
        {
            //ActivateItem(new SearchSelectPlaylistViewModel());
            
        }
    }
}
