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

        [HttpGet("{playdateId}/display")]
        [AllowAnonymous]
        public Playdate GetPlaydateForDisplay(int playdateId) //Will show city, state, and zip and not IDs
        {
            return playdateDao.GetPlaydateForDisplay(playdateId);
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

        [HttpGet("myplaydates/display")]
        [Authorize]
        public List<Playdate> GetLoggedInUserPlaydatesForDisplay(int userId) //Will show city name, state and zip not as IDs
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return playdateDao.GetLoggedInUserPlaydatesForDisplay(userId);
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
        public bool UpdateLoggedInUserPlaydate(Playdate updatedPlaydate, int userId, int playdateId) //Update their playdate
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return playdateDao.UpdateLoggedInUserPlaydate(updatedPlaydate, userId, playdateId);
        }

        [HttpPut("{playdateId}/request")]
        [Authorize]
        public bool RequestPlaydate(Playdate updatedPlaydate, int userId, int playdateId) //Request a playdate
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return playdateDao.RequestPlaydate(updatedPlaydate, userId, playdateId);
        }

        [HttpPut("{playdateId}/accept")]
        [Authorize]
        public bool AcceptPlaydate(Playdate updatedPlaydate, int userId, int playdateId) //Accept a playdate
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return playdateDao.AcceptPlaydate(updatedPlaydate, userId, playdateId);
        }

        [HttpPut("{playdateId}/reject")]
        [Authorize]
        public bool RejectPlaydate(Playdate updatedPlaydate, int userId, int playdateId) //Reject a playdate
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return playdateDao.RejectPlaydate(updatedPlaydate, userId, playdateId);
        }

        [HttpPut("{playdateId}/cancel")]
        [Authorize]
        public bool CancelPlaydate(Playdate updatedPlaydate, int userId, int playdateId) //Cancel a playdate (posted or requested user can do this)
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return playdateDao.CancelPlaydate(updatedPlaydate, userId, playdateId);
        }

        [HttpPut("{playdateId}/finish")]
        [Authorize]
        public bool FinishPlaydate(Playdate updatedPlaydate, int userId, int playdateId) //Cancel a playdate (posted or requested user can do this)
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return playdateDao.FinishPlaydate(updatedPlaydate, userId, playdateId);
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
