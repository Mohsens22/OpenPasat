using Pasat.Infrastructure.Features;
using Pasat.Services;
using Pasat.Services.Generic;
using Pasat.Wasm.Services;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pasat
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
                InAppDatabase = FeatureAvailability.Available,
                MultiUserEnabled = FeatureAvailability.Available
            };
        }
    }
}
