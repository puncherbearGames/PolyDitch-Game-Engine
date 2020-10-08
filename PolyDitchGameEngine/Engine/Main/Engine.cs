using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using PolyDitchGameEngine.Basics;
using PolyDitchGameEngine.Engine.Misc;

namespace PolyDitchGameEngine.Engine.Main
{
    public class Engine : GameWindow
    {
        public static List<GameObject> GameObjects = new List<GameObject>(0);

        public static float DeltaTime;
        public static int Width = 0;
        public static int Height = 0;
        public static List<MeshRenderer> OriginalGraphicsPipeline = new List<MeshRenderer>(0);
        public static List<PolyDitch> ScriptsToRun = new List<PolyDitch>(0);
        public static Vector2 MouseDelta = new Vector2(0,0);
        public static Vector2 MousePosition = new Vector2(0, 0);
        public MouseState mouse;
        Vector2 LastMousePosition;
        bool FirstMove = true;
        public static float AssetLoadingPercentage = 0;
        public Engine(int width, int height, string Title) : base(width, height,GraphicsMode.Default, Title)
        {
            OnSetup();
            Width = width;
            Height = height;
            base.Run(60.0);
        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (Focused) // check to see if the window is focused  
            {
                Mouse.SetPosition(X + Width / 2f, Y + Height / 2f);
            }
            mouse = OpenTK.Input.Mouse.GetState();
            if (FirstMove)
            {
                FirstMove = false;
                LastMousePosition = new Vector2(mouse.X, mouse.Y);
            }
            MouseDelta.X = mouse.X - LastMousePosition.X;
            MouseDelta.Y = mouse.Y - LastMousePosition.Y;
            LastMousePosition.X = mouse.X;
            LastMousePosition.Y = mouse.Y;
            OnMouseMove();
            base.OnMouseMove(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            foreach(GameObject gameObject in GameObjects)
            {
                foreach(PolyDitch polyDitch in gameObject.Scripts)
                {
                    polyDitch.Start();
                }
            }

            GraphicsEngine.GraphicsEngine.OnLoad();
            OnLoad();
        }
        protected override void OnResize(EventArgs e)
        {
            Width = base.Width;
            Height = base.Height;
            Console.WriteLine("Width : " + Width + " Height : " + Height);
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (GameObject gameObject in GameObjects)
            {
                foreach (PolyDitch polyDitch in gameObject.Scripts)
                {
                    polyDitch.Render();
                }
            }

            OnRender();
            GraphicsEngine.GraphicsEngine.Render3D();
            Context.SwapBuffers();
        }
        
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused)
            {
                CursorVisible = true;
                return;
            }

            foreach (GameObject gameObject in GameObjects)
            {
                foreach (PolyDitch polyDitch in gameObject.Scripts)
                {
                    polyDitch.Update();
                }
            }

            CursorVisible = false;
            DeltaTime = (float)e.Time;
            base.OnUpdateFrame(e);
            OnUpdate();
        }
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            GraphicsEngine.GraphicsEngine.OnUnload();
            OnUnload();
        }
        public virtual void OnLoad()
        {

        }
        public virtual void OnUpdate()
        {

        }
        public virtual void OnRender()
        {

        }
        public virtual void OnUnload()
        {

        }
        public virtual void OnMouseMove()
        {

        }
        public virtual void OnSetup()
        {

        }
    }
}
