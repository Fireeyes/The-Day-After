using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public static class Bullets
    {
        public static LinkedList<Bullet> ActiveBullets= new LinkedList<Bullet>();

        public static void Update(float gameTime)
        {
            LinkedListNode<Bullet> node = ActiveBullets.First;

            while (node != null)
            {
                node.Value.Update(gameTime);
                node = node.Next;
            }
        }



        public static void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            LinkedListNode<Bullet> node=null;
            if(ActiveBullets.Count()>0)
                node = ActiveBullets.First;

            while (node != null)
            {
                node.Value.Draw(spriteBatch);
                node = node.Next;
            }
        }
    }
}
