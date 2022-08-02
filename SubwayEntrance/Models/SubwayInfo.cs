using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubwayEntrance.Models
{
    public class SubwayInfo
    {
        public SubwayInfo()
        {
           // the_geom = new List<Coordinates>();
        }
        public int Objectid { get; set; }

        public string Name { get; set; }

        public GeoInformation The_geom { get; set; }
    }


    public class GeoInformation
    {
        public string Type { get; set; }

        public List<double> Coordinates { get; set; }
    }




}
