using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDayAfter_XNA_Project
{
    public static class InputHandler
    {
        public static MouseState Ms = Mouse.GetState();
        public static KeyboardState ks = Keyboard.GetState();
        public static KeyboardState oldks = Keyboard.GetState();
        public static int ScrollValue
        {
            get
            {
                return Ms.ScrollWheelValue;
            }
            set { }
        }
        /*
        public static InputHandler()
        {
            Ms = Mouse.GetState();
            ks = Keyboard.GetState();
            oldks = Keyboard.GetState();
        }*/
        public static Vector2 GetMousePos()
        {
            return new Vector2(Ms.X, Ms.Y);
        }
        public static bool IsMouseRClick()    //returns True if Right Mouse Buttoned is pressed
        {
            if (Ms.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsMouseLClick()  //returns True if Left Mouse Buttoned is pressed
        {
            if (Ms.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsMouseMClick()  //returns True if Middle Mouse Buttoned is pressed
        {
            if (Ms.MiddleButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Update()
        {
            ScrollValue = Ms.ScrollWheelValue;
            Ms = Mouse.GetState();
            oldks = ks;
            ks = Keyboard.GetState(); 
        }
        public static bool IsKeyPressed(Keys key)
        {
            return ks.IsKeyDown(key);
        }

        public static Vector2 GetTile()
        {
            return new Vector2((int)(((InputHandler.GetMousePos() + Player.position - new Vector2(320)).X) / 64), (int)(((InputHandler.GetMousePos() + Player.position - new Vector2(320)).Y) / 64));
        }
    }
}
