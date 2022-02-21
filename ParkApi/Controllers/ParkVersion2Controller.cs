using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkAPI.Models;
using ParkAPI.Models.Dtos;
using ParkAPI.Repository.InterfaceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI.Controllers
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkOpenApiSpec")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class ParkVersion2Controller : Controller
    {
        INationalPark _park;
        IMapper _mapper;
        public ParkVersion2Controller(INationalPark park, IMapper mapper)
        {
            _park = park;
            _mapper = mapper;
        }
          ///<summary>
          /// Get List of all Parks
          ///</summary>   
          ///<returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(List<NationalParkDto>))]
        public IActionResult GetAllParks()
        {
            var obj = _park.GetNationalParks().OrderBy(a => a.NationalParkId).FirstOrDefault();
            //var objDto = new List<NationalParkDto>();
            //foreach (var item in obj)
            //{
            //    objDto.Add(_mapper.Map<NationalParkDto>(item)); //obj is the domain model
            //}
            return Ok(_mapper.Map<NationalParkDto>(obj));
        }

        
        


    }
}
