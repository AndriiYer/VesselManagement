using VesselManagement.Application.Vessels.Models;
using VesselManagement.Data.Entities;

namespace VesselManagement.Application.Vessels.Mappers

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

        public static Vessel Map(this RegisterVesselModel vessel)
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
