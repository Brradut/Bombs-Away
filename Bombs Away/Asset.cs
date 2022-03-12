using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Bombs_Away
{
    public class Asset
    {   
        public int TextureId { get; set; }
        public IntRect TextureRectangle { get; set; }
        public Vector2f OffsetPosition { get; set; }
        public Vector2f OffsetSize { get; set; }

        public Asset(int textureId, IntRect textureRectangle, Vector2f offsetPosition, Vector2f offsetSize) {
            TextureId = textureId;
            TextureRectangle = textureRectangle;
            OffsetPosition = offsetPosition;
            OffsetSize = offsetSize;
        }
    }
}
