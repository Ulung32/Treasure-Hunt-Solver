using System;
using System.IO;
using System.Collections.Generic;
using util;

namespace Dfs {
    public class Dfs {
        // Prioritas arah : Atas Bawah Kiri Kanan
        public int[,] visited;
        public int visitCount {get; set;}
        public int stepCount {get; set;}
        public long execTime {get;set;}
        private int maxVisit; // jumlah maksimum visit suatu node. Untuk backtracking
        public List<Tuple<int,int>> pathResult = new List<Tuple<int, int>>(); // Jalur hasil
        public List<Tuple<int,int>> searchPath = new List<Tuple<int, int>>(); // Jalur pencarian lengkap
        public Dfs(ref Matrix m) {
            Stack<Tuple<int,int>> path = new Stack<Tuple<int,int>>(); // Stack penampung path sementara
            Stack<Tuple<int,int>> searchPathStack = new Stack<Tuple<int,int>>(); // Stack penampung jalur pencarian lengkap
            Tuple<int,int> temp;
            visitCount = 1;
            maxVisit = 1;
            stepCount = 0;
            visited = new int[m.nRow, m.nCol];
            int i = m.startRow, j = m.startCol;
            int treasureCount = 0;
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            path.Push(new Tuple<int,int>(i,j));
            searchPathStack.Push(new Tuple<int,int>(i,j));
            visited[i,j]++;
            while (treasureCount < m.totalTreasure) {
                bool isStuck = false;
                bool backTrack = false;
                // Periksa apakah ada tetangga yang belum dikunjungi sesuai prioritas arah
                if (canVisit(i-1,j,m,1)) {
                    i -= 1;
                } else if (canVisit(i+1,j,m,1)) {
                    i += 1;
                } else if (canVisit(i,j-1,m,1)) {
                    j -= 1;
                } else if (canVisit(i,j+1,m,1)) {
                    j += 1;
                } else {
                    backTrack = true;
                    // Periksa apakah ada tetangga yang jumlah dikunjunginya lebih kecil dari maxVisit
                    if (canVisit(i-1,j,m,maxVisit)) {
                        i -= 1;
                    } else if (canVisit(i+1,j,m,maxVisit)) {
                        i += 1;
                    } else if (canVisit(i,j-1,m,maxVisit)) {
                        j -= 1;
                    } else if (canVisit(i,j+1,m,maxVisit)) {
                        j += 1;
                    } else {
                        isStuck = true;
                        temp = path.Pop();
                        Tuple<int,int> prev = path.Peek();
                        if (m.container[i,j] == 'T') {
                            path.Push(temp);
                            path.Push(prev);
                            maxVisit++;
                        }
                        i = prev.Item1;
                        j = prev.Item2;
                    }
                }
                if (!isStuck) {
                    if (!backTrack) {
                        if (m.container[i,j] == 'T') {
                            treasureCount++;
                        }
                        visitCount++;
                    }
                    path.Push(new Tuple<int,int>(i,j));
                }
                visited[i,j]++;
                searchPathStack.Push(new Tuple<int,int>(i,j));
            }
            while(path.TryPop(out temp)) {
                pathResult.Insert(0,temp);
                m.container[temp.Item1,temp.Item2] = 'H';
                stepCount++;
            }
            while(searchPathStack.TryPop(out temp)) {
                searchPath.Insert(0,temp);
            }
            stepCount--;
            stopwatch.Stop();
            execTime = stopwatch.ElapsedMilliseconds;
        }
        
        private bool canVisit(int i, int j, Matrix m, int maxVisitNum) {
            if (i < m.nRow && i >=0 && j >=0 && j <m.nCol && m.container[i,j] != 'X' && visited[i,j] < maxVisitNum) {
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
    }
}