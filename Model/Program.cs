using System;
using System.Collections.Generic;

namespace Quoridor
{
    class Model
    {
        private char[,] mainField = new char[17,17];
        private Player player1, player2;
        private Coords []walls = new Coords[20];
        private int wallsSize = 0;

        public Player curPlayer;

        public Model()
        {
            player1 = new Player(8, 16, 0);
            player2 = new Player(8, 0, 16);
            curPlayer = player1;
        }

        private bool findWay(Player player)
        {
            Queue<Coords> q = new Queue<Coords>();
            char[,] checkField = (char[,])mainField.Clone();
            q.Enqueue(player.getCoords());
            checkField[player.y, player.x] = (char)1;
            while (q.Count != 0)
            {
                
                Coords cur = q.Dequeue();
                //Console.WriteLine(cur.x + " " + cur.y);
                if (cur.y == player.winLine)
                {
                    //printField(checkField);
                    return true;
                    
                }
                if (cur.y > 0 && checkField[cur.y - 1, cur.x] == 0
                              && checkField[cur.y - 2, cur.x] != 1)
                {
                    checkField[cur.y - 2, cur.x] = (char)1;
                    q.Enqueue(new Coords(cur.x, cur.y - 2));
                }
                if (cur.y < 16 && checkField[cur.y + 1, cur.x] == 0
                               && checkField[cur.y + 2, cur.x] != 1)
                {
                    checkField[cur.y + 2, cur.x] = (char)1;
                    q.Enqueue(new Coords(cur.x, cur.y + 2));
                }
                if (cur.x > 0 && checkField[cur.y, cur.x-1] == 0
                              && checkField[cur.y, cur.x-2] != 1)
                {
                    checkField[cur.y, cur.x-2] = (char)1;
                    q.Enqueue(new Coords(cur.x-2, cur.y));
                }
                if (cur.x < 16 && checkField[cur.y, cur.x + 1] == 0
                               && checkField[cur.y, cur.x + 2] != 1)
                {
                    checkField[cur.y, cur.x + 2] = (char)1;
                    q.Enqueue(new Coords(cur.x + 2, cur.y));
                }
            }
            //printField(checkField);
            return false;
        }

        private bool isNotCheckedCell(char[,] field)
        {
            for (int i = 0; i < 17; i+=2)
            {
                for (int j = 0; j < 17; j+=2)
                {
                    if (field[i, j] == 0) return true;
                }
            }
            return false;
        }

        public bool move(int x, int y)
        {
//            if (x % 2 == 1 || y % 2 == 1) return false;
//            if (mainField[(y + curPlayer.y) / 2, (x + curPlayer.x) / 2] == 0)
//            {
//                curPlayer.setCoords(x,y);
//                curPlayer = curPlayer == player1 ? player2 : player1;
//                return true;
//            }
            return false;
        }

        public void printField(char[,] field = null)
        {
            if (field == null)
            {
                field = mainField;
            }
            Console.WriteLine();
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if(i == player1.y && j == player1.x)
                        Console.Write("G ");
                    else if (i == player2.y && j == player2.x)
                        Console.Write("R ");
                    else if (i % 2 == 0 && j %2 == 0)
                        Console.Write(". ");
                    else
                        Console.Write(field[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private void fillWallCell(char ch, char dir, int x, int y)
        {
            for (int i = 0; i < 3; i++)
            {
                if (dir == 'v') mainField[y + i, x] = ch;
                else mainField[y, x + i] = ch;
            }
        }

        public bool placeWall(int x, int y)
        {
            if (x%2 == 1 && y%2 == 0 && y < 16 &&
                mainField[y,x] == 0 && mainField[y + 1, x] == 0 && mainField[y+2, x] == 0)
            {
                fillWallCell('#', 'v', x, y);
                if (!(findWay(player1) && findWay(player2)))
                {
                    Console.WriteLine("Close way");
                    fillWallCell('\0', 'v', x, y);
                    return false;
                }
                walls[wallsSize++] = new Coords(x,y);
            }
            if (x % 2 == 0 && y % 2 == 1 && x < 16 &&
                mainField[y, x] == 0 && mainField[y, x+1] == 0 && mainField[y, x+2] == 0)
            {
                fillWallCell('#', 'h', x, y);
                if (!(findWay(player1) && findWay(player2)))
                {
                    Console.WriteLine("Close way");
                    fillWallCell('\0', 'h', x, y);
                    return false;
                }
                walls[wallsSize++] = new Coords(x, y);
            }
            return false;
        }


        static void Main(string[] args)
        {
            Model model = new Model();
            model.move(8, 14);
            model.move(5, 4);
            model.placeWall(5, 4);
            model.placeWall(0, 1);
            model.placeWall(2, 1);
            model.placeWall(4, 1);
            model.placeWall(8, 1);
            model.placeWall(11, 0);
            model.printField();
            if (model.findWay(model.player1))
            {
                Console.WriteLine("Was Found!");
            }
            else
            {
                Console.WriteLine("No way!");
            }
        }
    }
}


/*
    . #
    . #
    0 


    */