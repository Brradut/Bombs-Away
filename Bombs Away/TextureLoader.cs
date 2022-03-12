using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
namespace Bombs_Away
{
    static class TextureLoader
    {
        public static void LoadTexture(string fileName) {
            World.Textures.Add(new Texture(fileName));
        }
        public static void LoadCharacters(string fileName) {
            LoadTexture(fileName);
        }
        public static void LoadBackgrounds(string fileName) {
            LoadTexture(fileName);
        }
        public static void LoadTiles(string fileName) {
            LoadTexture(fileName);
        }
        public static void LoadParticles(string fileName) {
            LoadTexture(fileName);
        }
    }
}
