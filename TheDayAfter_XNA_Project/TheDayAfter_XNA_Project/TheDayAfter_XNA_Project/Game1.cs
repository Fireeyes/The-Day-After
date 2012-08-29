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
        GraphicsDeviceManager graphics;
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
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Player.texture = Content.Load<Texture2D>(@"Textures\DebugPlayer"); // !!!!!!!!!! TEMPORARY!!!!!!!!!
            Player.sprite = new SpriteAnimation(Content.Load<Texture2D>(@"Textures\DebugPlayerTileMap"));
            Player.sprite.AddAnimation("Walk", 0, 0, 50, 50, 16, 0.05f);
            Player.sprite.AddAnimation("Idle", 4*50, 0, 50, 50, 1 , 0.2f);
            Player.sprite.CurrentAnimation = "Walk";
            DebugMap.Load(Content.Load<Texture2D>(@"Textures\DebugTileMap"), Content.Load<Texture2D>(@"Textures\Tilesets\debugtileset"));
            Database.Load(Content);
            
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
            if (InputHandler.IsMouseLClick())
            {
                Lighting.Databse.AddLight(InputHandler.GetMousePos());
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
            DebugMap.Draw(spriteBatch);                 
            Player.Draw(spriteBatch);
            DebugFrame.Draw(spriteBatch);
            GraphicsDevice.SetRenderTarget(shadowmap);
            GraphicsDevice.Clear(Color.White);
            Player.ShadowDraw(spriteBatch);
            GraphicsDevice.SetRenderTarget(null);
            Lighting.Databse.GetShadowMap(shadowmap);
            Lighting.Databse.CalculateShadows(spriteBatch, GraphicsDevice);
            spriteBatch.Draw(final, new Rectangle(0, 0, 640, 640), Color.White);
            
            spriteBatch.End();
            
        }
    }
}
