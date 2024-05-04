using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Timers;

using Timer = System.Timers.Timer;

namespace parabola_to_hyperbola
{
    public class Window : GameWindow
    {

        private bool leftButtonPressed = false;
        private float mouseX = 0;
        private float mouseY = 0;

        private float _progress = 0.0f;
        private int _direction = 1;
        private readonly float SPEED = 0.05f;

        private int shaderProgram;

        private Timer _timer = new();


        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            CenterWindow();
            Cursor = MouseCursor.Hand;

            _timer.Elapsed += TimerElapsed;
            _timer.Interval = 50;
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            _progress += _direction * SPEED;

            if (_progress >= 1.0f)
            {
                _direction = -1;
            }
            else if (_progress <= 0.0f)
            {
                _direction = 1;
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            _timer.Start();

            GL.ClearColor(Color4.White);

            GL.Enable(EnableCap.ColorMaterial);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, new Vector4(0.8f, 0.8f, 0f, 1f));
            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, new Vector4(0.2f, 0.2f, 0.2f, 1));
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, new Vector4(0.7f, 0.7f, 0.7f, 1));
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, 100);

            var matrix = Matrix4.LookAt(
                0f, 50f, 200f,
                0f, 0f, 0f,
                0f, 1f, 0f);

            GL.LoadMatrix(ref matrix);

            string vertexShaderSource = File.ReadAllText("./shaders/vertexShader.glsl");
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);

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
            GL.UseProgram(shaderProgram);

            GL.Uniform1(GL.GetUniformLocation(shaderProgram, "progress"), _progress);

            GL.Begin(PrimitiveType.Quads);
            for (int i = -50; i < 50; i++)
            {
                for (int k = -50; k < 50; k++)
                {
                    GL.Normal3(0f, 0f, 1f);
                    GL.Vertex3(i, k, 0.0f);
                    GL.Vertex3(i + 1, k, 0.0f);
                    GL.Vertex3(i + 1, k + 1, 0.0f);
                    GL.Vertex3(i, k + 1, 0.0f);
                }
            }
            GL.End();

            SwapBuffers();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButton.Left)
            {
                leftButtonPressed = true;

                mouseX = MousePosition.X;
                mouseY = MousePosition.Y;
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (!leftButtonPressed) return;
            // Вычисляем смещение курсора мыши
            float dx = e.X - mouseX;
            float dy = e.Y - mouseY;

            // Вычисляем угол поворота вокруг осей Y и X как линейно зависящие
            // от смещения мыши по осям X и Y
            float rotateX = dy * 180 / 200;
            float rotateY = dx * 180 / 200;
            RotateCamera(rotateX, rotateY);

            // Сохраняем текущие координаты мыши
            mouseX = e.X;
            mouseY = e.Y;

            base.OnMouseMove(e);

            OnRenderFrame(new FrameEventArgs());
        }

        private void RotateCamera(float x, float y)
        {
            GL.MatrixMode(MatrixMode.Modelview);

            // Извлекаем текущее значение матрицы моделирования-вида
            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 modelView);

            // Извлекаем направления координатных осей камеры в 3д пространстве
            // как коэффициенты строк матрицы моделирования-вида
            Vector3 xAxis = new(modelView[0, 0], modelView[1, 0], modelView[2, 0]);
            Vector3 yAxis = new(modelView[0, 1], modelView[1, 1], modelView[2, 1]);

            // Поворачиваем вокруг осей x и y камеры
            GL.Rotate(x, xAxis);
            GL.Rotate(y, yAxis);

            // В ходе умножения матриц могут возникать погрешности, которые,
            // накапливаясь могут сильно искажать картинку
            // Для их компенсации после каждой модификации матрицы моделирования-вида
            // проводим ее ортонормирование
            NormalizeModelViewMatrix();
        }

        private void NormalizeModelViewMatrix()
        {
            /*
            Ортонормирование - приведение координатных осей к единичной длине (нормирование)
            и взаимной перпендикулярности (ортогонализация)
            Достичь этого можно при помощи нормализации координатных осей
            и векторного произведения
            */
            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 modelView);

            Vector3 xAxis = new Vector3(modelView[0, 0], modelView[1, 0], modelView[2, 0]);
            xAxis.Normalize();
            Vector3 yAxis = new Vector3(modelView[0, 1], modelView[1, 1], modelView[2, 1]);
            yAxis.Normalize();

            // Ось Z вычисляем через векторное произведение X и Y
            // Z будет перпендикулярна плоскости векторов X и Y
            Vector3 zAxis = Vector3.Cross(xAxis, yAxis);
            // И иметь единичную длину
            zAxis.Normalize();

            xAxis = Vector3.Cross(yAxis, zAxis);
            xAxis.Normalize();
            yAxis = Vector3.Cross(zAxis, xAxis);
            yAxis.Normalize();

            modelView[0, 0] = xAxis.X; modelView[1, 0] = xAxis.Y; modelView[2, 0] = xAxis.Z;
            modelView[0, 1] = yAxis.X; modelView[1, 1] = yAxis.Y; modelView[2, 1] = yAxis.Z;
            modelView[0, 2] = zAxis.X; modelView[1, 2] = zAxis.Y; modelView[2, 2] = zAxis.Z;

            GL.LoadMatrix(ref modelView);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            leftButtonPressed = false;
            base.OnMouseUp(e);
            OnRenderFrame(new FrameEventArgs());
        }

        protected override void OnMouseLeave()
        {
            leftButtonPressed = false;
            base.OnMouseLeave();
            OnRenderFrame(new FrameEventArgs());
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
                frustumSize * 0.5, frustumSize * 200 // znear, zfar
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