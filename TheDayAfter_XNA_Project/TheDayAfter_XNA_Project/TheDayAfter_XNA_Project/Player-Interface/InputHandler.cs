using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDayAfter_XNA_Project.Player_Interface
{
    public class InputHandler
    {
        MouseState Ms = Mouse.GetState();
        KeyboardState ks = Keyboard.GetState();
        KeyboardState oldks = Keyboard.GetState(); 
        int ScrollValue
        {
            get
            {
                return Ms.ScrollWheelValue-ScrollValue;
            }
            set
            {
                ScrollValue = value;
            }
        }
        public InputHandler()
        {
            Ms = Mouse.GetState();
            ks = Keyboard.GetState();
            oldks = Keyboard.GetState();
        }
        Vector2 GetMousePos()
        {
            return new Vector2(Ms.X, Ms.Y);
        }
        bool IsMouseRClick()    //returns True if Right Mouse Buttoned is pressed
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
        bool IsMouseLClick()  //returns True if Left Mouse Buttoned is pressed
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
        void Update()
        {
            ScrollValue = Ms.ScrollWheelValue;
            Ms = Mouse.GetState();
            oldks = ks;
            ks = Keyboard.GetState(); 
        }
        bool IsKeyPressed(Keys key)
        {
            return ks.IsKeyDown(key);
        }
        
    }
}
