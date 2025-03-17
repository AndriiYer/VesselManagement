using MediatR;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Application.Vessels.Mappers;
using VesselManagement.Application.Vessels.Models;
using VesselManagement.Data;

namespace VesselManagement.Application.Vessels.Commands;

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
            var vesselEntity = await dbContext.Vessels.FirstOrDefaultAsync(x => x.Id == request.VesselModel.Id, cancellationToken);
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