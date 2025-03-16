using MediatR;
using VesselManagement.Data;
using VesselManagement.Data.Entities;
using VesselManagement.Infrastructure.Vessels.Mappers;
using VesselManagement.Infrastructure.Vessels.Models;

namespace VesselManagement.Infrastructure.Vessels.Commands;

public static class CreateVessel
{
    public class Command(CreateVesselModel model) : IRequest<VesselModel>
    {
        public CreateVesselModel VesselModel { get; init; } = model;
    }
    
    public class Handler(VesselDbContext dbContext) : IRequestHandler<Command, VesselModel>
    {
        public async Task<VesselModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var createVesselModel = request.VesselModel;
            var vesselEntity = createVesselModel.Map();

            await dbContext.Vessels.AddAsync(vesselEntity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            var vesselModel = vesselEntity.Map();

            return vesselModel;
        }
    }
}