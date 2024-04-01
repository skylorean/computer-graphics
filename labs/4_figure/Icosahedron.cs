using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace figure
{
    public class Icosahedron : IShape
    {
        private static readonly float _t = (float)((1.0f + Math.Sqrt(5.0f)) / 2.0f);

        private readonly float[][] _vertices =
        [
            [-1f, _t, 0f],
            [1f, _t, 0f],
            [-1f, -_t, 0f],
            [1f, -_t, 0f],
            [0f, -1f, _t],
            [0f, 1f, _t],
            [0f, -1f, -_t],
            [0f, 1f, -_t],
            [_t, 0f, -1f],
            [_t, 0f, 1f],
            [-_t, 0f, -1f],
            [-_t, 0f, 1f]
        ];

        private readonly int[][] _sides =
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

        private readonly int[][] _lines =
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


        private readonly Color4[] _sidesColors =
        [
            Color4.AliceBlue,
            Color4.Coral,
            Color4.SkyBlue,
            Color4.PeachPuff,
            Color4.Lime,
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
            GL.Color4(0f, 0f, 0f, 1f);

            GL.Begin(PrimitiveType.Lines);

            foreach (var line in _lines)
            {
                foreach (var vertexIndex in line)
                {
                    GL.Vertex3(_vertices[vertexIndex]);
                }
            }

            GL.End();
        }

        // Colors
        private void DrawSides()
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 matrix);

            for (int i = 0; i < _sides.Length; i++)
            {
                var color = _sidesColors[i % _sidesColors.Length];
                color.A = 0.8f;
                GL.Color4(color);
                var side = _sides[i];

                var arV0 = _vertices[side[0]];
                var arV1 = _vertices[side[1]];
                var arV2 = _vertices[side[2]];

                Vector3 v0 = new Vector3(arV0[0], arV0[1], arV0[2]);
                Vector3 v1 = new Vector3(arV1[0], arV1[1], arV1[2]);
                Vector3 v2 = new Vector3(arV2[0], arV2[1], arV2[2]);

                var normal = Vector3.Cross(
                    v1 - v0,
                    v2 - v0);

                normal.Normalize();
                GL.Normal3(normal);

                foreach (var vertexIndex in side)
                {
                    GL.Vertex3(_vertices[vertexIndex]);
                }
            }

            GL.End();
        }
    }
}