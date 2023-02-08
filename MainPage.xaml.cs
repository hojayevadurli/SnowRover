
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace SnowRover;

public partial class MainPage : ContentPage
{
    HttpClient httpClient;
    JoinResponse gameStatus;
    StatusResult statusResult;
    int ingenuityRow;
    int ingenuityCol;
    public MainPage()
	{
		InitializeComponent();

    }
    private async void JoinGame_Clicked(object sender, EventArgs e)
    {
        httpClient = new HttpClient { BaseAddress = new Uri("https://snow-rover.azurewebsites.net") };

        var gameID = GameIDEntry.Text;
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

        }


    }

    private async void Forward_Clicked(object sender, EventArgs e)
    {
        var direction = "forward";
        var response = await httpClient.PostAsync($"/game/move?token={gameStatus.Token}&direction={direction}", null);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            statusResult = JsonConvert.DeserializeObject<StatusResult>(content);
            GameStatusLabel.Text = $"Game status: {statusResult.status}";
        }
        else
        {
            // Handle the error response
            await DisplayAlert("Error", "Move failed, please try again.", "OK");
        }
    }

    private async void Left_Clicked(object sender, EventArgs e)
    {
        var direction = "left";
        var response = await httpClient.PostAsync($"/game/move?token={gameStatus.Token}&direction={direction}", null);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            statusResult = JsonConvert.DeserializeObject<StatusResult>(content);
            GameStatusLabel.Text = $"Game status: {statusResult.status}";
        }
        else
        {
            // Handle the error response
            await DisplayAlert("Error", "Move failed, please try again.", "OK");
        }
    }
    private async void Right_Clicked(object sender, EventArgs e)
    {
        var direction = "right";
        var response = await httpClient.PostAsync($"/game/move?token={gameStatus.Token}&direction={direction}", null);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            statusResult = JsonConvert.DeserializeObject<StatusResult>(content);
            GameStatusLabel.Text = $"Game status: {statusResult.status}";
        }
        else
        {
            // Handle the error response
            await DisplayAlert("Error", "Move failed, please try again.", "OK");
        }
    }
    private async void Back_Clicked(object sender, EventArgs e)
    {
        var direction = "back";
        var response = await httpClient.PostAsync($"/game/move?token={gameStatus.Token}&direction={direction}", null);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            statusResult = JsonConvert.DeserializeObject<StatusResult>(content);
            GameStatusLabel.Text = $"Game status: {statusResult.status}";
        }
        else
        {
            // Handle the error response
            await DisplayAlert("Error", "Move failed, please try again.", "OK");
        }
    }


}


public class StatusResult
{
    public string status { get; set; }
}
