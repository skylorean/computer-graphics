using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace cottage
{
    public class House
    {
        public int WallTexture { get; set; }
        public int DoorTexture { get; set; }
        public int RoofTexture { get; set; }
        public int WindowTexture { get; set; }

        // Main House
        public Box2 MainWallTextureCoords { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 MainDoorTextureCoords { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 MainRoofTextureCoords { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 MainWindowTextureCoords { get; set; } = new(0f, 0f, 1f, 1f);

        public Box3 MainWallCoords { get; set; } = new(-20f, -10f, -15f, 0f, 10f, 15f);
        public Box3 MainDoorCoords { get; set; } = new(-12f, -10f, 15.1f, -8f, -3f, 15.1f);
        public Box3 MainRoofCoords { get; set; } = new(-22f, 8f, -17f, 2f, 20f, 17f);
        public Box3[] MainWindowsCoords { get; set; } =
        {
            new Box3(-16f, 2f, 15.1f, -12f, 9f, 15.1f),
            new Box3(-8f, 2f, 15.1f, -4f, 9f, 15.1f),
            new Box3(-16f, 2f, -15.1f, -12f, 9f, -15.1f),
            new Box3(-8f, 2f, -15.1f, -4f, 9f, -15.1f),
        };

        // Extra house
        public Box2 ExtraWallTextureCoords { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 ExtraDoorTextureCoords { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 ExtraRoofTextureCoords { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 ExtraWindowTextureCoords { get; set; } = new(0f, 0f, 1f, 1f);

        public Box3 ExtraWallCoords { get; set; } = new(-30f, -10f, -5f, -20f, 5f, 15f);
        public Box3 ExtraDoorCoords { get; set; } = new(-25f, -10f, 15.1f, -21f, -3f, 15.1f);
        public Box3 ExtraRoofCoords { get; set; } = new(-30f, 5f, -6f, -10f, 8f, 16f);
        public Box3[] ExtraWindowsCoords { get; set; } =
        {
            new Box3(-30.1f, -3f, 8f, -30.1f, 3f, 12f),
            new Box3(-30.1f, -3f, -2f, -30.1f, 3f, 2f),
        };

        public void Draw()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            DrawMainHouse();
            DrawExtraHouse();
        }

        private void DrawMainHouse()
        {
            DrawHouseBox(MainWallCoords, MainWallTextureCoords);
            DrawHouseRoof(MainRoofCoords, MainRoofTextureCoords, MainWallCoords, MainWallTextureCoords);
            DrawHouseWindows(MainWindowsCoords, MainWindowTextureCoords, MainWallCoords);
            DrawHouseDoor(MainDoorCoords, MainDoorTextureCoords);
        }

        private void DrawExtraHouse()
        {
            DrawHouseBox(ExtraWallCoords, ExtraWallTextureCoords);
            DrawHouseRoof(ExtraRoofCoords, ExtraRoofTextureCoords, ExtraWallCoords, ExtraWallTextureCoords);
            DrawHouseWindows(ExtraWindowsCoords, ExtraWindowTextureCoords, ExtraWallCoords);
            DrawHouseDoor(ExtraDoorCoords, ExtraDoorTextureCoords);
        }

        private void DrawHouseBox(Box3 wallCoords, Box2 wallTextureCoords)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, WallTexture);

            GL.ActiveTexture(TextureUnit.Texture1);
            GL.Enable(EnableCap.Texture2D);
            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)All.Decal);

            GL.Begin(PrimitiveType.Quads);

            // Back Side Wall
            GL.Normal3(0f, 0f, -1f);
            GL.MultiTexCoord2(TextureUnit.Texture0, wallTextureCoords.Min.X, wallTextureCoords.Min.Y);
            GL.Vertex3(wallCoords.Min.X, wallCoords.Max.Y, wallCoords.Min.Z);
            GL.MultiTexCoord2(TextureUnit.Texture0, wallTextureCoords.Min.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Min.X, wallCoords.Min.Y, wallCoords.Min.Z);
            GL.MultiTexCoord2(TextureUnit.Texture0, wallTextureCoords.Max.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Max.X, wallCoords.Min.Y, wallCoords.Min.Z);
            GL.MultiTexCoord2(TextureUnit.Texture0, wallTextureCoords.Max.X, wallTextureCoords.Min.Y);
            GL.Vertex3(wallCoords.Max.X, wallCoords.Max.Y, wallCoords.Min.Z);

            // Left Side Wall
            GL.Normal3(-1f, 0f, 0f);
            GL.MultiTexCoord2(TextureUnit.Texture0, wallTextureCoords.Max.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Min.X, wallCoords.Min.Y, wallCoords.Min.Z);
            GL.MultiTexCoord2(TextureUnit.Texture0, wallTextureCoords.Max.X, wallTextureCoords.Min.Y);
            GL.Vertex3(wallCoords.Min.X, wallCoords.Max.Y, wallCoords.Min.Z);
            GL.MultiTexCoord2(TextureUnit.Texture0, wallTextureCoords.Min.X, wallTextureCoords.Min.Y);
            GL.Vertex3(wallCoords.Min.X, wallCoords.Max.Y, wallCoords.Max.Z);
            GL.MultiTexCoord2(TextureUnit.Texture0, wallTextureCoords.Min.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Min.X, wallCoords.Min.Y, wallCoords.Max.Z);

            // Front Side Wall
            GL.Normal3(0f, 0f, 1f);
            GL.TexCoord2(wallTextureCoords.Min.X, wallTextureCoords.Min.Y);
            GL.Vertex3(wallCoords.Min.X, wallCoords.Max.Y, wallCoords.Max.Z);
            GL.TexCoord2(wallTextureCoords.Min.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Min.X, wallCoords.Min.Y, wallCoords.Max.Z);
            GL.TexCoord2(wallTextureCoords.Max.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Max.X, wallCoords.Min.Y, wallCoords.Max.Z);
            GL.TexCoord2(wallTextureCoords.Max.X, wallTextureCoords.Min.Y);
            GL.Vertex3(wallCoords.Max.X, wallCoords.Max.Y, wallCoords.Max.Z);

            // Right Side Wall
            GL.Normal3(1f, 0f, 0f);
            GL.TexCoord2(wallTextureCoords.Max.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Max.X, wallCoords.Min.Y, wallCoords.Max.Z);
            GL.TexCoord2(wallTextureCoords.Max.X, wallTextureCoords.Min.Y);
            GL.Vertex3(wallCoords.Max.X, wallCoords.Max.Y, wallCoords.Max.Z);
            GL.TexCoord2(wallTextureCoords.Min.X, wallTextureCoords.Min.Y);
            GL.Vertex3(wallCoords.Max.X, wallCoords.Max.Y, wallCoords.Min.Z);
            GL.TexCoord2(wallTextureCoords.Min.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Max.X, wallCoords.Min.Y, wallCoords.Min.Z);

            GL.End();
        }

        private void DrawHouseRoof(Box3 roofCoords, Box2 roofTextureCoords, Box3 wallCoords, Box2 wallTextureCoords)
        {
            GL.ActiveTexture(TextureUnit.Texture0);

            // Roof
            GL.BindTexture(TextureTarget.Texture2D, RoofTexture);
            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(1f, 1f, 0f);
            GL.TexCoord2(roofTextureCoords.Max.X, roofTextureCoords.Max.Y);
            GL.Vertex3(roofCoords.Max.X, roofCoords.Min.Y, roofCoords.Max.Z);
            GL.TexCoord2(roofTextureCoords.Min.X, roofTextureCoords.Max.Y);
            GL.Vertex3(roofCoords.Max.X, roofCoords.Min.Y, roofCoords.Min.Z);
            GL.TexCoord2(roofTextureCoords.Min.X, roofTextureCoords.Min.Y);
            GL.Vertex3(roofCoords.Center.X, roofCoords.Max.Y, roofCoords.Min.Z);
            GL.TexCoord2(roofTextureCoords.Max.X, roofTextureCoords.Min.Y);
            GL.Vertex3(roofCoords.Center.X, roofCoords.Max.Y, roofCoords.Max.Z);

            GL.Normal3(-1f, 1f, 0f);
            GL.TexCoord2(roofTextureCoords.Max.X, roofTextureCoords.Max.Y);
            GL.Vertex3(roofCoords.Min.X, roofCoords.Min.Y, roofCoords.Max.Z);
            GL.TexCoord2(roofTextureCoords.Max.X, roofTextureCoords.Min.Y);
            GL.Vertex3(roofCoords.Center.X, roofCoords.Max.Y, roofCoords.Max.Z);
            GL.TexCoord2(roofTextureCoords.Min.X, roofTextureCoords.Min.Y);
            GL.Vertex3(roofCoords.Center.X, roofCoords.Max.Y, roofCoords.Min.Z);
            GL.TexCoord2(roofTextureCoords.Min.X, roofTextureCoords.Max.Y);
            GL.Vertex3(roofCoords.Min.X, roofCoords.Min.Y, roofCoords.Min.Z);

            GL.End();

            // Wall-Roof
            GL.Begin(PrimitiveType.Triangles);

            GL.Normal3(0f, 0f, 1f);
            GL.TexCoord2(wallTextureCoords.Min.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Min.X, wallCoords.Max.Y, wallCoords.Max.Z);
            GL.TexCoord2(wallTextureCoords.Max.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Max.X, wallCoords.Max.Y, wallCoords.Max.Z);
            GL.TexCoord2(
                wallTextureCoords.Center.X,
                0);
            GL.Vertex3(roofCoords.Center.X, roofCoords.Max.Y, wallCoords.Max.Z);

            GL.Normal3(0f, 0f, -1f);
            GL.TexCoord2(wallTextureCoords.Min.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Min.X, wallCoords.Max.Y, wallCoords.Min.Z);
            GL.TexCoord2(wallTextureCoords.Max.X, wallTextureCoords.Max.Y);
            GL.Vertex3(wallCoords.Max.X, wallCoords.Max.Y, wallCoords.Min.Z);
            GL.TexCoord2(
                wallTextureCoords.Center.X,
                0);
            GL.Vertex3(roofCoords.Center.X, roofCoords.Max.Y, wallCoords.Min.Z);
            GL.End();
        }

        private void DrawHouseWindows(Box3[] windowCoords, Box2 windowTextureCoords, Box3 wallCoords)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, WindowTexture);

            GL.Begin(PrimitiveType.Quads);

            foreach (var window in windowCoords)
            {
                SetNormalForWindow(window, wallCoords);
                GL.TexCoord2(windowTextureCoords.Max.X, windowTextureCoords.Max.Y);
                GL.Vertex3(window.Min.X, window.Min.Y, window.Min.Z);

                GL.TexCoord2(windowTextureCoords.Max.X, windowTextureCoords.Min.Y);
                GL.Vertex3(window.Min.X, window.Max.Y, window.Min.Z);

                GL.TexCoord2(windowTextureCoords.Min.X, windowTextureCoords.Min.Y);
                GL.Vertex3(window.Max.X, window.Max.Y, window.Max.Z);

                GL.TexCoord2(windowTextureCoords.Min.X, windowTextureCoords.Max.Y);
                GL.Vertex3(window.Max.X, window.Min.Y, window.Max.Z);
            }

            GL.End();
        }

        private void SetNormalForWindow(Box3 window, Box3 wallCoords)
        {
            if (window.Max.Z >= wallCoords.Max.Z)
            {
                GL.Normal3(0f, 0f, 1f);
            }
            else if (window.Min.Z <= wallCoords.Min.Z)
            {
                GL.Normal3(0f, 0f, -1f);
            }
            else if (window.Max.X >= wallCoords.Max.X)
            {
                GL.Normal3(1f, 0f, 0f);
            }
            else if (window.Min.X <= wallCoords.Min.X)
            {
                GL.Normal3(-1f, 0f, 0f);
            }
            else
            {
                GL.Normal3(1f, 0f, 0f);
            }
        }

        private void DrawHouseDoor(Box3 doorCoords, Box2 doorTextureCoords)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, DoorTexture);

            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 0f, 1f);
            GL.TexCoord2(doorTextureCoords.Max.X, doorTextureCoords.Max.Y);
            GL.Vertex3(doorCoords.Min.X, doorCoords.Min.Y, doorCoords.Max.Z);
            GL.TexCoord2(doorTextureCoords.Max.X, doorTextureCoords.Min.Y);
            GL.Vertex3(doorCoords.Min.X, doorCoords.Max.Y, doorCoords.Max.Z);
            GL.TexCoord2(doorTextureCoords.Min.X, doorTextureCoords.Min.Y);
            GL.Vertex3(doorCoords.Max.X, doorCoords.Max.Y, doorCoords.Max.Z);
            GL.TexCoord2(doorTextureCoords.Min.X, doorTextureCoords.Max.Y);
            GL.Vertex3(doorCoords.Max.X, doorCoords.Min.Y, doorCoords.Max.Z);

            GL.End();
        }
    }
}
