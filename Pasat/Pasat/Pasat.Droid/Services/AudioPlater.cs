﻿using Android.App;
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
using Pasat.Services;
using Pasat.Extentions;

namespace Pasat.Droid.Services
{
    public class AudioPlater : IMediaPlayer
    {
        MediaPlayer player;


        public void Play(int num)
        {
            AssetFileDescriptor afd = Application.Context.Assets.OpenFd($"Assets/{LanguageHelper.GetTag()}/Audio/__{num}.mp3");
            player = new MediaPlayer();
            player.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
            player.Prepare();
            player.Start();
        }
    }
}