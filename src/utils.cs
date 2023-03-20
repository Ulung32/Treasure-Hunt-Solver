using System;
using System.Collections.Generic;

namespace utils {
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
}