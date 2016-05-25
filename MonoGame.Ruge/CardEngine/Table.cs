/* 
© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)

 */
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Ruge.DragonDrop;

namespace MonoGame.Ruge.CardEngine {

    public class Table {

        // Z-Index constants
        protected const int ON_TOP = 1000;
        
        protected int _stackOffsetHorizontal, _stackOffsetVertical;
        protected Texture2D _cardBack, _slot;
        protected SpriteBatch _spriteBatch;
        protected DragonDrop<IDragonDropItem> _dragonDrop;
        
        public List<Slot> slots = new List<Slot>();
        public List<Stack> stacks = new List<Stack>();

        public Table(DragonDrop<IDragonDropItem> dd, Texture2D cardback, Texture2D slot, int stackOffsetH, int stackOffsetV) {
            _spriteBatch = dd.spriteBatch;
            _dragonDrop = dd;
            _stackOffsetHorizontal = stackOffsetH;
            _stackOffsetVertical = stackOffsetV;
            _cardBack = cardback;
            _slot = slot;
        }


        public void Clear() { stacks.Clear(); }

        public void AddSlot(Slot slot) {
            slots.Add(slot);
            _dragonDrop.Add(slot);
        }
        public void AddStack(Stack stack) {
            stacks.Add(stack);
            foreach (Card card in stack.cards) _dragonDrop.Add(card);
        }


        /// <summary>
        /// override this
        /// </summary>
        /// <param name="resetTable"></param>
        public void SetTable(bool resetTable = false) { }

        public void ResetTable() { SetTable(true); }


    }

}
