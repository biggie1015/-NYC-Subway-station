using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubwayEntrance.Data;
using SubwayEntrance.Data.EFCore;
using SubwayEntrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SubwayEntrance.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SubwaysController : GenericController<SubwayUser, EFSubwayRepository>
    {
        private readonly IAuthRepository<User> userRepo;
        private readonly EFUserWithSubwayRepository repositoryUs;
        private readonly string Email;

        public SubwaysController(EFSubwayRepository _repository,
            IAuthRepository<User> _userRepo,
            EFUserWithSubwayRepository _repositoryUs,
            IHttpContextAccessor httpContextAccessor) : base(_repository)
        {
            userRepo = _userRepo;
            repositoryUs = _repositoryUs;
            Email = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        }




        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubwayInfo>>> Get()
        {
            return await repository.GetListSubwayAPI();
        }


        [HttpGet("GetFrecuencytStationByUser")]
        public async Task<ActionResult<string>> GetFrecuenctStationByUser()
        {
            int userId = await repository.GetUserIdByEmail(Email);
            var result = await repository.GetStationFrecuencyByUser(userId);
            return Ok(result);
        }

        [HttpPost("UserWithStation/{stationId}")]
        public async Task<IActionResult> SaveUserStation(int stationId)
        {
            // stations exists ?   // userId has the station?

            var model = await repository.GetSubwayById(stationId);

            SubwayUser station = new SubwayUser
            {
                Id = model.Objectid,
                Station = model.Name,
                Latitude = model.The_geom.Coordinates[0],
                Longitute = model.The_geom.Coordinates[1]

            };



            //Get the UserID 
            int userId = await repository.GetUserIdByEmail(Email);

            UserWithSubway userWithSubway = new UserWithSubway
            {
                userId = userId,
                SubwayUserId = model.Objectid
            };

            var existSation = await repository.ExistStation(model.Objectid); 
           
            if (!existSation)
                await repository.Add(station);

            try
            {

                await repositoryUs.Add(userWithSubway);
            }
            catch (Exception ex)
            {
            }

            return Ok();


        }

        //  return Conflict(new { message = $"An existing record with the name '{model.Name}' was already found." });



    }
}
