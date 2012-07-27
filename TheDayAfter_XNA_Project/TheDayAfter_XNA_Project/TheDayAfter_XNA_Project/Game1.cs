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

namespace TheDayAfter_XNA_Project
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont pericles6;

        TileMap myMap;
        int squaresAcross = 17;
        int squaresDown = 37;
        int baseOffsetX = -32;
        int baseOffsetY = -64;
        float heightRowDepthMod = 0.00001f;

        Texture2D hilight;

        SpriteAnimation vlad;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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

            myMap = new TileMap(
                           Content.Load<Texture2D>(@"Textures\Tilesets\mousemap"),
                           Content.Load<Texture2D>(@"Textures\Tilesets\slopemaps"));

            hilight = Content.Load<Texture2D>(@"Textures\TileSets\hilight");

            Tile.TileSetTexture = Content.Load<Texture2D>(@"Textures\TileSets\tileset");

            //pericles6 = Content.Load<SpriteFont>(@"Fonts\Pericles6");

            Camera.ViewWidth = this.graphics.PreferredBackBufferWidth;
            Camera.ViewHeight = this.graphics.PreferredBackBufferHeight;
            Camera.WorldWidth = ((myMap.MapWidth - 2) * Tile.TileStepX);
            Camera.WorldHeight = ((myMap.MapHeight - 2) * Tile.TileStepY);
            Camera.DisplayOffset = new Vector2(baseOffsetX, baseOffsetY);

            Player.sprite = new SpriteAnimation(Content.Load<Texture2D>(@"Textures\Characters\player"));


            Player.sprite.AddAnimation("WalkWest"     , 128 * 4, 128 * 0, 128, 128, 8, 0.1f);
            Player.sprite.AddAnimation("WalkNorthWest", 128 * 4, 128 * 1, 128, 128, 8, 0.1f);
            Player.sprite.AddAnimation("WalkNorth"    , 128 * 4, 128 * 2, 128, 128, 8, 0.1f);
            Player.sprite.AddAnimation("WalkNorthEast", 128 * 4, 128 * 3, 128, 128, 8, 0.1f);
            Player.sprite.AddAnimation("WalkEast"     , 128 * 4, 128 * 4, 128, 128, 8, 0.1f);
            Player.sprite.AddAnimation("WalkSouthEast", 128 * 4, 128 * 5, 128, 128, 8, 0.1f);
            Player.sprite.AddAnimation("WalkSouth"    , 128 * 4, 128 * 6, 128, 128, 8, 0.1f);
            Player.sprite.AddAnimation("WalkSouthWest", 128 * 4, 128 * 7, 128, 128, 8, 0.1f);

            Player.sprite.AddAnimation("IdleWest"     , 0, 128 * 0, 128, 128, 4, 0.1f);
            Player.sprite.AddAnimation("IdleNorthWest", 0, 128 * 1, 128, 128, 4, 0.1f);
            Player.sprite.AddAnimation("IdleNorth"    , 0, 128 * 2, 128, 128, 4, 0.1f);
            Player.sprite.AddAnimation("IdleNorthEast", 0, 128 * 3, 128, 128, 4, 0.1f);
            Player.sprite.AddAnimation("IdleEast"     , 0, 128 * 4, 128, 128, 4, 0.1f);
            Player.sprite.AddAnimation("IdleSouthEast", 0, 128 * 5, 128, 128, 4, 0.1f);
            Player.sprite.AddAnimation("IdleSouth"    , 0, 128 * 6, 128, 128, 4, 0.1f);
            Player.sprite.AddAnimation("IdleSouthWest", 0, 128 * 7, 128, 128, 4, 0.1f);

            Player.sprite.Position = new Vector2(100, 100);
            Player.sprite.DrawOffset = new Vector2(-24, -38);
            Player.sprite.CurrentAnimation = "IdleWest";
            Player.sprite.IsAnimating = true;

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

            Vector2 moveVector = Vector2.Zero;
            Vector2 moveDir = Vector2.Zero;
            string animation = "";

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.NumPad7))
            {
                moveDir = new Vector2(-2, -1);
                animation = "WalkNorthWest";
                moveVector += new Vector2(-2, -1);
            }

            if (ks.IsKeyDown(Keys.NumPad8))
            {
                moveDir = new Vector2(0, -1);
                animation = "WalkNorth";
                moveVector += new Vector2(0, -1);
            }

            if (ks.IsKeyDown(Keys.NumPad9))
            {
                moveDir = new Vector2(2, -1);
                animation = "WalkNorthEast";
                moveVector += new Vector2(2, -1);
            }

            if (ks.IsKeyDown(Keys.NumPad4))
            {
                moveDir = new Vector2(-2, 0);
                animation = "WalkWest";
                moveVector += new Vector2(-2, 0);
            }

            if (ks.IsKeyDown(Keys.NumPad6))
            {
                moveDir = new Vector2(2, 0);
                animation = "WalkEast";
                moveVector += new Vector2(2, 0);
            }

            if (ks.IsKeyDown(Keys.NumPad1))
            {
                moveDir = new Vector2(-2, 1);
                animation = "WalkSouthWest";
                moveVector += new Vector2(-2, 1);
            }

            if (ks.IsKeyDown(Keys.NumPad2))
            {
                moveDir = new Vector2(0, 1);
                animation = "WalkSouth";
                moveVector += new Vector2(0, 1);
            }

            if (ks.IsKeyDown(Keys.NumPad3))
            {
                moveDir = new Vector2(2, 1);
                animation = "WalkSouthEast";
                moveVector += new Vector2(2, 1);
            }
            //// TODO: Add your update logic here

            //Point nopoint;
            //Point where = myMap.WorldToMapCell(new Point(Mouse.GetState().X, Mouse.GetState().Y), out nopoint);

            if (myMap.GetCellAtWorldPoint(Player.sprite.Position + moveDir).Walkable == false)
            {
                moveDir = Vector2.Zero;
            }

            if (Math.Abs(myMap.GetOverallHeight(Player.sprite.Position) - myMap.GetOverallHeight(Player.sprite.Position + moveDir)) > 10)
            {
                moveDir = Vector2.Zero;
            }

            if (moveDir.Length() != 0)
            {
                Player.sprite.MoveBy((int)moveDir.X, (int)moveDir.Y);
                if (Player.sprite.CurrentAnimation != animation)
                    Player.sprite.CurrentAnimation = animation;
            }
            else
            {
                if(Player.sprite.CurrentAnimation.Substring(0,4) != "Idle")
                    Player.sprite.CurrentAnimation = "Idle" + Player.sprite.CurrentAnimation.Substring(4);
            }
            float vladX = MathHelper.Clamp(
                Player.sprite.Position.X, 0 + Player.sprite.DrawOffset.X, Camera.WorldWidth);
            float vladY = MathHelper.Clamp(
                Player.sprite.Position.Y, 0 + Player.sprite.DrawOffset.Y, Camera.WorldHeight);

            Player.sprite.Position = new Vector2(vladX, vladY);

            Vector2 testPosition = Camera.WorldToScreen(Player.sprite.Position);

            if (testPosition.X < 100)
            {
                Camera.Move(new Vector2(testPosition.X - 100, 0));
            }

            if (testPosition.X > (Camera.ViewWidth - 100))
            {
                Camera.Move(new Vector2(testPosition.X - (Camera.ViewWidth - 100), 0));
            }

            if (testPosition.Y < 100)
            {
                Camera.Move(new Vector2(0, testPosition.Y - 100));
            }

            if (testPosition.Y > (Camera.ViewHeight - 100))
            {
                Camera.Move(new Vector2(0, testPosition.Y - (Camera.ViewHeight - 100)));
            }

            Player.sprite.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileStepX, Camera.Location.Y % Tile.TileStepY);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;

            float maxdepth = ((myMap.MapWidth + 1) * ((myMap.MapHeight + 1) * Tile.TileWidth)) / 10;
            float depthOffset;

            Point vladMapPoint = myMap.WorldToMapCell(new Point((int)Player.sprite.Position.X, (int)Player.sprite.Position.Y));

            for (int y = 0; y < squaresDown; y++)
            {
                int rowOffset = 0;
                if ((firstY + y) % 2 == 1)
                    rowOffset = Tile.OddRowXOffset;

                for (int x = 0; x < squaresAcross; x++)
                {
                    int mapx = (firstX + x);
                    int mapy = (firstY + y);
                    depthOffset = 0.7f - ((mapx + (mapy * Tile.TileWidth)) / maxdepth);

                    if ((mapx >= myMap.MapWidth) || (mapy >= myMap.MapHeight))
                        continue;

                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].BaseTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2((mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            1.0f);
                    }
                    int heightRow = 0;

                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].HeightTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2(
                                    (mapx * Tile.TileStepX) + rowOffset,
                                    mapy * Tile.TileStepY - (heightRow * Tile.HeightTileOffset))),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            depthOffset - ((float)heightRow * heightRowDepthMod));
                        heightRow++;
                    }

                    foreach (int tileID in myMap.Rows[y + firstY].Columns[x + firstX].TopperTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2((mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            depthOffset - ((float)heightRow * heightRowDepthMod));
                    }

                    if ((mapx == vladMapPoint.X) && (mapy == vladMapPoint.Y))
                    {
                        Player.sprite.DrawDepth = depthOffset - (float)(heightRow + 2) * heightRowDepthMod;
                    }

                    //spriteBatch.DrawString(pericles6, (x + firstX).ToString() + ", " + (y + firstY).ToString(),
                    //    new Vector2((x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX + 24,
                    //        (y * Tile.TileStepY) - offsetY + baseOffsetY + 48), Color.White, 0f, Vector2.Zero,
                    //        1.0f, SpriteEffects.None, 0.0f);
                }
            }

            Player.sprite.Draw(spriteBatch, 0, -myMap.GetOverallHeight(Player.sprite.Position));

            Vector2 hilightLoc = Camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            Point hilightPoint = myMap.WorldToMapCell(new Point((int)hilightLoc.X, (int)hilightLoc.Y));

            int hilightrowOffset = 0;
            if ((hilightPoint.Y) % 2 == 1)
                hilightrowOffset = Tile.OddRowXOffset;

            spriteBatch.Draw(
                            hilight,
                            Camera.WorldToScreen(
                            new Vector2(
                                (hilightPoint.X * Tile.TileStepX) + hilightrowOffset,
                                (hilightPoint.Y + 2) * Tile.TileStepY)),
                            new Rectangle(0, 0, 64, 32),
                            Color.White * 0.3f,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            0.0f);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
