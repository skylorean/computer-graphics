using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace figure
{
    public class Dodecahedron : IShape
    {
        private static readonly float _phi = (1f + (float)System.Math.Sqrt(5f)) / 2f;
        private static readonly float _invPhi = 1f / _phi;

        private readonly float[][] _vertices =
        {
            new float[] { 1, 1, 1 },
            new float[] { 1, 1, -1 },
            new float[] { 1, -1, 1 },
            new float[] { 1, -1, -1 },
            new float[] { -1, 1, 1 },
            new float[] { -1, 1, -1 },
            new float[] { -1, -1, 1 },
            new float[] { -1, -1, -1 },
            new float[] { 0, _phi, _invPhi },
            new float[] { 0, _phi, -_invPhi },
            new float[] { 0, -_phi, _invPhi },
            new float[] { 0, -_phi, -_invPhi },
            new float[] { _phi, _invPhi, 0 },
            new float[] { _phi, -_invPhi, 0 },
            new float[] { -_phi, _invPhi, 0 },
            new float[] { -_phi, -_invPhi, 0 },
            new float[] { _invPhi, 0, _phi },
            new float[] { -_invPhi, 0, _phi },
            new float[] { _invPhi, 0, -_phi },
            new float[] { -_invPhi, 0, -_phi }
        };

        private readonly int[][] _sides =
        {
            new int[] { 0, 8, 12, 14, 4 },
            new int[] { 0, 4, 16, 2, 10 },
            new int[] { 0, 10, 6, 18, 8 },
            new int[] { 8, 18, 12, 9, 1 },
            new int[] { 12, 18, 6, 17, 14 },
            new int[] { 1, 9, 11, 3, 16 },
            new int[] { 3, 11, 13, 7, 19 },
            new int[] { 7, 13, 5, 15, 2 },
            new int[] { 2, 15, 17, 6, 10 },
            new int[] { 3, 19, 7 },
            new int[] { 1, 16, 4 },
            new int[] { 5, 13, 11 },
            new int[] { 9, 12, 14 },
            new int[] { 8, 1, 0 },
            new int[] { 14, 17, 15 },
            new int[] { 7, 2, 19 },
            new int[] { 3, 19, 11 },
            new int[] { 16, 3, 1 },
            new int[] { 4, 0, 5 },
            new int[] { 5, 11, 9 }
        };

        private readonly Color4[] _sidesColors =
        {
            Color4.AliceBlue,
            Color4.Coral,
            Color4.SkyBlue,
            Color4.PeachPuff,
            Color4.Lime,
            Color4.Yellow,
            Color4.Orange,
            Color4.Magenta,
            Color4.Cyan,
            Color4.LightGreen
        };

        public void Draw()
        {
            DrawSides();
        }

        private void DrawSides()
        {
            GL.Begin(PrimitiveType.Triangles);

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

                var normal = Vector3.Cross(v1 - v0, v2 - v0);
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
