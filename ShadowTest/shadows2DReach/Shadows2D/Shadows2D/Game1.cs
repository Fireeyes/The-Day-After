using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Ziggyware;

namespace Shadows2D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 lightPosition;
        Vector2 lightPosition2;
        Vector2 lightPosition3;
        Texture2D testTexture;
        Texture2D dot;
        Texture2D tileTexture;
        QuadRenderComponent quadRender;
        Cat cat;
        ShadowmapResolver shadowmapResolver;
        LightArea lightArea1;
        LightArea lightArea2;
        LightArea lightArea3;
        RenderTarget2D screenShadows;
        RenderTarget2D screenGround;
        public int x;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            quadRender = new QuadRenderComponent(this);
            this.Components.Add(quadRender);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            shadowmapResolver = new ShadowmapResolver(GraphicsDevice, quadRender, ShadowmapSize.Size256, ShadowmapSize.Size1024);
            shadowmapResolver.LoadContent(Content);
            lightArea1 = new LightArea(GraphicsDevice, ShadowmapSize.Size512);
            lightArea2 = new LightArea(GraphicsDevice, ShadowmapSize.Size512);
            lightArea3 = new LightArea(GraphicsDevice, ShadowmapSize.Size512);
            // Create a new SpriteBatch, which can be used to draw textures.
            testTexture = Content.Load<Texture2D>("cat4");
            dot = Content.Load<Texture2D>("dot");
            tileTexture = Content.Load<Texture2D>("tile");
            lightPosition = new Vector2(276, 276);
            lightPosition2 = new Vector2(560, 154);
            lightPosition3 = new Vector2(560, 154);
            screenShadows = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            screenGround = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            //load cat texture
            Texture2D catTexture = Content.Load<Texture2D>("catWalk");

            //create cat object
            cat = new Cat(catTexture, 14, new Vector2(64, 128));

            //place it in the center of the screen
            cat.Position = new Vector2(600, 300);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            Vector2 movement = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
            movement.Y *= -1.0f;

            Vector2 movement2 = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right;
            movement2.Y *= -1.0f;

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Left))
                movement2.X = -1.0f;
            if (ks.IsKeyDown(Keys.Right))
                movement2.X = 1.0f;
            if (ks.IsKeyDown(Keys.Up))
                movement2.Y = -1.0f;
            if (ks.IsKeyDown(Keys.Down))
                movement2.Y = 1.0f;

            lightPosition += movement * 100.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            lightPosition2 += movement2 * 100.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //update cat's state
            cat.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //first light area
            lightArea1.LightPosition = lightPosition;
            lightArea1.BeginDrawingShadowCasters();
            DrawCasters(lightArea1);
            lightArea1.EndDrawingShadowCasters();
            shadowmapResolver.ResolveShadows(lightArea1.RenderTarget, lightArea1.RenderTarget, lightPosition);

            //second light area
            lightArea2.LightPosition = lightPosition2;
            lightArea2.BeginDrawingShadowCasters();
            DrawCasters(lightArea2);
            lightArea2.EndDrawingShadowCasters();
            shadowmapResolver.ResolveShadows(lightArea2.RenderTarget, lightArea2.RenderTarget, lightPosition2);

            //third light area
            lightArea3.LightPosition = lightPosition3;
            lightArea3.BeginDrawingShadowCasters();
            DrawCasters(lightArea3);
            lightArea3.EndDrawingShadowCasters();
            shadowmapResolver.ResolveShadows(lightArea3.RenderTarget, lightArea3.RenderTarget, lightPosition3);

            GraphicsDevice.SetRenderTarget(screenShadows);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            spriteBatch.Draw(lightArea1.RenderTarget, lightArea1.LightPosition - lightArea1.LightAreaSize * 0.5f, Color.Red);
            spriteBatch.Draw(lightArea2.RenderTarget, lightArea2.LightPosition - lightArea2.LightAreaSize * 0.5f, Color.Green);
            spriteBatch.Draw(lightArea3.RenderTarget, lightArea3.LightPosition - lightArea3.LightAreaSize * 0.5f, Color.Blue);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(screenGround);

            GraphicsDevice.Clear(Color.Black);

            DrawGround();

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, null, RasterizerState.CullNone, ShadowmapResolver.blender);
            graphics.GraphicsDevice.Textures[1] = screenShadows;
            graphics.GraphicsDevice.Textures[1].GraphicsDevice.SamplerStates[1] = SamplerState.LinearClamp;
            spriteBatch.Draw(screenGround, Vector2.Zero, Color.White);
            spriteBatch.End();

            DrawScene();

            base.Draw(gameTime);
        }

        private void DrawCasters(LightArea lightArea)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(testTexture, lightArea.ToRelativePosition(Vector2.Zero), Color.Black);
            cat.Draw(spriteBatch, lightArea.ToRelativePosition(cat.Position), Color.Black);
            spriteBatch.End();
        }

        private void DrawScene()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(testTexture, new Rectangle(0, 0, 512, 512), new Color(new Vector4(1, 1, 1, 1.0f)));
            cat.Draw(spriteBatch, cat.Position, Color.White);
            DrawDot(lightPosition);
            DrawDot(lightPosition2);
            DrawDot(lightPosition3);
            spriteBatch.End();
        }
        private void DrawGround()
        {
            //draw the tile texture tiles across the screen
            Rectangle source = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(tileTexture, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.End();

        }

        private void DrawDot(Vector2 pos)
        {
            pos -= new Vector2(8, 8);
            spriteBatch.Draw(dot, pos, Color.White);
        }
    }
}
