using SQLite;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace MauiApp5;

public class TeamMatch
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed(Name = "TeamMatchUnique", Order = 1, Unique = true)]
    public int TeamNumber { get; set; }

    [Indexed(Name = "TeamMatchUnique", Order = 2, Unique = true)]
    public int MatchNumber { get; set; }

    [Indexed(Unique = true)]
    public string Uuid { get; set; }

    [Indexed(Unique = true)]
    public string AirtableId { get; set; }

    public string ScoutName { get; set; }

    public string Comments { get; set; }

    public override string ToString()
    {
        JsonSerializerOptions WriteOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        return JsonSerializer.Serialize(this, WriteOptions);
    }
}