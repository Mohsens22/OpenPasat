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
using Pasat.Droid.Services;
using Pasat.Infrastructure.Features;
using Pasat.Services;
using Pasat.Services.Generic;
using Splat;

namespace Pasat
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