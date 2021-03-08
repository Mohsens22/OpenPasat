using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;
using Olive;
using System.Diagnostics;

namespace UnoTest.Services
{
    class AudioPlayer : IMediaPlayer
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        public void Play(int num)
        {
            var uri = new Uri(@$".\Assets\Audio\{num}.mp3",UriKind.Relative);
            mediaPlayer.Open(uri);
            mediaPlayer.Play();
        }
    }
}
