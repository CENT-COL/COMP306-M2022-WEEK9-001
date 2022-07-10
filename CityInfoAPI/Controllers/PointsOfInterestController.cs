using AutoMapper;
using CityInfoAPI.DTOs;
using CityInfoAPI.Services;
using CityInfoLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityInfoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public async Task<IActionResult> GetPointsOfInterest(int cityId)
        {
            try
            {
                if(!await _cityInfoRepository.CityExists(cityId))
                {
                    return NotFound();
                }

                var pointsOfInterestForCity = await _cityInfoRepository.GetPointsOfInterestForCity(cityId);
                var poinstOfInterestForCityResults = _mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity);
                return Ok(poinstOfInterestForCityResults); 
            }
            catch (Exception ex)
            {

                return StatusCode(500, value:"A problem happened while handling your request");
            }
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public async Task<IActionResult> GetPointOfInterest(int cityId, int id)
        {
            if (!await _cityInfoRepository.CityExists(cityId)) return NotFound();

            var pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterest == null) return NotFound();

            var pointOfInterestResult = _mapper.Map<PointOfInterestDto>(pointOfInterest);

            return Ok(pointOfInterestResult);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null) return BadRequest();

            if(pointOfInterest.Description == pointOfInterest.NameofPoi)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _cityInfoRepository.CityExists(cityId)) return NotFound();

            var finalPointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            if (!await _cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }

            var createdPointOfInterestToReturn = _mapper.Map<PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtAction("GetPointOfInterest", new
            {
                cityId = cityId,
                id = createdPointOfInterestToReturn.PointsOfInterestId
            }, createdPointOfInterestToReturn);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null) return BadRequest();

            if(pointOfInterest.Description == pointOfInterest.NameofPoi)
            {
                ModelState.AddModelError(key: "Description", errorMessage: "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid) return BadRequest();

            if (!await _cityInfoRepository.CityExists(cityId)) return NotFound();

            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterest == null) return NotFound();

            _mapper.Map(pointOfInterest, pointOfInterestEntity);

            if(!await _cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem ocurred while handling your request");
            }

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public async Task<IActionResult> DeletePointOfInterest(int cityId, int id)
        {
            if(!await _cityInfoRepository.CityExists(cityId)) return NotFound();

            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null) return NotFound();

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

            if(!await _cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }

            return NoContent();

        }

        

    }
}
