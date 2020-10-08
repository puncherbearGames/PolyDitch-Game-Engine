using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyDitchGameEngine.Engine.Misc
{
    public class PolyDitch
    {
        public GameObject gameObject;
        public virtual void Start()
        {

        }
        public virtual void Update()
        {

        }
        /// <summary>
        /// This is run on the CPU render loop not on the GPU. It is not advised that you use this UNLESS you are working with rendering.
        /// </summary>
        public virtual void Render()
        {

        }
    }
}
