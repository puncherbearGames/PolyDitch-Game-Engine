using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PolyDitchGameEngine.Engine.GraphicsEngine
{
    public enum ProjectionType{
        Orthographic,
        Perspective
    }
    public class Camera
    {
        public Vector3 Position;
        public Vector3 Target = Vector3.Zero;
        public Vector3 Direction
        {
            get
            {
                return Vector3.Normalize(Position - Target);
            }
        }
        public Vector3 EulerAngles
        {
            get
            {
                return Forward;
            }
            set
            {
                if (value.X > 89.0f)
                {
                    value.X = 89.0f;
                }
                else if (value.X < -89.0f)
                {
                    value.X = -89.0f;
                }
                else
                {
                    value.X -= Engine.Main.Engine.MouseDelta.X * 0.1f;
                }
                eulerAngles = value;
                Forward.X = (float)Math.Cos(MathHelper.DegreesToRadians(value.X)) * (float)Math.Cos(MathHelper.DegreesToRadians(value.Y));
                Forward.Y = (float)Math.Sin(MathHelper.DegreesToRadians(value.X));
                Forward.Z = (float)Math.Cos(MathHelper.DegreesToRadians(value.X)) * (float)Math.Sin(MathHelper.DegreesToRadians(value.Y));
                Forward = Vector3.Normalize(Forward);
            }
        }
        private Vector3 eulerAngles;
        public float FOV, Near, Far;
        public Matrix4 ProjectionMatrix;
        public Vector3 Up = Vector3.UnitY;
        public Vector3 Forward = -Vector3.UnitZ;
        public Vector3 Right
        {
            get
            {
                return Vector3.Normalize(Vector3.Cross(Up, Direction));
            }
        }
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Forward, Up);
        }
        public Vector3 CUp
        {
            get
            {
                return Vector3.Cross(Direction, Right);
            }
        }
        public Matrix4 View
        {
            get
            {
                return Matrix4.LookAt(Position, Position + Forward, Up);
            }
        }
        public Camera(Vector3 Position, Vector3 EulerAngles, float FOV, float Near, float Far, ProjectionType ProjectionType)
        {
            this.Position = Position;
            //this.EulerAngles = EulerAngles;
            this.FOV = FOV;
            this.Near = Near;
            this.Far = Far;
            if(ProjectionType == ProjectionType.Orthographic)
            {
                ProjectionMatrix = Matrix4.CreateOrthographic(Engine.Main.Engine.Width, Engine.Main.Engine.Height, this.Near, this.Far);
            }
            if (ProjectionType == ProjectionType.Perspective)
            {
                //Console.WriteLine("Perspective");
                ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(this.FOV), (float)Engine.Main.Engine.Width / (float)Engine.Main.Engine.Height, this.Near, this.Far);
            }
        }
        public void Rotate(float Pitch, float Yaw)
        {
            Forward.X += (float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(Yaw));
            Forward.Y += (float)Math.Sin(MathHelper.DegreesToRadians(Pitch));
            Forward.Z += (float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(Yaw));
            Forward = Vector3.Normalize(Forward);
        }
    }
}
