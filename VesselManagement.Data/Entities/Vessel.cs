namespace VesselManagement.Data.Entities;

public class Vessel : EntityBase
{
    public string Name { get; set; }

    public string Imo { get; set; }

    public string Type { get; set; }

    public decimal Capacity { get; set; }
}