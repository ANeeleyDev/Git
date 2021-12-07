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
    public class PetController : ControllerBase
    {
        //Properties
        private readonly IPetDao petDao;

        //Constructor
        public PetController(IPetDao _petDao)
        {
            petDao = _petDao;
        }

        [HttpPost("register")]
        public Pet RegisterPet(Pet petToSave)
        {
            return petDao.RegisterPet(petToSave);
        }

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

        [HttpGet("user/{userId}")]

        public List<Pet> GetPetByUserId(int userId)
        {
            return petDao.GetPetByUserId(userId);
        }
    }


}