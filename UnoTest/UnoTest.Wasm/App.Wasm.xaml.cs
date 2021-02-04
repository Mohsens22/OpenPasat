﻿using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnoTest.Shared.Infrastructure.Features;

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