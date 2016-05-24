/* 
© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)

 */

using System;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Ruge.CardEngine {

    class Deck : Stack {

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="cardBack"></param>
        /// <param name="sb"></param>
        public Deck(Texture2D cardBack, SpriteBatch sb) : base(cardBack, sb) {

            type = StackType.deck;

        }

        /// <summary>
        /// populate your deck with a typical set of cards
        /// </summary>
        public void freshDeck() {

            cards.Clear();

            foreach (Suit mySuit in Enum.GetValues(typeof(Suit))) {

                foreach (Rank myRank in Enum.GetValues(typeof(Rank))) {

                    cards.Add(new Card(myRank, mySuit, _cardBack, _spriteBatch));

                }

            }

        }

        /// <summary>
        /// makes a smaller random deck for testing
        /// </summary>
        /// <param name="numCards"></param>
        public void testDeck(int numCards) {

            cards.Clear();

            Deck subDeck = new Deck(_cardBack, _spriteBatch);
            subDeck.freshDeck();
            subDeck.shuffle();

            if (numCards <= subDeck.Count) {

                for (int i = 0; i < numCards; i++) {
                    cards.Add(subDeck.drawCard());
                }

            }

            subDeck = null;

        }
        


    }

}