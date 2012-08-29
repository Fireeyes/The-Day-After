using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace WindowsGame1
{
    class Particle
    {
        public Vector2 Position;
        Vector2 StartDirection;
        Vector2 EndDirection;
        float LifeLeft;
        float StartingLife;
        float ScaleBegin;
        float ScaleEnd;
        Color StartColor;
        Color EndColor;
        Emitter Parent;
        float lifePhase;


        public Particle(Vector2 Position, Vector2 StartDirection, Vector2 EndDirection, float StartingLife, float ScaleBegin, float ScaleEnd, Color StartColor, Color EndColor, Emitter Yourself)
        {
            this.Position = Position;
            this.StartDirection = StartDirection;
            this.EndDirection = EndDirection;
            this.StartingLife = StartingLife;
            this.LifeLeft = StartingLife;
            this.ScaleBegin = ScaleBegin;
            this.ScaleEnd = ScaleEnd;
            this.StartColor = StartColor;
            this.EndColor = EndColor;
            this.Parent = Yourself;
        }

        public bool Update(float gameTime)
        {
            LifeLeft -= gameTime;
            if (LifeLeft <= 0)
                return false;
            lifePhase = LifeLeft / StartingLife;  // 1 means newly created 0 means dead.
            Position += MathLib.LinearInterpolate(EndDirection, StartDirection, lifePhase) * gameTime;
            return true;
        }

        public void Draw(SpriteBatch spriteBatch, int Scale, Vector2 Offset)
        {
            float currScale = MathLib.LinearInterpolate(ScaleEnd, ScaleBegin, lifePhase);
            Color currCol = MathLib.LinearInterpolate(EndColor, StartColor, lifePhase);
            spriteBatch.Draw(Parent.ParticleSprite,
                     new Rectangle((int)((Position.X - 0.5f * currScale) * Scale + Offset.X),
                              (int)((Position.Y - 0.5f * currScale) * Scale + Offset.Y),
                              (int)(currScale * Scale),
                              (int)(currScale * Scale)),
                     null, currCol, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
