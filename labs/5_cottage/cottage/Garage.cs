using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace cottage
{
    internal class Garage
    {
        public int WallTexture { get; set; }
        public int GarageDoorTexture { get; set; }
        public int RoofTexture { get; set; }
        public int WindowTexture { get; set; }

        public Box2 WallTextureCoord { get; set; } = new(0.5f, 0.5f, 1f, 1f);
        public Box2 GarageDoorTextureCoord { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 RoofTextureCoord { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 GrafiryTextureCoord { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 WindowTextureCoord { get; set; } = new(0f, 0f, 1f, 1f);

        public Box3 WallCoords { get; set; } = new(0f, -10f, 15f, 20f, 0f, -5f);
        public Box3 GarageDoorCoords { get; set; } = new(5f, -10f, 15.1f, 15f, -1f, 15.1f);
        public Box3 RootCoords { get; set; } = new(0f, 0f, 17f, 21f, 5f, -7f);
        public Box3[] WindowsCoords { get; set; } =
        {
            new Box3(20.1f, -8f, 5f, 20.1f, -1f, 9f),
            new Box3(20.1f, -8f, 1f, 20.1f, -1f, 5f),
        };


        public void Draw()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            DrawGarageBox();
            DrawGarageRoof();
            DrawGarageWindows();
            DrawGarageDoor();
        }

        private void DrawGarageBox()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, WallTexture);

            GL.Begin(PrimitiveType.Quads);

            // Back Side Wall
            GL.Normal3(0f, 0f, -1f);
            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Min.Z);
            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Min.Y, WallCoords.Min.Z);
            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Min.Y, WallCoords.Min.Z);
            GL.TexCoord2(WallTextureCoord.Max.Y, WallTextureCoord.Min.Y);
            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Min.Z);

            // Left Side Wall
            GL.Normal3(-1f, 0f, 0f);
            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Min.Y, WallCoords.Min.Z);
            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Min.Z);
            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Max.Z);
            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Min.Y, WallCoords.Max.Z);

            // Front Side Wall
            GL.Normal3(0f, 0f, 1f);
            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Max.Z);
            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Min.Y, WallCoords.Max.Z);
            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Min.Y, WallCoords.Max.Z);
            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Max.Z);

            // Right Side Wall
            GL.Normal3(1f, 0f, 0f);
            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Min.Y, WallCoords.Max.Z);
            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Max.Z);
            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Min.Z);
            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Min.Y, WallCoords.Min.Z);

            GL.End();

        }

        private void DrawGarageRoof()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, RoofTexture);
            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(1f, 1f, 0f);
            GL.TexCoord2(RoofTextureCoord.Max.X, RoofTextureCoord.Max.Y);
            GL.Vertex3(RootCoords.Max.X, RootCoords.Min.Y, RootCoords.Max.Z);
            GL.TexCoord2(RoofTextureCoord.Min.X, RoofTextureCoord.Max.Y);
            GL.Vertex3(RootCoords.Max.X, RootCoords.Min.Y, RootCoords.Min.Z);
            GL.TexCoord2(RoofTextureCoord.Min.X, RoofTextureCoord.Min.Y);
            GL.Vertex3(RootCoords.Min.X, RootCoords.Max.Y, RootCoords.Min.Z);
            GL.TexCoord2(RoofTextureCoord.Max.X, RoofTextureCoord.Min.Y);
            GL.Vertex3(RootCoords.Min.X, RootCoords.Max.Y, RootCoords.Max.Z);

            GL.End();

            GL.Begin(PrimitiveType.Triangles);
            GL.Normal3(0f, 0f, 1f);
            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Max.Z);
            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Max.Z);
            GL.TexCoord2(
                WallTextureCoord.Min.X,
                0);
            GL.Vertex3(RootCoords.Min.X, RootCoords.Max.Y, WallCoords.Max.Z);
            GL.Normal3(0f, 0f, -1f);
            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Min.Z);
            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Min.Z);
            GL.TexCoord2(
                WallTextureCoord.Min.X,
                0);
            GL.Vertex3(RootCoords.Min.X, RootCoords.Max.Y, WallCoords.Min.Z);
            GL.End();
        }

        private void DrawGarageWindows()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, WindowTexture);

            GL.Begin(PrimitiveType.Quads);

            foreach (var window in WindowsCoords)
            {
                SetNormalForWindow(window);
                GL.TexCoord2(WindowTextureCoord.Max.X, WindowTextureCoord.Max.Y);
                GL.Vertex3(window.Min.X, window.Min.Y, window.Min.Z);

                GL.TexCoord2(WindowTextureCoord.Max.X, WindowTextureCoord.Min.Y);
                GL.Vertex3(window.Min.X, window.Max.Y, window.Min.Z);

                GL.TexCoord2(WindowTextureCoord.Min.X, WindowTextureCoord.Min.Y);
                GL.Vertex3(window.Max.X, window.Max.Y, window.Max.Z);

                GL.TexCoord2(WindowTextureCoord.Min.X, WindowTextureCoord.Max.Y);
                GL.Vertex3(window.Max.X, window.Min.Y, window.Max.Z);
            }

            GL.End();

        }

        private void SetNormalForWindow(Box3 window)
        {
            if (window.Max.Z >= WallCoords.Max.Z)
            {
                GL.Normal3(0f, 0f, 1f);
            }
            else if (window.Min.Z <= WallCoords.Min.Z)
            {
                GL.Normal3(0f, 0f, -1f);
            }
            else if (window.Max.X >= WallCoords.Max.X)
            {
                GL.Normal3(1f, 0f, 0f);
            }
            else if (window.Min.X <= WallCoords.Min.X)
            {
                GL.Normal3(-1f, 0f, 0f);
            }
            else
            {
                GL.Normal3(1f, 0f, 0f);
            }
        }

        private void DrawGarageDoor()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, GarageDoorTexture);

            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 0f, 1f);
            GL.TexCoord2(GarageDoorTextureCoord.Max.X, GarageDoorTextureCoord.Max.Y);
            GL.Vertex3(GarageDoorCoords.Min.X, GarageDoorCoords.Min.Y, GarageDoorCoords.Max.Z);

            GL.TexCoord2(GarageDoorTextureCoord.Max.X, GarageDoorTextureCoord.Min.Y);
            GL.Vertex3(GarageDoorCoords.Min.X, GarageDoorCoords.Max.Y, GarageDoorCoords.Max.Z);

            GL.TexCoord2(GarageDoorTextureCoord.Min.X, GarageDoorTextureCoord.Min.Y);
            GL.Vertex3(GarageDoorCoords.Max.X, GarageDoorCoords.Max.Y, GarageDoorCoords.Max.Z);

            GL.TexCoord2(GarageDoorTextureCoord.Min.X, GarageDoorTextureCoord.Max.Y);
            GL.Vertex3(GarageDoorCoords.Max.X, GarageDoorCoords.Min.Y, GarageDoorCoords.Max.Z);

            GL.End();
        }
    }
}
