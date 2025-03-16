using MediatR;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Data;
using VesselManagement.Infrastructure.Vessels.Mappers;
using VesselManagement.Infrastructure.Vessels.Models;

namespace VesselManagement.Infrastructure.Vessels.Queries;

public static class GetVesselById
{
    public class Query(Guid id) : IRequest<VesselModel>
    {
        public Guid Id { get; init; } = id;
    }

    public class Handler(VesselDbContext dbContext) : IRequestHandler<Query, VesselModel>
    {
        public async Task<VesselModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var vessel = await dbContext.Vessels.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return vessel?.Map() ?? throw new KeyNotFoundException($"Vessel with ID {request.Id} not found.");
        }
    }
}