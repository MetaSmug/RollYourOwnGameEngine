/* 
© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)


Sprite attribution:
http://opengameart.org/content/wraith-skelleton-king-from-dota-2-pixel-style
https://www.pinterest.com/pin/88946161366186725/

*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Ruge.Sprites;
using System.Collections.Generic;

namespace Demo.SpriteAnimation {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SpriteAnimationDemo : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spriteSheet, sprite;
        SpriteRPGPlayer player, selectedPlayer;
        SpriteRPG playerGroup;

        float time;
        float frameTime = .2f;

        public SpriteAnimationDemo() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //spriteSheet = Content.Load<Texture2D>("NPC_20");
            spriteSheet = Content.Load<Texture2D>("sprite_sheet");
            sprite = Content.Load<Texture2D>("sprite");

            player = new SpriteRPGPlayer(4, 16, 16);
            player.position = new Vector2(100, 100);

            //selectedPlayer = new SpriteRPGPlayer(3, 32, 45);
            selectedPlayer = new SpriteRPGPlayer(3, 32, 32);
            selectedPlayer.position = new Vector2(300, 100);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // Process elapsed time
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            while (time > frameTime) {

                GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
                
                GamePadState controller = GamePad.GetState(PlayerIndex.One);


                if (capabilities.IsConnected) {

                    if (capabilities.HasLeftXThumbStick) {
                        if (controller.ThumbSticks.Left.X < 0) {
                            player.moveLeft();
                            selectedPlayer.moveLeft();
                        }
                        if (controller.ThumbSticks.Left.X > 0) {
                            player.moveRight();
                            selectedPlayer.moveRight();
                        }
                        if (controller.ThumbSticks.Left.Y < 0) {
                            player.moveDown();
                            selectedPlayer.moveDown();
                        }
                        if (controller.ThumbSticks.Left.Y > 0) {
                            player.moveUp();
                            selectedPlayer.moveUp();
                        }

                    }

                }


                time = 0f;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(sprite, player.position, player.rect, Color.White, 0.0f, player.origin, 6.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(spriteSheet, selectedPlayer.position, selectedPlayer.rect, Color.White, 0.0f, selectedPlayer.origin, 4.0f, SpriteEffects.None, 0.0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
