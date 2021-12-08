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
    [Route("/pets/")]
    [ApiController]
    [Authorize]
    public class PetController : ControllerBase
    {
        //Properties:
        private readonly IPetDao petDao;

        //---------------------------------------------------------------------------------------------------
        //Constructor:
        public PetController(IPetDao _petDao)
        {
            petDao = _petDao;
        }

        //---------------------------------------------------------------------------------------------------
        //Methods:

        [HttpGet("{petId}")]
        public Pet GetPetByPetId(int petId)
        {
            return petDao.GetPet(petId);
        }

        
        [HttpDelete("{petId}")]

        public bool DeletePet(int petId)
        {
            return petDao.DeletePet(petId);
        }

        //View any user's pets

        [HttpGet("user/{userId}")]

        public List<Pet> GetPetsByUserId(int userId)
        {
            return petDao.GetPetsByUserId(userId);
        }

        [HttpPut("{petId}")]
        public bool UpdatePet(Pet updatedPet)
        {
            return petDao.UpdatePet(updatedPet);
        }

        [HttpGet("recent")]
        [AllowAnonymous]
        public List<Pet> GetLast5Pets()
        {
            return petDao.GetLast5Pets();
        }



        //-----------------LOGGED IN USER METHODS BELOW----------------------

        //Logged in users can register pets to DB
        [HttpPost("register")]
        [Authorize]
        public Pet RegisterPet(Pet petToSave)
        {
            string userIdString = User.FindFirst("sub")?.Value;
            petToSave.userId = Convert.ToInt32(userIdString);

            return petDao.RegisterPet(petToSave);
        }

        //Logged in user: View THEIR pets (user id taken from token)
        [HttpGet("mypets")]
        [Authorize]
        public List<Pet> GetLoggedInUserPets(int userId)
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return petDao.GetLoggedInUserPets(userId);
        }

        //Logged in user: Delete one of their pets (user id taken from token)
        [HttpDelete("mypets/{petId}")]
        [Authorize]
        public bool DeleteLoggedInUserPet(int petId, int userId)
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return petDao.DeleteLoggedInUserPet(petId, userId);
        }

        [HttpPut("mypets/{petId}")]
        [Authorize]
        public bool UpdateLoggedInUserPet(Pet updatedPet, int userId)
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return petDao.UpdateLoggedInUserPet(updatedPet, userId);
        }

    }


}