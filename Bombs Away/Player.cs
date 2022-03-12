using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombs_Away
{
    public class Player : MovingEntity {
        public string Name { get; set; }
        public Player() {
            JumpedMaxHeight = false;
            JumpingForce = 10;
            WalkingForce = 10;
        }


    }
}
