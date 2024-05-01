using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace canabola
{
    class Program
    {
        static void Main(string[] args)
        {
            var nativeWinSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(600, 600),
                Title = "Canabola",
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability,
            };

            Window window = new Window(GameWindowSettings.Default, nativeWinSettings);
            window.Run();
        }
    }
}