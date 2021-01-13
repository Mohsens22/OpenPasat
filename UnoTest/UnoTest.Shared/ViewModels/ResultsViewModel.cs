using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Models;
using UnoTest.Shared.UserModels;

namespace UnoTest.Shared.ViewModels
{
    public class ResultsViewModel : ReactiveObject, IRoutableViewModel
    {
        public ResultsViewModel(IScreen screen,TestSheet sheet)
        {
            HostScreen = screen;
            ActiveSheet = sheet;

            SplineData = new List<LineModel>();
            SplineData.Add(new LineModel() { XValue = "1995", YValue = 103 });
            SplineData.Add(new LineModel() { XValue = "1997", YValue = 221 });
            SplineData.Add(new LineModel() { XValue = "1999", YValue = 80 });
            SplineData.Add(new LineModel() { XValue = "2001", YValue = 110 });
            SplineData.Add(new LineModel() { XValue = "2003", YValue = 80 });
            SplineData.Add(new LineModel() { XValue = "2005", YValue = 160 });
            SplineData.Add(new LineModel() { XValue = "2007", YValue = 200 });

            this.Data = new List<Model>();
            Data.Add(new Model() { Country = "Uruguay", Count = 2807 });
            Data.Add(new Model() { Country = "Argentina", Count = 2577 });
            Data.Add(new Model() { Country = "USA", Count = 960 });
            Data.Add(new Model() { Country = "Germany", Count = 2120 });
        }

        [Reactive]
        public List<LineModel> SplineData { get; set; }
        [Reactive]
        public List<Model> Data { get; set; }

        public string UrlPathSegment => this.ToString();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }
        [Reactive]
        public TestSheet ActiveSheet { get; set; }
        public override string ToString() => "ResultVM";
    }
}
