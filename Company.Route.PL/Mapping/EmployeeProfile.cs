using AutoMapper;
using Company.Route.DAL.Models;
using Company.Route.PL.DTOs;

namespace Company.Route.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();
            //.ForMember(M => M.Name, o => o.MapFrom(Dto => Dto.EmpName));

            CreateMap<Employee, CreateEmployeeDto>();
            //.ForMember(M => M.EmpName, o => o.MapFrom(E => E.Name));

            //CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
            //CreateMap<Employee, CreateEmployeeDto>();
        }
    }
}
