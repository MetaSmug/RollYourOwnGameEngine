/* 
© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)

 */

using MonoGame.Ruge.DragonDrop;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Ruge.CardEngine {

    public class Slot : IDragonDropItem {

        public Vector2 Position { get; set; }
        public bool IsSelected { get; set; }
        public bool IsMouseOver { get; set; }
        public int ZIndex { get; set; }
        public Texture2D Texture { get; set; }

        public bool IsDraggable { get; set; } = false;
        public bool IsVisible { get; set; } = true;

        private SpriteBatch _spriteBatch;


        public Rectangle Border {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }


        #region constructor 

        public Slot(Texture2D tex, SpriteBatch sb, Vector2 pos) {

            Position = pos;
            Texture = tex;
            _spriteBatch = sb;

        }

        #endregion

        #region methods


        public bool Contains(Vector2 pointToCheck) {
            Point mouse = new Point((int)pointToCheck.X, (int)pointToCheck.Y);
            return Border.Contains(mouse);
        }

        public void Draw(GameTime gameTime) {
            _spriteBatch.Draw(Texture, Position, Color.White);
        }

        #endregion

        #region overrides

        // todo: override methods to set up your logic

        public void OnSelected() { }
        public void OnDeselected() { }

        public void Update(GameTime gameTime) { }
        public void HandleCollusion(IDragonDropItem item) { }

        #endregion



    }

}
