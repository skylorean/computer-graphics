using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace TestOpenGL
{
    public class Game : GameWindow
    {
        private int _vertexBufferHandle;

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
        {
            //CenterWindow(new Vector2i(1280, 768));
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);

            Console.WriteLine("Resized");
            base.OnResize(e);
        }

        protected override void OnLoad()
        {
            GL.ClearColor(Color4.Coral);

            float[] verteces =
            [
                0.0f, 0.5f, 0f,
                0.5f, -0.5f, 0f,
                -0.5f, -0.5f, 0f,
            ];

            _vertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, verteces.Length * sizeof(float), verteces, BufferUsageHint.StaticDraw);

            string vertexShaderCode = @"
                void main(
                "



            Console.WriteLine("Loaded");
            base.OnLoad();
        }

        protected override void OnUnload()
        {
            Console.WriteLine("UnLoaded");
            base.OnUnload();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
