﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "You should provide a name value")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [ForeignKey("CityId")]
        public City city { get; set; }

        public int CityId { get; set; }
    }
}
