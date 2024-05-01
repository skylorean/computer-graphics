using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace cottage
{
    public class Texture
    {
        public int LoadTexture(
            string filepath,
            TextureMagFilter magFilter,
            TextureMinFilter minFilter,
            TextureWrapMode wrapS,
            TextureWrapMode wrapT)
        {
            Bitmap bmp = new(filepath);

            // Генерация и привязка id
            GL.GenTextures(1, out int textureId);
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            // Параметры фильтрации и режимы обёртывания
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)wrapS);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)wrapT);

            BitmapData bmp_data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Загрузка изображения в текстуру
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            return textureId;
        }
    }
}
