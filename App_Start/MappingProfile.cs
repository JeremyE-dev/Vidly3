using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly3.Dtos;
using Vidly3.Models;

namespace Vidly3.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //generic object that takes two params source and target
            //here we want to map customer to customerdto
            
            //when we call create map method,
            //AutpMapper uses reflection to scan these type,
            //finds there properties and maps them based on their name

            // thi sis a convention based mapping tool because it uses property names
            // as teh convention to map objects 

            //Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();

            //Dto to Domain
            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<MovieDto, Movie>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}