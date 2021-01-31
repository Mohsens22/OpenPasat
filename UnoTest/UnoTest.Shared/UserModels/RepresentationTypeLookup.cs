using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Extentions;
using UnoTest.Shared.Models;

namespace UnoTest.Shared.UserModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class RepresentationTypeLookup
    {
        public RepresentationType Item { get; set; }
        public string Display { get; set; }

        public static List<RepresentationTypeLookup> Load()
        {

            var list = new List<RepresentationTypeLookup>();

            if (App.Features.AudioRepresentation.IsAvailable())
            {
                list.Add(new RepresentationTypeLookup
                {
                    Display="Audio",
                    Item= RepresentationType.Audio
                });
            }
            if (App.Features.UiInput.IsAvailable())
            {
                list.Add(new RepresentationTypeLookup
                {
                    Display = "UI",
                    Item = RepresentationType.UI
                });
            }
            if (App.Features.MixedRepresentation.IsAvailable())
            {
                list.Add(new RepresentationTypeLookup
                {
                    Display = "Mixed",
                    Item = RepresentationType.Mixed
                });
            }

            return list;
        }

    }
}
