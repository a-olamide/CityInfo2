using CityInfo2.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.API
{
    public class CityDataStore
    {
        public static CityDataStore Current { get; } = new CityDataStore();

        public List<CityDetail> Cities { get; set; }

        public CityDataStore()
        {
            Cities = new List<CityDetail>()
            {
                new CityDetail()
                {
                    Id=1,
                    Name="New York City",
                    Description = "A very buzzling city of the USA",
                    PointsOfInterest = new List<PointsOfInterestDto>()
                    {
                       new PointsOfInterestDto
                       {
                           Id=1,
                           Name = "Central Park",
                           Description = "The beautiful greenery park"
                       },
                        new PointsOfInterestDto
                       {
                           Id=2,
                           Name = "Empire State Building",
                           Description = "The 102-storey sky scrapper"
                       }


                    }
                },
                new CityDetail()
                {
                    Id=2,
                    Name="Lagos",
                    Description = "A very buzzling city of Nigeria",
                    PointsOfInterest = new List<PointsOfInterestDto>()
                    {
                       new PointsOfInterestDto
                       {
                           Id=1,
                           Name = "JJT Park",
                           Description = "The beautiful greenery park"
                       },
                        new PointsOfInterestDto
                       {
                           Id=2,
                           Name = "Eko Antlantic city",
                           Description = "The beautiful new well designed and planned City on the Ocean"
                       }


                    }
                },
                new CityDetail()
                {
                    Id=3,
                    Name="PAris",
                    Description = "A very buzzling city of the French",
                    PointsOfInterest = new List<PointsOfInterestDto>()
                    {
                       new PointsOfInterestDto
                       {
                           Id=1,
                           Name = "Central Park",
                           Description = "The beautiful greenery park"
                       },
                        new PointsOfInterestDto
                       {
                           Id=2,
                           Name = "Eiffel Tower",
                           Description = "The High rise iconic portrait of Paris"
                       }


                    }
                }
            };

        }
    }
}
