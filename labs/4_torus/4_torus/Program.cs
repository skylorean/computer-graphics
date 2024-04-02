using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace torus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(500, 500),
                Title = "Lab 4 - Torus",
                Profile = ContextProfile.Compatability,
                Flags = ContextFlags.Default
            };

            IShape shape = new Torus();

            Window window = new Window(shape, GameWindowSettings.Default, nativeWindowSettings);
            window.Run();
        }
    }
}