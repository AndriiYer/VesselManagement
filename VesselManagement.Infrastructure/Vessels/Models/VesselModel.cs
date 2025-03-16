using System.Text.Json.Serialization;

namespace VesselManagement.Infrastructure.Vessels.Models
{
    public class VesselModel : RegisterVesselModel
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }
    }
}
