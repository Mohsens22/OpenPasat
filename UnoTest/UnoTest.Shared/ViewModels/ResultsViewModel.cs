using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Models;
using UnoTest.Shared.UserModels;

namespace UnoTest.Shared.ViewModels
{
    public class ResultsViewModel : ViewModelBase, IActivatableViewModel, IRoutableViewModel
    {
        public ResultsViewModel(IScreen screen,TestSheet sheet)
        {
            HostScreen = screen;
            ActiveSheet = sheet;

            SplineData = new List<LineModel>();
            SplineData.AddRange(ActiveSheet.Answers.Where(x => x.Status != CorrectionStatus.NoEntry).Select(x => new LineModel { XValue = x.Status.ToString(), YValue = x.InputSpeed.Value }));

            ConData = new List<LineModel>();
            foreach (var item in ActiveSheet.Answers)
            {
                switch (item.Status)
                {
                    case CorrectionStatus.NoEntry:
                        ConData.Add(new LineModel { XValue = "-", YValue = 0 });
                        break;
                    case CorrectionStatus.False:
                        ConData.Add(new LineModel { XValue = "-", YValue = -1 });
                        break;
                    case CorrectionStatus.True:
                        ConData.Add(new LineModel { XValue = "-", YValue = 1 });
                        break;
                    default:
                        break;
                }
            }

            this.Data = new List<Model>();
            var a = ActiveSheet.Answers.GroupBy(x => x.Status);
            foreach (var item in a)
            {
                Data.Add(new Model { Country= item.Key.ToString(),Count=item.Count()});
            }
        }

        [Reactive]
        public List<LineModel> SplineData { get; set; }

        [Reactive]
        public List<LineModel> ConData { get; set; }
        [Reactive]
        public List<Model> Data { get; set; }

        public string UrlPathSegment => this.ToString();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }
        [Reactive]
        public TestSheet ActiveSheet { get; set; }
        public ViewModelActivator Activator { get; } = new ViewModelActivator();
        public override string ToString() => "ResultVM";
    }
}
