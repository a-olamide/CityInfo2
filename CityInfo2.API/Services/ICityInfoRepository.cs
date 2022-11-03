using CityInfo2.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.API.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();

        City GetCity(int cityId, bool includePointsOfInterest);

        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityID, int pointOfInterestId);

        bool CityExists(int cityId);

        void AddPointOfInterestForCity(int cityID, PointOfInterest pointOfInterest);

        void UpdatePointOfInterestForCity(int cityID, PointOfInterest pointOfInterest);

        void DeletePointOfInterestForCity(PointOfInterest pointOfInterest);

        bool save();
    }
}
