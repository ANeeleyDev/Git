using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class PetSqlDao : IPetDao
    {
        private readonly string connectionString;

        public PetSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Pet GetPet(int petId) //returning a single pet based on the unique petId
        {
            Pet pet = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT pet_id, user_id, pet_image, pet_name, age, breed, species, playful, nervous, confident, shy, mischievous, independent, " +
                             "other_comments " +
                             "FROM pets " +
                             "WHERE pet_id = @pet_id;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@pet_id", petId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pet = CreatePetFromReader(reader);
                }

                return pet;
            }
        }

        //public List<String> DisplayPetPersonality(int petId)
        //{
        //    List<String> petPersonalities = new List<String>();

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand("SELECT pet_id, playful, nervous, confident, shy, mischievous, " +
        //                                            "independent " +
        //                                            "FROM pets " +
        //                                            "WHERE pet_id = @pet_id", conn);
        //            cmd.Parameters.AddWithValue("@pet_id", petId);

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                Pet pet = CreatePetFromReader(reader);
        //                allPetsByUserId.Add(pet);
        //            }
        //        }
        //    }
        //    catch (SqlException)
        //    {

        //        throw;
        //    }

        //    return allPetsByUserId;
        //}

        public Pet GetPetForDisplay(int petId) //Will display species and breeds as not id
        {
            Pet pet = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT pet_id, user_id, pet_image, pet_name, age, breeds.breed, species.species, playful, nervous, confident, shy, mischievous, independent, " +
                             "other_comments " +
                             "FROM pets " +
                             "JOIN breeds on pets.breed = breed_id " +
                             "JOIN species on pets.species = species_id " +
                             "WHERE pet_id = @pet_id;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@pet_id", petId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pet = CreatePetFromReader(reader);
                }

                return pet;
            }
        }

        public bool DeletePet(int petId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM pets " +
                        "WHERE pet_id = @pet_id", conn);
                    cmd.Parameters.AddWithValue("@pet_id", petId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {
                
                throw;
            }
        }

        public List<Pet> GetPetsByUserId(int userId)
        {
            List<Pet> allPetsByUserId = new List<Pet>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT pet_id, user_id, pet_name, pet_image, age, breed, species, playful, nervous, confident, shy, mischievous, " +
                                                    "independent, other_comments " +
                                                    "FROM pets " +
                                                    "WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Pet pet = CreatePetFromReader(reader);
                        allPetsByUserId.Add(pet);
                    }
                }
            }
            catch (SqlException)
            {
                
                throw;
            }

            return allPetsByUserId;
        }

        public bool UpdatePet(Pet updatedPet, int petId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE pets " +
                        "SET pet_image = @pet_image, pet_name = @pet_name, breed = @breed, species = @species, age = @age, other_comments = @other_comments, " +
                        "playful = @playful, nervous = @nervous, confident = @confident, shy = @shy, mischievous = @mischievous, independent = @independent " +
                        "WHERE pet_id = @pet_id", conn);
                    cmd.Parameters.AddWithValue("@pet_image", updatedPet.petImage);
                    cmd.Parameters.AddWithValue("@pet_name", updatedPet.petName);
                    cmd.Parameters.AddWithValue("@breed", updatedPet.breed);
                    cmd.Parameters.AddWithValue("@species", updatedPet.species);
                    cmd.Parameters.AddWithValue("@age", updatedPet.age);
                    cmd.Parameters.AddWithValue("@other_comments", updatedPet.otherComments);
                    cmd.Parameters.AddWithValue("@playful", updatedPet.playful);
                    cmd.Parameters.AddWithValue("@nervous", updatedPet.nervous);
                    cmd.Parameters.AddWithValue("@confident", updatedPet.confident);
                    cmd.Parameters.AddWithValue("@shy", updatedPet.shy);
                    cmd.Parameters.AddWithValue("@mischievous", updatedPet.mischievous);
                    cmd.Parameters.AddWithValue("@independent", updatedPet.independent);
                    cmd.Parameters.AddWithValue("@pet_id", updatedPet.petId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public List<Pet> GetLast5Pets()
        {
            List<Pet> last5Pets = new List<Pet>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT TOP 5 * " +
                        "FROM pets " +
                        "ORDER BY pet_id " +
                        "DESC;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Pet pet = CreatePetFromReader(reader);
                        last5Pets.Add(pet);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return last5Pets;
        }

        //-----------------LOGGED IN USER METHODS BELOW----------------------

        public Pet RegisterPet(Pet newPet)
        {
            int newPetId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO pets (user_id, pet_image, pet_name, breed, species, age, other_comments, playful, nervous, confident, shy, " +
                    "mischievous, independent) " +
                "OUTPUT INSERTED.pet_id " +
                "VALUES (@user_id, @pet_image, @pet_name, @breed, @species, @age, @other_comments, @playful, @nervous, @confident, @shy, @mischievous, @independent)" +
                "", conn);

                cmd.Parameters.AddWithValue("@user_id", newPet.userId);
                cmd.Parameters.AddWithValue("@pet_image", newPet.petImage);
                cmd.Parameters.AddWithValue("@pet_name", newPet.petName);
                cmd.Parameters.AddWithValue("@breed", newPet.breed);
                cmd.Parameters.AddWithValue("@species", newPet.species);
                cmd.Parameters.AddWithValue("@age", newPet.age);
                cmd.Parameters.AddWithValue("@other_comments", newPet.otherComments);
                cmd.Parameters.AddWithValue("@playful", newPet.playful);
                cmd.Parameters.AddWithValue("@nervous", newPet.nervous);
                cmd.Parameters.AddWithValue("@confident", newPet.confident);
                cmd.Parameters.AddWithValue("@shy", newPet.shy);
                cmd.Parameters.AddWithValue("@mischievous", newPet.mischievous);
                cmd.Parameters.AddWithValue("@independent", newPet.independent);

                newPetId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return GetPet(newPetId);
        }

        public List<Pet> GetLoggedInUserPets(int userId)
        {
            List<Pet> allPetsByUserId = new List<Pet>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT pet_image, pet_id, user_id, pet_name, age, breed, species, playful, nervous, confident, shy, mischievous, " +
                                                    "independent, other_comments " +
                                                    "FROM pets " +
                                                    "WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Pet pet = CreatePetFromReader(reader);
                        allPetsByUserId.Add(pet);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return allPetsByUserId;
        }

        public List<Pet> GetLoggedInUserPetsForDisplay(int userId)
        {
            List<Pet> allPetsByUserId = new List<Pet>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT pet_image, pet_id, user_id, pet_name, age, breeds.breed, species.species, playful, nervous, confident, shy, mischievous, " +
                                                    "independent, other_comments " +
                                                    "FROM pets " +
                                                    "JOIN breeds ON pets.breed = breed_id " +
                                                    "JOIN species ON pets.species = species_id " +
                                                    "WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Pet pet = CreatePetFromReader(reader);
                        allPetsByUserId.Add(pet);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return allPetsByUserId;
        }

        public bool DeleteLoggedInUserPet(int petId, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM pets " +
                        "WHERE pet_id = @pet_id " +
                        "AND user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@pet_id", petId);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool UpdateLoggedInUserPet(Pet updatedPet, int userId, int petId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE pets " +
                        "SET pet_image = @pet_image, pet_name = @pet_name, breed = @breed, species = @species, age = @age, other_comments = @other_comments, " +
                        "playful = @playful, nervous = @nervous, confident = @confident, shy = @shy, mischievous = @mischievous, independent = @independent " +
                        "WHERE user_id = @user_id AND pet_id = @pet_id", conn);
                    cmd.Parameters.AddWithValue("pet_image", updatedPet.petImage);
                    cmd.Parameters.AddWithValue("@pet_name", updatedPet.petName);
                    cmd.Parameters.AddWithValue("@breed", updatedPet.breed);
                    cmd.Parameters.AddWithValue("@species", updatedPet.species);
                    cmd.Parameters.AddWithValue("@age", updatedPet.age);
                    cmd.Parameters.AddWithValue("@other_comments", updatedPet.otherComments);
                    cmd.Parameters.AddWithValue("@playful", updatedPet.playful);
                    cmd.Parameters.AddWithValue("@nervous", updatedPet.nervous);
                    cmd.Parameters.AddWithValue("@confident", updatedPet.confident);
                    cmd.Parameters.AddWithValue("@shy", updatedPet.shy);
                    cmd.Parameters.AddWithValue("@mischievous", updatedPet.mischievous);
                    cmd.Parameters.AddWithValue("@independent", updatedPet.independent);
                    cmd.Parameters.AddWithValue("@user_id", updatedPet.userId);
                    cmd.Parameters.AddWithValue("@pet_id", updatedPet.petId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }


        private Pet CreatePetFromReader(SqlDataReader reader)
        {
            Pet pet = new Pet();
            pet.petId = Convert.ToInt32(reader["pet_id"]);
            pet.userId = Convert.ToInt32(reader["user_id"]);
            pet.petImage = Convert.ToString(reader["pet_image"]);
            pet.petName = Convert.ToString(reader["pet_name"]);
            pet.age = Convert.ToInt32(reader["age"]);
            pet.species = Convert.ToString(reader["species"]);
            pet.breed = Convert.ToString(reader["breed"]);
            pet.otherComments = Convert.ToString(reader["other_comments"]);
            pet.playful = Convert.ToBoolean(reader["playful"]);
            pet.nervous = Convert.ToBoolean(reader["nervous"]);
            pet.confident = Convert.ToBoolean(reader["confident"]);
            pet.shy = Convert.ToBoolean(reader["shy"]);
            pet.mischievous = Convert.ToBoolean(reader["mischievous"]);
            pet.independent = Convert.ToBoolean(reader["independent"]);

            return pet;
        }
    }
}








