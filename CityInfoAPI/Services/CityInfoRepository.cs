using CityInfoLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {

        private CityInfoDBContext _context;

        public CityInfoRepository(CityInfoDBContext context)
        {
            this._context = context;
        }

        public async Task<bool> CityExists(int cityId)
        {
            return await _context.CityInfo.AnyAsync<CityInfo>(c => c.CityId == cityId);
        }

        public async Task<IEnumerable<CityInfo>> GetCities()
        {
            var result = _context.CityInfo.OrderBy(c => c.CityName);
            return await result.ToListAsync();
        }
        public async Task<CityInfo> GetCityById(int cityId, bool includePointsOfInterest)
        {
            IQueryable<CityInfo> result;

            if (includePointsOfInterest)
            {
                result = _context.CityInfo.Include(c => c.PointsOfInterest)
                    .Where(c => c.CityId == cityId);
            }else
            {
                result = _context.CityInfo.Where(c => c.CityId == cityId);
            }

            return await result.FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest> GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            IQueryable<PointOfInterest> result = _context.PointsOfInterest.Where(p => p.CityId == cityId && p.PointsOfInterestId == pointOfInterestId);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCity(int cityId)
        {
            IQueryable<PointOfInterest> result = _context.PointsOfInterest.Where(p => p.CityId == cityId);
            return await result.ToListAsync();
        }

        public async Task AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            CityInfo city = await GetCityById(cityId, true);
            city.PointsOfInterest.Add(pointOfInterest);
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
