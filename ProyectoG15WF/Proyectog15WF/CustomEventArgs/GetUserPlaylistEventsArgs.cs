using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class GetUserPlaylistEventsArgs : EventArgs
    {
        public string ActualLoggedUsername { get; set; }

        public string ActualUsernameSelected { get; set; }

        public string ActualPlaylistSelected { get; set; }

        public string PlaylistNameText { get; set; }

        public string UserSelectedPrivacy { get; set; }

        public string SelectedSong { get; set; }

    }
}
