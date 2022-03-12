using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
namespace Bombs_Away {
    public class SpawnMode {
        public float Bounciness { get; set; }
        public int BombNumber { get; set; }
        public int SpawnRate { get; set; }
        public Vector2f Velocity { get; set; }
        public float MaximumAge { get; set; }
        public int FrameStep { get; set; }
        public SpawnMode(float bounciness, int bombNumber, int spawnRate, Vector2f velocity, float maximumAge, int frameStep) {
            Bounciness = bounciness;
            BombNumber = bombNumber;
            SpawnRate = spawnRate;
            Velocity = velocity;
            MaximumAge = maximumAge;
            FrameStep = frameStep;
        }


    }
}
