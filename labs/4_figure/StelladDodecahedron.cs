using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace figure
{
    public class StelladDodecahedron : IShape
    {
        private static readonly double RATIO = (1 + Math.Sqrt(5)) / 2;
        private static readonly double RATIO_2 = RATIO * RATIO;
        private static readonly double RATIO_3 = RATIO * RATIO * RATIO;

        private static readonly double[][] VERTICES =
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
        private static readonly int[][] ICOSAHEDRON_FACES =
        [
            [8, 7, 2, 8],
            [11, 8, 4, 11],
            [8, 1, 4, 8],
            [1, 8, 2, 1],
            [8, 11, 7, 8],

            [10, 0, 5, 10],
            [5, 9, 10, 5],
            [10, 9, 6, 10],
            [6, 3, 10, 6],
            [10, 3, 0, 10],

            [11, 3, 7, 11],
            [3, 6, 7, 3],
            [7, 6, 2, 7],
            [2, 6, 9, 2],
            [2, 9, 1, 2],

            [1, 9, 5, 1],
            [1, 5, 4, 1],
            [0, 4, 5, 0],
            [4, 0, 11, 4],
            [3, 11, 0, 3],
        ];
        private static readonly int[][] STELLA_FACES =
        [
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
        private static readonly Color4[] ICOSAHENDRON_FACES_COLORS =
        [
            //Color4.Orange,
            //Color4.Magenta,
            //Color4.Cyan,
            //Color4.LightGreen,
            //Color4.DarkRed
            Color4.Red
        ];
        private static readonly Color4[] STELLA_FACES_COLORS =
        [
            //Color4.Coral,
            //Color4.BlueViolet,
            //Color4.Red,
            Color4.Blue
        ];
        private static readonly float FACE_COLOR_APLHA = 0.8f;

        public void Draw()
        {
            DrawVertices(VERTICES);
            DrawLines(VERTICES, ICOSAHEDRON_FACES);
            DrawLines(VERTICES, STELLA_FACES);

            GL.Enable(EnableCap.CullFace);

            // Отбраковка ближних граней
            GL.CullFace(CullFaceMode.Front);
            DrawFaces(VERTICES, ICOSAHEDRON_FACES, GetModifiedAplhaColors(ICOSAHENDRON_FACES_COLORS, FACE_COLOR_APLHA));
            DrawFaces(VERTICES, STELLA_FACES, GetModifiedAplhaColors(STELLA_FACES_COLORS, FACE_COLOR_APLHA));

            // Отбраковка дальних граней
            GL.CullFace(CullFaceMode.Back);
            DrawFaces(VERTICES, ICOSAHEDRON_FACES, GetModifiedAplhaColors(ICOSAHENDRON_FACES_COLORS, FACE_COLOR_APLHA));
            DrawFaces(VERTICES, STELLA_FACES, GetModifiedAplhaColors(STELLA_FACES_COLORS, FACE_COLOR_APLHA));

            GL.Disable(EnableCap.CullFace);
        }

        private Color4[] GetModifiedAplhaColors(Color4[] colors, float newAplha)
        {
            return colors.Select(c => { c.A = newAplha; return c; }).ToArray();
        }

        private void DrawVertices(double[][] vertices)
        {
            GL.PointSize(10);
            GL.Color4(0f, 0f, 0f, 1f);

            foreach (var vertex in vertices)
            {
                GL.Begin(PrimitiveType.Points);
                GL.Vertex3(vertex[0], vertex[1], vertex[2]);
                GL.End();
            }
        }

        private void DrawLines(double[][] vertices, int[][] faces)
        {
            GL.LineWidth(2);
            GL.Color4(0f, 0f, 0f, 1f);

            foreach (var facePoints in faces)
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

        private void DrawFaces(double[][] vertices, int[][] faces, Color4[] colors)
        {
            for (int faceIndex = 0; faceIndex < faces.Length; faceIndex++)
            {
                GL.Color4(colors[faceIndex % colors.Length]);

                int[] facePoints = faces[faceIndex];

                var v0 = new Vector3((float)vertices[facePoints[0]][0], (float)vertices[facePoints[0]][1], (float)vertices[facePoints[0]][2]);
                var v1 = new Vector3((float)vertices[facePoints[1]][0], (float)vertices[facePoints[1]][1], (float)vertices[facePoints[1]][2]);
                var v2 = new Vector3((float)vertices[facePoints[2]][0], (float)vertices[facePoints[2]][1], (float)vertices[facePoints[2]][2]);

                var normal = Vector3.Cross(v1 - v0, v2 - v0).Normalized();
                GL.Normal3(normal);

                GL.Begin(PrimitiveType.TriangleFan);
                foreach (var vertexIndex in facePoints)
                {
                    var vertex = vertices[vertexIndex];
                    GL.Vertex3(vertex[0], vertex[1], vertex[2]);
                }
                GL.End();
            }
        }
    }
}
