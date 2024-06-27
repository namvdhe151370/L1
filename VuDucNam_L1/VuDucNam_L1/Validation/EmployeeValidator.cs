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
                .NotEmpty().WithMessage(Validates.EmployeeNameEmpty)
                .MaximumLength(Validates.EmployeeNameMaxLength).WithMessage(Validates.EmployeeNameLength);

            RuleFor(e => e.Dob)
                .NotEmpty().WithMessage(Validates.DobLength);

            RuleFor(e => e.Age)
                .GreaterThan(Validates.AgeLengthMin).WithMessage(Validates.AgeLengthGreater)
                .LessThan(Validates.AgeLengthMax).WithMessage(Validates.AgeLengthLess);

            RuleFor(e => e.EthnicId)
                .NotEmpty().WithMessage(Validates.EthnicityEmpty);

            RuleFor(e => e.JobId)
                .NotEmpty().WithMessage(Validates.JobEmpty);

            RuleFor(e => e.CitizenNumber)
                .Matches(@"^\d{12}$").WithMessage(Validates.CitizenNumberFormat)
                .Must((model, citizenNumber) => BeUniqueCitizenNumber(model, citizenNumber)).WithMessage(Validates.CitizenNumberUnique);

            RuleFor(e => e.PhoneNumber)
                .Matches(@"^\d{10,12}$").WithMessage(Validates.PhoneNumberFormat);

            RuleFor(e => e.CityId)
                .NotEmpty().WithMessage(Validates.CityEmpty);

            RuleFor(e => e.DistrictId)
                .NotEmpty().WithMessage(Validates.DistrictEmpty);

            RuleFor(e => e.WardId)
                .NotEmpty().WithMessage(Validates.WardEmpty);

            RuleFor(e => e.SpecificAddress)
               .NotEmpty().WithMessage(Validates.SpecificAddressEmpty)
               .MaximumLength(Validates.SpecificAddressLengthMax).WithMessage(Validates.SpecificAddressLength);

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
