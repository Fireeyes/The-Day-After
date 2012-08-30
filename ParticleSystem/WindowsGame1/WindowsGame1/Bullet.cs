using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    public class Bullet
    {
        public Vector2 Position;
        public Texture2D Texture;
        public float Rotation;
        public int speed=10;
        public Bullet(Vector2 Position, Texture2D Texture, float Rotation, int speed)
        {
            this.Position = Position;
            this.Texture = Texture;
            this.Rotation = Rotation;
            this.speed = speed;
        }
        public void Update(float gameTime)
        {
            Position = Position + (new Vector2((float)(speed * Math.Cos(Rotation - Math.PI / 2)),(float)( speed * Math.Sin(Rotation - Math.PI / 2))));

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,Position,null,Color.White,(float)(Rotation-(float)Math.PI/2),new Vector2(Texture.Width/2,Texture.Height/2),1,SpriteEffects.None,1);
        }
    }
}
