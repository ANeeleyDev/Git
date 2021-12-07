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

        //TODO
        //Make a "GetAllPetsBySpecies"
    }
}
