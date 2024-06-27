using FluentValidation;
using System.Globalization;
using VuDucNam_L1.Constants;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Validation
{
    public class CertificateValidator : AbstractValidator<CertificateModel>
    {
        public CertificateValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(Validates.CertificateEmpty)
                .MaximumLength(Validates.CertificateMaxLength).WithMessage(Validates.CertificateLength);

            RuleFor(c => c.IssuedDate)
                .NotEmpty().WithMessage(Validates.IssuedDateEmpty);

            RuleFor(c => c.IssuedBy)
                .NotEmpty().WithMessage(Validates.IssuedByEmpty)
                .MaximumLength(Validates.IssuedByMaxLength).WithMessage(Validates.IssuedByLength);

            RuleFor(c => c.ExpiryDate)
               .NotEmpty().WithMessage(Validates.ExpiryDateEmpty);
        }
    }
}
