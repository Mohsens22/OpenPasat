using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Infrastructure.Features;
using UnoTest.Services;
using UnoTest.Services.Generic;

namespace UnoTest
{
    public partial class App
    {
        void RegisterPlatformServices()
        {
            Locator.CurrentMutable.Register(() => new GenericPicker(), typeof(ISaver));
        }

        FeatureConfiguration GetPlatformFeatures()
        {
            return new FeatureConfiguration
            {
                AudioRepresentation = FeatureAvailability.Unavailable,
                UiRepresentation = FeatureAvailability.Available,
                MixedRepresentation = FeatureAvailability.Unavailable,
                KeyboardInput = FeatureAvailability.Available,
                UiInput = FeatureAvailability.Available,
                InAppDatabase = FeatureAvailability.Unavailable
            };
        }
    }
}
