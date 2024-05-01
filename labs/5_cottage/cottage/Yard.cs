using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace cottage
{
    public class Yard
    {
        public int GrassTexture { get; set; }
        public Box2 GrassTextureCoord { get; set; } = new(0f, 0f, 1f, 1f);
        public Box3 GrassCoords { get; set; } = new(-35f, -10f, -25f, 35f, -10f, 25f);

        public void Draw()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            DrawGrass();
        }

        private void DrawGrass()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, GrassTexture);
            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 1f, 0f);

            GL.TexCoord2(GrassTextureCoord.Min.X, GrassTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Max.Y, GrassCoords.Min.Z);
            GL.TexCoord2(GrassTextureCoord.Min.X, GrassTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Min.Y, GrassCoords.Max.Z);
            GL.TexCoord2(GrassTextureCoord.Max.X, GrassTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Min.Y, GrassCoords.Max.Z);
            GL.TexCoord2(GrassTextureCoord.Max.X, GrassTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Max.Y, GrassCoords.Min.Z);

            GL.End();
        }
    }
}
