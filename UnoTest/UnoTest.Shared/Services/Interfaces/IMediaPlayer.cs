using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace UnoTest.Shared.Services.Interfaces
{
    interface IMediaPlayer
    {
        void Play(Uri file);
        void Play(int num);

    }
}
