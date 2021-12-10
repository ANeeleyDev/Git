using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("/playdates/")]
    [ApiController]
    [Authorize]
    public class PlaydateController : ControllerBase
    {
        //Properties:
        private readonly IPlaydateDao playdateDao;



        //Constructor:
        public PlaydateController(IPlaydateDao _playdateDao)
        {
            playdateDao = _playdateDao;
        }



        //Anonymous user methods:

        [HttpGet("{playdateId}")]
        [AllowAnonymous]
        public Playdate GetPlaydateByPlaydateId(int playdateId) //View any playdate
        {
            return playdateDao.GetPlaydate(playdateId);
        }

        [HttpGet("user/{userId}")]
        [AllowAnonymous]
        public List<Playdate> GetPlaydatesByUserId(int userId) //View all playdates from a specified user
        {
            return playdateDao.GetPlaydatesByUserId(userId);
        }


        //Registered user methods

        [HttpPost("myplaydates/register")]
        [Authorize]
        public Playdate RegisterPlaydate(Playdate playdateToSave) //Register a playdate in system
        {
            string userIdString = User.FindFirst("sub")?.Value;
            playdateToSave.playdatePostedUserId = Convert.ToInt32(userIdString);

            return playdateDao.RegisterPlaydate(playdateToSave);
        }

        [HttpGet("myplaydates")]
        [Authorize]
        public List<Playdate> GetLoggedInUserPlaydates(int userId) //View their playdates
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return playdateDao.GetLoggedInUserPlaydates(userId);
        }

        [HttpDelete("myplaydates/{playdateId}")] 
        [Authorize]
        public bool DeleteLoggedInUserPlaydate(int playdateId, int userId) //Delete their playdate
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return playdateDao.DeleteLoggedInUserPlaydate(playdateId, userId);
        }

        [HttpPut("myplaydates/{playdateId}")] 
        [Authorize]
        public bool UpdateLoggedInUserPlaydate(Playdate updatedPlaydate, int userId, int playdateId) //Update their playdat
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return playdateDao.UpdateLoggedInUserPlaydate(updatedPlaydate, userId, playdateId);
        }



        //Admin methods

        [HttpDelete("admin/{playdateId}")] 
        [Authorize(Roles = "admin")]
        public bool DeletePlaydate(int playdateId) //Delete any playdate
        {
            return playdateDao.DeletePlaydate(playdateId);
        }

        [HttpPut("admin/{playdateId}")] 
        [Authorize(Roles = "admin")] 
        public bool UpdatePlaydate(Playdate updatedPlaydate, int playdateId) //Can update any playdate
        {
            return playdateDao.UpdatePlaydate(updatedPlaydate, playdateId);
        }


    }
}
