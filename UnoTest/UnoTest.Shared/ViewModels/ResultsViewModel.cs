using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using Uno.Extensions;
using UnoTest.Shared.Models;
using UnoTest.Shared.UserModels;

namespace UnoTest.Shared.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ResultsViewModel : ViewModelBase, IActivatableViewModel, IRoutableViewModel
    {
        public ResultsViewModel(IScreen screen,TestSheet sheet)
        {
            HostScreen = screen;
            ActiveSheet = sheet;
            FilteredData = new ObservableCollection<TestAnswer>();
            Mode = GraphResultShowModeLookup.Load();
            SelectedMode = Mode.FirstOrDefault();
            this.WhenActivated(disposables =>
            {
                this
                .WhenAnyValue(x => x.SelectedMode)
                .WhereNotNull()
                .Subscribe(x => 
                {
                    System.Diagnostics.Debug.WriteLine("Chart Update Requested");
                    FilteredData.Clear();
                    switch (x.Item)
                    {
                        case GraphResultShowMode.Mixed:
                            FilteredData.Clear();
                            FilteredData.AddRange(ActiveSheet.Answers.Where(z => z.Status != CorrectionStatus.NoEntry));
                            break;
                        case GraphResultShowMode.True:
                            FilteredData.AddRange(ActiveSheet.Answers.Where(z => z.Status == CorrectionStatus.True));
                            break;
                        case GraphResultShowMode.False:
                            FilteredData.AddRange(ActiveSheet.Answers.Where(z => z.Status == CorrectionStatus.False));
                            break;
                        default:
                            break;
                    }
                })
                .DisposeWith(disposables);
            });
            DataFix();

            
        }

        private void DataFix()
        {
            

            ConData = new List<LineModel>();
            foreach (var item in ActiveSheet.Answers)
            {
                var offset = item.InputTime.Subtract(ActiveSheet.StartTime);
                if (item.Status== CorrectionStatus.NoEntry)
                    offset= item.TestFragment.RepresentationTime.Subtract(ActiveSheet.StartTime);

                var time = $"{offset.Minutes}:{offset.Seconds}";
                switch (item.Status)
                {
                    case CorrectionStatus.NoEntry:
                        ConData.Add(new LineModel { XValue = time, YValue = 0 });
                        break;
                    case CorrectionStatus.False:
                        ConData.Add(new LineModel { XValue = time, YValue = -1 });
                        break;
                    case CorrectionStatus.True:
                        ConData.Add(new LineModel { XValue = time, YValue = 1 });
                        break;
                    default:
                        break;
                }
            }

            this.Data = new List<KeyValuePair<string, int>>();
            Data.AddRange(ActiveSheet.Answers.GroupBy(x => x.Status).Select(x=>new KeyValuePair<string,int>(x.Key.ToString(),x.Count())));
        }

        public ObservableCollection<TestAnswer> FilteredData { get; set; }
        [Reactive]
        public List<GraphResultShowModeLookup> Mode { get; set; }
        [Reactive]
        public GraphResultShowModeLookup SelectedMode { get; set; }


        [Reactive]
        public List<LineModel> ConData { get; set; }
        [Reactive]
        public List<KeyValuePair<string,int>> Data { get; set; }

        public string UrlPathSegment => this.ToString();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }
        [Reactive]
        public TestSheet ActiveSheet { get; set; }
        public ViewModelActivator Activator { get; } = new ViewModelActivator();
        public override string ToString() => "ResultVM";
    }
}
