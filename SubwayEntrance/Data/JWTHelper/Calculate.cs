using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SubwayEntrance.Data.JWTHelper
{
    public class Calculate
    {
        public string CalulateDistance(int lat, int lon)
        {
            double rlat1 = Math.PI * lat / 180;
            double rlat2 = Math.PI * lon / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist.ToString();
        }
    }
}
