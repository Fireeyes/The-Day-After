using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheDayAfter_XNA_Project
{
    public class FrameAnimation : ICloneable
    {
        #region Variables

        Rectangle initialRectangle;

        int frameCount = 1;

        int currentFrame = 0;

        float frameLenght = 0.2f;

        float frameTimer = 0.0f;

        string nextAnimation = null;

        public int playCount = 0;

        #endregion

        #region Properties

        public int FrameCount
        {
            get { return frameCount; }
            set { frameCount = value; }
        }

        public float FrameLenght
        {
            get { return frameLenght; }
            set { frameLenght = value; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = (int)MathHelper.Clamp(value, 0, frameCount - 1); }
        }

        public int Height
        {
            get { return initialRectangle.Height; }
        }

        public int Width
        {
            get { return initialRectangle.Width; }
        }

        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(initialRectangle.X + initialRectangle.Width * currentFrame,
                                     initialRectangle.Y, initialRectangle.Width, initialRectangle.Height);
            }
        }

        public string NextAnimation
        {
            get { return nextAnimation; }
            set { nextAnimation = value; }
        }

        public int PlayCount
        {
            get { return playCount; }
            set { playCount = value; }
        }

        #endregion

        #region Constructors

        public FrameAnimation(Rectangle InitialRect, int FrameCount)
        {
            initialRectangle = InitialRect;
            frameCount = FrameCount;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int FrameCount)
        {
            initialRectangle = new Rectangle(X, Y, Width, Height);
            frameCount = FrameCount;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int FrameCount, float FrameLenght)
        {
            initialRectangle = new Rectangle(X, Y, Width, Height);
            frameCount = FrameCount;
            frameLenght = FrameLenght;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int NrOfFrames, float FrameLenght, string NextAnimation)
        {
            initialRectangle = new Rectangle(X, Y, Width, Height);
            frameCount = NrOfFrames;
            frameLenght = FrameLenght;
            nextAnimation = NextAnimation;
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer > frameLenght)
            {
                frameTimer = 0.0f;
                currentFrame = (currentFrame + 1) % frameCount;
                if (currentFrame == 0)
                    playCount = (int)MathHelper.Min(playCount + 1, int.MaxValue);
            }
        }

        object ICloneable.Clone()
        {
            return new FrameAnimation(this.initialRectangle.X, this.initialRectangle.Y, this.initialRectangle.Width, this.initialRectangle.Height, this.frameCount, this.frameLenght, this.nextAnimation);
        }

    }
}
