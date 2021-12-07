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
        public Pet RegisterPet(Pet newPet);

        public bool DeletePet(int petId);

        public List<Pet> GetPetByUserId(int userId);

        //TODO
        //Make a "GetAllPetsBySpecies"
        //Make top 5 most recent pets added (5 largest pet IDs)
        //Make display pet methods based on drop down options



    }
}
