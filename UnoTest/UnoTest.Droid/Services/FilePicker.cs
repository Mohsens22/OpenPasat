using Android.App;
using Android.Content;
using Android;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoTest.Services;
using System.IO;
using System.Diagnostics;

namespace UnoTest.Droid.Services
{
    public class FilePicker : ISaver
    {
        public async Task Save(string suggestedName, byte[] bytes, string type, params string[] types)
        {
            try{
                var state = Android.OS.Environment.ExternalStorageState;
                if (state == "mounted")
                {
                    bool isWriteable = Android.OS.Environment.MediaMounted.Equals(Android.OS.Environment.ExternalStorageState);
                    if (isWriteable)
                    {
                        var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath;
                        var saveIn = Path.Combine(path, suggestedName + types[0]);
                        await File.WriteAllBytesAsync(saveIn, bytes);

                        Toast.MakeText(Application.Context, $"Saved at {saveIn}", ToastLength.Long).Show();
                    }
                }
            }
            catch(Exception ex)
            {
                Toast.MakeText(Application.Context, ex.Message, ToastLength.Long).Show();
            }
            
        }
    }
}