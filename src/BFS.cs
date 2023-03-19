using System;
using System.Collections.Generic;

class BFSsolver{
    static void reverseList(List<Tuple<int, int>> list, int indeks1, int indeks2){
        if(indeks2 > indeks1){
            int range = indeks2-indeks1;
            for(int i = 0; i <= range/2; i++){
                Tuple<int, int> temp = list[indeks1 + i];
                list[indeks1+i] = list[indeks2 -i];
                list[indeks2-i] = temp;
            }
        }
    }
    static List<Tuple<int, int>> BFS(string[,] maze, Tuple<int,int> K, int TreasureCount){
        Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        queue.Enqueue(new Tuple<int,int> (K.Item1, K.Item2));
        Dictionary<Tuple<int, int>, Tuple<int, int>> parent = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
        parent[new Tuple<int, int>(K.Item1, K.Item2)] = null;
        
        List<Tuple<int, int>> path = new List<Tuple<int, int>> ();
        while(queue.Count > 0 && TreasureCount >= 0){
            
            Tuple<int, int> current = queue.Dequeue();
            // Console.WriteLine("{0}, {1}", current.Item1, current.Item2);
            if(TreasureCount == 0){
                return path;
            }
            if(current.Item1 > 0 && maze[current.Item1-1, current.Item2] != "X" && !parent.ContainsKey(new Tuple<int, int>(current.Item1-1, current.Item2))){
                queue.Enqueue(new Tuple<int, int>(current.Item1 - 1, current.Item2));
                parent[new Tuple<int, int>(current.Item1 - 1, current.Item2)] = current;
                if(maze[current.Item1-1, current.Item2] == "T"){
                    Console.WriteLine("Masuk1");
                    TreasureCount -= 1;
                    Tuple<int, int> temp = new Tuple<int, int>(current.Item1 - 1, current.Item2);
                    int indeks1 = path.Count;
                    while(temp != null){
                        if(!path.Contains(temp)){
                            path.Add(temp);
                        }
                        temp = parent[temp];
                    }
                    int indeks2 = path.Count - 1 ;
                    reverseList(path, indeks1, indeks2);
                }
            }
            if(current.Item1 < maze.GetLength(0)-1 && maze[current.Item1+1, current.Item2] != "X" && !parent.ContainsKey(new Tuple<int, int>(current.Item1+1, current.Item2))){
                queue.Enqueue(new Tuple<int, int>(current.Item1 + 1, current.Item2));
                parent[new Tuple<int, int>(current.Item1 + 1, current.Item2)] = current;
                if(maze[current.Item1+1, current.Item2] == "T"){
                    Console.WriteLine("Masuk2");
                    TreasureCount -= 1;
                    Tuple<int, int> temp = new Tuple<int, int>(current.Item1 + 1, current.Item2);
                    int indeks1 = path.Count;
                    while(temp != null){
                        if(!path.Contains(temp)){
                            path.Add(temp);
                        }
                        temp = parent[temp];
                    }
                    int indeks2 = path.Count - 1;
                    reverseList(path, indeks1, indeks2);
                }
            }
            if(current.Item2 > 0 && maze[current.Item1, current.Item2-1] != "X" && !parent.ContainsKey(new Tuple<int, int>(current.Item1, current.Item2-1))){
                queue.Enqueue(new Tuple<int, int>(current.Item1 , current.Item2-1));
                parent[new Tuple<int, int>(current.Item1, current.Item2-1)] = current;
                if(maze[current.Item1, current.Item2-1] == "T"){
                    Console.WriteLine("Masuk3");
                    TreasureCount -= 1;
                    Tuple<int, int> temp = new Tuple<int, int>(current.Item1, current.Item2-1);
                    int indeks1 = path.Count;
                    while(temp != null){
                        if(!path.Contains(temp)){
                            path.Add(temp);
                        }
                        temp = parent[temp];
                    }
                    int indeks2 = path.Count - 1;
                    reverseList(path, indeks1, indeks2);
                }
            }
            if(current.Item2 < maze.GetLength(1)-1 && maze[current.Item1, current.Item2+1] != "X" && !parent.ContainsKey(new Tuple<int, int>(current.Item1, current.Item2+1))){
                queue.Enqueue(new Tuple<int, int>(current.Item1, current.Item2+1));
                parent[new Tuple<int, int>(current.Item1, current.Item2+1)] = current;
                if(maze[current.Item1, current.Item2+1] == "T"){
                    Console.WriteLine("Masuk4");
                    TreasureCount -= 1;
                    Tuple<int, int> temp = new Tuple<int, int>(current.Item1, current.Item2+1);
                    int indeks1 = path.Count;
                    while(temp != null){
                        if(!path.Contains(temp)){
                            path.Add(temp);
                        }
                        temp = parent[temp];
                    }
                    int indeks2 = path.Count - 1;
                    reverseList(path, indeks1, indeks2);
                }
                // current = queue.Dequeue();
            }
            
            
        }
        
        List<Tuple<int, int>> kosong = new List<Tuple<int, int>> ();
        return kosong;
    }   
    static List<Tuple<int,int>> BFS2point (string[,] maze, Tuple<int,int> titik1, Tuple<int,int> titik2){
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
            if(current.Item1 > 0 && maze[current.Item1-1, current.Item2] != "X" && !parent.ContainsKey(new Tuple<int, int>(current.Item1-1, current.Item2))){
                queue.Enqueue(new Tuple<int, int>(current.Item1 - 1, current.Item2));
                parent[new Tuple<int, int>(current.Item1 - 1, current.Item2)] = current;
            }
            if(current.Item1 < maze.GetLength(0)-1 && maze[current.Item1+1, current.Item2] != "X" && !parent.ContainsKey(new Tuple<int, int>(current.Item1+1, current.Item2))){
                queue.Enqueue(new Tuple<int, int>(current.Item1 + 1, current.Item2));
                parent[new Tuple<int, int>(current.Item1 + 1, current.Item2)] = current;
            }
            if(current.Item2 > 0 && maze[current.Item1, current.Item2-1] != "X" && !parent.ContainsKey(new Tuple<int, int>(current.Item1, current.Item2-1))){
                queue.Enqueue(new Tuple<int, int>(current.Item1 , current.Item2-1));
                parent[new Tuple<int, int>(current.Item1, current.Item2-1)] = current;
            }
            if(current.Item2 < maze.GetLength(1)-1 && maze[current.Item1, current.Item2+1] != "X" && !parent.ContainsKey(new Tuple<int, int>(current.Item1, current.Item2+1))){
                queue.Enqueue(new Tuple<int, int>(current.Item1, current.Item2+1));
                parent[new Tuple<int, int>(current.Item1, current.Item2+1)] = current;
            }
        }
        List<Tuple<int, int>> kosong = new List<Tuple<int, int>> ();
        return kosong;

    }
    static List<Tuple<int,int>> attachSolution (string[,] maze,List<Tuple<int, int>> path){
        List<Tuple<int, int>> completePath = new List<Tuple<int, int>>();
        completePath.Add(path[0]);
        for(int i = 0; i<path.Count-1; i++){
            if(Math.Abs(path[i].Item1 - path[i+1].Item1) + Math.Abs(path[i].Item2 - path[i+1].Item2) > 1){
                // int indeks1 = i;
                // int indeks2 = i+1;
                List<Tuple<int,int>> pathbetween = BFS2point(maze, path[i], path[i+1]);
                for(int j = 0; j < pathbetween.Count; j++){
                    completePath.Add(pathbetween[j]);
                }
                // completePath.AddRange(pathbetween);
            }
            else{
                completePath.Add(path[i+1]);
            }
        }
        for(int i =0; i< completePath.Count -1 ; i++){
            if(completePath[i].Item1 == completePath[i+1].Item1 && completePath[i].Item2 == completePath[i+1].Item2){
                completePath.RemoveAt(i);
            }
        }
        return completePath;
    }
    static void Main(string[] args)
    {
        // Define the maze
        string[,] maze = new string [,]{
            {"K", "R", "R", "R"},
            {"X", "R", "X", "T"},
            {"X", "T", "R", "R"},
            {"X", "R", "X", "X"}
        };
        // string[,] maze = new string[,] {
        //     { "K", "R", "X", "R", "X" },
        //     { "R", "R", "X", "R", "R" },
        //     { "R", "T", "R", "T", "R" },
        //     { "X", "X", "R", "R", "R" },
        //     { "X", "X", "R", "X", "T" }
        // };
        // string [,] maze = new string [,]{
        //     {"R", "R", "R", "R", "R", "R", "R"},
        //     {"X", "T", "X", "T", "X", "T", "X"},
        //     {"X", "R", "X", "R", "R", "R", "X"},
        //     {"X", "R", "X", "X", "X", "X", "X"}
        // };
        Tuple<int,int> K = new Tuple<int, int>(0,0);
        // Solve the maze
        List<Tuple<int, int>> path = BFS(maze, K, 2);
        List<Tuple<int, int>> completePath = attachSolution(maze, path);
        // List<Tuple<int, int>> completePath = BFS2point(maze, new Tuple<int, int>(1,1), new Tuple<int, int>(0,2));

        // Print the path
        if (completePath.Count == 0)
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
            Console.WriteLine("Complete Path:");
            Console.WriteLine(completePath.Count);
            foreach (Tuple<int, int> point in completePath)
            {
                Console.WriteLine("{0}, {1}", point.Item1, point.Item2);
            }
        }
    }
}