using System;
using UnoTest.Services;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace UnoTest.Services
{
    public class AudioPlayer : IMediaPlayer
    {
        readonly MediaPlayer _mp;

        public AudioPlayer()
        {
            _mp = new MediaPlayer();
            _mp.SystemMediaTransportControls.IsPlayEnabled = false;
            _mp.SystemMediaTransportControls.IsEnabled = false;
            _mp.CommandManager.IsEnabled = false;
        }


        public void Play(Uri file)
        {
            _mp.Source = MediaSource.CreateFromUri(file);
            _mp.Play();
        }

        public void Play(int num) => Play(new Uri($"ms-appx:///Assets/Audio/{num}.mp3"));
    }
}
