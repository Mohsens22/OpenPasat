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
    public class ResultsViewModel : RoutableViewModel
    {
        public ResultsViewModel(IScreen screen,TestSheet sheet):base(screen)
        {
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
                    MinWindow = FilteredData.Count * 25;
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
                ConData.Add(new LineModel { XValue = time, YValue = (int)item.Status });
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
        public int MinWindow { get; set; }

        [Reactive]
        public List<LineModel> ConData { get; set; }
        [Reactive]
        public List<KeyValuePair<string,int>> Data { get; set; }
        [Reactive]
        public TestSheet ActiveSheet { get; set; }
        public override string ToString() => "ResultVM";
    }
}
