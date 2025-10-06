using AutoMapper;
using Company.Route.DAL.Models;
using Company.Route.PL.DTOs;

namespace Company.Route.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(M => M.Name, o => o.MapFrom(Dto => Dto.EmpName)).ReverseMap();

            //CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
            //CreateMap<Employee, CreateEmployeeDto>();
        }
    }
}
