using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheDayAfter_XNA_Project.UI;

namespace TheDayAfter_XNA_Project.TestClass
{
    public static class DebugFrame
    {
        static bool toggle = false;
        static bool BoxTest = false;
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (InputHandler.ks.IsKeyDown(Keys.F1) && InputHandler.oldks.IsKeyUp(Keys.F1))
                toggle = !toggle;
            if (InputHandler.ks.IsKeyDown(Keys.F2) && InputHandler.oldks.IsKeyUp(Keys.F2))
                BoxTest = !BoxTest;

            if (toggle)
            {
                string debugText = "Mouse State:\n     Window Pos: "
                    + InputHandler.GetMousePos().ToString() + "\n     World Pos: "
                    + (InputHandler.GetMousePos() + Player.position - new Vector2(320)).ToString()
                    // + "\n Tile: " + (int)(((InputHandler.GetMousePos() + Player.position - new Vector2(320)).X) / 64) + " , " + (int)(((InputHandler.GetMousePos() + Player.position - new Vector2(320)).Y) / 64);
                    + "\n     Tile: " + InputHandler.GetTile()
                    +"\nMouse Clicks:";


                if (InputHandler.IsMouseLClick())
                    debugText += "\n     Left Mouse Click";
                if (InputHandler.IsMouseRClick())
                    debugText += "\n     Right Mouse Click";
                if (InputHandler.IsMouseMClick())
                    debugText += "\n     Middle Mouse Click";

                debugText += "\nMouse Scroll: \n     " + InputHandler.ScrollValue;

                debugText += "\nPlayer State: \n     " + Player.position + "\n     Rot: " + (Player.sprite.Rotation-Math.PI/2);//(float)((int)(Player.sprite.Rotation*1000))/1000;
                if (InputHandler.Ms.X >= Player.position.X && InputHandler.Ms.X < Player.position.X + 32 &&
                    InputHandler.Ms.Y >= Player.position.Y && InputHandler.Ms.Y < Player.position.Y + 32)
                {
                    
                }
                debugText += "\nMouse-X:" + InputHandler.GetMousePos().X + "\nMouse-Y:" + InputHandler.GetMousePos().Y;
                spriteBatch.DrawString(Database.Fonts["debug"], debugText, new Vector2(10, 10), Color.White);
            }

            if (BoxTest)
            {
                DialogueBox Test = new DialogueBox(new Rectangle(200, 200, 240, 240), Database.Fonts["debug"], Database.BoxTexture["boxtest"], "TEST TEST TEST/nTEST TEST TEST/nTEST TEST TEST/n", 100);
                Test.Draw(spriteBatch);
            }
        }
    }
}