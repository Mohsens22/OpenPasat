using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Infrastructure.Features
{
    public class FeatureConfiguration
    {
        public FeatureAvailability AudioRepresentation { get; set; }
        public FeatureAvailability UiRepresentation { get; set; }
        public FeatureAvailability MixedRepresentation { get; set; }
        public FeatureAvailability RandomRepresentation { get; set; }
        public FeatureAvailability KeyboardInput { get; set; }
        public FeatureAvailability UiInput { get; set; }
        public FeatureAvailability VocalInput { get; set; }
        public FeatureAvailability VocalAutoCorrection { get; set; }
        public FeatureAvailability VocalManualCorrectoin { get; set; }
        public FeatureAvailability InAppDatabase { get; set; }
        public FeatureAvailability WebServices { get; set; }
        public FeatureAvailability CustomHeadBar { get; set; }
        public FeatureAvailability MultiUserEnabled { get; set; }
    }
}
