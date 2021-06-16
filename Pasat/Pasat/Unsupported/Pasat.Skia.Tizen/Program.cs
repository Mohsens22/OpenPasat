using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace Pasat.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new Pasat.App(), args);
            host.Run();
        }
    }
}
