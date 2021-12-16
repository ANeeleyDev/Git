using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IPetDao
    {
        //Misc methods
        public List<Pet> GetLast5Pets();

        public List<Breed> GetAllBreeds();

        //public List<String> DisplayPetPersonality(int petId);



        //Anonymous user methods
        public Pet GetPet(int petId); //Anonymous users can view any pet

        public List<Pet> GetPetsByUserId(int userId); //Anonymous users can view any pet of a specified user
        public Pet GetPetForDisplay(int petId); //Will display species and breeds as not id



        //Registered user methods
        public Pet RegisterPet(Pet newPet); //Registered users can register their pet in system

        public List<Pet> GetLoggedInUserPets(int userId); //Registered users can view their pets
        public List<Pet> GetLoggedInUserPetsForDisplay(int userId); //Will display species and breeds as not id

        public bool DeleteLoggedInUserPet(int petId, int userId); //Registered users can delete any of their pets

        public bool UpdateLoggedInUserPet(Pet updatedPet, int userId, int petId); //Registered users can update any of their pets



        //Admin methods
        public bool DeletePet(int petId); //Admins can delete any pet
        public bool UpdatePet(Pet updatedPet, int petId); //Admins can update any pet
    }
}
