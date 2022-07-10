using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CityInfoLibrary.Models
{
    public partial class PointOfInterest
    {
        [Key]
        public int PointsOfInterestId { get; set; }
        public string NameofPoi { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }

        public virtual CityInfo City { get; set; }
    }
}
