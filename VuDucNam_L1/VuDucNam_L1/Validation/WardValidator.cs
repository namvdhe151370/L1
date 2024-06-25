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
                .NotEmpty().WithMessage(NotificationMessage.WardNameEmpty)
                .MaximumLength(Validates.WardNameLength).WithMessage(NotificationMessage.WardNameLength);
            RuleFor(e => e.DistrictId)
            .NotEmpty().WithMessage(NotificationMessage.DistrictEmpty);
        }
    }
}
