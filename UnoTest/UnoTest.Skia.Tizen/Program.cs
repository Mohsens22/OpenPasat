using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace UnoTest.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new UnoTest.App(), args);
            host.Run();
        }
    }
}
