using AutoMapper;
using CityInfoAPI.DTOs;
using CityInfoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityInfoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityInfoController : ControllerBase
    {
        private ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CityInfoController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/api/cityInfos")]
        public async Task<IActionResult> GetCityinfos()
        {
            var cities = await _cityInfoRepository.GetCities();
            var results = _mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cities);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(int id, bool includePoinstOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityById(id, includePoinstOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePoinstOfInterest)
            {
                var cityRestult = _mapper.Map<CityInfoDto>(city);
                return Ok(cityRestult);
            }

            var cityWithoutPoinstofInterestResult = _mapper.Map<CityWithoutPointsOfInterestDto>(city);
            return Ok(cityWithoutPoinstofInterestResult);
        }
    }
}
