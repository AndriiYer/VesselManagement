using MediatR;
using VesselManagement.Application.Vessels.Mappers;
using VesselManagement.Application.Vessels.Models;
using VesselManagement.Data;

namespace VesselManagement.Application.Vessels.Commands;

public static class RegisterVessel
{
    public class Command(RegisterVesselModel model) : IRequest<VesselModel>
    {
        public RegisterVesselModel VesselModel { get; init; } = model;
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