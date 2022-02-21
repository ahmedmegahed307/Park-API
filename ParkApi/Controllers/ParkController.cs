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
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkOpenApiSpec")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public class ParkController : Controller
    {
        INationalPark _park;
        IMapper _mapper;
        public ParkController(INationalPark park, IMapper mapper)
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
            var obj = _park.GetNationalParks().OrderBy(a => a.NationalParkId);
            var objDto = new List<NationalParkDto>();
            foreach (var item in obj)
            {
                objDto.Add(_mapper.Map<NationalParkDto>(item)); //obj is the domain model
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get Individual park by id 
        /// </summary>
        /// <param name="id">Id of the park</param>
        /// <returns></returns>
        /// 
        [HttpGet("{id:int}",Name = "GetOnePark")] //we used name here to use it in post
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetOnePark(int id)
        {

            var obj = _park.GetParkById(id);
            if(obj ==null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<NationalParkDto>(obj);
            //var objDto = new NationalParkDto()
            //{
            //    Created = obj.Created,
            //    NationalParkId = obj.NationalParkId,
            //    ParkName=obj.ParkName,
            //    State=obj.State
            //};
            return Ok(objDto);
        }
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePark([FromBody] NationalParkDto parkdto)
        {
            if(parkdto==null)
            {
                return BadRequest(ModelState);
            }
            if(_park.NationalParkExist(parkdto.ParkName))//if it exists in database
            {
                ModelState.AddModelError("", "National Park Exists");
                return StatusCode(404, ModelState);
            }

            var obj = _mapper.Map<NationalPark>(parkdto); //convert dto to domain model(nationalpark) ->important

            if (!_park.CreatePark(obj))
            {
                ModelState.AddModelError("", $"something went wrong when saving record{obj.ParkName}");
                return StatusCode(500, ModelState);
            }
            //return Ok(); we used createdAtRoute to show 201 message
       return CreatedAtRoute("GetOnePark",new {version=HttpContext.GetRequestedApiVersion().ToString() 
           ,id =obj.NationalParkId},obj);//we are calling the get method with its parameters
        }


        [HttpPatch("{parkid:int}", Name = "UpdatePark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePark(int parkid, [FromBody] NationalParkDto parkdto)
        {
            if (parkdto == null|| parkid!=parkdto.NationalParkId)
            {
                return BadRequest(ModelState);
            }


            var obj = _mapper.Map<NationalPark>(parkdto); //convert dto to domain model(nationalpark) ->important

            if (!_park.UpdatePark(obj))
            {
                ModelState.AddModelError("", $"something went wrong when updating record{obj.ParkName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{parkid:int}", Name = "DeletePark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePark(int parkid)
        {
            if(!_park.NationalParkExist(parkid))
            {
                return NotFound();
            }

            var obj = _park.GetParkById(parkid);

            if (!_park.DeletePark(obj))
            {
                ModelState.AddModelError("", $"something went wrong when deleting record{obj.ParkName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


       
    }
}
