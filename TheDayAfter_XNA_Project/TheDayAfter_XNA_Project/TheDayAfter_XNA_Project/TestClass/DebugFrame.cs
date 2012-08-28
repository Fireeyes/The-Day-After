using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace TheDayAfter_XNA_Project
{
    public static class DebugFrame
    {
        static bool toggle = false;
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (InputHandler.ks.IsKeyDown(Keys.F1) && InputHandler.oldks.IsKeyUp(Keys.F1))
                toggle = !toggle;
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

                debugText += "\nPlayer State: \n     " + Player.position + "\n     Sin(Rot): " + Math.Sin((float)((int)(Player.sprite.Rotation * 1000)) / 1000) + "\n     Rot: " +((float)((int)(Player.sprite.Rotation * 1000)) / 1000);
                if (InputHandler.Ms.X >= Player.position.X && InputHandler.Ms.X < Player.position.X + 32 &&
                    InputHandler.Ms.Y >= Player.position.Y && InputHandler.Ms.Y < Player.position.Y + 32)
                {
                    
                }
                spriteBatch.DrawString(Database.Fonts["debug"], debugText, new Vector2(10, 10), Color.White);
            }

        }
    }
}