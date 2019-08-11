using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetMap.Models
{
    public class CoordinatesModel
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }
        public CoordinatesModel(double latitude, double longitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        public CoordinatesModel()
        {
            
        }


    }

}
