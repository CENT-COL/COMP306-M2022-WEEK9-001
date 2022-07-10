using AutoMapper;
using CityInfoAPI.DTOs;
using CityInfoLibrary.Models;

namespace CityInfoAPI.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //CityInfo is the source, while CityWithoutPointsOfInterestDto is the destination
            CreateMap<CityInfo, CityWithoutPointsOfInterestDto>();
            CreateMap<CityInfo, CityInfoDto>();
            CreateMap<PointOfInterest, PointOfInterestDto>();
            CreateMap<PointOfInterestForCreationDto, PointOfInterest>(); // to add new point of interest
            CreateMap<PointOfInterestForUpdateDto, PointOfInterest>(); // to update existing point of interest
        }        
    }
}
