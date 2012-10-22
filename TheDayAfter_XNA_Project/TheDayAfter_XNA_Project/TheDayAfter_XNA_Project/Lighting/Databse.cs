using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TheDayAfter_XNA_Project.Lighting
{
    static class Databse
    {
        static List<Effect> ShadowEffectList = new List<Effect>();
        static RenderTarget2D shadowMap;
        static List<LightSource> LightList=new List<LightSource>();
        public static void Initialise(GraphicsDevice graphicsDevice)
        {
            shadowMap = new RenderTarget2D(graphicsDevice, 640, 640);
        }
        public static void Load(ContentManager Content)
        {
            ShadowEffectList.Add(Content.Load<Effect>(@"Shaders\distort"));
            ShadowEffectList.Add(Content.Load<Effect>(@"Shaders\fade"));
            ShadowEffectList.Add(Content.Load<Effect>(@"Shaders\reduction"));
            ShadowEffectList.Add(Content.Load<Effect>(@"Shaders\resolve"));
        }            
        public static void AddLight(Vector2 worldpos, GraphicsDevice graphicsDevice)
        {
            LightList.Add(new LightSource(
                new float[3]{0.5f,0.5f,0.5f},
                worldpos,
                LightType.Point,
                1024,
                graphicsDevice
                ));
        }
        public static void Update()
        {
            foreach(LightSource CurrentLight in LightList)
            {
                CurrentLight.Update();
            }
            
        }
        public static RenderTarget2D GenerateShadows(RenderTarget2D shadowCaster,SpriteBatch spriteBatch,GraphicsDevice graphicsDevice)
        {
            foreach (LightSource CurrentLight in LightList)
            {
                if (CurrentLight.distance < 1000)
                {
                    CurrentLight.GenerateShadow(shadowCaster, spriteBatch, graphicsDevice, ShadowEffectList);
                }
            }
            graphicsDevice.SetRenderTarget(shadowMap);
            graphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.NonPremultiplied);
            //draw every small shadow map to the bigger one
            TestClass.DebugFrame.debugText += "Number of lights:" + LightList.Count().ToString()+"\n";
            foreach (LightSource CurrentLight in LightList)
            {
                
                    spriteBatch.Draw(CurrentLight.area,
                    CurrentLight.RenderArea,
                    Color.White);
                
            }
            spriteBatch.End();
            return shadowMap;
        }
    }
}
