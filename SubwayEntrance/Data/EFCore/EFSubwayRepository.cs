using Newtonsoft.Json;
using SubwayEntrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubwayEntrance.Data.JWTHelper;

namespace SubwayEntrance.Data.EFCore
{
    public class EFSubwayRepository:EFCoreRepository<SubwayUser, SubwayContext>
    {

        public static List<SubwayInfo> subways = new List<SubwayInfo>();
        //public static SubwayInfo subway = new SubwayInfo();

        public EFSubwayRepository(SubwayContext subwayContext):base(subwayContext)
        {
             
        }

        public async Task<bool> ExistStation(int subwayId)
        {
            return await context.SubwayUsers
                .AnyAsync(x => x.Id == subwayId);
                         
        }

        public async Task<string> GetStationFrecuencyByUser(int userId)
        {
             return await context.UserWithSubways.GroupBy(s => s.SubwayUser.Station)
                                    .OrderBy(g => g.Count())
                                    .Select(g => g.Key).FirstOrDefaultAsync();
        }

        public async Task<bool> ExistStationForUser(int userId, int subwayId)
        {
            return  await context.UserWithSubways
                         .AnyAsync(w => w.SubwayUserId == subwayId && w.userId == userId);                 
        }

        public async Task<List<SubwayInfo>> GetListSubwayAPI()
        {
            
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using var response = await httpClient.GetAsync("https://data.cityofnewyork.us/resource/he7q-3hwy.json");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    subways = JsonConvert.DeserializeObject<List<SubwayInfo>>(apiResponse);
                    subways = PaginateSearch(subways);
                    
                }

                return subways;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public List<SubwayInfo> PaginateSearch(List<SubwayInfo> subways)
        {
            
            int page = 1, pageZise = 15;
            var list = subways.OrderBy(x => x.Objectid)
                .Skip((page - 1) * pageZise)
                .Take(pageZise).ToList();

            return list;
        }


        public  async Task<SubwayInfo> GetSubwayById(int id)
        {
            await GetListSubwayAPI();
            var model =   subways.Where(x => x.Objectid == id)
                                .Select(s =>
                                new SubwayInfo
                                {
                                    Name = s.Name,
                                    Objectid = s.Objectid,
                                    The_geom = s.The_geom
                                }).FirstOrDefault();

            return model;
      
        }


        public async Task<int> GetUserIdByEmail(string email)
        {
           return await context.Users.Where(x => x.Email == email).Select(s => s.Id).FirstOrDefaultAsync();
        }


        public string CalculateDistance(double lat, double lng)
        {
            var cal = new Calculate();
            return  cal.CalulateDistance(lat, lng);
        }

    }
}
