using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheDayAfter_XNA_Project
{
    static class Camera
    {
        public static int ViewWidth { get; set; }
        public static int ViewHeight { get; set; }
        public static int WorldWidth { get; set; }
        public static int WorldHeight { get; set; }

        public static Vector2 DisplayOffset { get; set; }

        static public Vector2 location = Vector2.Zero;

        public static Vector2 Location
        {
            get
            {
                return location;
            }
            set
            {
                location = new Vector2(
                    MathHelper.Clamp(value.X, 0f, WorldWidth - ViewWidth),
                    MathHelper.Clamp(value.Y, 0f, WorldHeight - ViewHeight));
            }
        }

        public static Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return worldPosition - Location + DisplayOffset;
        }

        public static Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return screenPosition + Location - DisplayOffset;
        }

        public static void Move(Vector2 offset)
        {
            Location += offset;
        }

        internal static void Update()
        {
            
            float heroX = MathHelper.Clamp(
               Player.sprite.Position.X, 100 + Player.sprite.DrawOffset.X, WorldWidth);
            float heroY = MathHelper.Clamp(
                Player.sprite.Position.Y, 150 + Player.sprite.DrawOffset.Y, WorldHeight);

            Player.sprite.Position = new Vector2(heroX, heroY);
            Vector2 testPosition = WorldToScreen(Player.sprite.Position);

            if (testPosition.X < 100)
            {
                Move(new Vector2(testPosition.X - 100, 0));
            }

            if (testPosition.X > (ViewWidth - 100))
            {
                Move(new Vector2(testPosition.X - (ViewWidth - 100), 0));
            }

            if (testPosition.Y < 100)
            {
                Move(new Vector2(0, testPosition.Y - 100));
            }

            if (testPosition.Y > (ViewHeight - 100))
            {
                Move(new Vector2(0, testPosition.Y - (ViewHeight - 100)));
            }
            
        }
    }
}
