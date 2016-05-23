/* 
© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)

Sprite attribution:
http://opengameart.org/content/wraith-skelleton-king-from-dota-2-pixel-style
https://www.pinterest.com/pin/88946161366186725/

(I don't recall where the last sprite sheet came from, so please don't use it in your projects.)

*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Ruge.Sprites;
using MonoGame.Ruge.ViewportAdapters;
using System.Collections.Generic;

namespace Demo.SpriteAnimation {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SpriteAnimationDemo : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spriteSheet, spriteSheet2, sprite;
        SpriteRPGPlayer player, selectedPlayer, selectedPlayer2;
        SpriteRPG playerGroup, playerGroup2;
        
        float time;
        float frameTime = .2f;

        public SpriteAnimationDemo() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 550;
            graphics.PreferredBackBufferHeight = 200;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteSheet = Content.Load<Texture2D>("sprite_sheet");
            spriteSheet2 = Content.Load<Texture2D>("NPC_20");
            sprite = Content.Load<Texture2D>("sprite");

            player = new SpriteRPGPlayer(4, 16, 16);
            player.position = new Vector2(50, 90);

            playerGroup = new SpriteRPG(8, 4, 3, 32, 32);

            selectedPlayer = playerGroup.player[0];
            selectedPlayer.position = new Vector2(250, 90);

            playerGroup2 = new SpriteRPG(6, 4, 3, 32, 45);

            selectedPlayer2 = playerGroup2.player[0];
            selectedPlayer2.position = new Vector2(450, 90);

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

                if (Keyboard.GetState().IsKeyDown(Keys.A)) MoveLeft();
                if (Keyboard.GetState().IsKeyDown(Keys.S)) MoveDown();
                if (Keyboard.GetState().IsKeyDown(Keys.D)) MoveRight();
                if (Keyboard.GetState().IsKeyDown(Keys.W)) MoveUp();
                if (Keyboard.GetState().IsKeyDown(Keys.Left)) PrevPlayer();
                if (Keyboard.GetState().IsKeyDown(Keys.Right)) NextPlayer();

                GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
                GamePadState controller = GamePad.GetState(PlayerIndex.One);
                
                if (capabilities.IsConnected) {

                    if (capabilities.HasLeftXThumbStick) {
                        if (controller.ThumbSticks.Left.X < 0) MoveLeft();
                        if (controller.ThumbSticks.Left.X > 0) MoveRight();
                        if (controller.ThumbSticks.Left.Y < 0) MoveDown();
                        if (controller.ThumbSticks.Left.Y > 0) MoveUp();
                    }

                    if (controller.IsButtonDown(Buttons.LeftShoulder)) PrevPlayer();
                    if (controller.IsButtonDown(Buttons.RightShoulder)) NextPlayer();
                    
                }


                time = 0f;
            }

            base.Update(gameTime);
        }


        private void MoveUp() {
            player.moveUp();
            selectedPlayer.moveUp();
            selectedPlayer2.moveUp();
        }
        private void MoveDown() {
            player.moveDown();
            selectedPlayer.moveDown();
            selectedPlayer2.moveDown();
        }
        private void MoveLeft() {
            player.moveLeft();
            selectedPlayer.moveLeft();
            selectedPlayer2.moveLeft();
        }
        private void MoveRight() {
            player.moveRight();
            selectedPlayer.moveRight();
            selectedPlayer2.moveRight();
        }

        private void NextPlayer() {


            Vector2 position = selectedPlayer.position;
            Vector2 position2 = selectedPlayer2.position;

            if (selectedPlayer.index == (playerGroup.Count - 1)) selectedPlayer = playerGroup.player[0];
            else selectedPlayer = playerGroup.player[selectedPlayer.index + 1];

            if (selectedPlayer2.index == (playerGroup2.Count - 1)) selectedPlayer2 = playerGroup2.player[0];
            else selectedPlayer2 = playerGroup2.player[selectedPlayer2.index + 1];

            selectedPlayer.position = position;
            selectedPlayer2.position = position2;
        }

        private void PrevPlayer() {

            Vector2 position = selectedPlayer.position;
            Vector2 position2 = selectedPlayer2.position;

            if (selectedPlayer.index == 0) selectedPlayer = playerGroup.player[playerGroup.Count - 1];
            else selectedPlayer = playerGroup.player[selectedPlayer.index - 1];

            if (selectedPlayer2.index == 0) selectedPlayer2 = playerGroup2.player[playerGroup2.Count - 1];
            else selectedPlayer2 = playerGroup2.player[selectedPlayer2.index - 1];

            selectedPlayer.position = position;
            selectedPlayer2.position = position2;

        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // pointClamp is important to set in if you're going to do rectangle clipping of sprite sheets
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(sprite, player.position, player.rect, Color.White, 0.0f, player.origin, 6.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(spriteSheet, selectedPlayer.position, selectedPlayer.rect, Color.White, 0.0f, selectedPlayer.origin, 4.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(spriteSheet2, selectedPlayer2.position, selectedPlayer2.rect, Color.White, 0.0f, selectedPlayer2.origin, 4.0f, SpriteEffects.None, 0.0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
