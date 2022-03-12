using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombs_Away
{
    public class Bomb : MovingEntity
    {

        public Bomb() {
            MaximumAge = 2;
            FrameStep = 5;
        }
    }
}
