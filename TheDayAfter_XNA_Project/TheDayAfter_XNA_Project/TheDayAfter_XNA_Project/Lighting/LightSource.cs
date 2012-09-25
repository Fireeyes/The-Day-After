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
        public Rectangle RenderArea;
        public RenderTarget2D area;
        public RenderTarget2D[] output=new RenderTarget2D[2];
        public LightSource(float[] RGB, Vector2 worldPos, LightType type, int range, GraphicsDevice graphicsDevice)
        {
            this.RGB = RGB;
            this.worldPos = worldPos;
            this.type = type;
            this.range = range;
            area = new RenderTarget2D(graphicsDevice, range*2, range*2);
            output[1] = new RenderTarget2D(graphicsDevice, range * 2, range * 2);
            output[0] = new RenderTarget2D(graphicsDevice, range * 2, range * 2);

            RenderArea.X = 0;
            RenderArea.Y = 0;
            RenderArea.Width = 2*range;
            RenderArea.Height = 2*range;

        }
        public void Update()
        {
            screenPos = worldPos- Player.sprite.position;
            screenPos.X = screenPos.X + 320;
            screenPos.Y = screenPos.Y + 320;
            RenderArea.X = (int)screenPos.X - range;
            RenderArea.Y = (int)screenPos.Y - range;
        }
        public void GenerateShadow(Texture2D shadowmap, SpriteBatch spriteBatch, Effect distort, GraphicsDevice graphicsDevice,Effect fade,Effect reduction)
        {
            #region Snatch texture
            graphicsDevice.SetRenderTarget(area);
            spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.Opaque);
            graphicsDevice.Clear(Color.White);
            spriteBatch.Draw(shadowmap,
                new Rectangle(0, 0, range * 2, range * 2),
                RenderArea,
                Color.White);
            #endregion
            #region Calculate Fade
            graphicsDevice.SetRenderTarget(output[1]);
            fade.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(area, new Rectangle(0, 0, range * 2, range * 2), Color.White);
            #endregion
            #region Calculate Deformed
            graphicsDevice.SetRenderTarget(output[0]);
            
            distort.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(output[1], new Rectangle(0, 0, range * 2, range * 2), Color.White);
            #endregion
            #region Horizontal Reduction
            int order = 0;
            //represents the order of the power of 2 used in the reduction
            //first pass of the lap makes the pixel the min of itself and the pixel near it (2^0)
            //second pass makes the pixel the min of itself and the one two pixels to the right
            // and so on until 2^order>range
            reduction.Parameters["range"].SetValue(range);
            reduction.CurrentTechnique.Passes[0].Apply();
            while (Math.Pow(2, order) < range)
            {
                graphicsDevice.SetRenderTarget(output[(order + 1) % 2]);
                reduction.Parameters["order"].SetValue((float)(Math.Pow(2, order))/(range*2));
                spriteBatch.Draw(output[order % 2], new Rectangle(0, 0, range * 2, range * 2), Color.White);
                reduction.CurrentTechnique.Passes[0].Apply();
                order++;
            }
            graphicsDevice.SetRenderTarget(output[(order + 1) % 2]);
            
            #endregion
            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);
                     
        }
    }
        
}