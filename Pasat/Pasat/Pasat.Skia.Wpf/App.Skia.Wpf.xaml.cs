using Pasat.Infrastructure.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pasat
{
    public partial class App
    {
        void RegisterPlatformServices()
        {

        }

        FeatureConfiguration GetPlatformFeatures()
        {
            return new FeatureConfiguration
            {
                AudioRepresentation = FeatureAvailability.Available,
                UiRepresentation = FeatureAvailability.Available,
                MixedRepresentation = FeatureAvailability.Unavailable,
                KeyboardInput = FeatureAvailability.Available,
                UiInput = FeatureAvailability.Available,
                InAppDatabase = FeatureAvailability.Available,
                MultiUserEnabled = FeatureAvailability.Available
            };
        }
    }
}
