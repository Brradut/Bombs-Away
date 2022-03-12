using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
namespace Bombs_Away
{
    class Program
    {
        public static RenderWindow MainWindow;
        public static Clock GameClock;
        public static float ElapsedTime;
        public static Player player1;
        public static Player player2;
        public static Sprite bg;
        public static Sprite info;
        public static Image icon;
        public static bool GamePaused = false;
        public static Vector2i counter = new Vector2i(0, 0);
        public static int GameState = 0;
        //0 - Main Menu, 1-Info, 2-PreGame, 3-InGame, 4-PostGame, 5-GamePaused

        public static Vector2f ScreenResolution = new Vector2f(1, 1);

        static void Game() {
            GameClock.Restart();
            AssetHandler.LoadAllAssets();
            AssetHandler.LoadAllAnimations();
            GameLogic.LoadAllSpawnModes();
            PlayerInterfaceLogic.LoadGUIElements();

            while(MainWindow.IsOpen) {
                MainWindow.DispatchEvents();
                ElapsedTime += GameClock.Restart().AsSeconds();

                if(ElapsedTime >= World.FrameRate) {
                    switch(GameState) {
                        case 0:
                        MainWindow.Clear(Color.Red);
                        MainWindow.Draw(bg);
                        PlayerInterfaceLogic.DrawMainMenu();
                        break;

                        case 1:
                        MainWindow.Clear(Color.Red);
                        MainWindow.Draw(info);
                        PlayerInterfaceLogic.DrawInfoScreen();
                        break;

                        case 2:
                        World.MovingEntities.Clear();
                        World.TileEntities.Clear();
                        player1 = new Player();
                        player2 = new Player();
                        player2.Position = new Vector2f(100, 700);
                        player1.Position = new Vector2f(1240, 700);
                        player1.Direction = -1;
                        player2.Direction = 1;
                        player1.EntityType = World.MovingEntityType.Barrel;
                        player2.EntityType = World.MovingEntityType.Bomber;
                        player1.Mode = World.SpawnModes[0];
                        player2.Mode = World.SpawnModes[0];
                        AssetHandler.UpdateAsset(player1);
                        AssetHandler.UpdateAsset(player2);

                        World.MovingEntities.Add(player1);
                        World.MovingEntities.Add(player2);

                        counter = new Vector2i(0, 0);
                        PlayerInterfaceLogic.LoadGUIElements();

                        World.LoadMap(0);
                        GameState = 5;
                        break;

                        case 3:
                        World.HandleExistance();

                        MainWindow.Clear(Color.Red);
                        MainWindow.Draw(bg);

                        for(int i = 0; i < World.TileEntities.Count; i++) {
                            World.TileEntities[i].Age += World.FrameRate;
                            AssetHandler.UpdateAsset(World.TileEntities[i]);
                            if(World.TileEntities[i].EntityType != World.StaticEntityType.Explosion)
                                MainWindow.Draw(World.TileEntities[i]);
                        }

                        for(int i = 0; i < World.MovingEntities.Count; i++) {
                            World.MovingEntities[i].Age += World.FrameRate;
                            World.HandlePassive(World.MovingEntities[i]);
                            Physics.CalculateVelocity(World.MovingEntities[i]);
                            Physics.Move(World.MovingEntities[i], World.TileEntities);
                            AssetHandler.UpdateAsset(World.MovingEntities[i]);
                            MainWindow.Draw(World.MovingEntities[i]);
                        }


                        for(int i = 0; i < World.TileEntities.Count; i++) {
                            if(World.TileEntities[i].EntityType == World.StaticEntityType.Explosion)
                                MainWindow.Draw(World.TileEntities[i]);
                        }


                        GameLogic.TestForDeath();

                        PlayerInterfaceLogic.DrawScores();
                        if(counter.X == 9 || counter.Y == 9) {
                            GameState = 4;
                        }
                        break;

                        case 4:
                        MainWindow.Clear(Color.Red);
                        MainWindow.Draw(bg);

                        for(int i = 0; i < World.TileEntities.Count; i++) {
                            if(World.TileEntities[i].EntityType != World.StaticEntityType.Explosion)
                                MainWindow.Draw(World.TileEntities[i]);
                        }

                        for(int i = 0; i < World.MovingEntities.Count; i++) {
                            MainWindow.Draw(World.MovingEntities[i]);
                        }


                        for(int i = 0; i < World.TileEntities.Count; i++) {
                            if(World.TileEntities[i].EntityType == World.StaticEntityType.Explosion)
                                MainWindow.Draw(World.TileEntities[i]);
                        }
                        PlayerInterfaceLogic.DrawScores();

                        if(counter.X > counter.Y) {
                            PlayerInterfaceLogic.DrawWinScreen(0);
                        }
                        if(counter.X < counter.Y) {
                            PlayerInterfaceLogic.DrawWinScreen(1);
                        }
                        if(counter.X == counter.Y){
                            PlayerInterfaceLogic.DrawWinScreen(2);
                        }
                        
                        break;

                        case 5:
                        MainWindow.Clear(Color.Red);
                        MainWindow.Draw(bg);

                        for(int i = 0; i < World.TileEntities.Count; i++) {
                            if(World.TileEntities[i].EntityType != World.StaticEntityType.Explosion)
                                MainWindow.Draw(World.TileEntities[i]);
                        }

                        for(int i = 0; i < World.MovingEntities.Count; i++) {
                            MainWindow.Draw(World.MovingEntities[i]);
                        }


                        for(int i = 0; i < World.TileEntities.Count; i++) {
                            if(World.TileEntities[i].EntityType == World.StaticEntityType.Explosion)
                                MainWindow.Draw(World.TileEntities[i]);
                        }

                        PlayerInterfaceLogic.DrawPauseScreen();
                        break;
                    
                    }
                    ElapsedTime -= World.FrameRate;
                    MainWindow.Display();
                }

            }

        }


