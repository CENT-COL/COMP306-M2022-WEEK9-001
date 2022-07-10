using CityInfoAPI.DTOs;
using System.Collections;
using System.Collections.Generic;

namespace CityInfoAPI.DTOs
{
    public class CityInfoDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Description { get; set; }
        public int NumberOfPointsOfInterest() => PointsOfInterest.Count;
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } 
            = new List<PointOfInterestDto>();
    }
}
