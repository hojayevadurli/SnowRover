using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowRover
{
    public class RoverMap
    {

        private JoinResponse _joinResponse;
        private LowResolutionCell[] map;
        private Grid MapGrid;
        public int[,] inMemoryMap; 
        
        public RoverMap(JoinResponse joinResponse)
        {
            _joinResponse = joinResponse;
            map = joinResponse.LowResolutionMap;
            int numberOfRows = map.Max(cell => cell.UpperRightRow) + 1;
            int numberOfColumns = map.Max(cell => cell.UpperRightColumn) + 1;
            inMemoryMap = new int[numberOfRows, numberOfColumns];
            InitializeInMemoryMap(map);
            CreateHighResolutionMap();
        }
        public void InitializeInMemoryMap(LowResolutionCell[] lowResolutionMap)
        {
            foreach (var lowResolutionCell in lowResolutionMap)
            {
                int lowerLeftRow = lowResolutionCell.LowerLeftRow;
                int lowerLeftColumn = lowResolutionCell.LowerLeftColumn;
                int upperRightRow = lowResolutionCell.UpperRightRow;
                int upperRightColumn = lowResolutionCell.UpperRightColumn;
                int averageDifficulty = lowResolutionCell.AverageDifficulty; for (int row = lowerLeftRow; row <= upperRightRow; row++)
                {
                    for (int column = lowerLeftColumn; column <= upperRightColumn; column++)
                    {
                        inMemoryMap[row, column] = averageDifficulty;
                    }
                }
            }
          
        }
        public int[,] CreateHighResolutionMap()
        {
            int[,] highResolutionMap = new int[10, 10];
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    int averageDifficulty = 0;
                    int numberOfLowResolutionCells = 0;
                    for (int i = row * 250; i < (row + 1) * 250 && i < inMemoryMap.GetLength(0); i++)
                    {
                        for (int j = column * 250; j < (column + 1) * 250 && j < inMemoryMap.GetLength(1); j++)
                        {
                            averageDifficulty = inMemoryMap[i, j];
                            numberOfLowResolutionCells++;
                        }
                    }

                    highResolutionMap[row, column] = averageDifficulty;

                    Console.WriteLine("Row: " + row + " Column: " + column + " Average Difficulty: " + averageDifficulty);

                }
            }

            return highResolutionMap;
        }


    }
}
