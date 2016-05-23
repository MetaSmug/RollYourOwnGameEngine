/*
The MIT License (MIT)

Copyright ©2016 HathorsLove.com

Creative Commons Assets Used:
http://opengameart.org/content/girl-animated?destination=node/55810
http://opengameart.org/content/parallax-mountain-background

Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
and associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, 
sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies 
or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR 
PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
DEALINGS IN THE SOFTWARE.
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Girl_Run_Animation {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        Texture2D spriteSheet, girlStill;
        Texture2D mountainBG, skyBG, smallCloud, bigCloud, smallMountain, bigMountain, ground, sun;

        int smallCloudX = 50;
        int bigCloudX = 660;
        int smallMountainX = 850;
        int smallMountainX2 = 1660;
        int smallMountainX3 = 40;
            
        int bigMountainX, groundX, sunY;

        int lastSlowMountainX;

        bool mountainSlow = false;


        bool isMoving = false;
        Rectangle source;
        Vector2 position, origin;
        SpriteEffects fx = SpriteEffects.None;

        float time;
        float frameTime = 0.1f;
        int frameIndex;
        const int totalFrames = 16;
        const int frameRows = 2;
        const int framesPerRow = totalFrames / frameRows;

        const int frameWidth = 248;
        const int frameHeight = 404;

        const int girlY = 600;

        int horizontalPosition = frameWidth/4;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

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



            // TODO: use this.Content to load your game content here.

            // attribution: http://opengameart.org/content/girl-animated?destination=node/55810
            spriteSheet = Content.Load<Texture2D>("girl run ss");
            girlStill = Content.Load<Texture2D>("girl still");

            // attribution: http://opengameart.org/content/parallax-mountain-background
            mountainBG = Content.Load<Texture2D>("mountain_bg");
            bigCloud = Content.Load<Texture2D>("big cloud");
            smallCloud = Content.Load<Texture2D>("small cloud");
            bigMountain = Content.Load<Texture2D>("big mountain");
            smallMountain = Content.Load<Texture2D>("small mountain");
            sun = Content.Load<Texture2D>("sun");
            skyBG = Content.Load<Texture2D>("sky bg");
            ground = Content.Load<Texture2D>("ground");

            sunY = 700;

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

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                isMoving = true;
                fx = SpriteEffects.None;
                horizontalPosition += 4;

                if (horizontalPosition - (frameWidth / 2) > Window.ClientBounds.Width) {

                    horizontalPosition = 0;

                }

                // make the sun rise
                sunY -= 2;
                if (sunY < -sun.Height) sunY = 720;

                groundX -= 2;
                if (groundX < -ground.Width) groundX = 1280;

                if (mountainSlow) {
                    bigMountainX--;
                    mountainSlow = false;
                }
                else mountainSlow = true;
                if (bigMountainX < -bigMountain.Width) bigMountainX = 1280;

                smallMountainX--;
                smallMountainX2--;
                smallMountainX3--;
                if (smallMountainX < -smallMountain.Width) smallMountainX = 1280;

                smallCloudX += 2;
                if (smallCloudX > Window.ClientBounds.Width) smallCloudX = -smallCloud.Width;

                bigCloudX--;
                if (bigCloudX < -bigCloud.Width) bigCloudX = 1280;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                isMoving = true;
                fx = SpriteEffects.FlipHorizontally;
                horizontalPosition -= 4;
                
                if (horizontalPosition < (0 - (frameWidth / 2))){

                    horizontalPosition = Window.ClientBounds.Width;

                }

                // make the sun set
                sunY += 2;
                if (sunY > Window.ClientBounds.Height) sunY = -sun.Height;

                groundX += 2;
                if (groundX > Window.ClientBounds.Width) groundX = -ground.Width;


                if (mountainSlow) {
                    bigMountainX++;
                    mountainSlow = false;
                }
                else mountainSlow = true;
                if (bigMountainX > Window.ClientBounds.Width) bigMountainX = -bigMountain.Width;

                smallMountainX++;
                smallMountainX2++;
                smallMountainX3++;
                if (smallMountainX > Window.ClientBounds.Width) smallMountainX = -smallMountain.Width;

                smallCloudX -= 2;
                if (smallCloudX < -smallCloud.Width) smallCloudX = 1280;

                bigCloudX++;
                if (bigCloudX > Window.ClientBounds.Width) bigCloudX = -bigCloud.Width;

            }
            else isMoving = false;
            
            // determines which row to look for the sprite, minus 1 to zero-index the result
            int frameRow = (int)Math.Ceiling((decimal)frameIndex / framesPerRow) - 1;

            //Console.WriteLine(frameIndex + "," + frameRow);

            // seems kinda messy but I'm not sure of a more elegant solution
            int x = ((frameIndex - 1) - (framesPerRow * frameRow)) * frameWidth;

            // Calculate the source rectangle of the current frame.
            source = new Rectangle(x, frameHeight * frameRow, frameWidth, frameHeight);

            //Console.WriteLine(source);

            // Calculate position and origin to draw in the center of the screen
            position = new Vector2(horizontalPosition, girlY);
            origin = new Vector2(frameWidth / 2.0f, frameHeight / 2.0f);           


            // Process elapsed time
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            while (time > frameTime) {
                // Play the next frame in the SpriteSheet
                frameIndex++;


                // reset elapsed time
                time = 0f;
            }
            if (frameIndex > totalFrames) frameIndex = 1;
            
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here            
            
            spriteBatch.Begin();

            spriteBatch.Draw(skyBG, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            spriteBatch.Draw(sun, new Rectangle(900, sunY, sun.Width, sun.Height), Color.White);
            spriteBatch.Draw(smallCloud, new Rectangle(smallCloudX, 100, smallCloud.Width, smallCloud.Height), Color.White);
            spriteBatch.Draw(bigCloud, new Rectangle(bigCloudX, 230, bigCloud.Width, bigCloud.Height), Color.White);
            spriteBatch.Draw(bigMountain, new Rectangle(bigMountainX, 375, bigMountain.Width, bigMountain.Height), Color.White);
            spriteBatch.Draw(smallMountain, new Rectangle(smallMountainX, 495, smallMountain.Width, smallMountain.Height), Color.White);
            spriteBatch.Draw(smallMountain, new Rectangle(smallMountainX2, 495, smallMountain.Width, smallMountain.Height), Color.White);
            spriteBatch.Draw(smallMountain, new Rectangle(smallMountainX3, 495, smallMountain.Width, smallMountain.Height), Color.White);

            if (smallMountainX != lastSlowMountainX) {
                Console.WriteLine(smallMountainX + "," + (smallMountainX + (smallMountain.Width * 2)) + "," +
                    (smallMountainX - (smallMountain.Width * 2)));
                lastSlowMountainX = smallMountainX;
            }

            spriteBatch.Draw(ground, new Rectangle(groundX, 590, ground.Width, ground.Height), Color.White);

            // cloning the ground texture to make it wrap around
            int ground2x = 0;

            if (groundX >= 0) ground2x = groundX - ground.Width;
            else if (groundX < 0) ground2x = groundX + ground.Width;

            spriteBatch.Draw(ground, new Rectangle(ground2x, 590, ground.Width, ground.Height), Color.White);


            // Draw the current frame.
            if (isMoving) spriteBatch.Draw(spriteSheet, position, source, Color.White, 0.0f, origin, .5f, fx, 0.0f);
            else spriteBatch.Draw(girlStill, position, new Rectangle(0, 0, 293, frameHeight), Color.White, 0.0f, origin, .5f, fx, 0.0f);

            spriteBatch.End();
            


            base.Draw(gameTime);
        }
    }
}
