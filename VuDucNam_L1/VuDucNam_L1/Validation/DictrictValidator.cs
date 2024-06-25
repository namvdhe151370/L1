using FluentValidation;
using VuDucNam_L1.Constants;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Validation
{
    public class DictrictValidator : AbstractValidator<DistrictModel>
    {
        public DictrictValidator()
        {
            RuleFor(e => e.DistrictName)
                .NotEmpty().WithMessage(NotificationMessage.DistrictNameEmpty)
                .MaximumLength(Validates.DistrictNameMaxLength).WithMessage(NotificationMessage.DistrictNameLength);
        }
    }
}
