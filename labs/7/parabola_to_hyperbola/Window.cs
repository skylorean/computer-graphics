using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Timers;

namespace parabola_to_hyperbola
{
    internal class Window : GameWindow
    {

        private bool leftButtonPressed = false;
        private float mouseX = 0;
        private float mouseY = 0;

        private float progress = 0.0f;
        private int direction = 1;
        private float speed = 0.05f;

        private int shaderProgram;

        System.Timers.Timer timer = new();


        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            CenterWindow();
            Cursor = MouseCursor.Hand;

            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 50;
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            progress += direction * speed;
            if (progress >= 1.0f)
                direction = -1;
            else if (progress <= 0.0f)
                direction = 1;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            timer.Start();

            GL.ClearColor(1f, 1f, 1f, 1);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            GL.Light(LightName.Light2, LightParameter.Position, new Vector4(1f, 1f, 1f, 0f));
            GL.Light(LightName.Light2, LightParameter.Diffuse, new Vector4(1f, 1f, 1f, 1f));
            GL.Light(LightName.Light2, LightParameter.Ambient, new Vector4(0.2f, 0.2f, 0.2f, 1f));
            GL.Light(LightName.Light2, LightParameter.Specular, new Vector4(1f, 1f, 1f, 1f));
            GL.Enable(EnableCap.Light2);

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
            //сделать через матрицу нормалей

            //свет на modelview
            //FragPos на modelview


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

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.UseProgram(shaderProgram);

            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 modelMatrix);
            GL.GetFloat(GetPName.ProjectionMatrix, out Matrix4 projectionMatrix);

            var modelViewProjectionMatrix = projectionMatrix * modelMatrix;
            Matrix3 normalMatrix = new Matrix3(modelMatrix).Inverted();
            normalMatrix.Transpose();

            GL.UniformMatrix3(GL.GetUniformLocation(shaderProgram, "normalMatrix"), false, ref normalMatrix);
            GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram, "modelMatrix"), false, ref modelMatrix);
            GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram, "projectionMatrix"), false, ref projectionMatrix);
            GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram, "ModelViewProjectionMatrix"), false, ref modelViewProjectionMatrix);
            GL.Uniform1(GL.GetUniformLocation(shaderProgram, "progress"), progress);

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
            float rotateX = dy * 180 / 500;
            float rotateY = dx * 180 / 500;
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

            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 modelView);

            Vector3 xAxis = new(modelView[0, 0], modelView[1, 0], modelView[2, 0]);
            Vector3 yAxis = new(modelView[0, 1], modelView[1, 1], modelView[2, 1]);

            GL.Rotate(x, xAxis);
            GL.Rotate(y, yAxis);
            NormalizeModelViewMatrix();
        }

        private void NormalizeModelViewMatrix()
        {
            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 modelView);

            Vector3 xAxis = new(modelView[0, 0], modelView[1, 0], modelView[2, 0]);
            xAxis.Normalize();
            Vector3 yAxis = new(modelView[0, 1], modelView[1, 1], modelView[2, 1]);
            yAxis.Normalize();

            Vector3 zAxis = Vector3.Cross(xAxis, yAxis);
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
            double frustumSize = 2;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            double aspectRatio = ((double)width) / ((double)height);
            double frustumHeight = frustumSize;
            double frustumWidth = frustumHeight * aspectRatio;
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
    }
}