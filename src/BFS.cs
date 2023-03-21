using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using util;
class BFSsolver{
    static List<Tuple<int, int>> BFS(char[,] maze, Tuple<int,int> K, int TreasureCount){
        List<Tuple<int, int>> path = new List<Tuple<int, int>> ();
        Tuple<int,int> start = K;
        List<Tuple<int,int>> searchPath = new List<Tuple<int, int>>();
        while(TreasureCount > 0){
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
            searchPath.Add(start);
            queue.Enqueue(new Tuple<int,int> (start.Item1, start.Item2));
            Dictionary<Tuple<int, int>, Tuple<int, int>> parent = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
            parent[new Tuple<int, int>(start.Item1, start.Item2)] = null;
            
            // Tuple<int, int> current = K;
            bool found = false;
            Tuple<int, int> current;
            while(queue.Count > 0 && !found){
                // Console.WriteLine("before dequeue");
                // Console.WriteLine("{0}, {1}", current.Item1, current.Item2);
                current = queue.Dequeue();
                Console.WriteLine("dequeue");
                Console.WriteLine("{0}, {1}", current.Item1, current.Item2);
                if(maze[current.Item1, current.Item2] == 'T' && start!= current){
                    // Console.WriteLine("Masuk");
                    // Console.WriteLine("{0}, {1}", start.Item1, start.Item2);
                    List<Tuple<int, int>> temp = BFS2point(maze, start, current);
                    for(int i = 0; i < temp.Count; i++){
                        path.Add(temp[i]);
                        // Console.WriteLine("{0}, {1}", temp[i].Item1, temp[i].Item2);
                    }
                    start= current;
                    // Console.WriteLine("{0}, {1}", current.Item1, current.Item2);
                    // Console.WriteLine("{0}, {1}", start.Item1, start.Item2);
                    TreasureCount --;
                    // found = true;
                    // break;
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
        for(int i =0; i< path.Count -1 ; i++){
            if(path[i].Item1 == path[i+1].Item1 && path[i].Item2 == path[i+1].Item2){
                path.RemoveAt(i);
            }
        }
        

        // for(int i = 0; i< path.Count; i++){
        //     Console.WriteLine("{0}, {1}", path[i].Item1, path[i].Item2);
        // }
        return path;
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
    static void Main(string[] args)
    {
        Matrix m = new Matrix("../test/sampel-5.txt");
        m.printMatrix();
        char[,] maze  = m.container;
        int TreasureCount = m.totalTreasure;
        Tuple<int,int> K = new Tuple<int, int> (m.startRow, m.startCol);
        // // Define the maze
        // string[,] maze = new string [,]{
        //     {"K", "R", "R", "R"},
        //     {"X", "R", "X", "T"},
        //     {"X", "T", "R", "R"},
        //     {"X", "R", "X", "X"}
        // };
        // // string[,] maze = new string[,] {
        // //     { "K", "R", "X", "R", "X" },
        // //     { "R", "R", "X", "R", "R" },
        // //     { "R", "T", "R", "T", "R" },
        // //     { "X", "X", "R", "R", "R" },
        // //     { "X", "X", "R", "X", "T" }
        // // };
        // // string [,] maze = new string [,]{
        // //     {"R", "R", "R", "R", "R", "R", "R"},
        // //     {"X", "T", "X", "T", "X", "T", "X"},
        // //     {"X", "R", "X", "R", "R", "R", "X"},
        // //     {"X", "R", "X", "X", "X", "X", "X"}
        // // };
        // Tuple<int,int> K = new Tuple<int, int>(0,0);
        // // Solve the maze
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        List<Tuple<int, int>> path = BFS(maze, K, TreasureCount);
        stopwatch.Stop();
        TimeSpan timeSpan = stopwatch.Elapsed;
        // Print the path
        if (path.Count == 0)
        {
            Console.WriteLine("No path found");
        }
        else
        {
            Console.WriteLine("Path:");
            Console.WriteLine(path.Count);
            foreach (Tuple<int, int> point in path)
            {
                Console.WriteLine("{0}, {1}", point.Item1, point.Item2);
            }
             Console.WriteLine("Execution time: {0} milliseconds", timeSpan.TotalMilliseconds);
            
            string route = utils.convertRoute(path);
            Console.WriteLine(route);
        }
    }
}