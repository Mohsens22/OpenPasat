using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Infrastructure.Features;

namespace UnoTest
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
                UiRepresentation = FeatureAvailability.Available,
                KeyboardInput = FeatureAvailability.Available,
                UiInput = FeatureAvailability.Available
            };
        }
    }
}
