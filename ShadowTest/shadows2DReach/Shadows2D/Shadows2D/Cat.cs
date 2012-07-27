using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Shadows2D
{
    class Cat
    {
        Texture2D texture;              //sprite-sheet containing the cat animation
        Animation walkingAnimation;     //animation object used for animating
        Vector2 origin;                 //origin of the image

        public Vector2 Position { get; set; }   //position on the screen

        /// <summary>
        /// Creates a new Cat object
        /// </summary>
        /// <param name="texture">texture to use for the animation</param>
        /// <param name="frameCount">how many frames are in the texture</param>
        /// <param name="origin">origin to use for drawing individual sprites</param>
        public Cat(Texture2D texture, int frameCount, Vector2 origin)
        {
            this.texture = texture;
            //create a new animation object
            walkingAnimation = new Animation(texture.Width, texture.Height, frameCount, 0, 0);

            //tweak the FramesPerSecond and movementSpeed values until you're satisfied with how things move
            walkingAnimation.FramesPerSecond = 14*2;

            //reset position
            Position = Vector2.Zero;

            this.origin = origin;
        }

        /// <summary>
        /// Update position of the cat, and the animation
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            walkingAnimation.Update(elapsed);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(texture, position, walkingAnimation.CurrentFrame,
                            color, 0.0f, origin, 1.0f, SpriteEffects.None, 1.0f);
        }

    }
}
