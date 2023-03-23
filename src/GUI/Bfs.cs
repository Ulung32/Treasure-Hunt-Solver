using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace Bfs {
    using Utils;
    using Matrix;
    class BFSsolver {
        public static void BFS(char[,] maze, Tuple<int,int> K, int TreasureCount, bool tsp, ref List<Tuple<int, int>> path, ref List<Tuple<int, int>> searchPath)
        {
            Tuple<int,int> start = K;
            while(TreasureCount > 0){
                Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
                searchPath.Add(start);
                queue.Enqueue(new Tuple<int,int> (start.Item1, start.Item2));
                Dictionary<Tuple<int, int>, Tuple<int, int>> parent = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
                parent[new Tuple<int, int>(start.Item1, start.Item2)] = null;
                
                
                bool found = false;
                Tuple<int, int> current;
                while(queue.Count > 0 && !found){
                    current = queue.Dequeue();
                    if(maze[current.Item1, current.Item2] == 'T' && start!= current){
                        List<Tuple<int, int>> temp = BFS2point(maze, start, current);
                        for(int i = 0; i < temp.Count; i++){
                            path.Add(temp[i]);
                        }
                        start= current;
                        TreasureCount --;
                    }
                    if(current.Item1 > 0 && maze[current.Item1-1, current.Item2] != 'X' && !parent.ContainsKey(new Tuple<int, int>(current.Item1-1, current.Item2))){
                        queue.Enqueue(new Tuple<int, int>(current.Item1 - 1, current.Item2));
                        Tuple<int,int> temp = new Tuple<int, int>(current.Item1 - 1, current.Item2);
                        searchPath.Add(temp);
                        parent[new Tuple<int, int>(current.Item1 - 1, current.Item2)] = current;
                    }
                    if(current.Item1 < maze.GetLength(0)-1 && maze[current.Item1+1, current.Item2] != 'X' && !parent.ContainsKey(new Tuple<int, int>(current.Item1+1, current.Item2))){
                        queue.Enqueue(new Tuple<int, int>(current.Item1 + 1, current.Item2));
                        Tuple<int,int> temp = new Tuple<int, int>(current.Item1 + 1, current.Item2);
                        searchPath.Add(temp);
                        parent[new Tuple<int, int>(current.Item1 + 1, current.Item2)] = current;
                    }
                    if(current.Item2 > 0 && maze[current.Item1, current.Item2-1] != 'X' && !parent.ContainsKey(new Tuple<int, int>(current.Item1, current.Item2-1))){
                        queue.Enqueue(new Tuple<int, int>(current.Item1 , current.Item2-1));
                        Tuple<int,int> temp = new Tuple<int,int>(current.Item1 , current.Item2-1);
                        searchPath.Add(temp);
                        parent[new Tuple<int, int>(current.Item1, current.Item2-1)] = current;
                    }
                    if(current.Item2 < maze.GetLength(1)-1 && maze[current.Item1, current.Item2+1] != 'X' && !parent.ContainsKey(new Tuple<int, int>(current.Item1, current.Item2+1))){
                        queue.Enqueue(new Tuple<int, int>(current.Item1, current.Item2+1));
                        Tuple<int,int> temp = new Tuple<int,int> (current.Item1, current.Item2+1);
                        searchPath.Add(temp);
                        parent[new Tuple<int, int>(current.Item1, current.Item2+1)] = current;
                    }
                    if(maze[current.Item1, current.Item2] == 'T' && start!= current){
                        found = true;
                    } 
                }


                
            }
            if (tsp)
            {
                List<Tuple<int, int>> backHome = BFS2point(maze, start, K);
                for (int i = 0; i < backHome.Count; i++)
                {
                    path.Add(backHome[i]);
                }
            }
            for(int i =0; i< path.Count -1 ; i++){
                if(path[i].Item1 == path[i+1].Item1 && path[i].Item2 == path[i+1].Item2){
                    path.RemoveAt(i);
                }
            }
            

            
            Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>> hasil = new Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>> (path, searchPath);
        }
    
        static List<Tuple<int,int>> BFS2point (char[,] maze, Tuple<int,int> titik1, Tuple<int,int> titik2){
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(new Tuple<int,int> (titik1.Item1, titik1.Item2));
            Dictionary<Tuple<int, int>, Tuple<int, int>> parent = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
            parent[new Tuple<int, int>(titik1.Item1, titik1.Item2)] = null;
            while(queue.Count > 0){
                Tuple<int, int> current = queue.Dequeue();
                if(current.Item1 == titik2.Item1 && current.Item2 == titik2.Item2){
                    List<Tuple<int, int>> path = new List<Tuple<int, int>>();
                    while (current != null){
                        path.Add(current);
                        current = parent[current];
                    }
                    path.Reverse();
                    return path;
                }
                if(current.Item1 > 0 && maze[current.Item1-1, current.Item2] != 'X' && !parent.ContainsKey(new Tuple<int, int>(current.Item1-1, current.Item2))){
                    queue.Enqueue(new Tuple<int, int>(current.Item1 - 1, current.Item2));
                    parent[new Tuple<int, int>(current.Item1 - 1, current.Item2)] = current;
                }
                if(current.Item1 < maze.GetLength(0)-1 && maze[current.Item1+1, current.Item2] != 'X' && !parent.ContainsKey(new Tuple<int, int>(current.Item1+1, current.Item2))){
                    queue.Enqueue(new Tuple<int, int>(current.Item1 + 1, current.Item2));
                    parent[new Tuple<int, int>(current.Item1 + 1, current.Item2)] = current;
                }
                if(current.Item2 > 0 && maze[current.Item1, current.Item2-1] != 'X' && !parent.ContainsKey(new Tuple<int, int>(current.Item1, current.Item2-1))){
                    queue.Enqueue(new Tuple<int, int>(current.Item1 , current.Item2-1));
                    parent[new Tuple<int, int>(current.Item1, current.Item2-1)] = current;
                }
                if(current.Item2 < maze.GetLength(1)-1 && maze[current.Item1, current.Item2+1] != 'X' && !parent.ContainsKey(new Tuple<int, int>(current.Item1, current.Item2+1))){
                    queue.Enqueue(new Tuple<int, int>(current.Item1, current.Item2+1));
                    parent[new Tuple<int, int>(current.Item1, current.Item2+1)] = current;
                }
            }
            List<Tuple<int, int>> kosong = new List<Tuple<int, int>> ();
            return kosong;

        }

    }
}