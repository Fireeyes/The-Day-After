using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDayAfter_XNA_Project
{
    public class SpriteAnimation
    {
   
        /*
        #region Variables

        Texture2D texture;

        bool isAnimating = true;

        Color colorTint = Color.White;

        Vector2 position = new Vector2(0, 0);

        Vector2 lastPosition = new Vector2(0, 0);

        Dictionary<string, FrameAnimation> Animations = new Dictionary<string, FrameAnimation>();

        string currentAnimation = null;

        float rotation = 0f;

        Vector2 center;

        int width;

        int height;

        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return position; }
            set
            {
                lastPosition = position;
                position = value;
            }
        }

        public int X
        {
            get { return (int)position.X; }
            set
            {
                lastPosition.X = position.X;
                position.X = value;
            }
        }

        public int Y
        {
            get { return (int)position.Y; }
            set
            {
                lastPosition.Y = position.Y;
                position.Y = value;
            }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle(X, Y, width, height); }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public Color Tint
        {
            get { return colorTint; }
            set { colorTint = value; }
        }


        public bool IsAnimating
        {
            get { return isAnimating; }
            set { isAnimating = value; }
        }

        public FrameAnimation CurrentFrameAnimation
        {
            get
            {
                if (!string.IsNullOrEmpty(currentAnimation))
                    return Animations[currentAnimation];
                else
                    return null;
            }
        }


        public string CurrentAnimation
        {
            get { return currentAnimation; }
            set
            {
                if (Animations.ContainsKey(value))
                {
                    currentAnimation = value;
                    Animations[currentAnimation].CurrentFrame = 0;

                }
            }
        }

        #endregion

        #region Constructors

        public SpriteAnimation(Texture2D Texture)
        {
            texture = Texture;
        }

        #endregion

        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength)
        {
            Animations.Add(Name, new FrameAnimation(X, Y, Width, Height, Frames, FrameLength));
            width = Width;
            height = Height;
            center = new Vector2(width / 2, height / 2);
        }

        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength, string NextAnimation)
        {
            Animations.Add(Name, new FrameAnimation(X, Y, Width, Height, Frames, FrameLength, NextAnimation));
            width = Width;
            height = Height;
            center = new Vector2(width / 2, height / 2);
        }

        public FrameAnimation GetAnimationByName(string Name)
        {
            if (Animations.ContainsKey(Name))
            {
                return Animations[Name];
            }
            else
            {
                return null;
            }
        }

        public void MoveBy(double x, double y)
        {
            lastPosition = position;
            position.X += (float)x;
            position.Y += (float)y;
        }

        public void Update(GameTime gameTime)
        {
            if (isAnimating)
            {
                if (CurrentFrameAnimation == null)
                {

                    if (Animations.Count > 0)
                    {
                        string[] sKeys = new string[Animations.Count];
                        Animations.Keys.CopyTo(sKeys, 0);
                        CurrentAnimation = sKeys[0];

                    }
                }

                CurrentFrameAnimation.Update(gameTime);

                if (!String.IsNullOrEmpty(CurrentFrameAnimation.NextAnimation))
                {
                    if (CurrentFrameAnimation.PlayCount > 0)
                    {
                        CurrentAnimation = CurrentFrameAnimation.NextAnimation;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, int XOffset, int YOffset)
        {
            if (isAnimating)
                spriteBatch.Draw(texture, (position + new Vector2(XOffset, YOffset) + center),
                                CurrentFrameAnimation.FrameRectangle, colorTint,
                                rotation, center, 1f, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAnimating)
                spriteBatch.Draw(texture, new Vector2(320, 320),
                                CurrentFrameAnimation.FrameRectangle, colorTint,
                                rotation, center, 1f, SpriteEffects.None, 0);
        }

        public bool IsMoving()
        {
            if (lastPosition != position)
                return true;
            else
                return false;
        }
        */        
    }
}
