﻿/* 
© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)
 
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Ruge.DragonDrop;

namespace Demo.DragonDrop {

    class Card : IDragonDropItem {

        private Vector2 _position;

        public Vector2 Position {

            get { return _position; }

            set { _position = value; OnPositionUpdate(); }

        }

        public Texture2D Texture { get; set; }
        public Card Child { get; set; }
        public bool IsDraggable { get; set; } = true;

        private const int ON_TOP = 1000;

        // many examples use an enum for ZIndex, which is great for many 
        // applications but an int seems easier for playing card stacks
        public int ZIndex {
            get { return _zIndex; }
            set {
                _zIndex = value;
                if (hasChild) Child.ZIndex = _zIndex + 1;
            }
        }

        public int stackValue;
        private int _zIndex;

        private Vector2 origin, resetMe;
        private bool returnToOrigin = false;
        public bool hasChild = false;
        public bool isDiscarded = false;


        private readonly SpriteBatch spriteBatch;

        public bool IsSelected { get; set; }
        public bool IsMouseOver { get; set; }
        public bool Contains(Vector2 pointToCheck) {
            Point mouse = new Point((int)pointToCheck.X, (int)pointToCheck.Y);
            return Border.Contains(mouse);
        }

        public void Reset() {

            if (isDiscarded) {
                IsDraggable = true;
                _zIndex = -stackValue;
            }
            origin = resetMe;
            hasChild = false;
            isDiscarded = false;
            if (Position != origin) returnToOrigin = true;
        }

        public void OnPositionUpdate() {

            if (hasChild) {

                Card child = Child;

                if (!child.isDiscarded) {
                    var pos = Position;

                    pos.Y = Position.Y + 28;
                    pos.X = Position.X;

                    child.origin = pos;
                    child.Position = pos;

                    Child = child;
                }
            }

        }

        public void OnSelected() {

            if (IsDraggable) {
                IsSelected = true;
                ZIndex += ON_TOP;
            }

        }

        public void OnDeselected() {

            // don't reset ZIndex here; let returnToOrigin handle it

            IsSelected = false;

            if (Position != origin) returnToOrigin = true;

        }

        public void HandleCollusion(IDragonDropItem item) {

            if (item.Border.Intersects(Border)) {
                Card parent = (Card)item;

                if (!isDiscarded) {
                    if (parent.stackValue == (stackValue + 1)) {
                        parent.AttachChild(this);
                        item = parent;
                    }
                    else if (!parent.IsDraggable && !hasChild && (parent.stackValue == 0)) {

                        ZIndex = parent.ZIndex + 1;
                        Position = parent.Position;
                        origin = Position;
                        isDiscarded = true;
                        IsDraggable = false;

                    }
                }
            }



        }


        public void AttachChild(Card child) {

            var pos = Position;

            pos.Y = Position.Y + 28;
            pos.X = Position.X;

            child.origin = pos;
            child.Position = pos;

            Child = child;
            hasChild = true;

            ZIndex = -stackValue;

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

                ZIndex = -stackValue;

            }
            else Position = pos;

            return backAtOrigin;

        }


        public Card(SpriteBatch sb, Texture2D texture, Vector2 position, int value) {
            spriteBatch = sb;
            Texture = texture;
            Position = position;
            origin = position;
            stackValue = value;
            ZIndex = -stackValue;
            resetMe = origin;
        }

        public void Update(GameTime gameTime) {

            // feels kinda hacky but this fixes a really annoying bug
            if (hasChild && Child.isDiscarded) hasChild = false;

            if (returnToOrigin) {

                returnToOrigin = !ReturnToOrigin();

            }


        }

        public void Draw(GameTime gameTime) {
            Color colorToUse = Color.White;


            // uncomment below to add mouseover hover coloring

            if (IsSelected) {
                colorToUse = Color.Orange;
            }
            else {
                if (IsMouseOver) { colorToUse = Color.Cyan; }
            }


            spriteBatch.Draw(Texture, Position, colorToUse);
        }







        public Rectangle Border {
            get {

                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

            }
        }


    }

}
