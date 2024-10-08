﻿using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace torus
{
    public class Window : GameWindow
    {
        private bool _leftButtonPressed = false;
        private float _mouseX = 0;
        private float _mouseY = 0;

        private readonly IShape _shape;

        public Window(IShape shape, GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _shape = shape;
            Cursor = MouseCursor.Hand;
            CenterWindow();
        }

        private void SetupProjectionMatrix(int width, int height)
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
                frustumSize * 0.5, frustumSize * 50 // znear, zfar
                );
        }

        private void NormalizeModelViewMatrix()
        {
            Matrix4 modelView;
            GL.GetFloat(GetPName.ModelviewMatrix, out modelView);
            /*
            Ортонормирование - приведение координатных осей к единичной длине (нормирование)
            и взаимной перпендикулярности (ортогонализация)
            Достичь этого можно при помощи нормализации координатных осей
            и векторного произведения
            */
            Vector3 xAxis = new Vector3(modelView[0, 0], modelView[1, 0], modelView[2, 0]);
            xAxis.Normalize();
            Vector3 yAxis = new Vector3(modelView[0, 1], modelView[1, 1], modelView[2, 1]);
            yAxis.Normalize();

            // Ось Z вычисляем через векторное произведение X и Y
            // Z будет перпендикулярна плоскости векторов X и Y
            Vector3 zAxis = Vector3.Cross(xAxis, yAxis);
            // И иметь единичную длину
            zAxis.Normalize();
            // То же самое проделываем с осями x и y
            xAxis = Vector3.Cross(yAxis, zAxis);
            xAxis.Normalize();
            yAxis = Vector3.Cross(zAxis, xAxis);
            yAxis.Normalize();

            // Сохраняем вектора координатных осей обратно в массив
            modelView[0, 0] = xAxis.X; modelView[1, 0] = xAxis.Y; modelView[2, 0] = xAxis.Z;
            modelView[0, 1] = yAxis.X; modelView[1, 1] = yAxis.Y; modelView[2, 1] = yAxis.Z;
            modelView[0, 2] = zAxis.X; modelView[1, 2] = zAxis.Y; modelView[2, 2] = zAxis.Z;

            // И загружаем матрицу моделирвания-вида
            GL.LoadMatrix(ref modelView);
        }

        private void RotateCamera(float x, float y)
        {
            //нужно ортонормировать матрицу
            GL.MatrixMode(MatrixMode.Modelview);

            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 modelView);

            Vector3 xAxis = new Vector3(modelView[0, 0], modelView[1, 0], modelView[2, 0]);
            Vector3 yAxis = new Vector3(modelView[0, 1], modelView[1, 1], modelView[2, 1]);

            GL.Rotate(x, xAxis);
            GL.Rotate(y, yAxis);
            NormalizeModelViewMatrix();
        }

        protected override void OnLoad()
        {
            //base.OnLoad();

          

            //GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.AlphaTest);//управление вывода прозрачности

            ////разробаться спараметрами
            //GL.Light(LightName.Light1, LightParameter.Position, new Vector4(0f, 10f, 0f, 0f));
            //GL.Light(LightName.Light1, LightParameter.Ambient, new Vector4(0.2f, 0.2f, 0.2f, 1f));
            //GL.Light(LightName.Light1, LightParameter.Diffuse, new Vector4(1f, 1f, 1f, 1f));
            //GL.Light(LightName.Light1, LightParameter.Specular, new OpenTK.Mathematics.Vector4(0f, 0f, 0f, 1f));


            //GL.Enable(EnableCap.Lighting);
            //GL.Enable(EnableCap.Light1);

            //GL.Enable(EnableCap.ColorMaterial);
            //GL.Enable(EnableCap.Blend);
            //GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            //GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, new Vector4(1, 1, 1, 1));
            //GL.Material(MaterialFace.Front, MaterialParameter.Ambient, new Vector4(0.2f, 0.2f, 0.2f, 1));
            //GL.Material(MaterialFace.Front, MaterialParameter.Specular, new Vector4(1f, 1f, 1f, 1));
            //GL.Material(MaterialFace.Front, MaterialParameter.Shininess, 1f);


            //GL.Translate(0f, 0f, -5f);

            base.OnLoad();

            GL.ClearColor(Color4.White);

            //GL.ClearColor(Color.White);

            GL.Enable(EnableCap.DepthTest);

            GL.Light(LightName.Light1, LightParameter.Position, new Vector4(0f, 10f, 0f, 0f));
            GL.Light(LightName.Light1, LightParameter.Diffuse, new Vector4(1f, 1f, 1f, 1f));
            GL.Light(LightName.Light1, LightParameter.Ambient, new Vector4(0.2f, 0.2f, 0.2f, 1f));
            GL.Light(LightName.Light1, LightParameter.Specular, new Vector4(1f, 1f, 1f, 1f));


            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light1);

            GL.Enable(EnableCap.ColorMaterial);

            //GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, new Vector4(0.8f, 0.8f, 0f, 1f));
            //GL.Material(MaterialFace.Front, MaterialParameter.Ambient, new Vector4(0.2f, 0.2f, 0.2f, 1));
            //GL.Material(MaterialFace.Front, MaterialParameter.Specular, new Vector4(0.7f, 0.7f, 0.7f, 1));
            //GL.Material(MaterialFace.Front, MaterialParameter.Shininess, 100);


            GL.LoadIdentity();
            var matrix = Matrix4.LookAt(
                0f, 0f, 25f,
                0f, 0f, 0f,
                0, 1, 0);
            GL.LoadMatrix(ref matrix);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                _leftButtonPressed = true;

                _mouseX = MousePosition.X;
                _mouseY = MousePosition.Y;
            }


            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (!_leftButtonPressed) return;
            // Вычисляем смещение курсора мыши
            float dx = e.X - _mouseX;
            float dy = e.Y - _mouseY;

            // Вычисляем угол поворота вокруг осей Y и X как линейно зависящие
            // от смещения мыши по осям X и Y
            float rotateX = dy * 180 / Size.X;
            float rotateY = dx * 180 / Size.Y;
            RotateCamera(rotateX, rotateY);

            // Сохраняем текущие координаты мыши
            _mouseX = e.X;
            _mouseY = e.Y;

            base.OnMouseMove(e);

            OnRenderFrame(new FrameEventArgs());
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            _leftButtonPressed = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave()
        {
            _leftButtonPressed = false;
            base.OnMouseLeave();
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

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.MatrixMode(MatrixMode.Modelview);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            GL.LineWidth(3);
            _shape.Draw();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            float rotateCoef = 10.0f;

            if (e.Key == Keys.Left)
            {
                RotateCamera(0, -rotateCoef);
            }
            if (e.Key == Keys.Right)
            {
                RotateCamera(0, rotateCoef);
            }
            if (e.Key == Keys.Down)
            {
                RotateCamera(rotateCoef,0);
            }
            if (e.Key == Keys.Up)
            {
                RotateCamera(-rotateCoef,0);
            }

            base.OnKeyDown(e);
        }
    }
}
