﻿namespace VuDucNam_L1.Constants
{
    public class Validates
    {
        public const string NoCitizenNumber = "No Citizen Number";
        public const string NoPhoneNumber = "No Phone Number";
        public const string FormatDate = "dd/MM/yyyy";
        public const string SuccessMessage = "SuccessMessage";
        public const string ErrorMessage = "ErrorMessage";
        public const int CertificateMaxLength = 50;
        public const int IssuedByMaxLength = 50;
        public const int CityNameMaxLength = 100;
        public const int DistrictNameMaxLength = 50;
        public const int EmployeeNameMaxLength = 150;
        public const int AgeLengthMin = 0;
        public const int AgeLengthMax = 150;
        public const int SpecificAddressLengthMax = 150;
        public const int WardNameLengthMax = 100;
        public const string NoEmployeesSelectedForExport = "No employees selected for export.";
        public const string NoFileUploaded = "No file uploaded.";
        public const string InvalidFileFormat = "Please upload an Excel file with format: .xls or .xlsx.";
        public const string ValidationErrorsOccurred = "Validation error occurred while importing employees.";
        public const string ErrorExportingEmployees = "Error exporting employees: {0}";
        public const string ErrorCreatingEmployee = "Error creating employee: {0}";
        public const string ErrorUpdatingEmployee = "Error updating employee: {0}";
        public const string ErrorDeletingEmployee = "Error deleting employee: {0}";
        public const string ErrorProcessingFile = "Error while processing file: {0}";
        public const string EmployeeCreatedSuccessfully = "Employee created successfully.";
        public const string EmployeeUpdatedSuccessfully = "Employee updated successfully.";
        public const string EmployeeDeletedSuccessfully = "Employee deleted successfully.";
        public const string EmployeesExportedSuccessfully = "Employees Exported successfully.";
        public const string EmployeesImportedSuccessfully = "Employees imported successfully.";
        public const string ExcelHeader = "EmployeeName,Dob,Age,EthnicName,JobName,CitizenNumber,PhoneNumber,CityName,DistrictName,WardName,SpecificAddress";
        public const string CertificateEmpty = "Certificate Name is required.";
        public const string CertificateLength = "Certificate Name must not exceed 50 characters.";
        public const string IssuedDateEmpty = "Issued Date is required.";
        public const string IssuedByEmpty = "Issued By is required.";
        public const string IssuedByLength = "Issued By must not exceed 50 characters.";
        public const string ExpiryDateEmpty = "Expiry Date is required.";
        public const string CityNameEmpty = "City Name is required.";
        public const string CityNameLength = "City Name must not exceed 100 characters.";
        public const string DistrictNameEmpty = "District Name is required.";
        public const string DistrictNameLength = "District Name must not exceed 50 characters.";
        public const string EmployeeNameEmpty = "Employee Name is required.";
        public const string EmployeeNameLength = "Employee Name must not exceed 150 characters.";
        public const string DobLength = "Date of Birth is required.";
        public const string AgeLengthGreater = "Age must be greater than 0.";
        public const string AgeLengthLess = "Age must be less than 150.";
        public const string EthnicityEmpty = "Ethnicity is required.";
        public const string JobEmpty = "Job is required.";
        public const string CitizenNumberFormat = "Citizen Number must be exactly 12 digits.";
        public const string CitizenNumberUnique = "Citizen Number must be unique.";
        public const string PhoneNumberFormat = "Phone Number must be between 10 and 12 digits.";
        public const string CityEmpty = "City is required.";
        public const string DistrictEmpty = "District is required.";
        public const string WardEmpty = "Ward is required.";
        public const string SpecificAddressEmpty = "Specific Address is required.";
        public const string SpecificAddressLength = "Specific Address must not exceed 150 characters.";
        public const string WardNameEmpty = "Ward Name is required.";
        public const string WardNameLength = "Employee Name must not exceed 100 characters.";
        public const string CityCreatedSuccessfully = "City created successfully.";
        public const string CityUpdatedSuccessfully = "City updated successfully.";
        public const string CityDeletedSuccessfully = "City deleted successfully.";
        public const string CityValidatorError = "City validator error.";
        public const string ErrorCreatingCity = "Error creating city: {0}";
        public const string ErrorUpdatingCity = "Error updating city: {0}";
        public const string ErrorDeletingCity = "Error deleting city: {0}";
        public const string DistrictCreatedSuccessfully = "District created successfully.";
        public const string DistrictUpdatedSuccessfully = "District updated successfully.";
        public const string DistrictDeletedSuccessfully = "District deleted successfully.";
        public const string DistrictValidatorError = "District validator error.";
        public const string ErrorCreatingDistrict = "Error creating district: {0}";
        public const string ErrorUpdatingDistrict = "Error updating district: {0}";
        public const string ErrorDeletingDistrict = "Error deleting district: {0}";
        public const string WardCreatedSuccessfully = "Ward created successfully.";
        public const string WardUpdatedSuccessfully = "Ward updated successfully.";
        public const string WardDeletedSuccessfully = "Ward deleted successfully.";
        public const string WardValidatorError = "Ward validator error.";
        public const string ErrorCreatingWard = "Error creating ward: {0}";
        public const string ErrorUpdatingWard = "Error updating ward: {0}";
        public const string ErrorDeletingWard = "Error deleting ward: {0}";
    }
}
