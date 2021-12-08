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

        public Pet GetPet(int petId) //returning a single pet based on the unique pet 
        {
            Pet pet = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT pet_id, user_id, pet_name, age, breed, species, playful, nervous, confident, shy, mischievous, independent, " +
                             "other_comments FROM pets WHERE pet_id = @pet_id;";
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

        public Pet RegisterPet(Pet newPet)
        {
            int newPetId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO pets (user_id, pet_name, breed, species, age, other_comments, playful, nervous, confident, shy, mischievous, independent) " +
                "OUTPUT INSERTED.pet_id " +
                "VALUES (@user_id, @pet_name, @breed, @species, @age, @other_comments, @playful, @nervous, @confident, @shy, @mischievous, @independent);" +
                "", conn);

                cmd.Parameters.AddWithValue("@user_id", newPet.userId);
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

        public bool DeletePet(int petId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM pets WHERE pet_id = @pet_id", conn);
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

        public List<Pet> GetPetByUserId(int userId)
        {
            List<Pet> allPetsByUserId = new List<Pet>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT pet_id, user_id, pet_name, age, breed, species, playful, nervous, confident, shy, mischievous, " +
                                                    "independent, other_comments FROM pets WHERE user_id = @user_id", conn);
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

        private Pet CreatePetFromReader(SqlDataReader reader)
        {
            Pet pet = new Pet();
            pet.petId = Convert.ToInt32(reader["pet_id"]);
            pet.userId = Convert.ToInt32(reader["user_id"]);
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








