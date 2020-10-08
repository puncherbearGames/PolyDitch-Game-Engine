using OpenTK.Graphics.OpenGL;
using OpenTK;
using PolyDitchGameEngine.Engine.GraphicsEngine.Geometry.Third_Dimension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyDitchGameEngine.Engine.GraphicsEngine.Rendering;
using PolyDitchGameEngine.Basics;
using PolyDitchGameEngine.Engine.Misc;

namespace PolyDitchGameEngine.Engine.GraphicsEngine
{
    static class GraphicsEngine
    {
        private class RenderedMesh
        {
            public Shader shader;
            public Material material;
            public readonly int vaoID;
            public int VertexCount;
            public RenderedMesh(int vaoID, int VertexCount, Material material, Shader shader)
            {
                this.vaoID = vaoID;
                this.VertexCount = VertexCount;
                this.material = material;
                this.shader = shader;
            }
        }
        private class Loader
        {
            
            public RenderedMesh LoadToVao(Material material, Shader shader, float[] positions, float[] textureCoordinates, int[] indicies)
            {
                int vaoID = CreateVAO();
                BindIndicies(indicies);
                StoreDataInAttributeList(0, 3, positions);
                StoreDataInAttributeList(1, 2, textureCoordinates);
                UnBindVAO();
                return new RenderedMesh(vaoID, indicies.Length, material, shader);
            }
            private void BindIndicies(int[] indicies)
            {
                int[] vboID = new int[0];
                GL.GenBuffers(1, vboID);
                GL.BindBuffer(BufferTarget.ArrayBuffer, vboID[0]);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
            }
            public void DeleteAll()
            {

            }
            private int CreateVAO()
            {
                int vaoID = GL.GenVertexArray();
                GL.BindVertexArray(vaoID);
                return vaoID;
            }
            private void StoreDataInAttributeList(int AttributeNumber, int size,float[] data)
            {
                int vboID = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
                GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(AttributeNumber, size, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            }
            private void UnBindVAO()
            {
                GL.BindVertexArray(0);
            }
        }
        private class Renderer
        {
            public void Render(RenderedMesh mesh, Texture texture)
            {
                GL.BindVertexArray(mesh.vaoID);
                GL.EnableVertexAttribArray(0);
                GL.EnableVertexAttribArray(1);
                texture.Use(TextureUnit.Texture0);
                GL.DrawElements(PrimitiveType.Triangles, mesh.VertexCount, DrawElementsType.UnsignedInt, 0);
                GL.DisableVertexAttribArray(0);
                GL.DisableVertexAttribArray(1);
                GL.BindVertexArray(0);
            }
        }
        public static List<Camera> Cameras = new List<Camera>(0);
        public static void AddCamera(Camera Camera)
        {
            Cameras.Add(Camera);
        }

        public static List<MeshRenderer> MeshRenderers = new List<MeshRenderer>(0);

        public static Texture BasicTexture;
        public static Texture SecondBasicTexture;
        public static Material BasicMaterial;
        public static Shader BasicShader = new Shader("Engine\\GraphicsEngine\\Basic Shaders\\BasicVertexShader.shader", "Engine\\GraphicsEngine\\Basic Shaders\\BasicFragmentShader.shader");

        public static List<float> OriginalRP_Points = new List<float>();
        public static List<float> OriginalRP_TexCords = new List<float>();

    //    public static float[] verticies =
    //    {
    //             //Position          Texture coordinates
    // 0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
    // 0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
    //-0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
    //-0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
    //    };

        public static float[] verticies =
        {
         -0.5f,0.5f,0,
         -0.5f,-0.5f,0,
         0.5f,-0.5f,0,
         0.5f,0.5f,0,
        };

        public static int[] Indicies =
        {
            0,1,3,
            3,1,2
        };

        public static float[] TextureCoordinates =
        {
            0,0,
            0,1,
            1,1,
            1,0
        };

        public static uint[] indices = {
             0, 1, 3,   // first triangle
             1, 2, 3    // second triangle
                        };


        
        public static Matrix4 Model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-55.0f));

        public static Matrix4 view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);

        public static Matrix4 Perspective_Matrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)Engine.Main.Engine.Width / (float)Engine.Main.Engine.Height, 0.1f, 100.0f);

        static List<RenderedMesh> renderedMeshes = new List<RenderedMesh>(0);

        static Loader loader = new Loader();
        static Renderer First_RP = new Renderer();
        static List<RenderedMesh> Test = new List<RenderedMesh>(0);

        public static void OnLoad()
        {
            MeshRenderers = Engine.Main.Engine.OriginalGraphicsPipeline;
            BasicTexture = new Texture("ImphenziaPalette01.png");
            SecondBasicTexture = new Texture("In game screenshot.PNG");
            BasicMaterial = new Material(BasicTexture);

            //foreach (MeshRenderer meshRenderer in MeshRenderers)
            //{
            //   Test.Add(loader.LoadToVao(meshRenderer.Material, meshRenderer.Shader, meshRenderer.Mesh.Verticies, meshRenderer.Mesh.TextureCoordinates, meshRenderer.Mesh.Indicies)); 
            //}

            Test.Add(loader.LoadToVao(BasicMaterial, BasicShader, verticies, TextureCoordinates, Indicies));

        }
        public static void OnUnload()
        {
            loader.DeleteAll();
        }
        public static void Render3D()
        {
            BasicShader.Use();
            foreach (RenderedMesh renderedMesh in Test)
            {
                BasicShader.UseMatrix("model", Model);
                BasicShader.UseMatrix("projection", Perspective_Matrix);
                BasicShader.UseMatrix("view", view);
                First_RP.Render(renderedMesh, BasicTexture);
            }
            Shader.Halt();
        }

    }
}
