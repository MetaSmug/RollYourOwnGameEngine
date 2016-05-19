﻿/* 
© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)

Attribution: adapted from XNA example by Jakob Krarup (xnafan.net)
 
 */
 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Ruge.DragonDrop;
using MonoGame.Extended.ViewportAdapters;

namespace Demo.DragonDrop {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DragonDropDemo : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ViewportAdapter viewport;
        DragonDrop<Card> dragonDrop;
        MouseState oldMouse, currentMouse;

        Texture2D background, slot, reset;
        Rectangle resetRect;


        public DragonDropDemo() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            this.Window.Title = "The Ruge Project - DragonDrop Demo";

            // the odd screen size is just to make the math easy for placing the items
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 750;

            this.Window.AllowUserResizing = true;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {

            // viewport allows for dynamic screen scaling
            viewport = new BoxingViewportAdapter(Window, GraphicsDevice, 1000, 750);




            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            dragonDrop = new DragonDrop<Card>(this, spriteBatch, viewport);

            background = Content.Load<Texture2D>("grass");
            slot = Content.Load<Texture2D>("slot");
            reset = Content.Load<Texture2D>("reset");
            resetRect = new Rectangle(350, 600, reset.Width, reset.Height);

            // make this slot droppable
            Card slotItem = new Card(spriteBatch, slot, new Vector2(425, 325), 0);
            slotItem.IsDraggable = false;
            slotItem.ZIndex = -2000;
            dragonDrop.Add(slotItem);

            dragonDrop.Add(new Card(spriteBatch, Content.Load<Texture2D>("2"), new Vector2(25, 50), 2));
            dragonDrop.Add(new Card(spriteBatch, Content.Load<Texture2D>("3"), new Vector2(225, 50), 3));
            dragonDrop.Add(new Card(spriteBatch, Content.Load<Texture2D>("4"), new Vector2(425, 50), 4));
            dragonDrop.Add(new Card(spriteBatch, Content.Load<Texture2D>("5"), new Vector2(625, 50), 5));
            dragonDrop.Add(new Card(spriteBatch, Content.Load<Texture2D>("6"), new Vector2(825, 50), 6));

            Components.Add(dragonDrop);


            MouseState oldMouse = Mouse.GetState();

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

            MouseState currentMouse = Mouse.GetState();

            Point point = viewport.PointToScreen(currentMouse.X, currentMouse.Y);

            if (currentMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released) {

                if (resetRect.Contains(point)) {

                    foreach (var item in dragonDrop.Items) item.Reset();

                }

            }
            oldMouse = currentMouse;


            foreach (Card item in dragonDrop.Items) item.Update(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.PaleGreen);


            var destinationRectangle = new Rectangle(0, 0, 1000, 750);

            spriteBatch.Begin(transformMatrix: viewport.GetScaleMatrix());

            spriteBatch.Draw(background, destinationRectangle, Color.White);
            spriteBatch.Draw(reset, resetRect, Color.White);
            spriteBatch.Draw(slot, new Rectangle(25, 50, slot.Width, slot.Height), Color.Black);
            spriteBatch.Draw(slot, new Rectangle(225, 50, slot.Width, slot.Height), Color.Black);
            spriteBatch.Draw(slot, new Rectangle(425, 50, slot.Width, slot.Height), Color.Black);
            spriteBatch.Draw(slot, new Rectangle(625, 50, slot.Width, slot.Height), Color.Black);
            spriteBatch.Draw(slot, new Rectangle(825, 50, slot.Width, slot.Height), Color.Black);


            foreach (Card item in dragonDrop.Items) {

                item.Draw(gameTime);

            }

            spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
