using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace TheDayAfter_XNA_Project
{
    public static class Player
    {
       
        public static Vector2 position = new Vector2(320,320);
        public static double rotation=0;
        public static SpriteAnimation sprite;
        public static string state="Idle";

        public static void Draw(SpriteBatch spriteBatch)
        {
            Player.sprite.Draw(spriteBatch);
        }
        public static void ShadowDraw(SpriteBatch spriteBatch)
        {
            Player.sprite.ShadowDraw(spriteBatch);
        }

        internal static void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);

            #region Player Movement
            if (InputHandler.IsKeyPressed(Keys.A))
            {
                sprite.MoveBy(3 * Math.Sin(Player.sprite.Rotation - Math.PI / 2), -3 * Math.Cos(Player.sprite.Rotation - Math.PI / 2));
                state = "Walk";
            }
            else if (InputHandler.IsKeyPressed(Keys.W)) 
            {
                
                sprite.MoveBy(3 * Math.Cos(Player.sprite.Rotation - Math.PI / 2), 3 * Math.Sin(Player.sprite.Rotation - Math.PI / 2));
                state = "Walk";
            }
            else if (InputHandler.IsKeyPressed(Keys.D)) 
            {
                sprite.MoveBy(-3 * Math.Sin(Player.sprite.Rotation - Math.PI / 2), 3 * Math.Cos(Player.sprite.Rotation - Math.PI / 2));
                state = "Walk";
            }
            else if (InputHandler.IsKeyPressed(Keys.S))
            {
                sprite.MoveBy(-3 * Math.Cos(Player.sprite.Rotation - Math.PI / 2), -3 * Math.Sin(Player.sprite.Rotation - Math.PI / 2));
                state = "Walk";
            }
            else
            {
                state = "Idle";
            }
            if (state != sprite.CurrentAnimation)
                sprite.CurrentAnimation = state;

            sprite.Rotation = (float)(Math.Atan2((InputHandler.GetMousePos().Y - 320) , (InputHandler.GetMousePos().X - 320)) + Math.PI/2);
            #endregion
        }
    }
}
