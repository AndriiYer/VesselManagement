using MediatR;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Application.Vessels.Mappers;
using VesselManagement.Application.Vessels.Models;
using VesselManagement.Data;

namespace VesselManagement.Application.Vessels.Queries;

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