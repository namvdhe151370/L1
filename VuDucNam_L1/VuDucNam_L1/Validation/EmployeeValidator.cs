using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using VuDucNam_L1.Constants;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Validation
{
    public class EmployeeValidator : AbstractValidator<EmployeeModel>
    {
        private readonly AppDbContext _context;

        public EmployeeValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(e => e.EmployeeName)
                .NotEmpty().WithMessage(NotificationMessage.EmployeeNameEmpty)
                .MaximumLength(Validates.EmployeeNameMaxLength).WithMessage(NotificationMessage.EmployeeNameLength);

            RuleFor(e => e.Dob)
                .NotEmpty().WithMessage(NotificationMessage.DobLength);

            RuleFor(e => e.Age)
                .GreaterThan(Validates.AgeLengthGreater).WithMessage(NotificationMessage.AgeLengthGreater)
                .LessThan(Validates.AgeLengthLess).WithMessage(NotificationMessage.AgeLengthLess);

            RuleFor(e => e.EthnicId)
                .NotEmpty().WithMessage(NotificationMessage.EthnicityEmpty);

            RuleFor(e => e.JobId)
                .NotEmpty().WithMessage(NotificationMessage.JobEmpty);

            RuleFor(e => e.CitizenNumber)
                .Matches(@"^\d{12}$").WithMessage(NotificationMessage.CitizenNumberFormat)
                .Must((model, citizenNumber) => BeUniqueCitizenNumber(model, citizenNumber)).WithMessage(NotificationMessage.CitizenNumberUnique);

            RuleFor(e => e.PhoneNumber)
                .Matches(@"^\d{10,12}$").WithMessage(NotificationMessage.PhoneNumberFormat);

            RuleFor(e => e.CityId)
                .NotEmpty().WithMessage(NotificationMessage.CityEmpty);

            RuleFor(e => e.DistrictId)
                .NotEmpty().WithMessage(NotificationMessage.DistrictEmpty);

            RuleFor(e => e.WardId)
                .NotEmpty().WithMessage(NotificationMessage.WardEmpty);

            RuleFor(e => e.SpecificAddress)
               .NotEmpty().WithMessage(NotificationMessage.SpecificAddressEmpty)
               .MaximumLength(Validates.SpecificAddressLength).WithMessage(NotificationMessage.SpecificAddressLength);

            RuleForEach(e => e.Certificates).SetValidator(new CertificateValidator());
        }

        private bool BeUniqueCitizenNumber(EmployeeModel model, string citizenNumber)
        {
            if (string.IsNullOrEmpty(citizenNumber))
            {
                return true;
            }
            var existingEmployee = _context.Employees.FirstOrDefault(e => e.CitizenNumber == citizenNumber && e.EmployeeId != model.EmployeeId);
            return existingEmployee == null;
        }
    }
}
