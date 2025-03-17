using MediatR;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Application.Vessels.Mappers;
using VesselManagement.Application.Vessels.Models;
using VesselManagement.Data;

namespace VesselManagement.Application.Vessels.Queries
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
