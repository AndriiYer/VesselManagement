using MediatR;
using VesselManagement.Data;
using VesselManagement.Infrastructure.Vessels.Mappers;
using VesselManagement.Infrastructure.Vessels.Models;

namespace VesselManagement.Infrastructure.Vessels.Commands;

public static class UpdateVessel
{
    public class Command(VesselModel model) : IRequest<VesselModel>
    {
        public VesselModel VesselModel { get; init; } = model;
    }
    
    public class Handler(VesselDbContext dbContext) : IRequestHandler<Command, VesselModel>
    {
        public async Task<VesselModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var vesselEntity = await dbContext.Vessels.FindAsync([request.VesselModel.Id], cancellationToken);
            if (vesselEntity == null)
            {
                throw new KeyNotFoundException($"Vessel with ID {request.VesselModel.Id} not found.");
            }

            vesselEntity.Name = request.VesselModel.Name;
            vesselEntity.Imo = request.VesselModel.Imo;
            vesselEntity.Type = request.VesselModel.Type;
            vesselEntity.Capacity = request.VesselModel.Capacity;

            await dbContext.SaveChangesAsync(cancellationToken);
            var vesselModel = vesselEntity.Map();

            return vesselModel;
        }
    }
}