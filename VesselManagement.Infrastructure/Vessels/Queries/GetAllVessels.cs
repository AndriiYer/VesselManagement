using MediatR;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Data;
using VesselManagement.Infrastructure.Vessels.Mappers;
using VesselManagement.Infrastructure.Vessels.Models;

namespace VesselManagement.Infrastructure.Vessels.Queries
{
    public static class GetAllVessels
    {
        public class Query : IRequest<List<VesselModel>>
        {
        }

        public class Handler(VesselDbContext dbContext) : IRequestHandler<Query, List<VesselModel>>
        {
            public async Task<List<VesselModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var vessels = await dbContext.Vessels.AsNoTracking()
                    .ToListAsync(cancellationToken);

                return vessels.Select(x => x.Map()).ToList();
            }
        }
    }
}
