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



        //Constructor:
        public PetController(IPetDao _petDao)
        {
            petDao = _petDao;
        }



        //Misc methods:

        [HttpGet("recent")]
        [AllowAnonymous]
        public List<Pet> GetLast5Pets()
        {
            return petDao.GetLast5Pets();
        }


        //Anonymous user methods

        [HttpGet("{petId}")]
        [AllowAnonymous]
        public Pet GetPetByPetId(int petId) //View any pet
        {
            return petDao.GetPet(petId);
        }

        [HttpGet("{petId}/display")]
        [AllowAnonymous]
        public Pet GetPetForDisplay(int petId) //View any pet that displays species and breed
        {
            return petDao.GetPetForDisplay(petId);
        }

        [HttpGet("user/{userId}")]
        [AllowAnonymous]
        public List<Pet> GetPetsByUserId(int userId) //View all pets of a specified user
        {
            return petDao.GetPetsByUserId(userId);
        }



        //Registered user methods

        [HttpPost("mypets/register")]
        [Authorize]
        public Pet RegisterPet(Pet petToSave) //Register their pet in system
        {
            string userIdString = User.FindFirst("sub")?.Value;
            petToSave.userId = Convert.ToInt32(userIdString);

            return petDao.RegisterPet(petToSave);
        }

        [HttpGet("mypets")]
        [Authorize]
        public List<Pet> GetLoggedInUserPets(int userId) //View their pets
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return petDao.GetLoggedInUserPets(userId);
        }

        [HttpGet("mypets/display")]
        [Authorize]
        public List<Pet> GetLoggedInUserPetsForDisplay(int userId) //View their pets but with species and breed not as IDs
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return petDao.GetLoggedInUserPetsForDisplay(userId);
        }

        [HttpDelete("mypets/{petId}")]
        [Authorize]
        public bool DeleteLoggedInUserPet(int petId, int userId) //Delete their pet
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return petDao.DeleteLoggedInUserPet(petId, userId);
        }

        [HttpPut("mypets/{petId}")]
        [Authorize]
        public bool UpdateLoggedInUserPet(Pet updatedPet, int userId, int petId) //Update their pet
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return petDao.UpdateLoggedInUserPet(updatedPet, userId, petId);
        }


        //Admin methods

        [HttpDelete("admin/{petId}")]
        [Authorize(Roles = "admin")]
        public bool DeletePet(int petId) //Can delete any pet
        {
            return petDao.DeletePet(petId);
        }

        [HttpPut("admin/{petId}")]
        [Authorize(Roles = "admin")] 
        public bool UpdatePet(Pet updatedPet, int petId) //Can update any pet
        {
            return petDao.UpdatePet(updatedPet, petId);
        }

    }


}