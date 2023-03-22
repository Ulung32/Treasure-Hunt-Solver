using System;
using System.IO;
using System.Collections.Generic;

namespace Dfs
{
    using Matrix;
    public class Dfs
    {
        // Prioritas arah : Atas Bawah Kiri Kanan
        public int[,] visited;
        public int visitCount { get; set; } // Untuk nodes
        public int stepCount { get; set; } // Untuk steps
        public double execTime { get; set; } // Untuk execution time
        private int maxVisit; // jumlah maksimum visit suatu node. Untuk backtracking
        public List<Tuple<int, int>> pathResult = new List<Tuple<int, int>>(); // Jalur hasil, untuk Path, pake dengan utils.convertRoute
        public List<Tuple<int, int>> searchPath = new List<Tuple<int, int>>(); // Jalur pencarian lengkap, untuk bonus visualisasi pencarian

        public Dfs(Matrix m, bool tsp)
        {
            Stack<Tuple<int, int>> path = new Stack<Tuple<int, int>>(); // Stack penampung path sementara
            Stack<Tuple<int, int>> searchPathStack = new Stack<Tuple<int, int>>(); // Stack penampung jalur pencarian lengkap
            Tuple<int, int> temp;
            visitCount = 1;
            maxVisit = 1;
            stepCount = 0;
            visited = new int[m.nRow, m.nCol];
            int i = m.startRow, j = m.startCol;
            int treasureCount = 0;
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            path.Push(new Tuple<int, int>(i, j));
            searchPathStack.Push(new Tuple<int, int>(i, j));
            visited[i, j]++;
            while (treasureCount < m.totalTreasure)
            {
                bool isStuck = false; // Tidak ada node yang bisa dikunjungi
                bool backTrack = false; // Menyimpan jalur backtrack
                // Periksa apakah ada tetangga yang belum dikunjungi sesuai prioritas arah
                if (canVisit(i - 1, j, m, 1))
                {
                    i -= 1;
                }
                else if (canVisit(i + 1, j, m, 1))
                {
                    i += 1;
                }
                else if (canVisit(i, j - 1, m, 1))
                {
                    j -= 1;
                }
                else if (canVisit(i, j + 1, m, 1))
                {
                    j += 1;
                }
                else
                {
                    backTrack = true;
                    // Periksa apakah ada tetangga yang jumlah dikunjunginya lebih kecil dari maxVisit
                    if (canVisit(i - 1, j, m, maxVisit))
                    {
                        i -= 1;
                    }
                    else if (canVisit(i + 1, j, m, maxVisit))
                    {
                        i += 1;
                    }
                    else if (canVisit(i, j - 1, m, maxVisit))
                    {
                        j -= 1;
                    }
                    else if (canVisit(i, j + 1, m, maxVisit))
                    {
                        j += 1;
                    }
                    else
                    {
                        isStuck = true;
                        temp = path.Pop();
                        Tuple<int, int> prev = path.Peek(); // Mundur ke node sebelumnya. Tidak disimpan ke jalur hasil
                        if (m.container[i, j] == 'T')
                        {
                            path.Push(temp);
                            path.Push(prev);
                            visited[prev.Item1, temp.Item2]++;
                            maxVisit++;
                        }
                        i = prev.Item1;
                        j = prev.Item2;
                    }
                }
                if (!isStuck)
                {
                    if (!backTrack) // Sudah tidak melakukan backtrack
                    {
                        maxVisit = 1;
                        if (m.container[i, j] == 'T')
                        {
                            treasureCount++;
                        }
                        visitCount++;
                    }
                    path.Push(new Tuple<int, int>(i, j));
                }
                visited[i, j]++;
                searchPathStack.Push(new Tuple<int, int>(i, j));
            }

            if (tsp)
            {
                // Inisialisasi ulang matriks jumlah visit untuk TSP. Node yang sudah dikunjungi diset 1 untuk
                // mempermudah perhitungan jumlah node yang dikunjungi
                maxVisit = 2;
                int[,] tempVisited = new int[m.nRow, m.nCol];
                // Simpan matriks visited ke tempVisited dan inisialisasi ulang matriks visited
                for (int r = 0; r < m.nRow; r++)
                {
                    for (int c = 0; c < m.nCol; c++)
                    {
                        tempVisited[r, c] = visited[r, c];
                        if (visited[r, c] > 0)
                        {
                            visited[r, c] = 1; // Penanda sudah pernah dikunjungi
                        }
                    }
                }
                visited[i, j] = 2;
                while (i != m.startRow || j != m.startCol)
                {
                    bool isStuck = false;
                    // Cari node yang belum dikunjungi
                    if (canVisit(i - 1, j, m, 1))
                    {
                        i -= 1;
                    }
                    else if (canVisit(i + 1, j, m, 1))
                    {
                        i += 1;
                    }
                    else if (canVisit(i, j - 1, m, 1))
                    {
                        j -= 1;
                    }
                    else if (canVisit(i, j + 1, m, 1))
                    {
                        j += 1;
                    }
                    else
                    {
                        // Jika semua tetangga sudah dikunjungi, kunjungi ulang node lain
                        if (canVisit(i - 1, j, m, maxVisit))
                        {
                            i -= 1;
                        }
                        else if (canVisit(i + 1, j, m, maxVisit))
                        {
                            i += 1;
                        }
                        else if (canVisit(i, j - 1, m, maxVisit))
                        {
                            j -= 1;
                        }
                        else if (canVisit(i, j + 1, m, maxVisit))
                        {
                            j += 1;
                        }
                        else
                        {
                            isStuck = true;
                            path.Pop();
                            Tuple<int, int> prev = path.Peek();
                            i = prev.Item1;
                            j = prev.Item2;
                        }
                    }
                    if (!isStuck)
                    {
                        if (visited[i, j] == 0)
                        {
                            visitCount++;
                        }
                        path.Push(new Tuple<int, int>(i, j));
                    }
                    visited[i, j]++;
                    searchPathStack.Push(new Tuple<int, int>(i, j));
                }

                // Tambahkan jumlah visit ke matriks visited
                for (int r = 0; r < m.nRow; r++)
                {
                    for (int c = 0; c < m.nCol; c++)
                    {
                        visited[r, c] += tempVisited[r, c];
                        if (tempVisited[r, c] != 0)
                        {
                            visited[r, c]--;
                        }
                    }
                }
            }
            while (path.Count > 0)
            {
                temp = path.Pop();
                pathResult.Insert(0, temp);
                stepCount++;
            }
            while (searchPathStack.Count > 0)
            {
                temp = searchPathStack.Pop();
                searchPath.Insert(0, temp);
            }
            stepCount--;
            stopwatch.Stop();
            execTime = stopwatch.Elapsed.TotalMilliseconds;
        }

        // Periksa apakah node bisa dikunjungi (bukan tembok, range valid, dan jumlah dikunjunginya < maxVisitNum)
        private bool canVisit(int i, int j, Matrix m, int maxVisitNum)
        {
            if (i < m.nRow && i >= 0 && j >= 0 && j < m.nCol && m.container[i, j] != 'X' && visited[i, j] < maxVisitNum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int[,] getVisited()
        {
            return visited;
        }
    }
}