        static void Main(string[] args) 
        {
            
            MainWindow = new RenderWindow(new VideoMode(1440, 960), "Bombs Away", Styles.None);
            MainWindow.Size = new Vector2u(Convert.ToUInt32(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width), Convert.ToUInt32(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height)); //(new SFML.Graphics.View(new FloatRect(0, 0, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width)));
            MainWindow.Position = new Vector2i(0, 0);
            ScreenResolution.X = 1440f / System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            ScreenResolution.Y = 960f / System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            icon = new Image("Assets\\Icon.png");
            MainWindow.SetVerticalSyncEnabled(true);
            GameClock = new Clock();
            MainWindow.KeyPressed += MainWindow_KeyPressed;
            MainWindow.LostFocus += MainWindow_LostFocus;
            MainWindow.KeyReleased += MainWindow_KeyReleased;
            MainWindow.Closed += MainWindow_Closed;
            MainWindow.MouseButtonPressed += MainWindow_MouseButtonPressed;
            MainWindow.MouseMoved += MainWindow_MouseMove;
            player1 = new Player();
            player2 = new Player();
            bg = new Sprite(new Texture("Assets\\bg1.png"), new IntRect(new Vector2i(0, 0), new Vector2i(1440, 960)));
            info = new Sprite(new Texture("Assets\\info.png"), new IntRect(new Vector2i(0, 0), new Vector2i(1440, 960)));
            TextureLoader.LoadTexture("Assets\\Characters.png");
            TextureLoader.LoadTexture("Assets\\Platforms.png");
            TextureLoader.LoadTexture("Assets\\GUI.png");
            Game();
        }

        static void MainWindow_Resized(object sender, SizeEventArgs e) {
            
        }

        static void MainWindow_LostFocus(object sender, EventArgs e) {
            if(GameState == 3) {
                GameState = 5;
            }
        }

        static void MainWindow_MouseMove(object sender, MouseMoveEventArgs e) {
            if(GameState == 0) {
                if(PlayerInterfaceLogic.GUIElements[0].CollisionBox.Contains(e.X * ScreenResolution.X, e.Y * ScreenResolution.Y)) {
                    AssetHandler.GiveAsset(PlayerInterfaceLogic.GUIElements[0], 43);
                } else {
                    AssetHandler.GiveAsset(PlayerInterfaceLogic.GUIElements[0], 39);
                }
                if(PlayerInterfaceLogic.GUIElements[1].CollisionBox.Contains(e.X * ScreenResolution.X, e.Y * ScreenResolution.Y)) {
                    AssetHandler.GiveAsset(PlayerInterfaceLogic.GUIElements[1], 44);
                } else {
                    AssetHandler.GiveAsset(PlayerInterfaceLogic.GUIElements[1], 40);
                }
                if(PlayerInterfaceLogic.GUIElements[2].CollisionBox.Contains(e.X * ScreenResolution.X, e.Y * ScreenResolution.Y)) {
                    AssetHandler.GiveAsset(PlayerInterfaceLogic.GUIElements[2], 45);
                } else {
                    AssetHandler.GiveAsset(PlayerInterfaceLogic.GUIElements[2], 41);
                }
            } else if(GameState == 1 || GameState == 4) {
                if(PlayerInterfaceLogic.GUIElements[3].CollisionBox.Contains(e.X * ScreenResolution.X, e.Y * ScreenResolution.Y)) {
                    AssetHandler.GiveAsset(PlayerInterfaceLogic.GUIElements[3], 46);
                } else {
                    AssetHandler.GiveAsset(PlayerInterfaceLogic.GUIElements[3], 42);
                }
            }
        }

        static void MainWindow_Closed(object sender, EventArgs e) {
            MainWindow.Close();
        }

