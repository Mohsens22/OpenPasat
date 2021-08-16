using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pasat.Extentions;
using Pasat.Models;

namespace Pasat.UserModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class MaritalStatusTypeLookup
    {
        public MaritalStatusTypeLookup(MaritalStatus rp, string disply)
        {
            this.Item = rp;
            this.Display = disply;
        }
        public MaritalStatus Item { get; set; }
        public string Display { get; set; }

        public static List<MaritalStatusTypeLookup> Load()
        {

            var lst = new List<MaritalStatusTypeLookup>
            {
                new MaritalStatusTypeLookup(MaritalStatus.NoAnswer,LanguageHelper.GetString("NoAnswer","Text")),
                new MaritalStatusTypeLookup(MaritalStatus.Single,LanguageHelper.GetString("Single","Text")),
                new MaritalStatusTypeLookup(MaritalStatus.Married,LanguageHelper.GetString("Married","Text")),
                new MaritalStatusTypeLookup(MaritalStatus.Engaged,LanguageHelper.GetString("Engaged","Text")),
                new MaritalStatusTypeLookup(MaritalStatus.Divorced,LanguageHelper.GetString("Divorced","Text")),
                new MaritalStatusTypeLookup(MaritalStatus.Seperated,LanguageHelper.GetString("Seperated","Text")),
                new MaritalStatusTypeLookup(MaritalStatus.Relationship,LanguageHelper.GetString("Relationship","Text")),
                new MaritalStatusTypeLookup(MaritalStatus.OpenRelationship,LanguageHelper.GetString("OpenRelationship","Text")),
                new MaritalStatusTypeLookup(MaritalStatus.DomesticPartnership,LanguageHelper.GetString("DomesticPartnership","Text")),
                new MaritalStatusTypeLookup(MaritalStatus.Other,LanguageHelper.GetString("Other","Text"))
            };


            return lst;
        }
    }
}
