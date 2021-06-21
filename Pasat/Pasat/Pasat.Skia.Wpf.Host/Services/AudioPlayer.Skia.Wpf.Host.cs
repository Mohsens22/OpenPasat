using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Pasat.Services
{
    class AudioPlayer : IMediaPlayer
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        public void Play(int num)
        {
            var uri = new Uri(@$".\Assets\Fa\Audio\{num}.mp3", UriKind.Relative);
            mediaPlayer.Open(uri);
            mediaPlayer.Play();
        }
    }
}
