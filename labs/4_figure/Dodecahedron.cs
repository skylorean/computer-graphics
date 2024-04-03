using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace figure
{
    public class Dodecahedron : IShape
    {
        private static readonly double RATIO = 2 / (1 + Math.Sqrt(5));
        private static readonly double[][] VERTICES =
        [
            [1, 1, 1],
            [+1, +1, -1],
            [+1, -1, +1],
            [+1, -1, -1],
            [-1, +1, +1],
            [-1, +1, -1],
            [-1, -1, +1],
            [-1, -1, -1],
            [0, +(1 - RATIO * RATIO), +(1 + RATIO)],
            [0, +(1 - RATIO * RATIO), -(1 + RATIO)],
            [0, -(1 - RATIO * RATIO), +(1 + RATIO)],
            [0, -(1 - RATIO * RATIO), -(1 + RATIO)],
            [+(1 - RATIO * RATIO), +(1 + RATIO), 0],
            [+(1 - RATIO * RATIO), -(1 + RATIO), 0],
            [-(1 - RATIO * RATIO), +(1 + RATIO), 0],
            [-(1 - RATIO * RATIO), -(1 + RATIO), 0],
            [+(1 + RATIO), 0, +(1 - RATIO * RATIO)],
            [+(1 + RATIO), 0, -(1 - RATIO * RATIO)],
            [-(1 + RATIO), 0, +(1 - RATIO * RATIO)],
            [-(1 + RATIO), 0, -(1 - RATIO * RATIO)],
        ];
        private static readonly int[][] FACES =
        [
            [8, 10, 2, 16, 0],
            [12, 14, 4, 8, 0],
            [16, 17, 1, 12, 0],
            [17, 3, 11, 9, 1],
            [9, 5, 14, 12, 1],
            [10, 6, 15, 13, 2],
            [13, 3, 17, 16, 2],
            [13, 15, 7, 11, 3],
            [18, 6, 10, 8, 4],
            [14, 5, 19, 18, 4],
            [9, 11, 7, 19, 5],
            [6, 18, 19, 7, 15]
        ];
        private static readonly Color4[] FACES_COLORS =
        [
            Color4.AliceBlue,
            Color4.Coral,
            Color4.SkyBlue,
            Color4.DarkRed,
            Color4.Lime,
            Color4.Yellow,
            Color4.Orange,
            Color4.Magenta,
            Color4.Cyan,
            Color4.LightGreen
        ];

        public void Draw()
        {
            DrawLines();

            GL.Enable(EnableCap.CullFace);

            // Отбраковка ближних граней
            GL.CullFace(CullFaceMode.Front);
            DrawSides();

            // Отбраковка дальних граней
            GL.CullFace(CullFaceMode.Back);
            DrawSides();

            GL.Disable(EnableCap.CullFace);
        }

        private void DrawLines()
        {
            GL.LineWidth(3);
            GL.Color4(0f, 0f, 0f, 1f);

            foreach (var facePoints in FACES)
            {
                GL.Begin(PrimitiveType.LineStrip);
                foreach (var vertexIndex in facePoints)
                {
                    var vertex = VERTICES[vertexIndex];
                    GL.Vertex3(vertex[0], vertex[1], vertex[2]);
                }
                GL.End();
            }
        }

        private void DrawSides()
        {
            for (int face = 0; face < FACES.Length; ++face)
            {
                var color = FACES_COLORS[face % FACES_COLORS.Length];
                color.A = 0.8f;
                GL.Color4(color);

                int[] facePoints = FACES[face];

                var v0 = new Vector3((float)VERTICES[facePoints[0]][0], (float)VERTICES[facePoints[0]][1], (float)VERTICES[facePoints[0]][2]);
                var v1 = new Vector3((float)VERTICES[facePoints[1]][0], (float)VERTICES[facePoints[1]][1], (float)VERTICES[facePoints[1]][2]);
                var v2 = new Vector3((float)VERTICES[facePoints[2]][0], (float)VERTICES[facePoints[2]][1], (float)VERTICES[facePoints[2]][2]);

                var normal = Vector3.Cross(v1 - v0, v2 - v0).Normalized();
                GL.Normal3(normal);

                GL.Begin(PrimitiveType.TriangleFan);
                foreach (var vertexIndex in facePoints)
                {
                    var vertex = VERTICES[vertexIndex];
                    GL.Vertex3(vertex[0], vertex[1], vertex[2]);
                }
                GL.End();
            }
        }
    }
}
