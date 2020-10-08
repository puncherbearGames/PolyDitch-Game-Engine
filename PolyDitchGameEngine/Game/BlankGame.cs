using PolyDitchGameEngine.Engine.GraphicsEngine.Geometry.Third_Dimension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using PolyDitchGameEngine.Engine.Main;
using PolyDitchGameEngine.Engine.GraphicsEngine;
using OpenTK;
using PolyDitchGameEngine.Basics;
using PolyDitchGameEngine.Engine.Misc;

namespace PolyDitchGameEngine.Game
{
    public class BlankGame : Engine.Main.Engine
    {
        float Speed = 1.5f;
        public Mesh Test = new Mesh("dragon.obj");
        public GameObject gameObject = new GameObject(Vector3.One, Vector3.One, Vector3.One, "First GameObject");

        public float Sensitivity = 0.1f;
        KeyboardState Input = Keyboard.GetState();
        MeshRenderer TestMeshRenderer; //= new MeshRenderer(new Mesh("FirstSpaceShip2.obj"));
        MeshRenderer TestMeshRenderer2; //= new MeshRenderer(new Mesh("FirstSpaceShip2.obj"));
        public Camera MainCamera = new Camera(new Vector3(0.0f, 0.0f, 3.0f), new Vector3(0,0,-1), 45f, 0.01f, 100f, ProjectionType.Perspective);
        public BlankGame(int Width, int Height, string Title) : base(Width, Height, Title)
        {

        }

        public override void OnLoad()
        {
            base.OnLoad();
            GraphicsEngine.Cameras.Add(MainCamera);
        }
        public override void OnRender()
        {
            base.OnRender();
        }
        public override void OnUpdate()
        {
            Input = Keyboard.GetState();
            base.OnUpdate();
            if (Input.IsKeyDown(Key.W))
            {
                Console.WriteLine("W");
                MainCamera.Position += MainCamera.Forward * Speed * DeltaTime;
            }
            if (Input.IsKeyDown(Key.S))
            {
                MainCamera.Position -= MainCamera.Forward * Speed * DeltaTime;
            }
            if (Input.IsKeyDown(Key.A))
            {
                MainCamera.Position -= Vector3.Normalize(Vector3.Cross(MainCamera.Forward, MainCamera.Up)) * Speed * DeltaTime; //This moves the player left
            }
            if (Input.IsKeyDown(Key.D))
            {
                MainCamera.Position += Vector3.Normalize(Vector3.Cross(MainCamera.Forward, MainCamera.Up)) * Speed * DeltaTime; //This moves the player right
            }
            if (Input.IsKeyDown(Key.Space))
            {
                MainCamera.Position += MainCamera.Up * Speed * DeltaTime;
            }
            if (Input.IsKeyDown(Key.LShift))
            {
                MainCamera.Position -= MainCamera.Up * Speed * DeltaTime;
            }
        }

        public override void OnMouseMove()
        {
            //Console.WriteLine(MouseDelta);
            //MainCamera.Rotate(Sensitivity * MouseDelta.X, Sensitivity * MouseDelta.Y);
            MainCamera.Forward.X -= MathHelper.DegreesToRadians(MouseDelta.X) * Sensitivity;
            MainCamera.Forward.Y += MathHelper.DegreesToRadians(MouseDelta.Y) * Sensitivity;
        }

        public override void OnSetup()
        {
            base.OnSetup();
            //TestMeshRenderer = new MeshRenderer(new Mesh("FirstSpaceShip2.obj"));
            //TestMeshRenderer2 = new MeshRenderer(new Mesh("FirstSpaceShip2.obj"));
            TestMeshRenderer = new MeshRenderer(Test);
            gameObject.AddScript(TestMeshRenderer);
        }

    }
}
