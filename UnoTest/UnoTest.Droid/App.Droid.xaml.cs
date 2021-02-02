using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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