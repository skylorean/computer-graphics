using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace cottage
{
    class Program
    {
        static void Main(string[] args)
        {
            var nativeWinSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(1200, 768),
                Title = "Lab 5 - Cottage",
                Profile = ContextProfile.Compatability,
                Flags = ContextFlags.Default,
            };


            Window window = new Window(GameWindowSettings.Default, nativeWinSettings);
            window.Run();

        }
    }
}
