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
        static Effect distortFX;
        static Effect fadeFX;
        static Effect horizontalreductionFX;
        static Effect resolveFX;
        static RenderTarget2D shadowMap;
        static List<LightSource> LightList=new List<LightSource>();
        public static void Initialise(GraphicsDevice graphicsDevice)
        {
            shadowMap = new RenderTarget2D(graphicsDevice, 640, 640);
        }
        public static void Load(ContentManager Content)
        {
            testeffect = Content.Load<Effect>(@"Shaders\TestShader");
            distortFX = Content.Load<Effect>(@"Shaders\distort");
            fadeFX = Content.Load<Effect>(@"Shaders\fade");
            horizontalreductionFX = Content.Load<Effect>(@"Shaders\reduction");
            resolveFX = Content.Load<Effect>(@"Shaders\resolve");
            testeffect.CurrentTechnique = testeffect.Techniques["Technique1"];
        }            
        public static void AddLight(Vector2 worldpos, GraphicsDevice graphicsDevice)
        {
            LightList.Add(new LightSource(
                new float[3]{0.5f,0.5f,0.5f},
                worldpos,
                LightType.Point,
                200,
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
                CurrentLight.GenerateShadow(shadowCaster,spriteBatch, distortFX,graphicsDevice,fadeFX,horizontalreductionFX,resolveFX); 
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
