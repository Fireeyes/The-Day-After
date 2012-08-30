﻿using System;
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
        public RenderTarget2D output1;
        public LightSource(float[] RGB, Vector2 worldPos, LightType type, int range, GraphicsDevice graphicsDevice)
        {
            this.RGB = RGB;
            this.worldPos = worldPos;
            this.type = type;
            this.range = range;
            area = new RenderTarget2D(graphicsDevice, range*2, range*2);
            output1 = new RenderTarget2D(graphicsDevice, range * 2, range * 2);
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
        public void GenerateShadow(Texture2D shadowmap, SpriteBatch spriteBatch, Effect effect, GraphicsDevice graphicsDevice)
        {
            #region snatch texture
            graphicsDevice.SetRenderTarget(area);
            spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.Opaque);
            graphicsDevice.Clear(Color.White);
            spriteBatch.Draw(shadowmap,
                new Rectangle(0, 0, range * 2, range * 2),
                RenderArea,
                Color.White);
            #endregion
            #region Calculate Deformed
            graphicsDevice.SetRenderTarget(output1);
            
            effect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(area, new Rectangle(0, 0, range * 2, range * 2), Color.White);
            spriteBatch.End();
            #endregion
            graphicsDevice.SetRenderTarget(null);
                     
        }
    }
        
}