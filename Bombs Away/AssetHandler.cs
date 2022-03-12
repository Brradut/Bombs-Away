using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace Bombs_Away {
   public static class AssetHandler {

        public static void GiveAsset(GameObject gameObject, int assetIndex) {
            gameObject.Texture = World.Textures[World.Assets[assetIndex].TextureId];
            gameObject.OffsetPosition = World.Assets[assetIndex].OffsetPosition;
            gameObject.OffsetSize = World.Assets[assetIndex].OffsetSize;
            gameObject.TextureRect = World.Assets[assetIndex].TextureRectangle;
        }

        public static void UpdateAsset(MovingEntity movingEntity) {
            switch(movingEntity.EntityType) {
                case World.MovingEntityType.Bomber:
                if(movingEntity.Direction == -1)
                    GiveAsset(movingEntity, 0);
                else
                    GiveAsset(movingEntity, 1);

                if(movingEntity.AgeUntilNotInvincible > movingEntity.Age)
                    movingEntity.Color = new Color(255, 255, 255, 150);
                else
                    movingEntity.Color = new Color(255, 255, 255, 255);
                break;

                case World.MovingEntityType.Barrel:
                if(movingEntity.Direction == -1)
                    GiveAsset(movingEntity, 16);
                else
                    GiveAsset(movingEntity, 17);
                if(movingEntity.AgeUntilNotInvincible > movingEntity.Age)
                    movingEntity.Color = new Color(255, 255, 255, 150);
                else
                    movingEntity.Color = new Color(255, 255, 255, 255);
                break;

                case World.MovingEntityType.LittleBarrel:
                if(movingEntity.Direction == -1) {
                       GiveAsset(movingEntity, Convert.ToInt16(World.Animations[2][Convert.ToInt16(movingEntity.Age/World.FrameRate)/movingEntity.FrameStep]));
                } else {
                       GiveAsset(movingEntity, Convert.ToInt16(World.Animations[3][Convert.ToInt16(movingEntity.Age/World.FrameRate) / movingEntity.FrameStep]));
                }
                break;

                case World.MovingEntityType.LittleBomb:
                if(movingEntity.Direction == -1) {
                    GiveAsset(movingEntity, Convert.ToInt16(World.Animations[0][Convert.ToInt16(movingEntity.Age / World.FrameRate) / movingEntity.FrameStep]));
                } else {
                    GiveAsset(movingEntity, Convert.ToInt16(World.Animations[1][Convert.ToInt16(movingEntity.Age / World.FrameRate) / movingEntity.FrameStep]));
                }
                break;

            }
        }
        public static void UpdateAsset(TileEntity tileEntity) {
            switch(tileEntity.EntityType) {
                case World.StaticEntityType.Explosion:
                int frame = Convert.ToInt16(tileEntity.Age / World.FrameRate) / tileEntity.FrameStep;
                    GiveAsset(tileEntity, Convert.ToInt16(World.Animations[4][frame]));
                    tileEntity.Position = new Vector2f(tileEntity.AbsolutePosition.X + World.Animations[5][frame], tileEntity.AbsolutePosition.Y + World.Animations[5][frame]);
                break;
            }
        }

        public static void LoadAllAssets() {
            //Bomb left, right
            World.Assets.Add(new Asset(0, new IntRect(120, 0, 84, 120), new Vector2f(0, 34), new Vector2f(0, -34)));
            World.Assets.Add(new Asset(0, new IntRect(120, 150, 84, 120), new Vector2f(0, 34), new Vector2f(0, -34)));
            //LittleBombLeft
            World.Assets.Add(new Asset(0, new IntRect(150, 300, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(150, 350, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(150, 400, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(150, 450, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(150, 500, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(150, 550, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(150, 600, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            //LittleBombRight
            World.Assets.Add(new Asset(0, new IntRect(200, 300, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(200, 350, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(200, 400, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(200, 450, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(200, 500, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(200, 550, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            World.Assets.Add(new Asset(0, new IntRect(200, 600, 25, 35), new Vector2f(0, 10), new Vector2f(0, -10)));
            //Barrel left, right
            World.Assets.Add(new Asset(0, new IntRect(0, 0, 80, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(0, 150, 80, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            //LittleBarrelLeft
            World.Assets.Add(new Asset(0, new IntRect(0, 300, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(0, 350, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(0, 400, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(0, 450, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(0, 500, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(0, 550, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(0, 600, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            //LittleBarrelRight
            World.Assets.Add(new Asset(0, new IntRect(50, 300, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(50, 350, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(50, 400, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(50, 450, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(50, 500, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(50, 550, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(50, 600, 28, 36), new Vector2f(0, 0), new Vector2f(0, 0)));
            //Explosion
            World.Assets.Add(new Asset(0, new IntRect(350, 0, 10, 10), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(0, new IntRect(350, 50, 25, 25), new Vector2f(3, 3), new Vector2f(-6, -3)));
            World.Assets.Add(new Asset(0, new IntRect(350, 100, 40, 40), new Vector2f(8, 7), new Vector2f(-13, -7)));
            World.Assets.Add(new Asset(0, new IntRect(350, 150, 50, 50), new Vector2f(8, 10), new Vector2f(-13, -10)));
            //Platforms
            World.Assets.Add(new Asset(1, new IntRect(0, 0, 270, 60), new Vector2f(0, 5), new Vector2f(-15, -5)));
            World.Assets.Add(new Asset(1, new IntRect(0, 100, 180, 15), new Vector2f(0, 5), new Vector2f(-8, -5)));
            World.Assets.Add(new Asset(1, new IntRect(0, 200, 30, 130), new Vector2f(0, 5), new Vector2f(-5, -5)));
            //GUI Elements
            World.Assets.Add(new Asset(2, new IntRect(50, 50, 200, 99), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(50, 150, 200, 99), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(50, 250, 200, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(50, 350, 200, 100), new Vector2f(0, 0), new Vector2f(0, 0)));

            World.Assets.Add(new Asset(2, new IntRect(300, 50, 200, 99), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(300, 150, 200, 99), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(300, 250, 200, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(300, 350, 200, 100), new Vector2f(0, 0), new Vector2f(0, 0)));

            World.Assets.Add(new Asset(2, new IntRect(50, 500, 376, 155), new Vector2f(0, 0), new Vector2f(0, 0)));

            World.Assets.Add(new Asset(2, new IntRect(690, 240, 320, 170), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(690, 440, 320, 170), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(1090, 190, 320, 170), new Vector2f(0, 0), new Vector2f(0, 0)));
            //Numbers
            World.Assets.Add(new Asset(2, new IntRect(0, 730, 100, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(100, 730, 100, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(200, 730, 100, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(300, 730, 100, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(400, 730, 100, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(500, 730, 100, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(600, 730, 100, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(700, 730, 100, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(800, 730, 100, 100), new Vector2f(0, 0), new Vector2f(0, 0)));
            World.Assets.Add(new Asset(2, new IntRect(900, 730, 100, 100), new Vector2f(0, 0), new Vector2f(0, 0)));

        }
        public static void LoadAllAnimations() {
            World.Animations.Add(new float[] { 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 4, 5, 5, 5, 5, 7, 7, 8, 8, 7, 8, 7, 8, 7 });
            World.Animations.Add(new float[] { 9, 9, 9, 9, 9, 9, 9, 9, 11, 11, 11, 11, 12, 12, 12, 12, 14, 14, 15, 15, 14, 15, 14, 15, 14 });
            World.Animations.Add(new float[] { 18, 18, 18, 18, 18, 18, 18, 18, 20, 20, 20, 20, 21, 21, 21, 21, 23, 23, 24, 24, 23, 24, 23, 24, 23 });
            World.Animations.Add(new float[] { 25, 25, 25, 25, 25, 25, 25, 25, 27, 27, 27, 27, 28, 28, 28, 28, 30, 30, 31, 31, 30, 31, 30, 31, 30 });
            World.Animations.Add(new float[] { 32, 33, 33, 34, 34, 35, 35, 35, 35, 35, 35, 35, 35, 34, 34, 33, 33, 32, 32 });
            World.Animations.Add(new float[] { 0, -7.5f, -7.5f, -15f, -15f, -20f, -20f, -20f, -20, -20f, -20f, -20f, -20f, -15f, -15f, -7.5f, -7.5f, 0, 0 });
        }
    }
}
