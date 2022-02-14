using System.Collections.Generic;

namespace Quoridor
{
    class Player
    {
        public int x;
        public int y;
        public int winLine;

        public List<Coords> movies = null;

        public Player(int x, int y, int line)
        {
            this.x = x;
            this.y = y;
            winLine = line;
        }

        public Coords getCoords()
        {
            return new Coords(x, y);
        }
    }
}
