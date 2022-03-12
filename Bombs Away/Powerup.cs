using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombs_Away
{
    public class Powerup : MovingEntity
    {
        public PowerupLogic.Powerups PowerupType { get; set; }

        public Powerup(PowerupLogic.Powerups type)
        {
            PowerupType = type;
        }

    }
}
