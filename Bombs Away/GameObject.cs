using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace Bombs_Away
{
    public abstract class GameObject : SFML.Graphics.Sprite
    {
        private bool _exists;
        public bool Exists { get { if(Age > MaximumAge) { _exists = false; return false; } return _exists; } set { _exists = value; }  }
        public float Age { get; set; }
        public float MaximumAge { get; set; }
        public int FrameStep { get; set; }
        public Vector2f OffsetPosition { get; set; }
        public Vector2f OffsetSize { get; set; }
        public FloatRect CollisionBox { get { return Physics.ApplyOffset(this); } }
        public GameObject() {
            Exists = true;
            FrameStep = 5;
            Age = 0;
            MaximumAge = 3600f;
        }
    }
}
