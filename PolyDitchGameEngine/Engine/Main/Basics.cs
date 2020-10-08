using PolyDitchGameEngine.Engine.GraphicsEngine;
using PolyDitchGameEngine.Engine.GraphicsEngine.Geometry.Third_Dimension;
using PolyDitchGameEngine.Engine.GraphicsEngine.Rendering;
using PolyDitchGameEngine.Engine.Misc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyDitchGameEngine.Basics
{
    public class MeshRenderer : PolyDitch
    {
        public Mesh Mesh = null;
        public Material Material = null;
        public Shader Shader = null;
        public MeshRenderer(Mesh mesh = null, Material material = null, Shader shader = null)
        {
            if(shader == null)
            {
                if(GraphicsEngine.BasicShader == null)
                {
                    Console.WriteLine("Shader == null");
                }
                this.Shader = GraphicsEngine.BasicShader;
            }
            if(material == null)
            {
                this.Material = GraphicsEngine.BasicMaterial;
            }
            if(mesh == null)
            {
                Console.WriteLine("Mesh is never declared, so PolyDitch of type 'MeshRenderer' is never run.");
            }
            else
            {
                this.Mesh = mesh;
                Engine.Main.Engine.ScriptsToRun.Add(this);
                Engine.Main.Engine.OriginalGraphicsPipeline.Add(this);
            }
        }
        public MeshRenderer()
        {
            Console.WriteLine("MeshRenderer created");
            Mesh = null;
            Engine.Main.Engine.ScriptsToRun.Add(this);
            Engine.Main.Engine.OriginalGraphicsPipeline.Add(this);
            //GraphicsEngine.OriginalGraphicsPipeline.Add(this);
            //GraphicsEngine.MeshRenderers.Add(this);
        }
        public override void Start()
        {
            //Console.WriteLine("MeshRenderer");
        }

        public override void Update()
        {
            //Console.WriteLine("MeshRenderer");
        }

        public override void Render()
        {
            //Console.WriteLine("MeshRenderer");
        }
    }
}
