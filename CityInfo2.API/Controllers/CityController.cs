using AutoMapper;
using CityInfo2.API.Models;
using CityInfo2.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public IMapper _mapper { get; }

        public CityController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();

            //This is when you are not using Mapper
            //var results = new List<CityWithoutPointOfInterestDto>();

            //foreach (var cityEntity in cityEntities)
            //{
            //    results.Add(
            //        new CityWithoutPointOfInterestDto()
            //        {
            //            Id = cityEntity.Id,
            //            Name = cityEntity.Name,
            //            Description = cityEntity.Description
            //        });
            //}
            //return Ok(results);
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntities));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCity(int id, bool inlcudePointsOfInterest = false)
        {
            //            var cityToreturn = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);   
            //or
            //var cityToreturn = (from city in CityDataStore.Current.Cities
            //                    where city.Id == id
            //                    select city ).ToList();
            var city = _cityInfoRepository.GetCity(id, inlcudePointsOfInterest);
            if (city== null)
            {
                return NotFound();  
            }
           if (inlcudePointsOfInterest)
            {
                //if you are not using mapper
                // var cityResult = new CityDetail()
                // {
                //     Id = city.Id,
                //     Name = city.Name,
                //     Description = city.Description
                // };

                //foreach( var poi in city.PointsOfInterest)
                // {
                //     cityResult.PointsOfInterest.Add(
                //         new PointsOfInterestDto()
                //         {
                //             Id = poi.Id,
                //             Name = poi.Name,
                //             Description = poi.Description
                //         });
                // }
                // return Ok(cityResult);
                var cityResult = _mapper.Map<CityDetail>(city);
                return Ok(cityResult);
            }
            // if you are not using Mapper
            //var citywithoutPointOfInterestResult = new CityWithoutPointOfInterestDto()
            //{
            //    Id = city.Id,
            //    Name = city.Name,
            //    Description = city.Description
            //};
            //return Ok(citywithoutPointOfInterestResult);
            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));
            //return new JsonResult(CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));


        }
    }
}
