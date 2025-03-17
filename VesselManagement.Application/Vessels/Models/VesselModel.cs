using System.Text.Json.Serialization;

namespace VesselManagement.Application.Vessels.Models
{
    public class VesselModel : RegisterVesselModel
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }
    }
}
