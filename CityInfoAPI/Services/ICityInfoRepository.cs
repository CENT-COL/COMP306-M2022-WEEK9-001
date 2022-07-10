using CityInfoLibrary.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityInfoAPI.Services
{
    public interface ICityInfoRepository
    {
        Task<bool> CityExists(int cityId);
        Task<IEnumerable<CityInfo>> GetCities();
        Task<CityInfo> GetCityById(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCity(int cityId);
        Task<PointOfInterest> GetPointOfInterestForCity(int cityId, int pointOfInterestId);
        Task AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        Task<bool> Save();
    }
}
