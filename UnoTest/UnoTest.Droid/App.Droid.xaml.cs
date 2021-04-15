using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Droid.Services;
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
            Locator.CurrentMutable.Register(() => new AudioPlater(), typeof(IMediaPlayer));
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