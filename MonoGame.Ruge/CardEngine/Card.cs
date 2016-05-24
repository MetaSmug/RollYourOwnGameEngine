/* 

© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)

*/

using MonoGame.Ruge.DragonDrop;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Ruge.CardEngine {

    enum Suit {

        clubs,
        hearts,
        diamonds,
        spades

    };

    enum CardColor {

        red,
        black

    }

    enum Rank {
        _A,
        _2,
        _3,
        _4,
        _5,
        _6,
        _7,
        _8,
        _9,
        _10,
        _J,
        _Q,
        _K
    }

    class Card : IDragonDropItem {


        public Rank rank;
        public Suit suit;

        public bool isFaceUp = false;
        public bool faceUp { get { return isFaceUp; } }

        protected Texture2D _cardback, _texture;
        private readonly SpriteBatch _spriteBatch;

        #region properties

        public Card Child { get; set; }
        public bool IsSelected { get; set; }
        public bool IsMouseOver { get; set; }
        public bool IsDraggable { get; set; }
        public int ZIndex { get; set; }


        public Rectangle Border {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public bool Contains(Vector2 pointToCheck) {
            Point mouse = new Point((int)pointToCheck.X, (int)pointToCheck.Y);
            return Border.Contains(mouse);
        }



        private Vector2 _position;

        public Vector2 Position {

            get { return _position; }

            set { _position = value; OnPositionUpdate(); }

        }


        public Texture2D Texture {

            get {

                if (isFaceUp) return _texture;
                else return _cardback;

            }

        }

        #endregion


        #region constructor


        public Card(Rank rank, Suit suit, Texture2D cardback, SpriteBatch sb) {

            _spriteBatch = sb;
            this.rank = rank;
            this.suit = suit;
            _cardback = cardback;

        }

        #endregion


        #region methods

        public void flipCard() {
            isFaceUp = !isFaceUp;
        }

        public void Draw(GameTime gameTime) {
            _spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void SetTexture(Texture2D texture) { _texture = texture; }

        #endregion



        #region overrides

        /// <summary>
        /// todo: add selection logic
        /// </summary>
        public void OnSelected() { }

        /// <summary>
        /// todo: add deselection logic
        /// </summary>
        public void OnDeselected() { }

        /// <summary>
        /// todo: override with your collusion handling code
        /// </summary>
        /// <param name="item"></param>
        public void HandleCollusion(IDragonDropItem item) { }

        /// <summary>
        /// todo: override with logic if you want to do something while the card is moving, like drag child cards
        /// </summary>
        public void OnPositionUpdate() { }

        /// <summary>
        /// todo: override Update if needed
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime) { }


        #endregion


        }

}
