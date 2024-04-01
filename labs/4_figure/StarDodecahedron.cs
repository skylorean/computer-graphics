using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace figure
{
    public class StarDodecahedron : IShape
    {
        private static readonly float phi = (1f + (float)System.Math.Sqrt(5)) / 2f;

        private readonly Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1, -1, -1),
            new Vector3(1, -1, -1),
            new Vector3(1, -1, 1),
            new Vector3(-1, -1, 1),
            new Vector3(0, -1 / phi, -phi),
            new Vector3(0, -1 / phi, phi),
            new Vector3(-phi, 0, -1 / phi),
            new Vector3(phi, 0, -1 / phi),
            new Vector3(1 / phi, -phi, 0),
            new Vector3(1 / phi, phi, 0),
            new Vector3(-1 / phi, -phi, 0),
            new Vector3(-1 / phi, phi, 0),
            new Vector3(-phi, 0, 1 / phi),
            new Vector3(phi, 0, 1 / phi),
            new Vector3(0, 1 / phi, -phi),
            new Vector3(0, 1 / phi, phi),
            new Vector3(-1, 1, -1),
            new Vector3(1, 1, -1),
            new Vector3(1, 1, 1),
            new Vector3(-1, 1, 1)
        };

        private readonly int[][] faces = new int[][]
        {
            new int[]{ 0, 1, 4, 2, 3 },
            new int[]{ 0, 3, 7, 5, 4 },
            new int[]{ 0, 1, 9, 11, 8 },
            new int[]{ 1, 4, 10, 9, 8 },
            new int[]{ 1, 2, 14, 10, 9 },
            new int[]{ 2, 3, 7, 15, 14 },
            new int[]{ 3, 7, 17, 12, 11 },
            new int[]{ 3, 12, 19, 5, 4 },
            new int[]{ 4, 10, 18, 19, 5 },
            new int[]{ 2, 14, 16, 18, 10 },
            new int[]{ 0, 8, 16, 17, 12 },
            new int[]{ 0, 1, 9, 11, 8 }
        };

        private readonly Color4[] faceColors = new Color4[]
        {
            new Color4(1f, 1f, 1f, 0.8f),
            new Color4(1f, 0f, 0f, 0.8f),
            new Color4(0f, 1f, 0f, 0.8f),
            new Color4(0f, 0f, 1f, 0.8f),
            new Color4(1f, 1f, 0f, 0.8f),
            new Color4(0f, 1f, 1f, 0.8f),
            new Color4(1f, 0f, 1f, 0.8f),
            new Color4(0.5f, 0.8f, 0.5f, 0.8f),
            new Color4(0.8f, 0.5f, 0.8f, 0.8f),
            new Color4(0f, 0f, 0f, 0.8f),
            new Color4(0.8f, 0.8f, 0.8f, 0.8f),
            new Color4(0.8f, 0.8f, 0.8f, 0.8f)
        };

        public void Draw()
        {
            GL.Enable(EnableCap.CullFace);

            GL.CullFace(CullFaceMode.Back);
            DrawFaces();

            GL.Disable(EnableCap.CullFace);
        }

        private void DrawFaces()
        {
            GL.Begin(PrimitiveType.Triangles);

            int i = 0;
            foreach (var face in faces)
            {
                GL.Color4(faceColors[i % faceColors.Length]);

                Vector3 v0 = vertices[face[0]];
                Vector3 v1 = vertices[face[1]];
                Vector3 v2 = vertices[face[2]];
                Vector3 v3 = vertices[face[3]];
                Vector3 v4 = vertices[face[4]];

                Vector3 normal = Vector3.Cross(v2 - v0, v1 - v0);
                normal.Normalize();

                GL.Normal3(normal);
                GL.Vertex3(v0);
                GL.Vertex3(v1);
                GL.Vertex3(v2);

                GL.Normal3(normal);
                GL.Vertex3(v0);
                GL.Vertex3(v2);
                GL.Vertex3(v3);

                GL.Normal3(normal);
                GL.Vertex3(v0);
                GL.Vertex3(v3);
                GL.Vertex3(v4);

                i++;
            }

            GL.End();
        }
    }
}