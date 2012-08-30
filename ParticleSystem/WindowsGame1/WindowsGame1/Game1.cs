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

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        MouseState ms;
        MouseState prevms;
        SpriteBatch spriteBatch;
        ParticleSystem particleSystem;
        Texture2D ParticleBase;
        Texture2D Bullet;
        Texture2D Pixel;
        SpriteFont font;
        float Rotation;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferHeight = 1024;
            this.graphics.PreferredBackBufferWidth = 1024;
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
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
            spriteBatch = new SpriteBatch(this.GraphicsDevice);
            ParticleBase = this.Content.Load<Texture2D>("ParticleBase1");
            Bullet = this.Content.Load<Texture2D>("Bullet");
            Pixel = this.Content.Load<Texture2D>("Pixel");
            particleSystem = new ParticleSystem(new Vector2(0, 0));
            /*particleSystem.AddEmitter(new Vector2(0.01f, 0.015f),
                                        new Vector2(0, -1), new Vector2(0.1f * MathHelper.Pi, 0.1f * -MathHelper.Pi),
                                        new Vector2(0.5f, 0.75f),
                                        new Vector2(60, 70), new Vector2(15, 15f),
                                        Color.Orange, Color.Crimson, Color.Orange, Color.Orange,
                                        new Vector2(400, 500), new Vector2(100, 120), 1000, Vector2.Zero, ParticleBase); */
            font = this.Content.Load<SpriteFont>("SpriteFont1");
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

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            prevms = ms;
            ms = Mouse.GetState();
            Rotation = (float)(Math.Atan2((ms.Y - 512), (ms.X - 512)) + Math.PI / 2);
            if (ms.LeftButton == ButtonState.Pressed&&prevms.LeftButton==ButtonState.Released)
            {
                Bullets.ActiveBullets.AddLast(new Bullet(new Vector2(512,512),ParticleBase,Rotation,30));
                particleSystem.AddEmitter(new Vector2(0.001f, 0.008f),
                                        new Vector2((float)(1 * Math.Cos(Rotation - Math.PI / 2)), (float)(1 * Math.Sin(Rotation - Math.PI / 2))), new Vector2(0.1f * MathHelper.Pi, 0.1f * -MathHelper.Pi),
                                        new Vector2(0.75f, 1f),
                                        new Vector2(20, 30), new Vector2(5, 15f),
                                        Color.Aqua, Color.Blue, Color.RoyalBlue, Color.Purple,
                                        new Vector2(200, 500), new Vector2(100, 120), 1000, new Vector2(512,512), ParticleBase);
            }
            if (ms.RightButton == ButtonState.Pressed && prevms.RightButton == ButtonState.Released)
            {
                Bullets.ActiveBullets.AddLast(new Bullet(new Vector2(512, 512), Bullet, Rotation, 25));
                particleSystem.AddEmitter(new Vector2(0.1f, 0.0008f),
                                        new Vector2((float)(-1 * Math.Cos(Rotation - Math.PI / 2)), (float)(-1 * Math.Sin(Rotation - Math.PI / 2))), new Vector2(1f * MathHelper.Pi, 1f * -MathHelper.Pi),
                                        new Vector2(0.75f, 1f),
                                        new Vector2(20, 30), new Vector2(1, 10),
                                        Color.Red, Color.Red, Color.White, Color.Yellow,
                                        new Vector2(10, 20), new Vector2(200, 100), 1000, new Vector2(512, 512), ParticleBase);
            }
            if (ms.MiddleButton == ButtonState.Pressed && prevms.MiddleButton == ButtonState.Released)
            {
                Bullets.ActiveBullets.AddLast(new Bullet(new Vector2(512, 512), Pixel, Rotation, 1));
                particleSystem.AddEmitter(new Vector2(0.001f, 0.008f),
                                        new Vector2((float)(-1 * Math.Cos(Rotation - Math.PI / 2)), (float)(-1 * Math.Sin(Rotation - Math.PI / 2))), new Vector2(0.15f * MathHelper.Pi, 0.15f * -MathHelper.Pi),
                                        new Vector2(0.5f, 0.71f),
                                        new Vector2(20, 30), new Vector2(1, 10),
                                        Color.Red, Color.Red, Color.Red, Color.Red,
                                        new Vector2(300, 400), new Vector2(0, 0), 1000, new Vector2(512, 512), ParticleBase,0.1f);
            }
            particleSystem.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            if (Bullets.ActiveBullets.First != null)
            {
                int i = 0;
                LinkedListNode<Bullet> bullet = Bullets.ActiveBullets.First;
                while (bullet != null)
                {
 
                    particleSystem.EmitterList[i].RelPosition = bullet.Value.Position;
                    bullet = bullet.Next;
                    i++;
                }

            }
            Bullets.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.Additive);
            Bullets.Draw(spriteBatch);
            particleSystem.Draw(spriteBatch, 1, Vector2.Zero);
            spriteBatch.DrawString(font, ("Bullets: " + Bullets.ActiveBullets.Count), new Vector2(50), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
