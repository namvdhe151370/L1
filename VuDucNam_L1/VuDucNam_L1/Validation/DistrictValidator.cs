using FluentValidation;
using VuDucNam_L1.Constants;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Validation
{
    public class DistrictValidator : AbstractValidator<DistrictModel>
    {
        public DistrictValidator()
        {
            RuleFor(e => e.DistrictName)
                .NotEmpty().WithMessage(Validates.DistrictNameEmpty)
                .MaximumLength(Validates.DistrictNameMaxLength).WithMessage(Validates.DistrictNameLength);
        }
    }
}
