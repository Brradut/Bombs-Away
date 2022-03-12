using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace Bombs_Away
{
    public static class World
    {
        public static List<MovingEntity> MovingEntities = new List<MovingEntity>();
        public static List<TileEntity> TileEntities = new List<TileEntity>();
        public static List<Texture> Textures = new List<Texture>();
        public static List<Asset> Assets = new List<Asset>();
        public static List<float[]> Animations = new List<float[]>();
        public static List<SpawnMode> SpawnModes = new List<SpawnMode>();

        public enum CollisionType {Solid, SemiSolid, None};
        public enum MovingEntityType {None, Barrel, Bomber, LittleBomb, LittleBarrel};
        public enum StaticEntityType {None, Ground, Platform, Pillar, Explosion };


        public static float GravitationalAcceleration = 40f;
        public static float AirResistance = 30f;
        public static float HorizontalAcceleration = 10000f;
        public static float FrameRate = 1 / 60f;

        public static void HandleExistance() {
            for(int i = 0; i < MovingEntities.Count; i++) {
                if(MovingEntities[i].Exists == false) {
                    if(MovingEntities[i].EntityType == MovingEntityType.LittleBomb || MovingEntities[i].EntityType == MovingEntityType.LittleBarrel) {
                            SpawnEntity(new Vector2f(MovingEntities[i].Position.X + MovingEntities[i].CollisionBox.Width/2, MovingEntities[i].Position.Y +MovingEntities[i].CollisionBox.Height / 2), StaticEntityType.Explosion);
                            TileEntities[TileEntities.Count - 1].AbsolutePosition = TileEntities[TileEntities.Count - 1].Position;
                    }
                    MovingEntities[i].Dispose();
                    MovingEntities.RemoveAt(i);
                    i--;
                }       
            }
               for(int i = 0; i < TileEntities.Count; i++) {
                    if(TileEntities[i].Exists == false) {
                       TileEntities[i].Dispose();
                       TileEntities.RemoveAt(i);
                        i--;
                    }
                }
            }

        public static void HandlePassive(MovingEntity entity) {
            if(entity.EntityType == MovingEntityType.Bomber) {
                if(entity.Age/FrameRate % entity.Mode.SpawnRate >= entity.Mode.SpawnRate - entity.Mode.BombNumber) {
                    if(entity.Mode == SpawnModes[4]) {
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), entity.Direction, MovingEntityType.LittleBomb, entity.Mode);
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(4 + entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), 1, MovingEntityType.LittleBomb, entity.Mode);
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(-4 - entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), -1, MovingEntityType.LittleBomb, entity.Mode);
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(2 + entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), 1, MovingEntityType.LittleBomb, entity.Mode);
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(-2 - entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), -1, MovingEntityType.LittleBomb, entity.Mode);
                    } else {
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), entity.Direction, MovingEntityType.LittleBomb, entity.Mode);
                    }
                }
            } else if(entity.EntityType == MovingEntityType.Barrel) {
                if(entity.Age / FrameRate % entity.Mode.SpawnRate >= entity.Mode.SpawnRate - entity.Mode.BombNumber) {
                    if(entity.Mode == SpawnModes[4]) {
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), entity.Direction, MovingEntityType.LittleBarrel, entity.Mode);
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(4 + entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), 1, MovingEntityType.LittleBarrel, entity.Mode);
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(-4 - entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), -1, MovingEntityType.LittleBarrel, entity.Mode);
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(2 + entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), 1, MovingEntityType.LittleBarrel, entity.Mode);
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(-2 - entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), -1, MovingEntityType.LittleBarrel, entity.Mode);
                    } else {
                        SpawnEntity(new Vector2f(entity.Position.X + entity.CollisionBox.Width / 2, entity.CollisionBox.Top + entity.CollisionBox.Height / 2 - 10), new Vector2f(entity.Mode.Velocity.X * entity.Direction, entity.Mode.Velocity.Y), entity.Direction, MovingEntityType.LittleBarrel, entity.Mode);
                    } 
                }
            }
        }

        public static void SpawnEntity(Vector2f position, Vector2f velocity, int direction, MovingEntityType entityType, SpawnMode spawnMode) {
            if(entityType == MovingEntityType.Barrel) {
                MovingEntities.Add(new Player());
                MovingEntities[MovingEntities.Count - 1].Velocity = velocity;
                MovingEntities[MovingEntities.Count - 1].Position = position;
                MovingEntities[MovingEntities.Count - 1].Direction = direction;
                MovingEntities[MovingEntities.Count - 1].EntityType = MovingEntityType.Barrel;
            } else if(entityType == MovingEntityType.Bomber) {
                MovingEntities.Add(new Player());
                MovingEntities[MovingEntities.Count - 1].Velocity = velocity;
                MovingEntities[MovingEntities.Count - 1].Position = position;
                MovingEntities[MovingEntities.Count - 1].Direction = direction;
                MovingEntities[MovingEntities.Count - 1].EntityType = MovingEntityType.Bomber;
            } else if(entityType == MovingEntityType.LittleBomb) {
                MovingEntities.Add(new Bomb());
                MovingEntities[MovingEntities.Count - 1].Velocity = velocity;
                MovingEntities[MovingEntities.Count - 1].Position = position;
                MovingEntities[MovingEntities.Count - 1].Direction = direction;
                MovingEntities[MovingEntities.Count - 1].EntityType = MovingEntityType.LittleBomb;
                MovingEntities[MovingEntities.Count - 1].FrameStep = spawnMode.FrameStep;
                MovingEntities[MovingEntities.Count - 1].MaximumAge = spawnMode.MaximumAge;
            } else if(entityType == MovingEntityType.LittleBarrel) {
                MovingEntities.Add(new Bomb());
                MovingEntities[MovingEntities.Count - 1].Velocity = velocity;
                MovingEntities[MovingEntities.Count - 1].Position = position;
                MovingEntities[MovingEntities.Count - 1].Direction = direction;
                MovingEntities[MovingEntities.Count - 1].EntityType = MovingEntityType.LittleBarrel;
                MovingEntities[MovingEntities.Count - 1].FrameStep = spawnMode.FrameStep;
                MovingEntities[MovingEntities.Count - 1].MaximumAge = spawnMode.MaximumAge;
            }
            MovingEntities[MovingEntities.Count - 1].Mode = spawnMode;
        }
        public static void SpawnEntity(Vector2f position, StaticEntityType entityType) {
            if(entityType == StaticEntityType.Explosion) {
                TileEntities.Add(new TileEntity());
                TileEntities[TileEntities.Count - 1].Position = position;
                TileEntities[TileEntities.Count - 1].CollisionType = CollisionType.None;
                TileEntities[TileEntities.Count - 1].EntityType = StaticEntityType.Explosion;
                TileEntities[TileEntities.Count - 1].MaximumAge = 0.9f;
                TileEntities[TileEntities.Count - 1].FrameStep = 3;
            } else if(entityType == StaticEntityType.Ground) {
                TileEntities.Add(new TileEntity());
                TileEntities[TileEntities.Count - 1].Position = position;
                TileEntities[TileEntities.Count - 1].CollisionType = CollisionType.Solid;
                TileEntities[TileEntities.Count - 1].EntityType = StaticEntityType.Ground;
                AssetHandler.GiveAsset(TileEntities[TileEntities.Count - 1], 36);
            } else if(entityType == StaticEntityType.Platform) {
                TileEntities.Add(new TileEntity());
                TileEntities[TileEntities.Count - 1].Position = position;
                TileEntities[TileEntities.Count - 1].CollisionType = CollisionType.SemiSolid;
                TileEntities[TileEntities.Count - 1].EntityType = StaticEntityType.Platform;
                AssetHandler.GiveAsset(TileEntities[TileEntities.Count - 1], 37);
            } else if(entityType == StaticEntityType.Pillar) {
                TileEntities.Add(new TileEntity());
                TileEntities[TileEntities.Count - 1].Position = position;
                TileEntities[TileEntities.Count - 1].CollisionType = CollisionType.Solid;
                TileEntities[TileEntities.Count - 1].EntityType = StaticEntityType.Pillar;
                AssetHandler.GiveAsset(TileEntities[TileEntities.Count - 1], 38);
            }
        }

        public static void LoadMap(int mapIndex) {
            if(mapIndex == 0) {
                SpawnEntity(new Vector2f(0, 910), StaticEntityType.Ground);
                SpawnEntity(new Vector2f(248, 910), StaticEntityType.Ground);
                SpawnEntity(new Vector2f(496, 910), StaticEntityType.Ground);
                SpawnEntity(new Vector2f(744, 910), StaticEntityType.Ground);
                SpawnEntity(new Vector2f(992, 910), StaticEntityType.Ground);
                SpawnEntity(new Vector2f(1240, 910), StaticEntityType.Ground);

                SpawnEntity(new Vector2f(0, 790), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(0, 670), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(0, 550), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(0, 430), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(0, 310), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(0, 190), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(0, 70), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(0, -50), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(0, -170), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(0, -290), StaticEntityType.Pillar);

                SpawnEntity(new Vector2f(1415, 790), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(1415, 670), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(1415, 550), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(1415, 430), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(1415, 310), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(1415, 190), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(1415, 70), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(1415, -50), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(1415, -170), StaticEntityType.Pillar);
                SpawnEntity(new Vector2f(1415, -290), StaticEntityType.Pillar);

                SpawnEntity(new Vector2f(600, 700), StaticEntityType.Ground);
            }
        }

    }
}
