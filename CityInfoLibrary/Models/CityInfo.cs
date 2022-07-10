using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CityInfoLibrary.Models
{
    public partial class CityInfo
    {
        public CityInfo()
        {
            PointsOfInterest = new HashSet<PointOfInterest>();
        }

        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PointOfInterest> PointsOfInterest { get; set; }
    }
}
