using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewValley;
using ChibiKyu.StardewMods.FishingAssistant2; // Vẫn giữ using này phòng trường hợp cần dùng sau này

namespace ChibiKyu.StardewMods.FishingAssistant2.Frameworks
{
    public class AStarSolver
    {
        private class Node
        {
            public Point Position { get; set; }
            public Node? Parent { get; set; }
            public int G { get; set; } 
            public int H { get; set; } 
            public int F => G + H;
            public Node(Point position) { Position = position; }
        }

        public static List<Point> FindPath(byte[,] grid, GameLocation location, Point start, Point target)
        {
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);

            List<Node> openList = new List<Node>();
            HashSet<Point> closedList = new HashSet<Point>();
            
            Node startNode = new Node(start);
            startNode.H = GetHeuristic(start, target);
            openList.Add(startNode);
            Node closestNode = startNode;
            Point[] directions = new Point[] 
            { 
                new Point(0, -1), new Point(0, 1), new Point(-1, 0), new Point(1, 0),
                new Point(-1, -1), new Point(1, -1), new Point(-1, 1), new Point(1, 1)
            };
            while (openList.Count > 0)
            {
                Node currentNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].F < currentNode.F || (openList[i].F == currentNode.F && openList[i].H < currentNode.H))
                        currentNode = openList[i];
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode.Position);
                
                if (currentNode.H < closestNode.H) closestNode = currentNode;

                if (currentNode.Position == target) return RetracePath(currentNode);

                foreach (Point dir in directions)
                {
                    Point neighborPos = new Point(currentNode.Position.X + dir.X, currentNode.Position.Y + dir.Y);
                    if (!IsWalkable(grid, location, neighborPos, target, width, height)) continue;

                    if (dir.X != 0 && dir.Y != 0)
                    {
                        bool canWalkX = IsWalkable(grid, location, new Point(currentNode.Position.X + dir.X, currentNode.Position.Y), target, width, height);
                        bool canWalkY = IsWalkable(grid, location, new Point(currentNode.Position.X, currentNode.Position.Y + dir.Y), target, width, height);
                        if (!canWalkX || !canWalkY) continue;
                    }

                    if (closedList.Contains(neighborPos)) continue;
                    int moveCost = (dir.X != 0 && dir.Y != 0) ? 14 : 10;
                    int newCostToNeighbor = currentNode.G + moveCost;
                    Node? neighborNode = openList.Find(n => n.Position == neighborPos);

                    if (neighborNode == null || newCostToNeighbor < neighborNode.G)
                    {
                        if (neighborNode == null)
                        {
                            neighborNode = new Node(neighborPos);
                            openList.Add(neighborNode);
                        }
                        neighborNode.G = newCostToNeighbor;
                        neighborNode.H = GetHeuristic(neighborPos, target);
                        neighborNode.Parent = currentNode;
                    }
                }
            }
            
            if (closestNode != startNode) return RetracePath(closestNode);
            return new List<Point>();
        }

        private static bool IsWalkable(byte[,] grid, GameLocation location, Point pos, Point target, int width, int height)
        {
            if (pos.X < 0 || pos.X >= width || pos.Y < 0 || pos.Y >= height)
            {
                if (pos != target) return false;
                return true; 
            }

            // Chỉ dựa vào dữ liệu mảng grid đã được xử lý (0 = đi được, 1 = vật cản)
            if (grid[pos.X, pos.Y] == 1 && pos != target) return false;

            return true;
        }

        private static int GetHeuristic(Point a, Point b) 
        { 
            int dx = Math.Abs(a.X - b.X);
            int dy = Math.Abs(a.Y - b.Y);
            return 10 * (dx + dy) - 6 * Math.Min(dx, dy);
        }

        private static List<Point> RetracePath(Node endNode)
        {
            List<Point> path = new List<Point>();
            Node currentNode = endNode;
            while (currentNode.Parent != null)
            {
                path.Add(currentNode.Position);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return path;
        }
    }
}