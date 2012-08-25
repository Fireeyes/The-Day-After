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
        public static Texture2D texture;
        public static Vector2 position = new Vector2(100,100);
        public static double rotation=0;

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, (float)rotation, new Vector2(32, 32), 1, SpriteEffects.None, 0);
        }

        internal static void Update()
        {
            
            if (InputHandler.IsKeyPressed(Keys.A)) 
                position.X -= 5;
            if (InputHandler.IsKeyPressed(Keys.W)) // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                position.Y -= 5;                   //          !!TEMPORARY!!
            if (InputHandler.IsKeyPressed(Keys.D)) // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                position.X += 5;
            if (InputHandler.IsKeyPressed(Keys.S)) 
                position.Y += 5;
            rotation = Math.Atan2((InputHandler.GetMousePos().Y - position.Y) , (InputHandler.GetMousePos().X - position.X)) + Math.PI/2;
        }
    }
}
