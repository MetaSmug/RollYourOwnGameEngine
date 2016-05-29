/* 
© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)

*/


using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Ruge.DragonDrop;

namespace Demo.DragonDrop {

    class Card : IDragonDropItem {


        private readonly SpriteBatch spriteBatch;

        private Vector2 _position;

        public Vector2 Position {

            get { return _position; }

            set { _position = value; OnPositionUpdate(); }

        }
        public bool IsSelected { get; set; }
        public bool IsMouseOver { get; set; }
        public Rectangle Border => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        public bool IsDraggable { get; set; } = true;
        public int ZIndex { get; set; } = 0;
        public Texture2D Texture { get; }
        private bool returnToOrigin = false;
        private Card child;

        private const int ON_TOP = 1000;

        public Card(SpriteBatch sb, Texture2D texture, Vector2 position, int value) {
            spriteBatch = sb;
            Texture = texture;
            Position = position;
            origin = position;
            stackValue = value;
            resetMe = origin;
        }

        public void OnSelected() {

            if (IsDraggable) {
                IsSelected = true;
                ZIndex += ON_TOP;
            }

            
            if (child != null) FixChildren(child);
        }

        public void OnDeselected() {

            IsSelected = false;


            if (Position != origin) returnToOrigin = true;

        }

        public void OnPositionUpdate() {
            
            if (child != null) {
                var pos = Position;

                pos.Y = Position.Y + 28;
                pos.X = Position.X;

                child.origin = pos;
                child.Position = pos;

                child.ZIndex = ZIndex + 1;

            }
            

        }

        public bool Contains(Vector2 pointToCheck) {
            var mouse = new Point((int)pointToCheck.X, (int)pointToCheck.Y);
            return Border.Contains(mouse);
        }

        private Vector2 origin, resetMe;
        public int stackValue { get; set; }


        public void Reset() {

            origin = resetMe;
            child = null;
            ZIndex = ON_TOP;

            if (Position != origin) returnToOrigin = true;

        }

        public void OnCollusion(IDragonDropItem item) {

            var destination = (Card) item;
            
            Console.WriteLine(stackValue + " (" + ZIndex + ") -> " + destination.stackValue + " (" + destination.ZIndex + ")");

            if (stackValue + 1 == destination.stackValue) {

                destination.child = this;
                Position = new Vector2(destination.Position.X, destination.Position.Y + 28);
                origin = Position;
                ZIndex = destination.ZIndex + 1;

            }


        }

        public void Update(GameTime gameTime) {


            if (returnToOrigin) {

                returnToOrigin = !ReturnToOrigin();

            }
            else if (!IsSelected) {
                
                while (ZIndex >= ON_TOP) ZIndex -= ON_TOP;

            }




        }

        private void FixChildren(Card child) {

            child.ZIndex = ZIndex + 1;

            if (child.child != null) FixChildren(child.child);

        }



        /// <summary>
        /// Animation for returning the card to its original position if it can't find a new place to snap to
        /// </summary>
        /// <returns>returns true if the card is back in its original position; otherwise it increments the animation</returns>
        private bool ReturnToOrigin() {

            bool backAtOrigin = false;

            var pos = Position;
            float speed = 25.0f;

            float distance = (float)Math.Sqrt(Math.Pow(origin.X - pos.X, 2) + (float)Math.Pow(origin.Y - pos.Y, 2));
            float directionX = (origin.X - pos.X) / distance;
            float directionY = (origin.Y - pos.Y) / distance;

            pos.X += directionX * speed;
            pos.Y += directionY * speed;


            if (Math.Sqrt(Math.Pow(pos.X - Position.X, 2) + Math.Pow(pos.Y - Position.Y, 2)) >= distance) {

                Position = origin;

                backAtOrigin = true;

                ZIndex -= ON_TOP;

            }
            else Position = pos;

            return backAtOrigin;

        }



        public void Draw(GameTime gameTime) {


            var colorToUse = Color.White;


            // uncomment below to add mouseover hover coloring

            if (IsDraggable) { 

                if (IsSelected) {
                    colorToUse = Color.Orange;
                }
                else {
                    if (IsMouseOver) { colorToUse = Color.Cyan; }
                }
            }

            spriteBatch.Draw(Texture, Position, colorToUse);

        }


    }
}