        static void MainWindow_MouseButtonPressed(object sender, MouseButtonEventArgs e) {
            if(GameState == 0) {
                if(e.Button == Mouse.Button.Left) {
                    if(PlayerInterfaceLogic.GUIElements[0].CollisionBox.Contains(e.X * ScreenResolution.X, e.Y * ScreenResolution.Y)) {
                        GameState = 2;
                        PlayerInterfaceLogic.LoadGUIElements();
                    } else if(PlayerInterfaceLogic.GUIElements[1].CollisionBox.Contains(e.X * ScreenResolution.X, e.Y * ScreenResolution.Y)) {
                        GameState = 1;
                        PlayerInterfaceLogic.LoadGUIElements();
                    } else if(PlayerInterfaceLogic.GUIElements[2].CollisionBox.Contains(e.X * ScreenResolution.X, e.Y * ScreenResolution.Y)) {
                        MainWindow.Close();
                    }

                }
            } else if(GameState == 1 || GameState == 4) {
                if(PlayerInterfaceLogic.GUIElements[3].CollisionBox.Contains(e.X * ScreenResolution.X, e.Y * ScreenResolution.Y)) {
                    GameState = 0;
                    PlayerInterfaceLogic.LoadGUIElements();
                }
            } else if(GameState == 5) {
               if(e.Button == Mouse.Button.Left) {
                    World.SpawnEntity(new Vector2f(e.X, e.Y), World.StaticEntityType.Platform);
                } else if(e.Button == Mouse.Button.Right) {
                    World.SpawnEntity(new Vector2f(e.X, e.Y), World.StaticEntityType.Ground);
                }
            }
           
        }

         static void MainWindow_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            if(e.Code == Keyboard.Key.A)
            {
                player1.IsMovingLeft = true;
               
            }
            if(e.Code == Keyboard.Key.D)
            {
                player1.IsMovingRight = true;
                
            }
            if(e.Code == Keyboard.Key.S)
            {
                player1.IsMovingDown = true;
                

            }
            if(e.Code == Keyboard.Key.W)
            {
                player1.IsMovingUp = true;
                
            }

            if(e.Code == Keyboard.Key.Escape) {
                if(GameState == 3)
                    GameState = 5;
                else if(GameState == 5)
                    GameState = 3;
            }

            if(e.Code == Keyboard.Key.Left)
            {
                player2.IsMovingLeft = true;
               
            }
            if(e.Code == Keyboard.Key.Right)
            {
                player2.IsMovingRight = true;
               
            }
            if(e.Code == Keyboard.Key.Down)
            {
                player2.IsMovingDown = true;

            }
            if(e.Code == Keyboard.Key.Up)
            {
                player2.IsMovingUp = true;
            }

            if(e.Code == Keyboard.Key.Num1) {
                player1.Mode = World.SpawnModes[0];
            }
            if(e.Code == Keyboard.Key.Num2) {
                player1.Mode = World.SpawnModes[1];
            }
            if(e.Code == Keyboard.Key.Num3) {
                player1.Mode = World.SpawnModes[2];
            }
            if(e.Code == Keyboard.Key.Num4) {
                player1.Mode = World.SpawnModes[3];
            }
            if(e.Code == Keyboard.Key.Num5) {
                player1.Mode = World.SpawnModes[4];
            }
            if(e.Code == Keyboard.Key.Num6) {
                player1.Mode = World.SpawnModes[5];
            }

            if(e.Code == Keyboard.Key.Numpad1) {
                player2.Mode = World.SpawnModes[0];
            }
            if(e.Code == Keyboard.Key.Numpad2) {
                player2.Mode = World.SpawnModes[1];
            }
            if(e.Code == Keyboard.Key.Numpad3) {
                player2.Mode = World.SpawnModes[2];
            }
            if(e.Code == Keyboard.Key.Numpad4) {
                player2.Mode = World.SpawnModes[3];
            }
            if(e.Code == Keyboard.Key.Numpad5) {
                player2.Mode = World.SpawnModes[4];
            }
            if(e.Code == Keyboard.Key.Numpad6) {
                player2.Mode = World.SpawnModes[5];
            }
        }

        static void MainWindow_KeyReleased(object sender, SFML.Window.KeyEventArgs e) {
            if(e.Code == Keyboard.Key.A)
            {
                player1.IsMovingLeft = false;
               
            }
            if(e.Code == Keyboard.Key.D)
            {
                player1.IsMovingRight = false;
                
            }
            if(e.Code == Keyboard.Key.S)
            {
                player1.IsMovingDown = false;
              

            }
            if(e.Code == Keyboard.Key.W)
            {
                player1.IsMovingUp = false;
                
            }


            if(e.Code == Keyboard.Key.Left)
            {
                player2.IsMovingLeft = false;
               
            }
            if(e.Code == Keyboard.Key.Right)
            {
                player2.IsMovingRight = false;
               
            }
            if(e.Code == Keyboard.Key.Down)
            {
                player2.IsMovingDown = false;

            }
            if(e.Code == Keyboard.Key.Up)
            {
                player2.IsMovingUp = false;
            }
        }

       
    }
}
    