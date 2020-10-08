using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;
using Vector3 = System.Numerics.Vector3;
using Vector2 = System.Numerics.Vector2;

namespace PolyDitchGameEngine.Engine.GraphicsEngine.Geometry.Third_Dimension
{
    public class Mesh
    {
        public float[] Verticies = new float[0];
        public float[] TextureCoordinates = new float[0];
        public float[] Normals = new float[0];
        public int[] Indicies = new int[0];

        public Mesh()
        {

        }

        public void LoadFromOBJFile(string FilePath)
        {
            if (!(FilePath.Substring(FilePath.Length - 4) == ".obj"))
            {
                Environment.Exit(0);
            }
            StreamReader file = null;
            string line = "";
            if (!File.Exists(@"Game/Assets/" + FilePath))
            {
                Environment.Exit(0);
            }
            else
            {
                file = new StreamReader(@"Game/Assets/" + FilePath);
            }
            List<Vector3> verticies = new List<Vector3>(0);
            List<Vector3> normals = new List<Vector3>(0);
            List<Vector2> textures = new List<Vector2>(0);
            List<int> indicies = new List<int>(0);
            float[] vertA = null;
            float[] normA = null;
            float[] textA = null;
            int[] indA = null;
            while ((line = file.ReadLine()) != null)
            {
                string[] CurrentLine = line.Split(' ');
                if (line.StartsWith("v "))
                {
                    Vector3 vertex = new Vector3(float.Parse(CurrentLine[1]), float.Parse(CurrentLine[2]), float.Parse(CurrentLine[3]));
                    verticies.Add(vertex);
                }
                else if (line.StartsWith("vt "))
                {
                    Vector2 vertex = new Vector2(float.Parse(CurrentLine[1]), float.Parse(CurrentLine[2]));
                    textures.Add(vertex);
                }
                else if (line.StartsWith("vn "))
                {
                    Vector3 vertex = new Vector3(float.Parse(CurrentLine[1]), float.Parse(CurrentLine[2]), float.Parse(CurrentLine[3]));
                    normals.Add(vertex);
                }
                else if (line.StartsWith("f "))
                {
                    textA = new float[verticies.Count * 2];
                    normA = new float[verticies.Count * 3];
                    //vertA = new float[Verticies.Count * 3];
                    break;
                }
            }

            while ((line = file.ReadLine()) != null)
            {
                if(!line.StartsWith("f "))
                {
                    continue;
                }
                string[] currentLine = line.Split(' ');

                string[] vertex1 = currentLine[1].Split('/');
                string[] vertex2 = currentLine[2].Split('/');
                string[] vertex3 = currentLine[3].Split('/');
                
                ProcessVertex(vertex1, indicies, textures, normals, textA, normA);
                ProcessVertex(vertex2, indicies, textures, normals, textA, normA);
                ProcessVertex(vertex3, indicies, textures, normals, textA, normA);
            }
            file.Close();
            vertA = new float[verticies.Count * 3];
            indA = new int[indicies.Count];

            //for(int x = 0; x < verticies.Count;)
            //{
            //    vertA[x++] = verticies[x].X;
            //    vertA[x++] = verticies[x].Y;
            //    vertA[x++] = verticies[x].Z;
            //}

            int IndexPointer = 0;
           foreach(Vector3 vector in verticies)
           {
                vertA[IndexPointer++] = vector.X;
                vertA[IndexPointer++] = vector.Y;
                vertA[IndexPointer++] = vector.Z;
            }

            for(int x = 0; x < indicies.Count; x++)
            {
                indA[x] = indicies[x];
            }
            this.Verticies = vertA;
            //this.Normals = normA;
            this.TextureCoordinates = textA;
            this.Indicies = indA;

            Console.WriteLine(FilePath + " verticies count : " + this.Verticies.Length);
            Console.WriteLine(FilePath + " Texture coordinates count : " + this.TextureCoordinates.Length);
            Console.WriteLine(FilePath + " Indicies count : " + this.Indicies.Length);

        }
        private static void ProcessVertex(string[] vertexData, List<int> indicies, List<Vector2> textures, List<Vector3> normals, float[] textureArray, float[] normalsArray)
        {
            int curVP = int.Parse(vertexData[0]) - 1;
            indicies.Add(curVP);
            Vector2 curTex = textures[int.Parse(vertexData[1]) - 1];
            textureArray[curVP * 2] = curTex.X;
            textureArray[curVP * 2 + 1] = 1 - curTex.Y;
            Vector3 curNorm = normals[int.Parse(vertexData[2]) - 1];
            normalsArray[curVP * 3] = curNorm.X;
            normalsArray[curVP * 3 + 1] = curNorm.Y;
            normalsArray[curVP * 3 + 2] = curNorm.Z;
        }
        private static int[] IndexProccesing(string[] Data)
        {

            return null;
        }
        public Mesh(string FilePath)
        {
            LoadFromOBJFile(FilePath);
        }
        
    }

}
