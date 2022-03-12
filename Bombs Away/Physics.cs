using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace Bombs_Away
{
    public static class Physics
    {

        public static FloatRect ApplyOffset(GameObject gameObject) {
            FloatRect rect = gameObject.GetGlobalBounds();
            rect.Top += gameObject.OffsetPosition.Y;
            rect.Left += gameObject.OffsetPosition.X;
            rect.Height += gameObject.OffsetSize.Y;
            rect.Width += gameObject.OffsetSize.X;
            return rect;
        }

        public static bool WillCollideXY(MovingEntity movingEntity, TileEntity tileEntity, Vector2f velocity) {
            FloatRect rect1 = movingEntity.CollisionBox;
            FloatRect rect2 = tileEntity.CollisionBox;
            Vector2u position = RelativePosition(movingEntity, tileEntity);
            if(velocity.X < 0) {
                rect1.Left += velocity.X;
                rect1.Width -= velocity.X;
            } else if(velocity.X > 0){
                rect1.Width += velocity.X;
            }
            if(velocity.Y < 0) {
                rect1.Top += velocity.Y;
                rect1.Height -= velocity.Y;
            } else if(velocity.Y > 0){
                rect1.Height += velocity.Y;
            }
            if(rect1.Intersects(rect2) == true)
                return true;
            return false;
        }

        public static bool WillCollideX(MovingEntity movingEntity, TileEntity tileEntity, Vector2f velocity) {
            FloatRect rect1 = movingEntity.CollisionBox;
            FloatRect rect2 = tileEntity.CollisionBox;
            Vector2u position = RelativePosition(movingEntity, tileEntity);

            if(velocity.X < 0){
                rect1.Left += velocity.X;
                rect1.Width -= velocity.X;
            } else if(velocity.X > 0){
                rect1.Width += velocity.X;
            }
            
            if(rect1.Intersects(rect2) == true)
                return true;
            return false;
        }

        public static bool WillCollideY(MovingEntity movingEntity, TileEntity tileEntity, Vector2f velocity)
        {
            FloatRect rect1 = movingEntity.CollisionBox;
            FloatRect rect2 = tileEntity.CollisionBox;
            Vector2u position = RelativePosition(movingEntity, tileEntity);

            if(velocity.Y < 0)
            {
                rect1.Top += velocity.Y;
                rect1.Height -= velocity.Y;
            } else if(velocity.Y > 0)
            {
                rect1.Height += velocity.Y;
            }

            if(rect1.Intersects(rect2) == true)
                return true;
            return false;
        }

        public static void CalculateVelocity(MovingEntity movingEntity) {
            if(movingEntity.EntityType == World.MovingEntityType.Barrel || movingEntity.EntityType == World.MovingEntityType.Bomber) {

                if(movingEntity.IsOnGround == false) {
                    if(movingEntity.LastPosition == movingEntity.Position) {
                        movingEntity.JumpedMaxHeight = false;
                    } else if(movingEntity.LastPosition.Y - movingEntity.Position.Y >= 300 || movingEntity.IsMovingUp == false) {
                        movingEntity.LastPosition.Y = 10000;
                        if(movingEntity.Velocity.Y > movingEntity.TerminalVelocity.Y) {
                            movingEntity.Velocity.Y = movingEntity.TerminalVelocity.Y;
                        } else {
                            movingEntity.Velocity.Y += World.GravitationalAcceleration * World.FrameRate;
                        }
                    }
                    if(movingEntity.Velocity.X != 0) {
                        movingEntity.Velocity.X -= World.AirResistance * World.FrameRate * movingEntity.Direction;
                        if((movingEntity.Velocity.X < 0 && movingEntity.Direction == 1) || (movingEntity.Velocity.X > 0 && movingEntity.Direction == -1)) {
                            movingEntity.Velocity.X = 0;
                        }
                    }
                } else {

                    if(movingEntity.Velocity.X != 0) {
                        if(movingEntity.IsInGround) {
                            movingEntity.Velocity.X -= World.HorizontalAcceleration * World.FrameRate * movingEntity.Direction;
                            if((movingEntity.Velocity.X < 0 && movingEntity.Direction == 1) || (movingEntity.Velocity.X > 0 && movingEntity.Direction == -1)) {
                                movingEntity.Velocity.X = 0;
                            }
                        } else {
                            movingEntity.Velocity.X -= World.HorizontalAcceleration * World.FrameRate * movingEntity.Direction;
                            if((movingEntity.Velocity.X < 0 && movingEntity.Direction == 1) || (movingEntity.Velocity.X > 0 && movingEntity.Direction == -1)) {
                                movingEntity.Velocity.X = 0;
                            }
                        }

                    }
                    if(movingEntity.IsInGround) {
                        if(movingEntity.LastPosition == movingEntity.Position) {
                            movingEntity.JumpedMaxHeight = false;
                        } else if((movingEntity.LastPosition.Y - movingEntity.Position.Y >= 300 || movingEntity.IsMovingUp == false)) {
                            movingEntity.LastPosition.Y = 10000;
                            if(movingEntity.Velocity.Y > movingEntity.TerminalVelocity.Y) {
                                movingEntity.Velocity.Y = movingEntity.TerminalVelocity.Y;
                            } else {
                                movingEntity.Velocity.Y += World.GravitationalAcceleration * World.FrameRate;
                            }
                        } 
                       
                    } else {
                        movingEntity.JumpedMaxHeight = false;
                    }
                }
                if(movingEntity.IsMovingUp && !movingEntity.JumpedMaxHeight) {
                    movingEntity.Velocity.Y = -movingEntity.JumpingForce;
                    movingEntity.JumpedMaxHeight = true;
                }
                if(movingEntity.IsMovingLeft) {
                    movingEntity.Velocity.X = -movingEntity.WalkingForce;
                    movingEntity.Direction = -1;
                }
                if(movingEntity.IsMovingRight) {
                    movingEntity.Velocity.X = movingEntity.WalkingForce;
                    movingEntity.Direction = 1;
                }
                if(movingEntity.IsMovingRight && movingEntity.IsMovingLeft) {
                    movingEntity.Velocity.X = 0;
                }
                if(movingEntity.IsMovingDown) {
                    if(movingEntity.IsOnGround) {
                        movingEntity.Position += new Vector2f(0, 1);
                        TestInGround(movingEntity, World.TileEntities);
                    }
                }
            } else if(movingEntity.EntityType == World.MovingEntityType.LittleBomb || movingEntity.EntityType == World.MovingEntityType.LittleBarrel) {
                if(movingEntity.IsOnGround == false) {
                    if(movingEntity.LastPosition.Y - movingEntity.Position.Y >= 300 || movingEntity.IsMovingUp == false) {
                        movingEntity.LastPosition.Y = 10000;
                        if(movingEntity.Velocity.Y > movingEntity.TerminalVelocity.Y) {
                            movingEntity.Velocity.Y = movingEntity.TerminalVelocity.Y;
                        } else {
                            movingEntity.Velocity.Y += World.GravitationalAcceleration * World.FrameRate;
                        }
                    }
                    if(movingEntity.Velocity.X != 0) {
                        movingEntity.Velocity.X -= World.FrameRate * movingEntity.Direction;
                        if((movingEntity.Velocity.X < 0 && movingEntity.Direction == 1) || (movingEntity.Velocity.X > 0 && movingEntity.Direction == -1)) {
                            movingEntity.Velocity.X = 0;
                        }
                    }
                } else {

                    if(movingEntity.Velocity.X != 0) {
                        if(movingEntity.IsInGround) {
                            movingEntity.Velocity.X -= World.AirResistance * World.FrameRate * movingEntity.Direction;
                            if((movingEntity.Velocity.X < 0 && movingEntity.Direction == 1) || (movingEntity.Velocity.X > 0 && movingEntity.Direction == -1)) {
                                movingEntity.Velocity.X = 0;
                            }
                        } else {
                            movingEntity.Velocity.X -= World.HorizontalAcceleration * World.FrameRate * movingEntity.Direction;
                            if((movingEntity.Velocity.X < 0 && movingEntity.Direction == 1) || (movingEntity.Velocity.X > 0 && movingEntity.Direction == -1)) {
                                movingEntity.Velocity.X = 0;
                            }
                        }

                    }
                    if(movingEntity.IsInGround) {
                        if((movingEntity.LastPosition.Y - movingEntity.Position.Y >= 300 || movingEntity.IsMovingUp == false)) {
                            movingEntity.LastPosition.Y = 10000;
                            if(movingEntity.Velocity.Y > movingEntity.TerminalVelocity.Y) {
                                movingEntity.Velocity.Y = movingEntity.TerminalVelocity.Y;
                            } else {
                                movingEntity.Velocity.Y += World.GravitationalAcceleration * World.FrameRate;
                            }
                        }
                    } else {
                        movingEntity.JumpedMaxHeight = false;
                    }
                }
                if(movingEntity.IsMovingUp && !movingEntity.JumpedMaxHeight) {
                    movingEntity.Velocity.Y = -movingEntity.JumpingForce;
                    movingEntity.JumpedMaxHeight = true;
                }
                if(movingEntity.IsMovingLeft) {
                    movingEntity.Velocity.X = -movingEntity.WalkingForce;
                    movingEntity.Direction = -1;
                }
                if(movingEntity.IsMovingRight) {
                    movingEntity.Velocity.X = movingEntity.WalkingForce;
                    movingEntity.Direction = 1;
                }
                if(movingEntity.IsMovingRight && movingEntity.IsMovingLeft) {
                    movingEntity.Velocity.X = 0;
                }
            }
            
        }
        
        public static Vector2u RelativePosition(GameObject object1, GameObject object2) {
            //above Y = 1,below Y = 0, inside Y = 2, Left X = 1, Right X = 0 ,inside Y = 2

            FloatRect rect1 = object1.CollisionBox;
            FloatRect rect2 = object2.CollisionBox;
            Vector2u v = new Vector2u();
            if(rect1.Top + rect1.Height <= rect2.Top) {
                v.Y = 1;
            } else if(rect1.Top >= rect2.Top + rect2.Height) {
                v.Y = 0;
            } else if(rect1.Top > rect2.Top || rect1.Top < rect2.Top + rect2.Height || rect1.Top + rect1.Height > rect2.Top || rect1.Top + rect1.Height < rect2.Top + rect2.Height) {
                v.Y = 2;
            }
            if(rect1.Left + rect1.Width <= rect2.Left) {
                v.X = 1;
            } else if(rect1.Left >= rect2.Left + rect2.Width) {
                v.X = 0;
            } else if(rect1.Left > rect2.Left || rect1.Left < rect2.Left + rect2.Width || rect1.Left + rect1.Width > rect2.Left || rect1.Left + rect1.Width < rect2.Left + rect2.Width) {
                v.X = 2;
            }
            return v;
        }

        public static bool TestOnGround(MovingEntity movingEntity, List<TileEntity> tileEntities) {
            for(int i = 0; i < tileEntities.Count; i++)
            {
                if(WillCollideY(movingEntity, tileEntities[i], new Vector2f(0, 1f)) && tileEntities[i].CollisionType != World.CollisionType.None && RelativePosition(movingEntity, tileEntities[i]).Y == 1)
                {
                        movingEntity.LastPosition = movingEntity.Position;
                        return true;
                    
                }
            }
            return false;
        }
        public static bool TestInGround(MovingEntity movingEntity, List<TileEntity> tileEntities) {
            for(int i = 0; i < tileEntities.Count; i++)
            {
                if(WillCollideY(movingEntity, tileEntities[i], new Vector2f(0, 0f)) && tileEntities[i].CollisionType == World.CollisionType.Solid) {
                    movingEntity.Position += new Vector2f(0, -1);
                }
                if(WillCollideY(movingEntity, tileEntities[i], new Vector2f(0, 0f)) && tileEntities[i].CollisionType == World.CollisionType.SemiSolid && (RelativePosition(movingEntity, tileEntities[i]).Y == 2 || RelativePosition(movingEntity, tileEntities[i]).X ==2))
                {
                 
                    return true;
                }
            }
            return false;
        }

        public static void HandleCollisionXY(MovingEntity movingEntity, TileEntity tileEntity) {
            Vector2u relativePosition = RelativePosition(movingEntity, tileEntity);
            Vector2f newPosition = movingEntity.Position;

            FloatRect movingEntityRect = movingEntity.CollisionBox;
            FloatRect tileEntityRect = tileEntity.CollisionBox;

            if(relativePosition.X == 1 && tileEntity.CollisionType == World.CollisionType.Solid) {
                newPosition.X = tileEntityRect.Left - movingEntityRect.Width - movingEntity.OffsetPosition.X;
                if(movingEntity.EntityType == World.MovingEntityType.LittleBomb || movingEntity.EntityType == World.MovingEntityType.LittleBarrel) {
                    movingEntity.Velocity.X = -movingEntity.Velocity.X * movingEntity.Mode.Bounciness;
                    movingEntity.Direction = -movingEntity.Direction;
                } 

            } else if(relativePosition.X == 0 && tileEntity.CollisionType == World.CollisionType.Solid) {
                newPosition.X = tileEntityRect.Left + tileEntityRect.Width - movingEntity.OffsetPosition.X;
                if(movingEntity.EntityType == World.MovingEntityType.LittleBomb || movingEntity.EntityType == World.MovingEntityType.LittleBarrel) {
                    movingEntity.Velocity.X = -movingEntity.Velocity.X * movingEntity.Mode.Bounciness;
                    movingEntity.Direction = -movingEntity.Direction;
                } 
            }
            if(relativePosition.Y == 1) {
                newPosition.Y = tileEntityRect.Top - movingEntityRect.Height - movingEntity.OffsetPosition.Y;
                if((movingEntity.EntityType == World.MovingEntityType.LittleBomb || movingEntity.EntityType == World.MovingEntityType.LittleBarrel) && movingEntity.Velocity.Y >= 1.5f) {
                    movingEntity.Velocity.Y = -movingEntity.Velocity.Y * movingEntity.Mode.Bounciness;
                } else {
                    movingEntity.Velocity.Y = 0;
                }

            } else if(relativePosition.Y == 0 && tileEntity.CollisionType == World.CollisionType.Solid) {
                movingEntity.LastPosition.Y = 10000;
                newPosition.Y = tileEntityRect.Top + tileEntityRect.Height - movingEntity.OffsetPosition.Y;
                if((movingEntity.EntityType == World.MovingEntityType.LittleBomb || movingEntity.EntityType == World.MovingEntityType.LittleBarrel) && movingEntity.Velocity.Y >= 1.5f) {
                    movingEntity.Velocity.Y = -movingEntity.Velocity.Y * movingEntity.Mode.Bounciness;
                } else {
                    movingEntity.Velocity.Y = 0;
                }

            }

            movingEntity.Position = newPosition;
        }

        public static void HandleCollisionX(MovingEntity movingEntity, TileEntity tileEntity)
        {
            Vector2u relativePosition = RelativePosition(movingEntity, tileEntity);
            Vector2f newPosition = movingEntity.Position;

            FloatRect movingEntityRect = movingEntity.CollisionBox;
            FloatRect tileEntityRect = tileEntity.CollisionBox;

            if(relativePosition.X == 1 && tileEntity.CollisionType == World.CollisionType.Solid)
            {
                newPosition.X = tileEntityRect.Left - movingEntityRect.Width - movingEntity.OffsetPosition.X;
                if(movingEntity.EntityType == World.MovingEntityType.LittleBomb || movingEntity.EntityType == World.MovingEntityType.LittleBarrel) {
                    movingEntity.Velocity.X = -movingEntity.Velocity.X * movingEntity.Mode.Bounciness;
                    movingEntity.Direction = -movingEntity.Direction;
                } else {
                    movingEntity.Velocity.X = 0;
                }
               
            } else if(relativePosition.X == 0 && tileEntity.CollisionType == World.CollisionType.Solid)
            {
                newPosition.X = tileEntityRect.Left + tileEntityRect.Width - movingEntity.OffsetPosition.X;
                if(movingEntity.EntityType == World.MovingEntityType.LittleBomb|| movingEntity.EntityType == World.MovingEntityType.LittleBarrel) {
                    movingEntity.Velocity.X = -movingEntity.Velocity.X * movingEntity.Mode.Bounciness;
                    movingEntity.Direction = -movingEntity.Direction;
                } else {
                    movingEntity.Velocity.X = 0;
                }
            }
            movingEntity.Position = newPosition;
        }

        public static void HandleCollisionY(MovingEntity movingEntity, TileEntity tileEntity) {
            Vector2u relativePosition = RelativePosition(movingEntity, tileEntity);
            Vector2f newPosition = movingEntity.Position;

            FloatRect movingEntityRect = movingEntity.CollisionBox;
            FloatRect tileEntityRect = tileEntity.CollisionBox;

            if(relativePosition.Y == 1){
                newPosition.Y = tileEntityRect.Top - movingEntityRect.Height - movingEntity.OffsetPosition.Y;
                if((movingEntity.EntityType == World.MovingEntityType.LittleBomb || movingEntity.EntityType == World.MovingEntityType.LittleBarrel) && movingEntity.Velocity.Y >=1.5f) {
                    movingEntity.Velocity.Y = -movingEntity.Velocity.Y * movingEntity.Mode.Bounciness;
                } else {
                    movingEntity.Velocity.Y = 0;
                }
                
            } else if(relativePosition.Y == 0 && tileEntity.CollisionType == World.CollisionType.Solid){
                movingEntity.LastPosition.Y = 10000;
                newPosition.Y = tileEntityRect.Top + tileEntityRect.Height - movingEntity.OffsetPosition.Y;
                if((movingEntity.EntityType == World.MovingEntityType.LittleBomb || movingEntity.EntityType == World.MovingEntityType.LittleBarrel) && movingEntity.Velocity.Y >=1.5f) {
                    movingEntity.Velocity.Y = -movingEntity.Velocity.Y * movingEntity.Mode.Bounciness;
                } else {
                    movingEntity.Velocity.Y = 0;
                }
               
            }

            movingEntity.Position = newPosition;
        }


        public static void Move(MovingEntity movingEntity, List<TileEntity> tileEntities) {
            int collide = 0;
            for(int i = 0; i < tileEntities.Count; i++)
            {
                if(tileEntities[i].CollisionType == World.CollisionType.Solid || tileEntities[i].CollisionType == World.CollisionType.SemiSolid)
                {

                    if(WillCollideX(movingEntity, tileEntities[i], movingEntity.Velocity))
                    {
                        HandleCollisionX(movingEntity, tileEntities[i]);
                        collide = 1;
                    }
                    if(WillCollideY(movingEntity, tileEntities[i], movingEntity.Velocity))
                    {
                        HandleCollisionY(movingEntity, tileEntities[i]);
                        collide = 1;
                    }
                }

            }
            for(int i = 0; i < tileEntities.Count; i++) {
                if(tileEntities[i].CollisionType == World.CollisionType.Solid || tileEntities[i].CollisionType == World.CollisionType.SemiSolid) {
                    if(collide == 0 && WillCollideXY(movingEntity, tileEntities[i], movingEntity.Velocity)) {
                        HandleCollisionXY(movingEntity, tileEntities[i]);
                    }
                }

            }
            movingEntity.Position += movingEntity.Velocity;
        }
    }
}
