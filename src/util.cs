namespace util{
    class util{
        public static String convertRoute(List<Tuple<int,int>> path){
            String Route = "";
            for(int i = 0; i < path.Count -1 ; i++){
                if(path[i].Item1 == path[i+1].Item1 && path[i].Item2 == path[i+1].Item2 - 1){
                    Route = Route + "R";
                }
                else if(path[i].Item1 == path[i+1].Item1 && path[i].Item2 == path[i+1].Item2 + 1){
                    Route = Route + "L";
                }
                else if(path[i].Item1 == path[i+1].Item1-1 && path[i].Item2 == path[i+1].Item2){
                    Route = Route + "D";
                }
                else if(path[i].Item1 == path[i+1].Item1+1 && path[i].Item2 == path[i+1].Item2){
                    Route = Route + "U";
                }
                if(i < path.Count - 2){
                    Route = Route + " - ";
                }
            }
            return Route;
        }
        public static int getTreasureCount (string[,] maze){
            int count = 0;
            for(int i = 0; i < maze.GetLength(0); i++){
                for(int j = 0; j < maze.GetLength(1); j ++){
                    if(maze[i,j] == "T"){
                        count ++;
                    }
                }
            }
            return count;
        }
    }
    public class Matrix { // Isi matriks yg diperluin kurleb gini (yg DFS)
        public char[,] container = new char[100,100];
        public int startRow {get;set;} // Perlu start
        public int startCol {get;set;}
        public int nRow {get;set;}
        public int nCol {get;set;}
        public int totalTreasure{get;set;}
        public Matrix(string fileName) {
            nRow=0;
            nCol=0;
            totalTreasure = 0;
            if (File.Exists(fileName)) {
                foreach(string s in File.ReadLines(fileName)) {
                    int counter = 0;
                    for (int i=0; i<s.Length; i++) {
                        if (s[i] != ' ') {
                            container[nRow,counter] = s[i];
                            if (s[i] == 'K') {
                                startRow = nRow;
                                startCol = counter;
                            } else if (s[i] == 'T') {
                                totalTreasure++;
                            }
                            counter++;
                        }
                    }
                    if (nRow == 0) {
                        nCol = counter;
                    }
                    nRow++;
                }
            } else {
                Console.WriteLine("File not found");
            }
        }
        public void printMatrix() {
            for (int i=0; i<nRow; i++) {
                for (int j=0; j<nCol; j++) {
                    Console.Write(container[i,j]);
                }
                Console.WriteLine();
            }
        }
    }
}