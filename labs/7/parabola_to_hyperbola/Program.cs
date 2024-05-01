using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace parabola_to_hyperbola
{
    class Program
    {
        static void Main(string[] args)
        {
            var nativeWinSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(600, 600),
                Title = "Parabola To Hyperbola",
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability,
            };

            Window window = new Window(GameWindowSettings.Default, nativeWinSettings);
            window.Run();
        }
    }
}