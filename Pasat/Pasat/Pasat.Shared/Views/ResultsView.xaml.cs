using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Pasat.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pasat.Views
{

    public abstract partial class ResultsViewBase : AppReactivePage<ResultsViewModel>
    {

    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class ResultsView : ResultsViewBase
    {
        public ResultsView()
        {
            this.InitializeComponent();
        }

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    RenderTargetBitmap rtb = new RenderTargetBitmap();
        //    await rtb.RenderAsync(stackContent);

        //    var pixelBuffer = await rtb.GetPixelsAsync();
        //    var pixels = pixelBuffer.ToArray();
        //    var displayInformation = DisplayInformation.GetForCurrentView();
        //    var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("testImage" + ".png", CreationCollisionOption.ReplaceExisting);
        //    using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
        //    {
        //        var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
        //        encoder.SetPixelData(BitmapPixelFormat.Bgra8,
        //                             BitmapAlphaMode.Premultiplied,
        //                             (uint)rtb.PixelWidth,
        //                             (uint)rtb.PixelHeight,
        //                             displayInformation.RawDpiX,
        //                             displayInformation.RawDpiY,
        //                             pixels);
        //        await encoder.FlushAsync();
        //    }
        //}
    }
}
