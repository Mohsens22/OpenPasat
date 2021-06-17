using Pasat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uno.Foundation;

namespace Pasat.Wasm.Services
{
    public class AudioPlayer : IMediaPlayer
    {
        public void Play(int num)
        {
            var env = Environment.GetEnvironmentVariable("UNO_BOOTSTRAP_APP_BASE");
            WebAssemblyRuntime.InvokeJS(@"var sound = new Howl({
  src: [`" + env + @"/Assets/Audio/" + num + @".mp3`]
});

sound.play();");
        }
    }
}
