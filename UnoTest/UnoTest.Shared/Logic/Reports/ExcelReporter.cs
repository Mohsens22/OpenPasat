using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnoTest.ViewModels;
using Windows.Storage;
using Olive;
using Windows.Storage.Pickers;
using System.IO;
using OfficeOpenXml.Drawing.Chart;
using System.Linq;

namespace UnoTest.Shared.Logic.Reports
{
    static class ExcelReporter
    {
        public static async void SaveAsExcell(ResultsViewModel vm)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var p = new ExcelPackage();



            var overview = p.Workbook.Worksheets.Add("Overview");
            var info = p.Workbook.Worksheets.Add("TestInfo");
            var answers = p.Workbook.Worksheets.Add("Answers");
            var charts = p.Workbook.Worksheets.Add("Correction");
            var susChart = p.Workbook.Worksheets.Add("Sustain");

            overview.DefaultColWidth = 21;
            info.DefaultColWidth = 25;
            answers.DefaultColWidth = 10;


            fillOverview(overview,vm);

            fillTestInfo(info, vm);

            fillAnswers(answers, vm);

            fillCorrectionChart(charts, vm);

            fillSustain(susChart, vm);




            
            var stream = new MemoryStream();
            p.SaveAs(stream);
            var bytes = stream.ReadAllBytes();

            await save(bytes, vm);
            
            p.Dispose();



        }

        private static async Task save(byte[] bytes, ResultsViewModel vm)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Excel file", new List<string>() { ".xlsx" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = $"Export-{vm.ActiveSheet.User.Username}-{DateTime.Now.ToUnixTime()}";

