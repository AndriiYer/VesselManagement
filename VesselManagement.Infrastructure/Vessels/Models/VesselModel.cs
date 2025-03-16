using System.Text.Json.Serialization;

namespace VesselManagement.Infrastructure.Vessels.Models
{
    public class VesselModel : CreateVesselModel
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }
    }
}
