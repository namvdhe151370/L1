using FluentValidation;
using VuDucNam_L1.Constants;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Validation
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        public CityValidator() 
        {
            RuleFor(e => e.CityName)
                .NotEmpty().WithMessage(Validates.CityNameEmpty)
                .MaximumLength(Validates.CityNameMaxLength).WithMessage(Validates.CityNameLength);
        }
    }
}
