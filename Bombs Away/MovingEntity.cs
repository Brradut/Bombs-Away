using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
namespace Bombs_Away
{
    public abstract class MovingEntity : GameObject
    {
        public bool IsMovingUp { get; set; }
        public bool IsMovingDown { get; set; }
        public bool IsMovingRight { get; set; }
        public bool IsMovingLeft { get; set; }

        public Vector2f Velocity = new Vector2f(0, 0);
        public bool IsOnGround { get { return Physics.TestOnGround(this, World.TileEntities); }}
        public bool IsInGround { get { return Physics.TestInGround(this, World.TileEntities); }}
        public Vector2f TerminalVelocity = new Vector2f(40, 20);
        public int Direction = 1;
        public Vector2f LastPosition = new Vector2f(0, 0);
        public World.MovingEntityType EntityType { get; set; }
        public SpawnMode Mode { get; set; }
        public float AgeUntilNotInvincible { get; set; }

        public float JumpingForce { get; set; }
        public float WalkingForce { get; set; }
        public bool JumpedMaxHeight { get; set;}

        public MovingEntity() {
            AgeUntilNotInvincible = 0;
            IsMovingDown = false;
            EntityType = World.MovingEntityType.None;
            IsMovingLeft = false;
            IsMovingRight = false;
            IsMovingUp = false;
        }
    }
}
