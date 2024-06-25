using AutoMapper;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityModel>().ReverseMap();

            CreateMap<District, DistrictModel>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.CityName));
            CreateMap<DistrictModel, District>();

            CreateMap<Ward, WardModel>()
                .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District.DistrictName));
            CreateMap<WardModel, Ward>();

            CreateMap<Employee, EmployeeModel>()
                .ForMember(dest => dest.Certificates, opt => opt.MapFrom(src => src.Certificates));
            CreateMap<EmployeeModel, Employee>();
            CreateMap<Certificate, CertificateModel>().ReverseMap();

            CreateMap<Ethnic, EthnicModel>().ReverseMap();

            CreateMap<Job, JobModel>().ReverseMap();
        }
    }
}
