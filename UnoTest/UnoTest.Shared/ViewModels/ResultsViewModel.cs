using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using Uno.Extensions;
using UnoTest.Models;
using UnoTest.UserModels;
using Olive;
using System.Reactive;
using UnoTest.Shared.Logic.Reports;
using System.Threading.Tasks;

namespace UnoTest.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ResultsViewModel : RoutableViewModel
    {

        public int? _trueReaction ;
        public int? _falseReaction;
        public int? _mixReaction ;
        public ResultsViewModel(IScreen screen,TestIndentifier sheet):base(screen)
        {
            ActiveSheet = sheet;
            FilteredData = new ObservableCollection<TestAnswer>();
            HasTrue = sheet.Answers.Any(x => x.Status == CorrectionStatus.True);
            HasFalse = sheet.Answers.Any(x => x.Status == CorrectionStatus.False);
            HasNotAnswered = sheet.Answers.Any(x => x.Status == CorrectionStatus.NoEntry);
            Mode = GraphResultShowModeLookup.Load(HasTrue,HasFalse);
            SelectedMode = Mode.FirstOrDefault();
            ExportExcelCommand = ReactiveCommand.CreateFromTask(Export);

            if (HasNotAnswered)
            {
                Idle= getSustain(CorrectionStatus.NoEntry);
            }

            if (HasTrue)
            {
                _trueReaction = (int)sheet.Answers.Where(x => x.Status == CorrectionStatus.True).Select(x => x.InputSpeed).Average().Value;
                Sustain = getSustain(CorrectionStatus.True);
            }
               

            if (HasFalse)
            {
                _falseReaction = (int)sheet.Answers.Where(x => x.Status == CorrectionStatus.False).Select(x => x.InputSpeed).Average().Value;
                Fatigue = getSustain(CorrectionStatus.False); 
            }

            if (HasMixed)
                _mixReaction = (int)sheet.Answers.Where(x => x.Status != CorrectionStatus.NoEntry).Select(x => x.InputSpeed).Average().Value;

            var trueCount = ActiveSheet.Answers.Count(x => x.Status == CorrectionStatus.True);
            var falseCount = ActiveSheet.Answers.Count(x => x.Status == CorrectionStatus.False);
            var all = ActiveSheet.Answers.Count;
            Grade = $"{ trueCount - falseCount} / {all}";
            Percentage = $"{((trueCount - falseCount) * 100) / all}%";


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
                            FilteredData.AddRange(GetMixedList());
                            ReactionTime = _mixReaction.Value;
                            break;
                        case GraphResultShowMode.True:
                            FilteredData.AddRange(GetTrueList());
                            ReactionTime = _trueReaction.Value;
                            break;
                        case GraphResultShowMode.False:
                            FilteredData.AddRange(GetFalseList());
                            ReactionTime = _falseReaction.Value;
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

        private async Task Export() => await ExcelReporter.SaveAsExcell(this);

        private IEnumerable<TestAnswer> GetMixedList() => ActiveSheet.Answers.Where(z => z.Status != CorrectionStatus.NoEntry);

        private IEnumerable<TestAnswer> GetFalseList() => ActiveSheet.Answers.Where(z => z.Status == CorrectionStatus.False);

        private IEnumerable<TestAnswer> GetTrueList() => ActiveSheet.Answers.Where(z => z.Status == CorrectionStatus.True);

        private int getSustain(CorrectionStatus status)
        {
            var sustain = 0;
            var cache = 0;
            for (int i = 0; i < ActiveSheet.Answers.Count; i++)
            {
                var item = ActiveSheet.Answers.ElementAt(i);
                if (item.Status == status)
                {
                    cache += 1;
                }
                else
                {
                    if (cache > sustain)
                    {
                        sustain = cache;
                    }
                    cache = 0;
                }

                if (i== ActiveSheet.Answers.Count - 1)
                {
                    if (cache > sustain)
                    {
                        sustain = cache;
                    }
                }
            }
            
            return sustain;
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
        public int ReactionTime { get; set; }

        public bool HasMixed { get => HasTrue & HasFalse;  }
        public bool HasTrue { get; set; }
        public bool HasFalse { get; set; }
        public bool HasNotAnswered { get; set; }
        public bool HasAny { get => HasTrue | HasFalse; }

        public string Grade { get; set; }
        public string Percentage { get; set; }

        [Reactive]
        public int MinWindow { get; set; }

        
        public int Fatigue { get; set; }
        
        public int Sustain { get; set; }
        public int Idle { get; set; }

        [Reactive]
        public ReactiveCommand<Unit,Unit> ExportExcelCommand { get; set; }

        [Reactive]
        public List<LineModel> ConData { get; set; }
        [Reactive]
        public List<KeyValuePair<string,int>> Data { get; set; }
        [Reactive]
        public TestIndentifier ActiveSheet { get; set; }
        public override string ToString() => "ResultVM";
    }
}
