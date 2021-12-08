using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IPetDao
    {
        public Pet GetPet(int petId);

        public bool DeletePet(int petId);

        public List<Pet> GetPetsByUserId(int userId);

        public bool UpdatePet(Pet updatedPet);

        public List<Pet> GetLast5Pets();





        public Pet RegisterPet(Pet newPet);

        public List<Pet> GetLoggedInUserPets(int userId);

        public bool DeleteLoggedInUserPet(int petId, int userId);

        public bool UpdateLoggedInUserPet(Pet updatedPet, int userId);


    }
}
