using VesselManagement.Data.Entities;
using VesselManagement.Infrastructure.Vessels.Models;

namespace VesselManagement.Infrastructure.Vessels.Mappers

{
    public static class VesselMapper
    {
        public static VesselModel? Map(this Vessel vessel)
        {
            return new VesselModel
            {
                Id = vessel.Id,
                Name = vessel.Name,
                Imo = vessel.Imo,
                Type = vessel.Type,
                Capacity = vessel.Capacity
            };
        }

        public static Vessel Map(this CreateVesselModel vessel)
        {
            return new Vessel
            {
                Id = Guid.NewGuid(),
                Name = vessel.Name,
                Imo = vessel.Imo,
                Type = vessel.Type,
                Capacity = vessel.Capacity
            };
        }
    }
}
