namespace Quoridor
{
    class Coords
    {
        public int x, y;

        public Coords(int x, int y)
        {
            setCoords(x, y);
        }

        public void setCoords(int x, int y)
        {
            this.y = y;
            this.x = x;
        }
    }
}
