using System;
using System.IO;

namespace Matrix {
    public class Matrix { // Isi matriks yg diperluin kurleb gini (yg DFS)
        public char[,] container;
        public int startRow {get;set;} // Perlu start
        public int startCol {get;set;}
        public int nRow {get;set;}
        public int nCol {get;set;}
        public int totalTreasure{get;set;}
        public Matrix(string[] fileContent, int row, int col) {
            nRow = row;
            nCol = col;
            container = new char[nRow,nCol];
            totalTreasure = 0;
            int currRow = 0;
            foreach(string s in fileContent) {
                int counter = 0;
                for (int i=0; i<s.Length; i++) {
                    if (s[i] != ' ') {
                        container[currRow,counter] = s[i];
                        if (s[i] == 'K') {
                            startRow = currRow;
                            startCol = counter;
                        } else if (s[i] == 'T') {
                            totalTreasure++;
                        }
                        counter++;
                    }
                }
                currRow++;
            }
        }
        public string printMatrix() {
            string m = "";
            for (int i=0; i<nRow; i++) {
                for (int j=0; j<nCol; j++) {
                    m += container[i, j].ToString() + " ";
                }
                m += "\n";
            }
            return m;
        }
    }
}