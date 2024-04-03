using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace figure
{
    public class Dodecahedron : IShape
    {
        private static readonly double RATIO = 2 / (1 + Math.Sqrt(5));
        private static readonly double[][] VERTICES =
        [
            [1, 1, 1], // 0
            [1, -1, 1], // 1

            [-1, -1, 1],//2
            [-1, 1, 1],// 4 
            [-1, 1, -1],// 3

            [1, 1, -1], // 5
            [1, -1, -1],// 6
            [-1, -1, -1],// 7

            [0, +(1 - RATIO * RATIO), +(1 + RATIO)],// 8
            [0, +(1 - RATIO * RATIO), -(1 + RATIO)],// 9
            [0, -(1 - RATIO * RATIO), +(1 + RATIO)],// 10
            [0, -(1 - RATIO * RATIO), -(1 + RATIO)],// 11

            [+(1 - RATIO * RATIO), +(1 + RATIO), 0],// 12
            [+(1 - RATIO * RATIO), -(1 + RATIO), 0],// 13
            [-(1 - RATIO * RATIO), +(1 + RATIO), 0],// 14
            [-(1 - RATIO * RATIO), -(1 + RATIO), 0],// 15

            [+(1 + RATIO), 0, +(1 - RATIO * RATIO)],// 16
            [+(1 + RATIO), 0, -(1 - RATIO * RATIO)],// 17
            [-(1 + RATIO), 0, +(1 - RATIO * RATIO)],// 18
            [-(1 + RATIO), 0, -(1 - RATIO * RATIO)],// 19
        ];
        private static readonly int[][] FACES =
        [
            [8, 10, 1, 16, 0],
            [12, 14, 3, 8, 0],
            [16, 17, 5, 12, 0],
            [17, 6, 11, 9, 5],
            [9, 4, 14, 12, 5],
            [10, 2, 15, 13, 1],
            [13, 6, 17, 16, 1],
            [13, 15, 7, 11, 6],
            [18, 2, 10, 8, 3],
            [14, 4, 19, 18, 3],
            [9, 11, 7, 19, 4],
            [2, 18, 19, 7, 15]
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
