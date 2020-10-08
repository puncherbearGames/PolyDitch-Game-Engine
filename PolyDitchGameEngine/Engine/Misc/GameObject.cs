using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace PolyDitchGameEngine.Engine.Misc
{
    public class GameObject
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
        public bool Active
        {
            get
            {
                return active;
            }
        }
        private bool active = false;
        public string Name;
        public Matrix4 TransformationMatrix
        {
            get
            {
                Matrix4 XRot = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(this.Rotation.X));
                Matrix4 YRot = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(this.Rotation.Y));
                Matrix4 ZRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(this.Rotation.Z));
                Matrix4 Scale = Matrix4.CreateScale(this.Scale);
                Matrix4 Translation = Matrix4.CreateTranslation(Position);
                return XRot * YRot * ZRot * Scale * Translation;
            }
        }
        public readonly List<PolyDitch> Scripts;
        public GameObject(Vector3 Position, Vector3 Scale, Vector3 Rotation, string Name, bool isActive = true)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Rotation = Rotation;
            this.Name = Name;
            this.Scripts = new List<PolyDitch>(0);
            if (isActive)
            {
                this.active = true;
                Main.Engine.GameObjects.Add(this);
            }
        }
        public void SetActive(bool SetActive)
        {
            if(active && SetActive == false)
            {
                active = false;
                Main.Engine.GameObjects.Remove(this);
                return;
            }
            if(active == false && SetActive)
            {
                active = true;
                Main.Engine.GameObjects.Add(this);
                return;
            }
        }
        public void AddScript(PolyDitch Script)
        {
            Script.gameObject = this;
            Scripts.Add(Script);
        }
    }
}
