using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pasat.Extentions;
using Pasat.Models;

namespace Pasat.UserModels
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
                    Display=LanguageHelper.GetString("AudioType", "Text"),
                    Item= RepresentationType.Audio
                });
            }
            if (App.Features.UiInput.IsAvailable())
            {
                list.Add(new RepresentationTypeLookup
                {
                    Display = LanguageHelper.GetString("UIType","Text"),
                    Item = RepresentationType.UI
                });
            }
            if (App.Features.MixedRepresentation.IsAvailable())
            {
                list.Add(new RepresentationTypeLookup
                {
                    Display = LanguageHelper.GetString("MixedType", "Text"),
                    Item = RepresentationType.Mixed
                });
            }

            return list;
        }

    }
}
