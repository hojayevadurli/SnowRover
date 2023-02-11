//using Xamarin.Forms;


namespace SnowRover
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LowResolutionMap : ContentPage
    {
        private JoinResponse _joinResponse;
        private LowResolutionCell[] map;
        private Grid MapGrid;

        public LowResolutionMap(JoinResponse joinResponse)
        {
            _joinResponse = joinResponse;
             map = joinResponse.LowResolutionMap;       
            InitializeInMemoryMap(map);

        }
        public void InitializeInMemoryMap(LowResolutionCell[] lowResolutionMap)
        {
            int numberOfRows = lowResolutionMap.Max(cell => cell.UpperRightRow) + 1;
            int numberOfColumns = lowResolutionMap.Max(cell => cell.UpperRightColumn) + 1;

            var inMemoryMap = new int[numberOfRows, numberOfColumns];

            foreach (var lowResolutionCell in lowResolutionMap)
            {
                int lowerLeftRow = lowResolutionCell.LowerLeftRow;
                int lowerLeftColumn = lowResolutionCell.LowerLeftColumn;
                int upperRightRow = lowResolutionCell.UpperRightRow;
                int upperRightColumn = lowResolutionCell.UpperRightColumn;
                int averageDifficulty = lowResolutionCell.AverageDifficulty;

                for (int row = lowerLeftRow; row <= upperRightRow; row++)
                {
                    for (int column = lowerLeftColumn; column <= upperRightColumn; column++)
                    {
                        inMemoryMap[row, column] = averageDifficulty;
                    }
                }
            }

            //for (int row = 0; row < numberOfRows; row++)
            //{
            //    MapGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //    for (int column = 0; column < numberOfColumns; column++)
            //    {
            //        if (column == 0)
            //        {
            //            MapGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //        }

            //        // Add a label to represent the difficulty at this cell
            //        var difficultyLabel = new Label
            //        {
            //            Text = inMemoryMap[row, column].ToString(),
            //            HorizontalOptions = LayoutOptions.FillAndExpand,
            //            VerticalOptions = LayoutOptions.FillAndExpand,
            //            BackgroundColor = GetDifficultyColor(inMemoryMap[row, column]),
            //            TextColor = Color.White
            //        };
            //        MapGrid.Children.Add(difficultyLabel, column, row);
            //    }
            //}

        }

        //private Xamarin.Forms.Color GetDifficultyColor(int difficulty)
        //{
        //    switch (difficulty)
        //    {
        //        case 1:
        //            return Xamarin.Forms.Color.Green;
        //        case 2:
        //            return Xamarin.Forms.Color.Yellow;
        //        case 3:
        //            return Xamarin.Forms.Color.Orange;
        //        case 4:
        //            return Xamarin.Forms.Color.Red;
        //        default:
        //            return Xamarin.Forms.Color.White;
        //    }
        //}




    }
}
