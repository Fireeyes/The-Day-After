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
        public double distance;
        LightType type;
        public Rectangle RenderArea;
        public RenderTarget2D area;
        List<RenderTarget2D> output=new List<RenderTarget2D>();
        public LightSource(float[] RGB, Vector2 worldPos, LightType type, int range, GraphicsDevice graphicsDevice)
        {
            this.RGB = RGB;
            this.worldPos = worldPos;
            this.type = type;
            this.range = range;
            area = new RenderTarget2D(graphicsDevice, range*2, range*2);
            output.Add(new RenderTarget2D(graphicsDevice, range * 2, range * 2));
            output.Add(new RenderTarget2D(graphicsDevice, range * 2, range * 2));
            int order = 2;
            while(Math.Pow(2,order)<=range*2)
            {
                output.Add(new RenderTarget2D(graphicsDevice, (range * 2) / (int)Math.Pow(2, order), (range * 2)));
                order++;
            }

            RenderArea.X = 0;
            RenderArea.Y = 0;
            RenderArea.Width = 2*range;
            RenderArea.Height = 2*range;

        }
        public void Update()
        {
            screenPos = worldPos - Player.sprite.position;
            screenPos.X = screenPos.X + 320;
            screenPos.Y = screenPos.Y + 320;
            distance = Math.Sqrt((Math.Pow((screenPos.X - 320), 2) + Math.Pow((screenPos.X - 320), 2)));
            RenderArea.X = (int)screenPos.X - range;
            RenderArea.Y = (int)screenPos.Y - range;
        }
        public void GenerateShadow(Texture2D shadowmap, SpriteBatch spriteBatch,GraphicsDevice graphicsDevice,List<Effect>EffectList)
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
            EffectList[1].CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(area, new Rectangle(0, 0, range * 2, range * 2), Color.White);
            #endregion
            #region Calculate Distorted
            graphicsDevice.SetRenderTarget(output[0]);
       
            EffectList[0].CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(output[1], new Rectangle(0, 0, range * 2, range * 2), Color.White);
            #endregion
            
            #region Horizontal Reduction
            int order = 0;
            //represents the order of the power of 2 used in the reduction
            //first pass of the lap makes the pixel the min of itself and the pixel near it (2^0)
            //second pass makes the pixel the min of itself and the one two pixels to the right
            // and so on until 2^order>range
            EffectList[2].Parameters["range"].SetValue(range);
            EffectList[2].CurrentTechnique.Passes[0].Apply();
            while (Math.Pow(2, order) <= range)
            {
                graphicsDevice.SetRenderTarget(output[(order + 1) ]);
                EffectList[2].Parameters["order"].SetValue((float)(Math.Pow(2, order)) / (range * 2));
                spriteBatch.Draw(output[order ], new Rectangle(0, 0, range * 2, range * 2), Color.White);
                EffectList[2].CurrentTechnique.Passes[0].Apply();
                order++;
            }    
            #endregion
            #region Shadow Resolve
            graphicsDevice.SetRenderTarget(area);
            EffectList[3].CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(output[order  ], new Rectangle(0, 0, range * 2, range * 2), Color.White);
            #endregion region Shadow Resolve 
            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);
                     
        }
    }
        
}