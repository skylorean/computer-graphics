using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace chess
{
    public class Window : GameWindow
    {

        private Chess chess;
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            CenterWindow();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(Color4.White);
            GL.Enable(EnableCap.Texture2D);

            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Lighting);
            GL.Light(LightName.Light2, LightParameter.Position, new Vector4(1f, 1f, 1f, 0f));
            GL.Light(LightName.Light2, LightParameter.Diffuse, new Vector4(1f, 1f, 1f, 1f));
            GL.Light(LightName.Light2, LightParameter.Ambient, new Vector4(0.2f, 0.2f, 0.2f, 1f));
            GL.Light(LightName.Light2, LightParameter.Specular, new Vector4(1f, 1f, 1f, 1f));
            GL.Enable(EnableCap.Light2);

            GL.Enable(EnableCap.ColorMaterial);

            GL.Enable(EnableCap.Normalize);

            var matrix = Matrix4.LookAt(
                0f, 300f, 350f,
                0f, 0f, 0f,
                0, 1, 0);
            GL.LoadMatrix(ref matrix);

            chess = new();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            int width = e.Width;
            int height = e.Height;

            GL.Viewport(0, 0, width, height);

            SetupProjectionMatrix(width, height);

            GL.MatrixMode(MatrixMode.Modelview);
            base.OnResize(e);

            OnRenderFrame(new FrameEventArgs());
        }

        void SetupProjectionMatrix(int width, int height)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            // Вычисляем соотношение сторон клиентской области окна
            double aspectRatio = ((double)width) / ((double)height);

            // Размер видимого объема, которые должен поместиться в порт просмотра
            double frustumSize = 2;

            // Считаем, что высота видимой области равна FRUSTUM_SIZE
            // (на расстоянии до ближней плоскости отсечения)
            double frustumHeight = frustumSize;

            // Ширина видимой области рассчитывается согласно соотношению сторон окна
            // (шире окно - шире область видимости и наоборот)
            double frustumWidth = frustumHeight * aspectRatio;

            // Если ширина видимой области получилась меньше, чем FRUSTUM_SIZE,
            // то корректируем размеры видимой области
            if (frustumWidth < frustumSize && (aspectRatio != 0))
            {
                frustumWidth = frustumSize;
                frustumHeight = frustumWidth / aspectRatio;
            }
            GL.Frustum(
                -frustumWidth * 0.5, frustumWidth * 0.5, // left, right
                -frustumHeight * 0.5, frustumHeight * 0.5, // top, bottom
                frustumSize * 0.5, frustumSize * 500 // znear, zfar
                );
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            chess.Draw();

            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }
    }
}
