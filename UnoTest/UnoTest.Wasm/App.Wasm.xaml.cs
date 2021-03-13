using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnoTest.Infrastructure.Features;
using UnoTest.Services;
using UnoTest.Services.Generic;
using UnoTest.Wasm.Services;

namespace UnoTest
{
    public partial class App
    {
        void RegisterPlatformServices()
        {
            Locator.CurrentMutable.Register(() => new AudioPlayer(), typeof(IMediaPlayer));
            Locator.CurrentMutable.Register(() => new GenericPicker(), typeof(ISaver));
        }

        FeatureConfiguration GetPlatformFeatures()
        {
            return new FeatureConfiguration
            {
                AudioRepresentation = FeatureAvailability.Available,
                UiRepresentation = FeatureAvailability.Available,
                KeyboardInput = FeatureAvailability.Available,
                UiInput = FeatureAvailability.Available,
                InAppDatabase = FeatureAvailability.Available
            };
        }
    }
}
