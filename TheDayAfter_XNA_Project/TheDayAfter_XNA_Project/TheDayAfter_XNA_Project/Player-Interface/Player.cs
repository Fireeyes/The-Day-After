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
        //public static Texture2D texture;
        public static SpriteAnimation sprite;
        public static Vector2 position = new Vector2(320,320);
        public static double rotation=0;
        public static string state="Idle";
        public static void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, new Vector2(320,320), null, Color.White, (float)rotation, new Vector2(32, 32), 1, SpriteEffects.None, 0);
            sprite.Draw(spriteBatch);
        }

        internal static void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
            if (InputHandler.IsKeyPressed(Keys.A))
            {
                sprite.MoveBy(-5, 0);

                state = "Walk";
            }
            else if (InputHandler.IsKeyPressed(Keys.W)) // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                //sprite.MoveBy(Math.Sin(sprite.Rotation) * 5, Math.Cos(sprite.Rotation) * 5);                 
                sprite.MoveBy(0, -5);
                state = "Walk";
            }
            else if (InputHandler.IsKeyPressed(Keys.D)) // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                sprite.MoveBy(5, 0);
                state = "Walk";
            }
            else if (InputHandler.IsKeyPressed(Keys.S))
            {
                sprite.MoveBy(0, 5);
                state = "Walk";
            }
            else
                state = "Idle";
            if (sprite.CurrentAnimation != state)
                sprite.CurrentAnimation = state;



            sprite.Rotation = (float)(Math.Atan2((InputHandler.GetMousePos().Y - 320) , (InputHandler.GetMousePos().X - 320)) + Math.PI/2);
            
        }
    }
}
