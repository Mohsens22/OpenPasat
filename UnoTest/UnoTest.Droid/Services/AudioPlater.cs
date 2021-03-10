using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Services;

namespace UnoTest.Droid.Services
{
    public class AudioPlater : IMediaPlayer
    {
        MediaPlayer player;
        

        public void Play(int num)
        {
            AssetFileDescriptor afd = Application.Context.Assets.OpenFd($"Assets/Fa/Audio/__{num}.mp3");
            player = new MediaPlayer();
            player.SetDataSource(afd.FileDescriptor,afd.StartOffset,afd.Length);
            player.Prepare();
            player.Start();
        }
    }
}