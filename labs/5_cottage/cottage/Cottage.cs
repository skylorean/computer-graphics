using OpenTK.Graphics.OpenGL;

namespace cottage
{
    public class Cottage
    {
        private readonly Yard _yard = new();
        private readonly House _house = new();
        private readonly Garage _garage = new();

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

        public Cottage()
        {
            _yard.GrassTexture = grassTexture;

            _house.WallTexture = brickWallTexture;
            _house.DoorTexture = doorTexture;
            _house.RoofTexture = rootTilesTexture;
            _house.WindowTexture = windowTexture;

            _garage.WallTexture = brickWallTexture;
            _garage.GarageDoorTexture = garageDoorTexture;
            _garage.RoofTexture = rootTilesTexture;
            _garage.WindowTexture = windowTexture;
        }

        public void Draw()
        {
            _yard.Draw();
            _house.Draw();
            _garage.Draw();

            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Light1);

            GL.Disable(EnableCap.Light0);
            GL.Disable(EnableCap.Light1);
        }
    }
}
