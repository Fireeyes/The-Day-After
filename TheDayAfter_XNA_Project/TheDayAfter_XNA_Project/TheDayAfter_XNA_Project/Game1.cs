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
using TheDayAfter_XNA_Project.Player_Interface;
using TheDayAfter_XNA_Project.TestClass;
using TheDayAfter_XNA_Project.UI;

namespace TheDayAfter_XNA_Project
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TileMap DebugMap = new TileMap();
        RenderTarget2D final;
        RenderTarget2D shadowmap;
        //InputHandler Input=new InputHandler(); Input handler should be static (o,O) 
        // i mean.. u dont need more than one, 
        // and u need to see it everywhere                      
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 640;
            graphics.PreferredBackBufferWidth = 640;
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
            final = new RenderTarget2D(GraphicsDevice, 640, 640);
            shadowmap = new RenderTarget2D(GraphicsDevice, 640, 640);
            Lighting.Databse.Initialise(GraphicsDevice);
            
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            ParticleSystem.Initialize(new Vector2(0, 0));
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Player.texture = Content.Load<Texture2D>(@"Textures\DebugPlayer"); // !!!!!!!!!! TEMPORARY!!!!!!!!!
            Player.sprite = new SpriteAnimation(Content.Load<Texture2D>(@"Textures\DebugPlayerTileMap"));
            Player.sprite.AddAnimation("Walk", 0, 0, 50, 50, 16, 0.05f);
            Player.sprite.AddAnimation("Idle", 4*50, 0, 50, 50, 1 , 0.2f);
            Player.sprite.CurrentAnimation = "Walk";
            DebugMap.Load(Content.Load<Texture2D>(@"Textures\DebugTileMap"), Content.Load<Texture2D>(@"Textures\Tilesets\debugtileset"));
            Database.Load(Content);
            
            ParticleSystem.AddEmitter(new Vector2(0.01f, 0.015f),
                                        new Vector2(0, -1), new Vector2(0.1f * MathHelper.Pi, 0.1f * -MathHelper.Pi),
                                        new Vector2(0.5f, 0.75f),
                                        new Vector2(60, 70), new Vector2(15, 15f),
                                        Color.Orange, Color.Crimson, Color.Orange, Color.Orange,
                                        new Vector2(400, 500), new Vector2(100, 120), 1000, new Vector2(200,200), Database.BoxTexture["particleBase"]);
            Lighting.Databse.Load(Content);
            
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

            InputHandler.Update();

            Player.Update(gameTime);

            ParticleSystem.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);

            if (InputHandler.IsMouseLClick())
            {
                Lighting.Databse.AddLight(Player.sprite.position+(InputHandler.GetMousePos()-new Vector2(320)),GraphicsDevice );
            }

            Lighting.Databse.Update();

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            GraphicsDevice.SetRenderTarget(final);
            GraphicsDevice.Clear(Color.Black);
            DebugMap.Draw(spriteBatch);                 
            Player.Draw(spriteBatch);
            
            #region shadowmap
            GraphicsDevice.SetRenderTarget(shadowmap);
            GraphicsDevice.Clear(Color.White);
            Player.ShadowDraw(spriteBatch);
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.End();
            shadowmap=Lighting.Databse.GenerateShadows(shadowmap,spriteBatch, GraphicsDevice);
            #endregion shadowmap
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Draw(final, new Rectangle(0, 0, 640, 640), Color.White);
            spriteBatch.Draw(shadowmap, new Rectangle(0, 0, 640, 640), Color.White);

            //Lighting.Databse.ApplyShadows(spriteBatch);
            
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            ParticleSystem.Draw(spriteBatch, 1, -(Player.sprite.position- new Vector2(320)));
            DebugFrame.Draw(spriteBatch);
            spriteBatch.End();
            
        }
    }
}
