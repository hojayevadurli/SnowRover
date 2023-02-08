
using System.Net.Http;

namespace SnowRover;

public partial class MainPage : ContentPage
{
    HttpClient httpClient;

    public MainPage()
	{
		InitializeComponent();
	}
    private async void JoinGame_Clicked(object sender, EventArgs e)
    {
        httpClient = new HttpClient { BaseAddress = new Uri("https://snow-rover.azurewebsites.net") };

        var gameID = GameIDEntry.Text;
        var playerName = PlayerNameEntry.Text;
        // Your logic to join the game using gameID and playerName
        var response = await httpClient.GetAsync($"/game/join?gameId={gameID}&name={playerName}");
    }
}

