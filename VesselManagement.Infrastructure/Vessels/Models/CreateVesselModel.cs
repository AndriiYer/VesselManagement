using System.Text.Json.Serialization;

namespace VesselManagement.Infrastructure.Vessels.Models;

public class CreateVesselModel
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("IMO")]
    public string Imo { get; set; }

    [JsonPropertyName("Type")]
    public string Type { get; set; }

    [JsonPropertyName("Capacity")]
    public decimal Capacity { get; set; }
}