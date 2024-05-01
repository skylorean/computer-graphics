﻿using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace circle
{
    class Program
    {
        static void Main(string[] args)
        {
            var nativeWinSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(600, 600),
                Location = new Vector2i(30, 30),
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Normal,
                Title = "1",
                Flags = ContextFlags.Default,
                APIVersion = new Version(3, 3),
                Profile = ContextProfile.Compatability,
                API = ContextAPI.OpenGL,
                NumberOfSamples = 0
            };


            Window window = new Window(GameWindowSettings.Default, nativeWinSettings);
            window.Run();

        }
    }
}