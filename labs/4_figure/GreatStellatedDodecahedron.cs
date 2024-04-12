using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace figure
{
    public class GreatStellatedDodecahedron : IShape
    {
        private static readonly double RATIO = (1 + Math.Sqrt(5)) / 2;
        private static readonly double RATIO_2 = RATIO * RATIO;
        private static readonly double RATIO_3 = RATIO * RATIO * RATIO;

        private static double[][] VERTICES =
        [
            [0, -RATIO, 1],
            [0, RATIO, 1],
            [0, RATIO, -1],
            [0, -RATIO, -1],

            [1, 0, RATIO],
            [-1, 0, RATIO],
            [-1, 0, -RATIO],
            [1, 0, -RATIO],

            [RATIO, 1, 0,],
            [-RATIO, 1, 0,],
            [-RATIO, -1, 0,],
            [RATIO, -1, 0,],

            [RATIO_2, RATIO_2, RATIO_2],
            [RATIO_2, -RATIO_2, RATIO_2],
            [-RATIO_2, -RATIO_2, RATIO_2],
            [-RATIO_2, RATIO_2, RATIO_2],

            [-RATIO_2, RATIO_2, -RATIO_2],
            [RATIO_2, RATIO_2, -RATIO_2],
            [RATIO_2, -RATIO_2, -RATIO_2],
            [-RATIO_2, -RATIO_2, -RATIO_2],

            [RATIO, -RATIO_3, 0],
            [-RATIO, -RATIO_3, 0],
            [-RATIO, RATIO_3, 0],
            [RATIO, RATIO_3, 0],

            [RATIO_3, 0, -RATIO],
            [RATIO_3, 0, RATIO],
            [-RATIO_3, 0, RATIO],
            [-RATIO_3, 0, -RATIO],

            [0, RATIO, -RATIO_3],
            [0, -RATIO, -RATIO_3],
            [0, -RATIO, RATIO_3],
            [0, RATIO, RATIO_3],
        ];
        private static readonly int[][] FACES =
        [
            [8, 7, 2],
            [11, 8, 4],
            [8, 1, 4],
            [1, 8, 2],
            [8, 11, 7],

            [10, 0, 5],
            [5, 9, 10],
            [10, 9, 6],
            [6, 3, 10],
            [10, 3, 0],

            [11, 3, 7],
            [3, 6, 7],
            [7, 6, 2],
            [2, 6, 9],
            [2, 9, 1],

            [1, 9, 5],
            [1, 5, 4],
            [0, 4, 5],
            [4, 0, 11],
            [3, 11, 0],

            [4, 1, 31],
            [5, 4, 31],
            [31, 1, 5],

            [5, 1, 15],
            [15, 9, 5],
            [15, 1, 9],

            [4, 5, 30],
            [30, 5, 0 ],
            [4, 30, 0],

            [1, 4, 12],
            [8, 12, 4],
            [8, 1, 12],

            [14, 0, 5],
            [10, 14, 5],
            [10, 0, 14],

            [13, 4, 0],
            [0, 11, 13],
            [4, 13, 11],

            [20, 11, 0],
            [3, 11, 20],
            [3, 20, 0],

            [10, 21, 0],
            [3, 0, 21],
            [21, 10, 3],

            [26, 10, 5],
            [26, 5, 9],
            [9, 10, 26],

            [27, 10, 9],
            [6, 10, 27],
            [6, 27, 9],

            [4, 25, 8],
            [25, 4, 11],
            [25, 11, 8],

            [8, 11, 24],
            [11, 7, 24],
            [24, 7, 8],

            [11, 3, 18],
            [7, 11, 18],
            [18, 3, 7],

            [19, 3, 10],
            [6, 3, 19],
            [10, 6, 19],

            [6, 16, 2],
            [9, 16, 6],
            [16, 9, 2],

            [2, 8, 17],
            [2, 17, 7],
            [7, 17,8],

            [1, 23, 2],
            [1, 8, 23],
            [23, 8, 2],

            [22, 1, 2],
            [1, 22, 9],
            [9, 22, 2],

            [7, 28, 2],
            [28, 6, 2],
            [28, 7, 6],

            [7, 29, 6],
            [29, 7, 3],
            [29, 3, 6],

        ];
        private static readonly Color4[] FACES_COLORS =
        [
            new Color4(255, 0, 0, 180),
            new Color4(0, 0, 255, 180),
        ];

        public void Draw()

        // Как opengl определяет лицевая грань видна сейчас, или нелицевая. И как описывать объекты, чтобы opengl корректно отрисовал их
        {
            //DrawVertices();
            DrawLines();

            GL.Enable(EnableCap.CullFace);

            // Отбраковка ближних граней
            GL.CullFace(CullFaceMode.Front);
            DrawFaces(FACES_COLORS);
            // Отбраковка дальних граней
            GL.CullFace(CullFaceMode.Back);
            DrawFaces(FACES_COLORS);

            GL.Disable(EnableCap.CullFace);
        }

        private void DrawVertices()
        {
            GL.PointSize(10);
            GL.Color4(0f, 0f, 0f, 1f);

            foreach (var vertex in VERTICES)
            {
                GL.Begin(PrimitiveType.Points);
                GL.Vertex3(vertex[0], vertex[1], vertex[2]);
                GL.End();
            }
        }

        private void DrawLines()
        {
            GL.LineWidth(2);
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

        private void DrawFaces(Color4[] colors)
        {
            for (int faceIndex = 0; faceIndex < FACES.Length; faceIndex++)
            {
                GL.Color4(colors[faceIndex % colors.Length]);

                int[] facePoints = FACES[faceIndex];

                var vertex1 = facePoints[0];
                var vertex2 = facePoints[1];
                var vertex3 = facePoints[2];

                var v0 = new Vector3((float)VERTICES[vertex1][0], (float)VERTICES[vertex1][1], (float)VERTICES[vertex1][2]);
                var v1 = new Vector3((float)VERTICES[vertex2][0], (float)VERTICES[vertex2][1], (float)VERTICES[vertex2][2]);
                var v2 = new Vector3((float)VERTICES[vertex3][0], (float)VERTICES[vertex3][1], (float)VERTICES[vertex3][2]);

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
