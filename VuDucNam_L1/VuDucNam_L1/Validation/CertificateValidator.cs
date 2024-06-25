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
                .NotEmpty().WithMessage(NotificationMessage.CertificateEmpty)
                .MaximumLength(Validates.CertificateMaxLength).WithMessage(NotificationMessage.CertificateLength);

            RuleFor(c => c.IssuedDate)
                .NotEmpty().WithMessage(NotificationMessage.IssuedDateEmpty);

            RuleFor(c => c.IssuedBy)
                .NotEmpty().WithMessage(NotificationMessage.IssuedByEmpty)
                .MaximumLength(Validates.IssuedByMaxLength).WithMessage(NotificationMessage.IssuedByLength);

            RuleFor(c => c.ExpiryDate)
               .NotEmpty().WithMessage(NotificationMessage.ExpiryDateEmpty);
        }
    }
}
