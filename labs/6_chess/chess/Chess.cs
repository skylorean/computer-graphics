using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace chess
{
    public class Chess
    {
        private readonly Model _rook;
        private readonly Model _knight;
        private readonly Model _bishop;
        private readonly Model _queen;
        private readonly Model _king;
        private readonly Model _pawn;
        private readonly Model _board;

        private readonly float CELL_SIZE = 80f;

        public Chess()
        {
            _board = new Model(); ;
            _board.LoadModel("models/ChessBoard.obj");

            _rook = new Model();
            _rook.LoadModel("models/Rook.obj");

            _knight = new Model();
            _knight.LoadModel("models/Knight.obj");

            _bishop = new Model();
            _bishop.LoadModel("models/Bishop.obj");

            _queen = new Model();
            _queen.LoadModel("models/Queen.obj");

            _king = new Model();
            _king.LoadModel("models/King.obj");

            _pawn = new Model();
            _pawn.LoadModel("models/Pawn.obj");
        }

        public void Draw()
        {
            DrawBoard();

            GL.PushMatrix();
            GL.Translate(0f, 20f, 0f);
            GL.Rotate(-90f, 1f, 0f, 0f);

            DrawWhiteRooks();
            DrawBlackRooks();

            DrawWhiteKhights();
            DrawBlackKhights();

            DrawWhiteBishops();
            DrawBlackBishops();

            DrawKings();
            DrawQueens();

            DrawBlackPawns();
            DrawWhitePawns();

            GL.PopMatrix();
        }

        private void DrawKings()
        {
            GL.PushMatrix();
            GL.Translate(CELL_SIZE / 2, -(CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.White);
            _king.RenderModel();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(CELL_SIZE / 2, (CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.Black);
            _king.RenderModel();
            GL.PopMatrix();
        }

        private void DrawQueens()
        {
            GL.PushMatrix();
            GL.Translate(-(CELL_SIZE / 2), -(CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.White);
            _queen.RenderModel();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-(CELL_SIZE / 2), (CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.Black);
            _queen.RenderModel();
            GL.PopMatrix();
        }

        private void DrawBoard()
        {
            GL.PushMatrix();
            GL.Scale(90f, 90f, 90f);
            GL.Color4(Color4.White);
            _board.RenderModel();
            GL.PopMatrix();
        }

        private void DrawWhiteRooks()
        {
            GL.PushMatrix();
            GL.Translate(CELL_SIZE * 3 + CELL_SIZE / 2, -(CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.White);
            _rook.RenderModel();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-(CELL_SIZE * 3 + CELL_SIZE / 2), -(CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.White);
            _rook.RenderModel();
            GL.PopMatrix();
        }

        private void DrawBlackRooks()
        {
            GL.PushMatrix();
            GL.Translate(CELL_SIZE * 3 + CELL_SIZE / 2, (CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.Black);
            _rook.RenderModel();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-(CELL_SIZE * 3 + CELL_SIZE / 2), (CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.Black);
            _rook.RenderModel();
            GL.PopMatrix();
        }

        private void DrawWhiteKhights()
        {
            GL.PushMatrix();
            GL.Translate(CELL_SIZE * 2 + CELL_SIZE / 2, -(CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Rotate(180f, 0f, 0f, 1f);
            GL.Color4(Color4.White);
            _knight.RenderModel();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-(CELL_SIZE * 2 + CELL_SIZE / 2), -(CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Rotate(180f, 0f, 0f, 1f);
            GL.Color4(Color4.White);
            _knight.RenderModel();
            GL.PopMatrix();
        }

        private void DrawBlackKhights()
        {
            GL.PushMatrix();
            GL.Translate(CELL_SIZE * 2 + CELL_SIZE / 2, (CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.Black);
            _knight.RenderModel();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-(CELL_SIZE * 2 + CELL_SIZE / 2), (CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.Black);
            _knight.RenderModel();
            GL.PopMatrix();
        }

        private void DrawWhiteBishops()
        {
            GL.PushMatrix();
            GL.Translate(CELL_SIZE + CELL_SIZE / 2, -(CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.White);
            _bishop.RenderModel();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-(CELL_SIZE + CELL_SIZE / 2), -(CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.White);
            _bishop.RenderModel();
            GL.PopMatrix();
        }

        private void DrawBlackBishops()
        {
            GL.PushMatrix();
            GL.Translate(CELL_SIZE + CELL_SIZE / 2, (CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.Black);
            _bishop.RenderModel();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-(CELL_SIZE + CELL_SIZE / 2), (CELL_SIZE * 3 + CELL_SIZE / 2), 0f);
            GL.Color4(Color4.Black);
            _bishop.RenderModel();
            GL.PopMatrix();
        }

        private void DrawBlackPawns()
        {

            for (int i = -4; i < 4; i++)
            {
                GL.PushMatrix();
                GL.Translate(i * CELL_SIZE + CELL_SIZE / 2, (CELL_SIZE * 2 + CELL_SIZE / 2), 0f);
                GL.Color4(Color4.Black);
                _pawn.RenderModel();
                GL.PopMatrix();
            }
        }

        private void DrawWhitePawns()
        {
            for (int i = -4; i < 4; i++)
            {
                GL.PushMatrix();
                GL.Translate(i * CELL_SIZE + CELL_SIZE / 2, -(CELL_SIZE * 2 + CELL_SIZE / 2), 0f);
                GL.Color4(Color4.White);
                _pawn.RenderModel();
                GL.PopMatrix();
            }
        }
    }
}
