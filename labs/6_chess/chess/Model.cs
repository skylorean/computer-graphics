using Assimp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace chess
{
    public class Model
    {
        private Scene scene = new();
        // Механизм для предварительной компиляции набора комманд OpenGL.
        private int[] displayLists = [];
        private MaterialLoader materialLoader = new();

        public void LoadModel(string filePath)
        {
            // Импорт файла модели
            AssimpContext importer = new AssimpContext();
            scene = importer.ImportFile(
                filePath,
                PostProcessSteps.FlipUVs);
            importer.Dispose();

            LoadTextures();
            CreateDisplayLists();
        }

        // Генерация дисплейных списков. Эффективный рендер сложных объектов.
        private void CreateDisplayLists()
        {
            displayLists = new int[scene.MeshCount];

            for (int i = 0; i < scene.MeshCount; i++)
            {
                // Mesh - сетка (коллекция вершин, рёбер и граней)
                Mesh mesh = scene.Meshes[i];

                // Создание дисплейного списка для каждого меша
                displayLists[i] = GL.GenLists(1);

                // Компиляция дисплейного списка
                GL.NewList(displayLists[i], ListMode.Compile);

                materialLoader.ApplyMaterial(scene.Materials[mesh.MaterialIndex], i);
                Vector3[] vertices = AssimpVectorToOpenTKVector([.. mesh.Vertices]);
                Vector3[] normals = AssimpVectorToOpenTKVector([.. mesh.Normals]);
                Vector3[] textureCoordinates = AssimpVectorToOpenTKVector([.. mesh.TextureCoordinateChannels[0]]);

                GL.Begin(mesh.Faces[0].IndexCount % 3 == 0 ? OpenTK.Graphics.OpenGL.PrimitiveType.Triangles : OpenTK.Graphics.OpenGL.PrimitiveType.Quads);
                // Геометрия для рендеренга
                for (int k = 0; k < vertices.Length; ++k)
                {
                    GL.Normal3(normals[k]);
                    if (scene.Materials[mesh.MaterialIndex].HasTextureDiffuse)
                    {
                        GL.TexCoord2(textureCoordinates[k].X, textureCoordinates[k].Y);
                    }
                    GL.Vertex3(vertices[k]);
                }
                GL.End();

                GL.EndList();
            }
        }

        // Для каждого меша загружаю необходимые текстуры для материалов.
        private void LoadTextures()
        {
            materialLoader = new MaterialLoader();

            for (int i = 0; i < scene.MeshCount; i++)
            {
                Mesh mesh = scene.Meshes[i];

                if (mesh.MaterialIndex >= 0)
                {
                    Material material = scene.Materials[mesh.MaterialIndex];
                    materialLoader.LoadMaterialTextures(material);
                }
            }
        }

        public void RenderModel()
        {
            for (int i = 0; i < scene.MeshCount; i++)
            {
                GL.CallList(displayLists[i]);
            }
        }

        private Vector3[] AssimpVectorToOpenTKVector(Vector3D[] vecArr)
        {
            Vector3[] vector3s = new Vector3[vecArr.Length];

            for (int i = 0; i < vecArr.Length; i++)
            {
                vector3s[i].X = vecArr[i].X;
                vector3s[i].Y = vecArr[i].Y;
                vector3s[i].Z = vecArr[i].Z;
            }

            return vector3s;
        }
    }
}
