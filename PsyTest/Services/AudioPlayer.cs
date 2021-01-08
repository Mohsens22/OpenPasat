using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Media.Audio;
using Windows.Media.Render;
using Windows.Storage;

namespace PsyTest.Services
{
    public static class AudioPlayer
    {
        private static AudioGraph graph;
        private static AudioDeviceOutputNode deviceOutput;
        public static async Task CreateAudioGraph()
        {
            // Create an AudioGraph with default settings
            var settings = new AudioGraphSettings(AudioRenderCategory.Media);
            var result = await AudioGraph.CreateAsync(settings);

            if (result.Status != AudioGraphCreationStatus.Success)
            {
                // Cannot create graph
                return;
            }

            graph = result.Graph;

            // Create a device output node
            var deviceOutputNodeResult = await graph.CreateDeviceOutputNodeAsync();

            if (deviceOutputNodeResult.Status != AudioDeviceNodeCreationStatus.Success)
            {
                // Cannot create device output node
                return;
            }

            deviceOutput = deviceOutputNodeResult.DeviceOutputNode;
        }

        public static async Task Play(StorageFile file)
        {
            graph.Stop();
            AudioFileInputNode fileInput;
            var fileInputResult = await graph.CreateFileInputNodeAsync(file);
            if (AudioFileNodeCreationStatus.Success != fileInputResult.Status)
            {
                // Cannot read input file
                return;
            }

            fileInput = fileInputResult.FileInputNode;

            fileInput.AddOutgoingConnection(deviceOutput);
            graph.Start();
        }
        public static async Task Play(int num) => await Play(await StorageFile.GetFileFromApplicationUriAsync(new Uri($@"ms-appx:///Assets/Samples/Numbers/{num}.wav")));
    }
}
