using System.Text;

namespace MauiApp5;

public partial class MainPage : ContentPage
{
    int count = 0;

    private readonly LocalDatabase db;

    private readonly Random rand;

    public MainPage()
    {
        InitializeComponent();
        db = new();
        rand = new();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private async void AddDatabaseBtn_Clicked(object sender, EventArgs e)
    {
        var item = new TeamMatch()
        {
            TeamNumber = 133,
            MatchNumber = 1, // rand.Next(1, 999),
            ScoutName = "Scott",
            AirtableId = $"row{rand.NextInt64()}",
        };
        try
        {
            var rows = await db.SaveItemAsync(item);
            HelloLabel.Text = $"{rows} added, new item Id = {item.Id}";
        }
        catch (Exception ex)
        {
            HelloLabel.Text = $"Error adding record:\r\n{ex.Message}";
        }
    }

    private async void GetTeamMatchBtn_Clicked(object sender, EventArgs e)
    {
        var item = await db.GetTeamMatchAsync(133, 1);
        if (item == null)
            HelloLabel.Text = "Not found!";
        else
            HelloLabel.Text = item.Uuid;
    }

    private async void GetTeamAllMatchesButton_Clicked(object sender, EventArgs e)
    {
        var matches = await db.GetTeamAllMatches(133);
        if (matches == null)
            HelloLabel.Text = "No matches found";
        else if (matches.Count == 1)
            HelloLabel.Text = $"There is {matches.Count} match found.";
        else
            HelloLabel.Text = $"There are {matches.Count} matches found.";
        StringBuilder matchNumberList = new();
        foreach (TeamMatch item in matches)
        {
            if (matchNumberList.Length > 0)
                matchNumberList.Append(", ");
            matchNumberList.Append(item.MatchNumber);
        }
        WelcomeLabel.Text = matchNumberList.ToString();
    }

    private async void UpdateTeamMatchButton_Clicked(object sender, EventArgs e)
    {
        var item = await db.GetTeamMatchAsync(133, 1);
        if (item == null)
            HelloLabel.Text = "Not found!";
        else
        {
            item.ScoutName = $"Fred-{rand.Next()}";
            var rows = await db.SaveItemAsync(item);
            HelloLabel.Text = $"Updated {rows} rows!";
        }
    }
}