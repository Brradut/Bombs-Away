using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace Bombs_Away {
    public static class PlayerInterfaceLogic {
        public static List<TileEntity> GUIElements = new List<TileEntity>();

        public static void LoadGUIElements() {
            GUIElements.Clear();
            //Play 
            GUIElements.Add(new TileEntity());
            AssetHandler.GiveAsset(GUIElements[0], 39);
            GUIElements[0].Position = new Vector2f(620, 200); 
            //Info
            GUIElements.Add(new TileEntity());
            AssetHandler.GiveAsset(GUIElements[1], 40);
            GUIElements[1].Position = new Vector2f(620, 400);
            //Exit
            GUIElements.Add(new TileEntity());
            AssetHandler.GiveAsset(GUIElements[2], 41);
            GUIElements[2].Position = new Vector2f(620, 600);
            //Back
            GUIElements.Add(new TileEntity());
            AssetHandler.GiveAsset(GUIElements[3], 42);
            GUIElements[3].Position = new Vector2f(1200, 800);
            //Game Pause
            GUIElements.Add(new TileEntity());
            AssetHandler.GiveAsset(GUIElements[4], 47);
            GUIElements[4].Position = new Vector2f(520, 300);
            //Bomber WIn
            GUIElements.Add(new TileEntity());
            AssetHandler.GiveAsset(GUIElements[5], 48);
            GUIElements[5].Position = new Vector2f(570, 600);
            //Barrel Win
            GUIElements.Add(new TileEntity());
            AssetHandler.GiveAsset(GUIElements[6], 49);
            GUIElements[6].Position = new Vector2f(570, 600);
            //Draw
            GUIElements.Add(new TileEntity());
            AssetHandler.GiveAsset(GUIElements[7], 50);
            GUIElements[7].Position = new Vector2f(570, 600);
            //Score Bomber
            GUIElements.Add(new TileEntity());
            AssetHandler.GiveAsset(GUIElements[8], 51);
            GUIElements[8].Position = new Vector2f(0, 10);
            //Score Barrel
            GUIElements.Add(new TileEntity());
            AssetHandler.GiveAsset(GUIElements[9], 51);
            GUIElements[9].Position = new Vector2f(1310, 10);
        }

        public static void DrawMainMenu() {
            Program.MainWindow.Clear(Color.Red);
            Program.MainWindow.Draw(Program.bg);
            Program.MainWindow.Draw(GUIElements[0]);
            Program.MainWindow.Draw(GUIElements[1]);
            Program.MainWindow.Draw(GUIElements[2]);
        }

        public static void DrawWinScreen(int win) {
            if(win == 0) {
                Program.MainWindow.Draw(GUIElements[5]);
            }
            if(win == 1){
                Program.MainWindow.Draw(GUIElements[6]);
            }
            if(win == 2) {
                Program.MainWindow.Draw(GUIElements[7]);
            }
            Program.MainWindow.Draw(GUIElements[3]);
        }
        public static void DrawPauseScreen() {
            Program.MainWindow.Draw(GUIElements[4]);
        }
        public static void DrawScores() {
            Program.MainWindow.Draw(GUIElements[8]);
            Program.MainWindow.Draw(GUIElements[9]);
        }
        public static void DrawInfoScreen() {
            Program.MainWindow.Draw(GUIElements[3]);
        }

    }
}
