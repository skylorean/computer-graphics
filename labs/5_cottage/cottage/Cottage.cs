using OpenTK.Graphics.OpenGL;

namespace cottage
{
    public class Cottage
    {
        private readonly Yard _yard = new();
        private readonly House _house = new();
        private readonly Garage _garage = new();

        static Texture _texture = new Texture();
        private int brickWallTexture = _texture.LoadTexture(
            "images/brick-wall.jpg",
            TextureMagFilter.LinearDetailSgis,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int doorTexture = _texture.LoadTexture(
            "images/door.jpg",
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int grassTexture = _texture.LoadTexture(
            "images/grass.jpg",
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int windowTexture = _texture.LoadTexture(
            "images/window.jpg",
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int steelTexture = _texture.LoadTexture(
            "images/steel.jpg",
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);

        public Cottage()
        {
            _yard.GrassTexture = grassTexture;

            _house.WallTexture = brickWallTexture;
            _house.DoorTexture = doorTexture;
            _house.RoofTexture = steelTexture;
            _house.WindowTexture = windowTexture;

            _garage.WallTexture = brickWallTexture;
            _garage.GarageDoorTexture = steelTexture;
            _garage.RoofTexture = steelTexture;
            _garage.WindowTexture = windowTexture;
        }

        public void Draw()
        {
            _yard.Draw();
            _house.Draw();
            _garage.Draw();
        }
    }
}
