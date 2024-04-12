using OpenTK.Graphics.OpenGL;


namespace cottage
{
    internal class Cottage
    {
        public Cottage()
        {
            house.WallTexture = brickWallTexture;
            house.DoorTexture = doorTexture;
            house.RootTexture = rootTilesTexture;
            house.WindowTexture = windowTexture;
            house.AtticBoardsTexture = atticBoardsTexture;

            garage.WallTexture = brickWallTexture;
            garage.GarageDoorTexture = garageDoorTexture;
            garage.RootTexture = rootTilesTexture;
            garage.WindowTexture = windowTexture;
            garage.AtticBoardsTexture = atticBoardsTexture;

            yard.GrassTexture = grassTexture;
        }

        static Texture texture = new Texture();
        private int brickWallTexture = texture.LoadTexture(
            "images/brick-wall.jpg",
            TextureMagFilter.LinearDetailSgis,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int doorTexture = texture.LoadTexture(
            "images/door.jpg",
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int garageDoorTexture = texture.LoadTexture(
            "images/garage-door.jpg",
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int grassTexture = texture.LoadTexture(
            "images/grass.jpg",
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int windowTexture = texture.LoadTexture(
            "images/window.jpg",
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int rootTilesTexture = texture.LoadTexture(
            "images/root-tiles.jpg",
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int atticBoardsTexture = texture.LoadTexture(
            "images/brick-wall.jpg",
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);

        House house = new();
        Garage garage = new();
        Yard yard = new();

        public bool ShowFog = false;

        public void Draw()
        {
            if (ShowFog)
            {
                GL.Enable(EnableCap.Fog);
            }
            else
            {
                GL.Disable(EnableCap.Fog);
            }

            // Задаем режим тумана
            GL.Fog(FogParameter.FogMode, (int)FogMode.Exp2);

            // Задаем цвет тумана
            GL.Fog(FogParameter.FogColor, new float[] { 0.5f, 0.5f, 0.5f, 0f });

            // и его плотность
            GL.Fog(FogParameter.FogDensity, 0.015f);

            house.Draw();
            garage.Draw();
            yard.Draw();
            GL.Disable(EnableCap.Fog);

            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Light1);

            GL.Disable(EnableCap.Light0);
            GL.Disable(EnableCap.Light1);
        }
    }
}
