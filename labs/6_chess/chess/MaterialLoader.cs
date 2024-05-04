using Assimp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;
using System.Drawing.Imaging;
using TextureWrapMode = OpenTK.Graphics.OpenGL.TextureWrapMode;

namespace chess
{
    public class MaterialLoader
    {
        // Айдишники текстур
        private int[] textureIds;

        private bool textureLoaded = false;

        public void LoadMaterialTextures(Material material)
        {
            textureIds = new int[material.GetMaterialTextureCount(TextureType.Diffuse)];
            textureLoaded = true;

            // Перебор всех текстур типа Diffuse
            for (int i = 0; i < material.GetMaterialTextureCount(TextureType.Diffuse); i++)
            {
                TextureSlot textureSlot;

                // Достаю информацию о текстуре и кладу в textureSlot
                if (!material.GetMaterialTexture(TextureType.Diffuse, i, out textureSlot))
                {
                    continue;
                }

                if (!File.Exists(textureSlot.FilePath))
                {
                    continue;
                }

                // Привязка идентификатора к контексту
                int textureId = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, textureId);

                // Чтение и загрузка изображения
                using (Bitmap bitmap = new Bitmap(textureSlot.FilePath))
                {
                    // Блокирую данные изображения в памяти для чтения
                    BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    // Загрузка в текстуру
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                    // Освобождаю данные изображения
                    bitmap.UnlockBits(data);
                }

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                textureIds[i] = textureId;
            }
        }

        // Применение assimp материалов к объектам OpenGL
        public void ApplyMaterial(Material material, int index)
        {
            Color4 ambientColor = Color4DToColor4(material.ColorAmbient);
            Color4 diffuseColor = Color4DToColor4(material.ColorDiffuse);
            Color4 specularColor = Color4DToColor4(material.ColorSpecular);
            float shininess = material.Shininess;

            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, ambientColor);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, diffuseColor);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, specularColor);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, shininess);

            if (textureLoaded && material.GetMaterialTexture(TextureType.Diffuse, index, out TextureSlot textureSlot))
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, textureIds[index]);
            }
        }

        private Color4 Color4DToColor4(Color4D color4d)
        {
            Color4 color4 = new()
            {
                A = color4d.A,
                R = color4d.R,
                G = color4d.G,
                B = color4d.B
            };
            return color4;
        }
    }
}