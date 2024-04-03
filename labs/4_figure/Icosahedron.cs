using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace figure
{
    public class Icosahedron : IShape
    {
        private static readonly float RATIO = (float)((1.0f + Math.Sqrt(5.0f)) / 2.0f);
        private static readonly float[][] VERTICES =
        [
            [-1f, RATIO, 0f],
            [1f, RATIO, 0f],
            [-1f, -RATIO, 0f],
            [1f, -RATIO, 0f],

            [0f, -1f, RATIO],
            [0f, 1f, RATIO],
            [0f, -1f, -RATIO],
            [0f, 1f, -RATIO],

            [RATIO, 0f, -1f],
            [RATIO, 0f, 1f],
            [-RATIO, 0f, -1f],
            [-RATIO, 0f, 1f]
        ];
        private static readonly int[][] FACES =
        [
            [11, 10, 2],
            [3, 4, 2],
            [8, 6, 7],
            [7, 1, 8],
            [1, 5, 9],

            [0, 11, 5],
            [3, 8, 9],
            [9, 8, 1],
            [10, 7, 6],
            [0, 10, 11],

            [0, 1, 7],
            [3, 2, 6],
            [0, 7, 10],
            [3, 9, 4],
            [6, 2, 10],

            [3, 6, 8],
            [0, 5, 1],
            [5, 11, 4],
            [2, 4, 11],
            [4, 9, 5],
        ];
        private static readonly int[][] LINES =
        [
            [0, 11],
            [0, 5],
            [5, 11],
            [0, 1],
            [1, 5],
            [1, 7],
            [0, 7],
            [0, 10],
            [7, 10],
            [0, 11],
            [10, 11],
            [1, 9],
            [5, 9],
            [4, 5],
            [4, 11],
            [2, 11],
            [2, 10],
            [6, 10],
            [6, 7],
            [1, 8],
            [7, 8],
            [3, 4],
            [3, 9],
            [4, 9],
            [2, 3],
            [2, 4],
            [3, 6],
            [2, 6],
            [3, 8],
            [6, 8],
            [8, 9],
        ];
        private static readonly Color4[] FACE_COLORS =
        [
            Color4.AliceBlue,
            Color4.Coral,
            Color4.SkyBlue,
            Color4.PeachPuff,
            Color4.Lime,
        ];

        public void Draw()
        {
            //DrawLines();

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
            GL.Color4(0f, 0f, 0f, 1f);
            GL.LineWidth(3);

            GL.Begin(PrimitiveType.Lines);

            foreach (var line in LINES)
            {
                foreach (var vertexIndex in line)
                {
                    GL.Vertex3(VERTICES[vertexIndex]);
                }
            }

            GL.End();
        }

        private void DrawSides()
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 matrix);

            for (int i = 0; i < FACES.Length; i++)
            {
                var color = FACE_COLORS[i % FACE_COLORS.Length];
                color.A = 0.8f;
                GL.Color4(color);
                var side = FACES[i];

                var arV0 = VERTICES[side[0]];
                var arV1 = VERTICES[side[1]];
                var arV2 = VERTICES[side[2]];

                Vector3 v0 = new Vector3(arV0[0], arV0[1], arV0[2]);
                Vector3 v1 = new Vector3(arV1[0], arV1[1], arV1[2]);
                Vector3 v2 = new Vector3(arV2[0], arV2[1], arV2[2]);

                var normal = Vector3.Cross(v1 - v0, v2 - v0).Normalized();
                GL.Normal3(normal);

                foreach (var vertexIndex in side)
                {
                    GL.Vertex3(VERTICES[vertexIndex]);
                }
            }

            GL.End();
        }
    }
}