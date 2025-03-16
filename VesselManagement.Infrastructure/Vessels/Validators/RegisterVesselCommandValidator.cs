using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Data;
using VesselManagement.Infrastructure.Vessels.Commands;

namespace VesselManagement.Infrastructure.Vessels.Validators
{
    public class RegisterVesselCommandValidator : AbstractValidator<RegisterVessel.Command>
    {
        private readonly VesselDbContext _vesselDbContext;
        
        public RegisterVesselCommandValidator(VesselDbContext vesselDbContext)
        {
            _vesselDbContext = vesselDbContext;
            
            RuleFor(v => v.VesselModel.Name).NotEmpty();
            RuleFor(v => v.VesselModel.Imo).MustAsync(BeUniqueImo).WithMessage("IMO number must be unique.");
            RuleFor(v => v.VesselModel.Type).NotEmpty();
            RuleFor(v => v.VesselModel.Capacity).GreaterThan(0);
        }

        private async Task<bool> BeUniqueImo(string imo, CancellationToken cancellationToken)
        {
            return await _vesselDbContext.Vessels.AllAsync(v => v.Imo != imo, cancellationToken);
        }
    }
}
