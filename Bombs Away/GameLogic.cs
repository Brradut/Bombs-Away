using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
namespace Bombs_Away {
    public static class GameLogic {

        public static int TestForDeath() {
            //1- player 1 won, 2- player 2 won, 0- draw, 3-nobody lost
            bool player1Lost = false;
            bool player2Lost = false;
            for(int i = 0; i < World.TileEntities.Count; i++) {
                if(World.TileEntities[i].EntityType == World.StaticEntityType.Explosion) {
                    if(Program.player1.CollisionBox.Intersects(World.TileEntities[i].CollisionBox)) {
                        if(Program.player1.AgeUntilNotInvincible < Program.player1.Age && !player1Lost) {
                            player1Lost = true;
                            Program.counter.X++;
                            AssetHandler.GiveAsset(PlayerInterfaceLogic.GUIElements[8], 51 + Program.counter.X);
                        }
                        
                    }
                    if(Program.player2.CollisionBox.Intersects(World.TileEntities[i].CollisionBox)) {
                        if(Program.player2.AgeUntilNotInvincible < Program.player2.Age && !player2Lost) {
                            player2Lost = true;
                            Program.counter.Y++;
                            AssetHandler.GiveAsset(PlayerInterfaceLogic.GUIElements[9], 51 + Program.counter.Y);
                        }
                        
                    }
                }  
            }
            if(player1Lost && player2Lost) {
                Program.player1.AgeUntilNotInvincible = Program.player1.Age + 2;
                Program.player2.AgeUntilNotInvincible = Program.player2.Age + 2;
                return 0;
            }

            if(player2Lost) {
                Program.player2.AgeUntilNotInvincible = Program.player2.Age + 2;
                return 1;
            }

            if(player1Lost) {
                Program.player1.AgeUntilNotInvincible = Program.player1.Age + 2;
                return 2;
            }
                
            return 3;

        }

        public static void LoadAllSpawnModes() {
            World.SpawnModes.Add(new SpawnMode(0.8f, 1, 30, new Vector2f(0, 0), 2, 5));
            World.SpawnModes.Add(new SpawnMode(0.8f, 7, 90, new Vector2f(0, 0), 2, 5));
            World.SpawnModes.Add(new SpawnMode(1.2f, 1, 60, new Vector2f(0, -15), 4, 10));
            World.SpawnModes.Add(new SpawnMode(0.8f, 1, 45, new Vector2f(15, 0), 2, 5));
            World.SpawnModes.Add(new SpawnMode(0.8f, 1, 120, new Vector2f(0, -20), 2, 5));
            World.SpawnModes.Add(new SpawnMode(0, 1, 120, new Vector2f(0, 10), 0.4f, 1));
        }

    }
}
