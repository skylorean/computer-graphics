using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

// избавиться от мельтишения
// и кружок желтый в центре
namespace rectangle
{
    internal class Window : GameWindow
    {

        private int shaderProgram;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            CenterWindow();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            var matrix = Matrix4.LookAt(
                0f, 0f, 2f,
                0f, 0f, 0f,
                0f, 1f, 0f);
            GL.LoadMatrix(ref matrix);

            // Шаг 1 - Создание шейдерного объъекта
            string vertexShaderSource = File.ReadAllText("./shaders/vertexShader.glsl");
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            // Шаг 2 - Загрузка исходного кода в шейдерный объект
            GL.ShaderSource(vertexShader, vertexShaderSource);

            // Шаг 3 - Компиляция шейдерного объекта
            GL.CompileShader(vertexShader);

            // Шаги 1,2,3
            string fragmentShaderSource = File.ReadAllText("./shaders/fragmentShader.glsl");
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);

            // Шаг 4 - создание программного объекта
            shaderProgram = GL.CreateProgram();

            // Шаг 5 - Связывание шейдерных объектов с программным объектом
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);

            // Шаг 6 - Компоновка шейдерной программы
            GL.LinkProgram(shaderProgram);


            // Шаг 7 - установка шейдерной программы
            GL.UseProgram(shaderProgram);

            // Шаг 8 - удаление ненужных шейдеров и программ
            GL.DetachShader(shaderProgram, vertexShader);
            GL.DetachShader(shaderProgram, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteProgram(shaderProgram);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex2(-1, -1);

            GL.TexCoord2(4, 0);
            GL.Vertex2(1, -1);

            GL.TexCoord2(4, 4);
            GL.Vertex2(1, 1);

            GL.TexCoord2(0, 4);
            GL.Vertex2(-1, 1);
            GL.End();

            SwapBuffers();
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
                frustumSize * 0.5, frustumSize * 50 // znear, zfar
                );
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