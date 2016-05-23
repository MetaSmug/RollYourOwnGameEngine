using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Animations;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;
using System;

namespace TileMap_Test {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TileMapGame : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Camera2D camera;
        TiledMap tiledMap;
        ViewportAdapter viewport;
        
        SpriteSheetAnimator player;
        Sprite sprite;


        public TileMapGame() {
            graphics = new GraphicsDeviceManager(this) { SynchronizeWithVerticalRetrace = false };
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            viewport = new BoxingViewportAdapter(Window, GraphicsDevice, 1280, 720);

            camera = new Camera2D(viewport);

            Window.Title = "Tile Map Game (MonoGame.Extended)";
            
          //  Window.AllowUserResizing = true;


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //tiledMap = Content.Load<TiledMap>("untitled");
            tiledMap = Content.Load<TiledMap>("new test");


            var motwTexture = Content.Load<Texture2D>("NPC_20");
            var motwAtlas = TextureAtlas.Create(motwTexture, 32, 46);
            var motwAnimationFactory = new SpriteSheetAnimationFactory(motwAtlas);

            motwAnimationFactory.Add("idle", new SpriteSheetAnimationData(new[] { 1 }));
            motwAnimationFactory.Add("down", new SpriteSheetAnimationData(new[] { 0, 1, 2, 1 }, isLooping: false));
            motwAnimationFactory.Add("left", new SpriteSheetAnimationData(new[] { 12, 13, 14, 13 }, isLooping: false));
            motwAnimationFactory.Add("up", new SpriteSheetAnimationData(new[] { 36, 37, 38, 37 }, isLooping: false));
            motwAnimationFactory.Add("right", new SpriteSheetAnimationData(new[] { 24, 25, 26, 25 }, isLooping: false));
            player = new SpriteSheetAnimator(motwAnimationFactory);
            player.Play("idle");
            sprite = player.CreateSprite(new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2));




        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {


            //tiledMap.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            Console.Write(capabilities.ToString());

            GamePadState controller = GamePad.GetState(PlayerIndex.One);
            if (controller.IsButtonDown(Buttons.LeftTrigger)) camera.ZoomIn(deltaSeconds);
            if (controller.IsButtonDown(Buttons.RightTrigger)) camera.ZoomOut(deltaSeconds);
                
            
            if (capabilities.IsConnected){
                
                if (capabilities.HasLeftXThumbStick){
                    if (controller.ThumbSticks.Left.X < 0) MovePlayer("left");
                    if (controller.ThumbSticks.Left.X > 0) MovePlayer("right");
                    if (controller.ThumbSticks.Left.Y < 0) MovePlayer("down");
                    if (controller.ThumbSticks.Left.Y > 0) MovePlayer("up");
                }

            }

            

            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up)) MovePlayer("up");

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left)) MovePlayer("left");

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down)) MovePlayer("down");

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right)) MovePlayer("right");


            if ((keyboardState.IsKeyDown(Keys.R) || controller.IsButtonDown(Buttons.LeftTrigger))) camera.ZoomIn(deltaSeconds);

            if ((keyboardState.IsKeyDown(Keys.F) || controller.IsButtonDown(Buttons.RightTrigger))) camera.ZoomOut(deltaSeconds);

            camera.LookAt(player.TargetSprite.Position);

            player.Update(deltaSeconds);
            base.Update(gameTime);
        }

        void MovePlayer(string direction, float speed = 0.15f) {

            player.Play(direction);

            switch (direction) {

                case "left":

                    sprite.Position = new Vector2(sprite.Position.X - speed, sprite.Position.Y);

                    break;

                case "right":

                    sprite.Position = new Vector2(sprite.Position.X + speed, sprite.Position.Y);

                    break;

                case "up":

                    sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y - speed);

                    break;

                case "down":

                    sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y + speed);

                    break;



            }

        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());


            // you can draw the whole map all at once
            //spriteBatch.Draw(tiledMap, gameTime: gameTime);

            
            foreach (var layer in tiledMap.Layers) {
                //spriteBatch.Draw(layer, camera, gameTime);
                spriteBatch.Draw(layer, camera);
                if (layer.Name == "Grass") {
                    spriteBatch.Draw(sprite);
                }
            }
            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
