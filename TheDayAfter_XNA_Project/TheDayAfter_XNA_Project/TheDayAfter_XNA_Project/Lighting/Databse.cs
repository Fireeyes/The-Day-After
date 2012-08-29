using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TheDayAfter_XNA_Project.Lighting
{
    static class Databse
    {
        static Effect testeffect;
        static Effect testshadow;
        static Texture2D shadowMap;

        static void Load(ContentManager Content)
        {
            testeffect = Content.Load<Effect>(@"Shaders\TestShader");
            testshadow = Content.Load<Effect>(@"Shaders\Shadow");
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
