/* 
© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)

 */

using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Ruge.CardEngine {

    enum StackMethod {
        normal,
        horizontal,
        vertical
    }

    enum StackType {
        draw,
        discard,
        stack,
        deck,
        hand
    }

    class Stack {

        protected SpriteBatch _spriteBatch;

        protected Texture2D _cardBack;
        public Texture2D cardBack { get { return _cardBack; } }

        public List<Card> cards = new List<Card>();
                
        public int Count { get { return cards.Count; } }

        public StackType type = StackType.hand;
        public StackMethod method = StackMethod.normal;

        #region public methods

        public void addCard(Card card) { cards.Add(card); }
        public void Clear() { cards.Clear(); }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="cardBack"></param>
        /// <param name="sb"></param>
        public Stack(Texture2D cardBack, SpriteBatch sb) {
            _cardBack = cardBack;
            _spriteBatch = sb;
        }


        /// <summary>
        /// attempts to pull a specific card from your hand using the rank and suit
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        /// <returns>Card if found, null otherwise</returns>
        public Card playCard(Rank rank, Suit suit) {

            foreach (Card card in cards) {

                if ((card.rank == rank) && (card.suit == suit)) {

                    cards.Remove(card);
                    return card;

                }

            }

            return null;

        }

        /// <summary>
        /// pulls a specific card from your hand using the index
        /// todo: test this method
        /// </summary>
        /// <param name="cardIndex"></param>
        /// <returns>Card if found, null otherwise</returns>
        public Card playCard(int cardIndex) {

            if (cards.Contains(cards[cardIndex])) {
                Card card = cards[cardIndex];
                cards.RemoveAt(cardIndex);
                return card;
            }
            else { return null; }
        }


        /// <summary>
        /// just picks the top card on the stack and returns it
        /// </summary>
        /// <returns></returns>
        public Card drawCard() {

            if (cards.Count > 0) {

                Card topCard = cards[cards.Count - 1];
                cards.RemoveAt(cards.Count - 1);
                return topCard;

            }
            else { return null; }

        }


        public void shuffle() {

            //wait a few ms to avoid seed collusion
            Thread.Sleep(30);

            Random rand = new Random();
            for (int i = cards.Count - 1; i > 0; i--) {
                int randomIndex = rand.Next(i + 1);
                Card tempCard = cards[i];
                cards[i] = cards[randomIndex];
                cards[randomIndex] = tempCard;
            }
        }

        
        public void debug() {

            Console.WriteLine("===");

            if (cards.Count > 0) {
                foreach (Card card in cards) {

                    String strFaceUp = (card.faceUp ? "face up" : "face down");
                    Console.WriteLine(card.ZIndex.ToString("00") + ": " + card.rank + " of " + card.suit + " (" + strFaceUp + ")");

                }
            }
            else { Console.WriteLine("(empty hand)"); }

        }


    }
    #endregion
}
