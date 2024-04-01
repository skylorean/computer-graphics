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
                //Location = new Vector2i(30, 30),
                //WindowBorder = WindowBorder.Resizable,
                //WindowState = WindowState.Normal,
                Title = "Lab 4 - Shape",
                //APIVersion = new Version(3, 3),
                //API = ContextAPI.OpenGL,
                //NumberOfSamples = 0,
                Profile = ContextProfile.Compatability,
                Flags = ContextFlags.Default,
            };

            IShape shape = new Dodecahedron();
            //IShape shape = new Icosahedron();
            //IShape shape = new StarDodecahedron();

            Window window = new Window(shape, GameWindowSettings.Default, nativeWindowSettings);
            window.Run();
        }
    }
}