using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Application.Vessels.Commands;
using VesselManagement.Data;

namespace VesselManagement.Application.Vessels.Validators
{
    public class UpdateVesselCommandValidator : AbstractValidator<UpdateVessel.Command>
    {
        private readonly VesselDbContext _vesselDbContext;
        
        public UpdateVesselCommandValidator(VesselDbContext vesselDbContext)
        {
            _vesselDbContext = vesselDbContext;

            RuleFor(v => v.VesselModel.Id).NotEmpty();
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
