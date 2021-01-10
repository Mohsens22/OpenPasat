using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Services.Interfaces;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;

namespace UnoTest.Shared.Services
{
    public class AudioPlayer: IMediaPlayer
    {
        readonly MediaPlayer _mp;

        public AudioPlayer()
        {
            _mp = new MediaPlayer();
        }

        public void Play(Uri file)
        {
            _mp.Source = MediaSource.CreateFromUri(file);
            _mp.Play();
        }

        public void Play(int num) => Play(new Uri($"ms-appx:///Assets/Audio/{num}.mp3"));
    }
}
