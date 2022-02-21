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

    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ApiExplorerSettings(GroupName = "ParkOpenApiSpecForTrail")]
    public class TrailController : Controller
    {
        ITrail _trail;
        IMapper _mapper;
        public TrailController(ITrail trail, IMapper mapper)
        {
           _trail = trail;
            _mapper = mapper;
        }
          ///<summary>
          /// Get List of all Trails
          ///</summary>   
          ///<returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(List<TrailDto>))]
        public IActionResult GetAllTrails()
        {
            var obj =_trail.GetTrails().OrderBy(a => a.TrailId);
            var objDto = new List<TrailDto>();
            foreach (var item in obj)
            {
                objDto.Add(_mapper.Map<TrailDto>(item)); //obj is the domain model
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get Individual trail by id 
        /// </summary>
        /// <param name="id">Id of the trail</param>
        /// <returns></returns>
        /// 
        [HttpGet("{id:int}",Name = "GetOneTrail")] //we used name here to use it in post
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetOneTrail(int id)
        {

            var obj =_trail.GetTrailById(id);
            if(obj ==null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<TrailDto>(obj);
           
            return Ok(objDto);
        }

        [HttpGet("GetTrailInPark/{parkid:int}", Name = "GetTrailInPark")] //we used name here to use it in post
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrailInPark(int parkid)
        {

            var obj = _trail.GetTrailsInPark(parkid);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = new List<TrailDto>(); 
            foreach(var item in obj)
            {
                objDto.Add(_mapper.Map<TrailDto>(obj));
            }
           

            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] TrailCreateDto traildto)
        {
            if(traildto==null)
            {
                return BadRequest(ModelState);
            }
            if(_trail.TrailExist(traildto.TrailName))//if it exists in database
            {
                ModelState.AddModelError("", "Trail  Exists");
                return StatusCode(404, ModelState);
            }

            var obj = _mapper.Map<Trail>(traildto); //convert dto to domain model(trail) ->important

            if (!_trail.CreateTrail(obj))
            {
                ModelState.AddModelError("", $"something went wrong when saving record{obj.TrailName}");
                return StatusCode(500, ModelState);
            }
            //return Ok(); we used createdAtRoute to show 201 message
       return CreatedAtRoute("GetOneTrail",new { id =obj.TrailId},obj);//we are calling the get method with its parameters
        }


        [HttpPatch("{trailid:int}", Name = "UpdateTrail")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int trailid, [FromBody] TrailUpdateDto traildto)
        {
            if (traildto == null|| trailid!=traildto.TrailId)
            {
                return BadRequest(ModelState);
            }


            var obj = _mapper.Map<Trail>(traildto); //convert dto to domain model(trail) ->important

            if (!_trail.UpdateTrail(obj))
            {
                ModelState.AddModelError("", $"something went wrong when updating record{obj.TrailName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{trailid:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int trailid)
        {
            if(!_trail.TrailExist(trailid))
            {
                return NotFound();
            }

            var obj =_trail.GetTrailById(trailid);

            if (!_trail.DeleteTrail(obj))
            {
                ModelState.AddModelError("", $"something went wrong when deleting record{obj.TrailName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


    }
}
