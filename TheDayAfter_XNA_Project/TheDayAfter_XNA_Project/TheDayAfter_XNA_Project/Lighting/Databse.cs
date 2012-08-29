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
        static Effect testeffect;
        static Effect testshadow;
        static Texture2D shadowMap;
        static List<LightSource> LightList=new List<LightSource>();
        static void Initialise(GraphicsDevice graphicsDevice)
        {
            Texture2D shadowMap = new Texture2D(graphicsDevice, 640, 640);
        }

        public static void Load(ContentManager Content)
        {
            testeffect = Content.Load<Effect>(@"Shaders\TestShader");
            testshadow = Content.Load<Effect>(@"Shaders\Shadow");
            testeffect.CurrentTechnique = testeffect.Techniques["Technique1"];
        }
        public static void GetShadowMap(RenderTarget2D ShadowCasters)
        {
            shadowMap = ShadowCasters;
        }
        public static void CalculateShadows(SpriteBatch spritebatch, GraphicsDevice graphicsDevice)
        {
            foreach (LightSource CurrentLight in LightList)
            {
                //light calculations
                CurrentLight.SnatchArea(shadowMap, spritebatch, graphicsDevice);//gets the area around it to calculate shadows on
            }

        }
        public static void AddLight(Vector2 worldpos, GraphicsDevice graphicsDevice)
        {
            LightList.Add(new LightSource(
                new float[3]{0.5f,0.5f,0.5f},
                worldpos,
                LightType.Point,
                50,
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
        public static void GenerateShadows(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice)
        {
            foreach (LightSource CurrentLight in LightList)
            {
                CurrentLight.GenerateShadow(spriteBatch, testeffect,graphicsDevice); 
            }
        }
        public static void ApplyShadows(SpriteBatch spriteBatch)
        {
            foreach (LightSource CurrentLight in LightList)
            {
                CurrentLight.ApplyShadows(spriteBatch);
            }
        }
        //Lighting.Databse.testeffect.CurrentTechnique = Lighting.Databse.testeffect.Techniques["Technique1"];
        //EffectParameter red = Lighting.Databse.testeffect.Parameters["red"];
        //red.SetValue((float)InputHandler.ScrollValue / 6000);
        //EffectParameter mouseposdebug = Lighting.Databse.testeffect.Parameters["mousepos"];
        //mouseposdebug.SetValue(new float[2] { InputHandler.GetMousePos().X / 640, InputHandler.GetMousePos().Y / 640 });
        //GraphicsDevice.SetRenderTarget(null);
        //GraphicsDevice.Textures[1] = final;
        //foreach (EffectPass pass in Lighting.Databse.testeffect.Techniques[0].Passes)
        //{
        //    pass.Apply();
        //}

        //#endregion
        ////GraphicsDevice.SetRenderTarget(shadowmap);
        ////GraphicsDevice.Clear(Color.White);
        ////Player.ShadowDraw(spriteBatch);
        ////GraphicsDevice.SetRenderTarget(null);
        ////GraphicsDevice.Textures[1] = shadowmap;
        ////EffectParameter mouseshadow = Lighting.Databse.testshadow.Parameters["mousepos"];
        ////mouseshadow.SetValue(new float[2] { InputHandler.GetMousePos().X / 640, InputHandler.GetMousePos().Y / 640 });
        ////Lighting.Databse.testshadow.CurrentTechnique = Lighting.Databse.testshadow.Techniques["Technique1"];
        ////foreach (EffectPass pass in Lighting.Databse.testshadow.Techniques[0].Passes)
        ////{
        ////    pass.Apply();
        ////}

        //GraphicsDevice.SetRenderTarget(null);
    }
}
