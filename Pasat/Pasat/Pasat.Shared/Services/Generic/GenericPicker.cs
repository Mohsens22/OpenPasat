using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Olive;
using Windows.Storage.Pickers;

namespace Pasat.Services.Generic
{
    public class GenericPicker : ISaver
    {
        public async Task Save(string suggestedName, byte[] bytes, string type, params string[] types)
        {
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add(type, types);
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = suggestedName;

            var file = await savePicker.PickSaveFileAsync();


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
    }
}
