using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Reflection;
using System.Collections;

namespace TheDayAfter_XNA_Project.Lighting
{
    class LightSource
    {
        Vector2 worldPos;
        Vector2 screenPos;
        float[] RGB = new float[3];
        int range;
        LightType type;
        RenderTarget2D area;
        public void SnatchArea(Texture2D shadowMap, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            
            graphicsDevice.SetRenderTarget(area);
            spriteBatch.Draw(shadowMap,
                new Rectangle(0, 0, 2 * range, 2 * range),
                new Rectangle((int)screenPos.X - range, (int)screenPos.Y - range, range * 2, range * 2),
                Color.White);
            graphicsDevice.SetRenderTarget(null);
        }
        public LightSource(float[] RGB, Vector2 worldPos, LightType type, int range,GraphicsDevice graphicsDevice)
        {
            this.RGB = RGB;
            this.worldPos = worldPos;
            this.type = type;
            this.range = range;
            area = new RenderTarget2D(graphicsDevice, range, range);
            
        }
        public void Update()
        {
            screenPos = Player.position-worldPos;
            screenPos.X = screenPos.X + 320;
            screenPos.Y = screenPos.Y + 320;
        }
        public void GenerateShadow(SpriteBatch spriteBatch, Effect effect,GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Textures[1]=area;
            foreach (EffectPass pass in effect.Techniques[0].Passes)
            {
                pass.Apply();
            }
        }
        public void ApplyShadows(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(area,
                new Rectangle((int)screenPos.X - range, (int)screenPos.Y - range, range * 2, range * 2),
                Color.White
                );
        }

    }
}