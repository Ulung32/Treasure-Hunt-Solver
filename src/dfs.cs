using System;
using System.IO;
using System.Collections.Generic;
using util;

namespace Dfs {
    public class Dfs {
        // Prioritas arah : Kanan Bawah Kiri Atas
        public int[,] visited;
        public int visitCount {get; set;}
        public int stepCount {get; set;}
        public bool back {get;set;} // simpen backtrack atau ga
        public long execTime {get;set;}
        public List<Tuple<int,int>> pathResult = new List<Tuple<int, int>>();
        public Dfs(ref Matrix m) {
            Stack<Tuple<int,int>> path = new Stack<Tuple<int,int>>();
            Stack<Tuple<int,int>> backTrackPath = new Stack<Tuple<int,int>>();
            Tuple<int,int> temp;
            visitCount = 1;
            stepCount = 0;
            back = false;
            visited = new int[m.nRow, m.nCol];
            int i = m.startRow, j = m.startCol;
            int treasureCount = 0;
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            path.Push(new Tuple<int,int>(i,j));
            visited[i,j]++;
            while (treasureCount < m.totalTreasure) {
                bool isStuck = false;
                if (canVisit(i,j+1,m)) {
                    j += 1;
                } else if (canVisit(i+1,j,m)) {
                    i += 1;
                } else if (canVisit(i,j-1,m)) {
                    j -= 1;
                } else if (canVisit(i-1,j,m)) {
                    i -= 1;
                } else {
                    isStuck = true;
                    temp = path.Pop();
                    Tuple<int,int> prev = path.Peek();
                    if (m.container[i,j] == 'T' || back) {
                        back = true;
                        backTrackPath.Push(temp);
                    }
                    i = prev.Item1;
                    j = prev.Item2;
                }
                if (!isStuck) {
                    if (back) {
                        Stack<Tuple<int,int>> revBackTrackPath = new Stack<Tuple<int, int>>(backTrackPath.Reverse());
                        while (backTrackPath.TryPop(out temp)) {
                            path.Push(temp);
                        }
                        while(revBackTrackPath.TryPop(out temp)) {
                            path.Push(temp);
                        }
                        back = false;
                    }
                    path.Push(new Tuple<int,int>(i,j));
                    if (m.container[i,j] == 'T') {
                        treasureCount++;
                    }
                    visitCount++;
                }
                visited[i,j]++;
            }
            while(path.TryPop(out temp)) {
                pathResult.Insert(0,temp);
                m.container[temp.Item1,temp.Item2] = 'H';
                stepCount++;
            }
            stopwatch.Stop();
            execTime = stopwatch.ElapsedMilliseconds;
        }
        
        public bool canVisit(int i, int j, Matrix m) {
            if (i < m.nRow && i >=0 && j >=0 && j <m.nCol && m.container[i,j] != 'X' && visited[i,j] == 0) {
                return true;
            } else {
                return false;
            }
        }
        public int[,] getVisited() {
            return visited;
        }
        public void printVisited() {
            for (int i=0; i<visited.GetLength(0); i++) {
                for (int j=0; j<visited.GetLength(1); j++) {
                    Console.Write(visited[i,j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        public Stack<T> createReverseStack<T>(Stack<T> s) {
            Stack<T> reverse = new Stack<T>();
            T temp;
            while(s.TryPop(out temp)) {
                reverse.Push(temp);
            }
            return reverse;
        }
    }

    class testClass {
        public static void Main() {
            Matrix m = new Matrix("../../test/test.txt");
            m.printMatrix();
            Console.WriteLine();
            Dfs d = new Dfs(ref m);
            m.printMatrix();
            Console.WriteLine(d.stepCount);
            Console.WriteLine(d.visitCount);
            Console.WriteLine(d.execTime);
            for (int i=0; i<d.pathResult.Count; i++) {
                Console.Write(d.pathResult[i].Item1);
                Console.Write(",");
                Console.Write(d.pathResult[i].Item2);
                if (i != d.pathResult.Count-1) {
                    Console.Write(" - ");
                }
            }
            Console.WriteLine();
            d.printVisited();
        }
    }
}