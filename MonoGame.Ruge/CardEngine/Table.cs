/* 
© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)

 */
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Ruge.CardEngine {

    class Table {

        //Z-Index constants
        const int BACKGROUND = -1;
        const int ON_TOP = 1000;

        private int _stackOffsetHorizontal, _stackOffsetVertical;
        private Texture2D _cardBack;
        private SpriteBatch _spriteBatch;

        public List<Slot> slots = new List<Slot>();
        public List<Stack> stacks = new List<Stack>();

        public Table(SpriteBatch sb, Texture2D cardback, int stackOffsetH, int stackOffetV) {
            _spriteBatch = sb;
            _stackOffsetHorizontal = stackOffsetH;
            _stackOffsetVertical = stackOffetV;
            _cardBack = cardback;
        }


        public void Clear() { stacks.Clear(); }

        public void AddSlot(Slot slot) { slots.Add(slot); }
        public void AddStack(Stack stack) { stacks.Add(stack); }


        /// <summary>
        /// override this
        /// </summary>
        public void SetTable() { }


    }

}
