using AutoMapper;
using ParkAPI.Models;
using ParkAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI.AutoMapper
{
    public class ParkMapper : Profile
    {
        public ParkMapper()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap(); // reversemap-> map both to each other 
            CreateMap<Trail, TrailDto>().ReverseMap();
            CreateMap<Trail, TrailCreateDto>().ReverseMap();
            CreateMap<Trail, TrailUpdateDto>().ReverseMap();


        }
    }
}
