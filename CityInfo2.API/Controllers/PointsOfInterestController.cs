using AutoMapper;
using CityInfo2.API.Models;
using CityInfo2.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                //  var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                //    var pointsOfInterestToreturn = cityToreturn.PointsOfInterest.ToList<PointsOfInterestDto>();
               
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing point of interest");
                    return NotFound();
                }
                var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(cityId);
                //if you are not using Mapper
                //var pointsOfInterestForCityResult = new List<PointsOfInterestDto>();
                //foreach (var poi in pointsOfInterestForCity)
                //{
                //    pointsOfInterestForCityResult.Add(new PointsOfInterestDto()
                //    {
                //        Id = poi.Id,
                //        Name = poi.Name,
                //        Description = poi.Description
                //    });
                //}


                //return Ok(pointsOfInterestForCityResult);
                return Ok(_mapper.Map<List<PointsOfInterestDto>>(pointsOfInterestForCity));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception encountered while getting point of interest for cityId {cityId}.", ex);
                return StatusCode(500, "Error encountered while handling your request");
            }          
        }
        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            //var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            ////    var pointsOfInterestToreturn = cityToreturn.PointsOfInterest.ToList<PointsOfInterestDto>();
            //if (city == null)
            //{
            //    _logger.LogInformation($"City with id {cityId} wasn't found when accessing point of interest");
            //    return NotFound();
            //}
            //var pointOfInterestToreturn = city.PointsOfInterest.FirstOrDefault(poi => poi.Id == id);
            //if (pointOfInterestToreturn == null)
            //{
            //    _logger.LogInformation($"point of interest with id {id} wasn't found when accessing point of interest");
            //    return NotFound();
            //}
            //return Ok(pointOfInterestToreturn);

            if (!_cityInfoRepository.CityExists(cityId))
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing point of interest");
                return NotFound();
            }
            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterest == null)
            {
                _logger.LogInformation($"point of interest with id {id} wasn't found when accessing point of interest");
                return NotFound();
            }
            //var pointOfInterestResult = new PointsOfInterestDto()
            //{
            //    Id = pointOfInterest.Id,
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description
            //};
            //return Ok(pointOfInterestResult);
            return Ok(_mapper.Map<PointsOfInterestDto>(pointOfInterest));
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest.Description == pointOfInterest.Name)
                ModelState.AddModelError("Description", "The provided description should not be same as name");


            if (!ModelState.IsValid)
                return BadRequest();


            // var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            // if (city == null)
            //     return NotFound();

            // var maxPointOfInterestId = CityDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            //// return NoContent();
            // //ideally you should just return no contenht. You van as weel return the point of interest that was updated
            // var finalPointOfInterest = new PointsOfInterestDto()
            // {
            //     Id = ++maxPointOfInterestId,
            //     Name = pointOfInterest.Name,
            //     Description = pointOfInterest.Description
            // };

            // city.PointsOfInterest.Add(finalPointOfInterest);
            //return CreatedAtRoute("GetPointOfInterest",
            //    new { cityId = cityId, id = finalPointOfInterest.Id },
            //    finalPointOfInterest);
            if (!_cityInfoRepository.CityExists(cityId))
                return NotFound();

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);
            _cityInfoRepository.save();

            var createdPointOdInterestToReturn = _mapper.Map<Models.PointsOfInterestDto>(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest",
                new { cityId = cityId, id = createdPointOdInterestToReturn.Id },
                createdPointOdInterestToReturn);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest.Description == pointOfInterest.Name)
                ModelState.AddModelError("Description", "The provided description should not be same as name");


            if (!ModelState.IsValid)
                return BadRequest();

            //var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //    return NotFound();

            //var pointOfInterestStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            //if (pointOfInterestStore == null)
            //    return NotFound();

            //pointOfInterestStore.Name = pointOfInterest.Name;
            //pointOfInterestStore.Description = pointOfInterest.Description;

            //var finalPointOfInterest = new PointsOfInterestDto()
            //{
            //    Id = id,
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description
            //};

            //return CreatedAtRoute("GetPointOfInterest",
            //    new { cityId = cityId, id },
            //    finalPointOfInterest);
            if (!_cityInfoRepository.CityExists(cityId))
                return NotFound();
            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity== null)
                return NotFound();
            _mapper.Map(pointOfInterest, pointOfInterestEntity);
            _cityInfoRepository.UpdatePointOfInterestForCity(cityId, pointOfInterestEntity);
            _cityInfoRepository.save();
            return NoContent();

        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
           if (!_cityInfoRepository.CityExists(cityId))
                return NotFound();

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
                return NotFound();

            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
                ModelState.AddModelError("Description", "The provided description should not be same as name");

            if (!TryValidateModel(pointOfInterestToPatch))
                return BadRequest();

            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);
            _cityInfoRepository.UpdatePointOfInterestForCity(cityId, pointOfInterestEntity);
            _cityInfoRepository.save();
            return NoContent();
            //When not using AutoMapper
            //var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //    return NotFound();

            //var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            //if (pointOfInterestFromStore == null)
            //    return NotFound(); 

            //var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            //{
            //    Name = pointOfInterestFromStore.Name,
            //    Description = pointOfInterestFromStore.Description
            //};
            //patchDoc.ApplyTo(pointOfInterestToPatch,ModelState);

            //if (!ModelState.IsValid)
            //    return BadRequest();

            //if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            //    ModelState.AddModelError("Description", "The provided description should not be same as name");

            //if ( !TryValidateModel(pointOfInterestToPatch))
            //    return BadRequest();

            //pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            //pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            //var finalPointOfInterest = new PointsOfInterestDto()
            //{
            //    Id = id,
            //    Name = pointOfInterestFromStore.Name,
            //    Description = pointOfInterestFromStore.Description
            //};

            //return CreatedAtRoute("GetPointOfInterest",
            //    new { cityId = cityId, id },
            //    finalPointOfInterest);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_cityInfoRepository.CityExists(cityId))
                return NotFound();

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
                return NotFound();

            _cityInfoRepository.DeletePointOfInterestForCity(pointOfInterestEntity);

            _cityInfoRepository.save();

            _mailService.Send("Deletion", $"point of interest with name {pointOfInterestEntity.Name} and id {pointOfInterestEntity.Id} has been deleted.");
            return NoContent();

            //var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //    return NotFound();

            //var pointOfInterestStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            //if (pointOfInterestStore == null)
            //    return NotFound();


            //city.PointsOfInterest.Remove(pointOfInterestStore);
            //_mailService.Send("Deletion","mail sent for deletion");
            //return NoContent();

        }
    }

}
