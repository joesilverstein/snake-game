using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
/**
 * SnakeClass.cs
 * Contains the classes used in the snakegame, GamePoint and Snake.
 * Brian Belleville
 * 5/22/11
 * */
namespace WindowsFormsApplication1
{
    //class game point.  Converts location in game grid to pixel location
    public class GamePoint
    {
        public GamePoint()
        {
            x = 0;
            y = 0;
        }
        public GamePoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        private const int scalefactor=10;//factor for changing between game grid locations and pixel locations
        public int x;
        public int y;
        public Point toPoint()
        {
            return new Point(x*10,y*10);
        }
        public Rectangle toRectangle()
        {
            return new Rectangle(toPoint().X, toPoint().Y, 10, 10);
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            else
                return (this.x == ((GamePoint)obj).x && this.y == ((GamePoint)obj).y);
        }
    }
    /*
     * class snake
     * The snake that exists in the snake game
     * Contains a linked list of GamePoints, representing the body of the snake.
     * Contains methods to move the snake, check if it has hit itself, and draw
     * */
    public class Snake
    {
        //default snake has eight segments and appears centered in the game screen
        public Snake()
        {
            bodyLocation = new LinkedList<GamePoint>();
            for (int i = 0; i < 8; i++)
            {
                bodyLocation.AddLast(new GamePoint(27 + i, 20));
            }
            DirectionOfTravel = 'r';
        }
        private LinkedList<GamePoint> bodyLocation;//the location of each part of the snake
        public char DirectionOfTravel;//which way the snake is traveling
        //moves the snake in the direction of travel.  Creates new body point to be the head, and removes the tail
        public void move()
        {
            switch (DirectionOfTravel)
            {
                case 'r':
                    bodyLocation.AddLast(new GamePoint(bodyLocation.Last.Value.x + 1, bodyLocation.Last.Value.y));
                    break;
                case 'l':
                    bodyLocation.AddLast(new GamePoint(bodyLocation.Last.Value.x - 1, bodyLocation.Last.Value.y));
                    break;
                case 'u':
                    bodyLocation.AddLast(new GamePoint(bodyLocation.Last.Value.x, bodyLocation.Last.Value.y - 1));
                    break;
                case 'd':
                    bodyLocation.AddLast(new GamePoint(bodyLocation.Last.Value.x, bodyLocation.Last.Value.y + 1));
                    break;
            }
            bodyLocation.RemoveFirst();
        }

        //draws the snake
        public void draw(Graphics g)
        {
            foreach (GamePoint p in bodyLocation)
            {
                SolidBrush myBrush = new SolidBrush(Color.Green);
                g.FillRectangle(myBrush,p.toRectangle());
            }

        }
        //returns the location of the head of the snake
        public GamePoint head()
        {
            return bodyLocation.Last.Value;
        }

        //adds another segment to the end of the snake
        public void add()
        {
            bodyLocation.AddFirst(bodyLocation.First.Value);
        }
        //checks if the head has hit the rest of the body
        public bool headhit()
        {
            LinkedListNode<GamePoint> n = bodyLocation.First;
            while (!n.Equals(bodyLocation.Last))
            {
                if (n.Value.Equals(bodyLocation.Last.Value))
                    return true;
                n = n.Next;
            }
            return false;
        }
    }
}
