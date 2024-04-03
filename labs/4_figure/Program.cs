using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace figure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(1200, 768),
                Title = "Lab 4 - Shape",
                Profile = ContextProfile.Compatability,
                Flags = ContextFlags.Default,
            };

            //IShape shape = new Icosahedron();
            IShape shape = new Dodecahedron();

            Window window = new Window(shape, GameWindowSettings.Default, nativeWindowSettings);
            window.Run();
        }
    }
}