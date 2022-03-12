using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
namespace Bombs_Away
{
    public class TileEntity : GameObject
    {
        public World.CollisionType CollisionType { get; set; }
        public World.StaticEntityType EntityType { get; set; }
        public Vector2f AbsolutePosition { get; set; }
        public TileEntity() {
            EntityType = World.StaticEntityType.None;
            CollisionType = World.CollisionType.SemiSolid;
        }
    }
}

















