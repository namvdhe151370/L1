using FluentValidation;
using VuDucNam_L1.Constants;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Validation
{
    public class WardValidator : AbstractValidator<WardModel>
    {
        public WardValidator()
        {
            RuleFor(e => e.WardName)
                .NotEmpty().WithMessage(Validates.WardNameEmpty)
                .MaximumLength(Validates.WardNameLengthMax).WithMessage(Validates.WardNameLength);
            RuleFor(e => e.DistrictId)
            .NotEmpty().WithMessage(Validates.DistrictEmpty);
        }
    }
}