            StorageFile file = await savePicker.PickSaveFileAsync();


            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);

                await Windows.Storage.FileIO.WriteBytesAsync(file, bytes);
                Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    System.Diagnostics.Debug.WriteLine("Saved");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Unsaved");
                }
            }
        }

        private static void fillSustain(ExcelWorksheet susChart, ResultsViewModel vm)
        {
            #region sustain Chart

            //Fill the table
            var startCell2 = susChart.Cells[1, 1];
            startCell2.Offset(0, 0).Value = "Time";
            startCell2.Offset(0, 1).Value = "Correction";

            for (var i = 0; i < vm.ConData.Count; i++)
            {
                startCell2.Offset(i + 1, 1).Value = vm.ConData[i].YValue;
                startCell2.Offset(i + 1, 0).Value = vm.ConData[i].XValue;
            }

            var lineChart = susChart.Drawings.AddLineChart("crtExtensionsSize", eLineChartType.Line);
            //Set top left corner to row 1 column 2
            lineChart.SetPosition(1, 0, 3, 0);

            lineChart.Series.Add(susChart.Cells[2, 2, vm.ConData.Count + 1, 2], susChart.Cells[2, 1, vm.ConData.Count + 1, 1]);

            lineChart.Title.Text = "Sustain";
            //Set datalabels and remove the legend
            lineChart.DataLabel.ShowCategory = false;
            lineChart.DataLabel.ShowPercent = false;
            lineChart.DataLabel.ShowLeaderLines = false;
            lineChart.Legend.Remove();

            #endregion
        }

        private static void fillCorrectionChart(ExcelWorksheet charts, ResultsViewModel vm)
        {
            #region Correction Chart

            //Fill the table
            var startCell = charts.Cells[1, 1];
            startCell.Offset(0, 0).Value = "Result";
            startCell.Offset(0, 1).Value = "Count";

            for (var i = 0; i < vm.Data.Count; i++)
            {
                startCell.Offset(i + 1, 0).Value = vm.Data[i].Key;
                startCell.Offset(i + 1, 1).Value = vm.Data[i].Value;
            }

            var pieChart = charts.Drawings.AddPieChart("crtExtensionsSize", ePieChartType.Pie);
            //Set top left corner to row 1 column 2
            pieChart.SetPosition(1, 0, 2, 0);
            pieChart.SetSize(400, 400);

            pieChart.Series.Add(charts.Cells[2, 2, vm.Data.Count + 1, 2], charts.Cells[2, 1, vm.Data.Count + 1, 1]);

            pieChart.Title.Text = "Correction";
            //Set datalabels and remove the legend
            pieChart.DataLabel.ShowCategory = true;
            pieChart.DataLabel.ShowPercent = true;
            pieChart.DataLabel.ShowLeaderLines = true;

            #endregion
        }

        private static void fillAnswers(ExcelWorksheet answers, ResultsViewModel vm)
        {
            #region Answers

            answers.Cells["A1"].Value = "Question";
            answers.Cells["B1"].Value = "Input";
            answers.Cells["C1"].Value = "Status";
            answers.Cells["D1"].Value = "Speed";

            var row = 2;
            foreach (var item in vm.ActiveSheet.Answers)
            {
                answers.Cells[row, 1].Value = $"{item.PreFragment.Number}+{item.TestFragment.Number}={item.PreFragment.Number + item.TestFragment.Number}";
                answers.Cells[row, 2].Value = item.Input;
                answers.Cells[row, 3].Value = item.Status;
                answers.Cells[row, 4].Value = item.InputSpeed;
                row += 1;
            }

            #endregion
        }

        private static void fillTestInfo(ExcelWorksheet info, ResultsViewModel vm)
        {
            #region Test Info
            info.Cells["A1"].Value = "User";
            info.Cells["A2"].Value = "Username";
            info.Cells["A3"].Value = "Age";
            info.Cells["A4"].Value = "Gender";
            info.Cells["A5"].Value = "Quantum(ms)";
            info.Cells["A6"].Value = "Delay(ms)";
            info.Cells["A7"].Value = "Total Items";
            info.Cells["A8"].Value = "Type";
            info.Cells["A9"].Value = "Started At";
            info.Cells["A10"].Value = "Ended At";


            info.Cells["B1"].Value = vm.ActiveSheet.User.FullName;
            info.Cells["B2"].Value = "@" + vm.ActiveSheet.User.Username;
            info.Cells["B3"].Value = vm.ActiveSheet.User.Age;
            info.Cells["B4"].Value = vm.ActiveSheet.User.Gender;
            info.Cells["B5"].Value = vm.ActiveSheet.Quantum;
            info.Cells["B6"].Value = vm.ActiveSheet.ImpulseRate;
            info.Cells["B7"].Value = vm.ActiveSheet.TestCount;
            info.Cells["B8"].Value = vm.ActiveSheet.RepresentationType;
            info.Cells["B9"].Value = vm.ActiveSheet.StartTime.ToLocalTime();
            info.Cells["B10"].Value = vm.ActiveSheet.EndTime.ToLocalTime();

            #endregion
        }

        private static void fillOverview(ExcelWorksheet overview, ResultsViewModel vm)
        {
            #region Overview

            overview.Cells["A1"].Value = "Test Grade";
            overview.Cells["A2"].Value = "Test Percentage";
            overview.Cells["A3"].Value = "Sustain";
            overview.Cells["A4"].Value = "Fatigue";
            overview.Cells["A5"].Value = "Idle";
            overview.Cells["A6"].Value = "Correct Reaction Time";
            overview.Cells["A7"].Value = "False Reaction Time";
            overview.Cells["A8"].Value = "Mixed Reaction Time";

            overview.Cells["B1"].Value = vm.Grade;
            overview.Cells["B2"].Value = vm.Percentage;

            if (vm.HasTrue)
            {
                overview.Cells["B3"].Value = vm.Sustain;
                overview.Cells["B6"].Value = vm._trueReaction;
            }
            else
            {
                overview.Cells["B3"].Value = "-";
                overview.Cells["B6"].Value = "-";
            }

            if (vm.HasFalse)
            {
                overview.Cells["B4"].Value = vm.Fatigue;
                overview.Cells["B7"].Value = vm._falseReaction;
            }
            else
            {
                overview.Cells["B4"].Value = "-";
                overview.Cells["B7"].Value = "-";
            }

            if (vm.HasNotAnswered)
            {
                overview.Cells["B5"].Value = vm.Idle;
            }
            else
            {
                overview.Cells["B5"].Value = "-";
            }
            if (vm.HasMixed)
            {
                overview.Cells["B8"].Value = vm._mixReaction;
            }
            else
            {
                overview.Cells["B8"].Value = "-";
            }

            #endregion
        }
    }
}
