using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoTest.Services;
using UnoTest.Shared.Services;

namespace UnoTest
{
    public partial class App
    {
        void RegisterPlatformServices()
        {
            Locator.CurrentMutable.Register(() => new AudioPlayer(), typeof(IMediaPlayer));
        }
    }
}
