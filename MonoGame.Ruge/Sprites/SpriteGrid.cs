/* 
© 2016 The Ruge Project (http://ruge.metasmug.com/) 

Licensed under MIT (see License.txt)
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Ruge.Sprites {

    class SpriteGrid {

        private int rows, cols, width, height, playerNum;
        //private Texture2D spriteSheet;

        public SpriteGrid(int rows, int cols, int width, int height) {

            this.rows = rows;
            this.cols = cols;
            this.width = width;
            this.height = height;

        }

        public Rectangle getRectangle(int col, int row) {

            int x = col * width;
            int y = row * height;
            
            Rectangle rect = new Rectangle(x, y, width, height);
            return rect;

        }

        public Rectangle getRectangle(Vector2 vector) {

            int x = (int)vector.X * width;
            int y = (int)vector.Y * height;
            
            Rectangle rect = new Rectangle(x, y, width, height);
            return rect;

        }

    }
}
