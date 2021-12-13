using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class PlaydateSqlDao : IPlaydateDao
    {
        private readonly string connectionString;

        public PlaydateSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public Playdate GetPlaydate(int playdateId)
        {
            Playdate playdate = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT playdate_id, playdate_posted_user_id, playdate_posted_pet_id, playdate_requested_user_id, " +
                    "playdate_requested_pet_id, meeting_time, playdate_address, playdate_city, " +
                    "playdate_state, playdate_zip, playdate_status_id " +
                    "FROM playdates " +
                    "WHERE playdate_id = @playdate_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@playdate_id", playdateId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    playdate = CreatePlaydateFromReader(reader);
                }

                return playdate;
            }
        }

        public Playdate GetPlaydateForDisplay(int playdateId)
        {
            Playdate playdate = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT playdate_id, playdate_posted_user_id, playdate_posted_pet_id, playdate_requested_user_id, " +
                    "playdate_requested_pet_id, meeting_time, playdate_address, cities.city_name, states.state_abbreviation, " +
                    "zips.zipcode, playdate_status_id " +
                    "FROM playdates " +
                    "JOIN cities ON playdates.playdate_city = city_id " +
                    "JOIN states ON playdates.playdate_state = states.state_id " +
                    "JOIN zips ON playdates.playdate_zip = zip_id " +
                    "WHERE playdate_id = @playdate_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@playdate_id", playdateId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    playdate = CreatePlaydateFromReaderForDisplay(reader);
                }

                return playdate;
            }
        }

        public bool DeletePlaydate(int playdateId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM playdates WHERE playdate_id = @playdate_id", conn);
                    cmd.Parameters.AddWithValue("@playdate_id", playdateId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public List<Playdate> GetPlaydatesByUserId(int userId)
        {
            List<Playdate> allPlaydatesByUserId = new List<Playdate>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT playdate_id, playdate_posted_user_id, playdate_posted_pet_id, playdate_requested_user_id, meeting_time, playdate_address, " +
                        "playdate_city, playdate_state, playdate_zip, playdate_status_id " +
                        "FROM playdates " +
                        "WHERE playdate_posted_user_id = @playdate_posted_user_id", conn);
                    cmd.Parameters.AddWithValue("@playdate_posted_user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Playdate playdate = CreatePlaydateFromReader(reader);
                        allPlaydatesByUserId.Add(playdate);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return allPlaydatesByUserId;
        }

        //-----------------LOGGED IN USER METHODS BELOW----------------------

        public Playdate RegisterPlaydate(Playdate newPlaydate)
        {
            int newPlaydateId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO playdates (playdate_posted_user_id, playdate_posted_pet_id, playdate_requested_user_id, meeting_time, playdate_address, " +
                    "playdate_city, playdate_state, playdate_zip, playdate_status_id) " +
                "OUTPUT INSERTED.playdate_id " +
                "VALUES (@playdate_posted_user_id, @playdate_posted_pet_id, null, @meeting_time, @playdate_address, @playdate_city, @playdate_state, @playdate_zip, 0)" +
                "", conn);

                cmd.Parameters.AddWithValue("@playdate_posted_user_id", newPlaydate.playdatePostedUserId);
                cmd.Parameters.AddWithValue("@playdate_posted_pet_id", newPlaydate.playdatePostedPetId);
                cmd.Parameters.AddWithValue("@meeting_time", newPlaydate.meetingTime);
                cmd.Parameters.AddWithValue("@playdate_address", newPlaydate.playdateAddress);
                cmd.Parameters.AddWithValue("@playdate_city", newPlaydate.playdateCity);
                cmd.Parameters.AddWithValue("@playdate_state", newPlaydate.playdateState);
                cmd.Parameters.AddWithValue("@playdate_zip", newPlaydate.playdateZip);

                newPlaydateId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return GetPlaydate(newPlaydateId);
        }

        public List<Playdate> GetLoggedInUserPlaydates(int userId)
        {
            List<Playdate> allPlaydatesByUserId = new List<Playdate>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT playdate_id, playdate_posted_user_id, playdate_posted_pet_id, " +
                        "playdate_requested_user_id, playdate_requested_pet_id, meeting_time, playdate_address, " +
                        "playdate_city, playdate_state, playdate_zip, playdate_status_id " +
                        "FROM playdates " +
                        "WHERE playdate_posted_user_id = @playdate_posted_user_id", conn);
                    cmd.Parameters.AddWithValue("@playdate_posted_user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Playdate playdate = CreatePlaydateFromReader(reader);
                        allPlaydatesByUserId.Add(playdate);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return allPlaydatesByUserId;
        }

        public List<Playdate> GetLoggedInUserPlaydatesForDisplay(int userId)
        {
            List<Playdate> allPlaydatesByUserId = new List<Playdate>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT playdate_id, playdate_posted_user_id, playdate_posted_pet_id, " +
                        "playdate_requested_user_id, playdate_requested_pet_id, meeting_time, playdate_address, " +
                        "cities.city_name, states.state_abbreviation, zips.zipcode, playdate_status_id " +
                        "FROM playdates " +
                        "JOIN cities ON playdates.playdate_city = city_id " +
                        "JOIN states ON playdates.playdate_state = states.state_id " +
                        "JOIN zips ON playdates.playdate_zip = zip_id " +
                        "WHERE playdate_posted_user_id = @playdate_posted_user_id", conn);
                    cmd.Parameters.AddWithValue("@playdate_posted_user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Playdate playdate = CreatePlaydateFromReaderForDisplay(reader);
                        allPlaydatesByUserId.Add(playdate);
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return allPlaydatesByUserId;
        }

        public bool DeleteLoggedInUserPlaydate(int playdateId, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM playdates " +
                        "WHERE playdate_id = @playdate_id " +
                        "AND playdate_posted_user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@playdate_id", playdateId);
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

        public bool UpdateLoggedInUserPlaydate(Playdate updatedPlaydate, int userId, int playdateId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE playdates " +
                        "SET playdate_posted_pet_id = @playdate_posted_pet_id, meeting_time = @meeting_time, playdate_address = @playdate_address, playdate_city = @playdate_city, playdate_state = @playdate_state, " +
                        "playdate_zip = @playdate_zip, playdate_status_id = @playdate_status_id " +
                        "WHERE playdate_posted_user_id = @user_id AND playdate_id = @playdate_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", updatedPlaydate.playdatePostedUserId);
                    cmd.Parameters.AddWithValue("@playdate_posted_pet_id", updatedPlaydate.playdatePostedPetId);
                    cmd.Parameters.AddWithValue("@meeting_time", updatedPlaydate.meetingTime);
                    cmd.Parameters.AddWithValue("@playdate_address", updatedPlaydate.playdateAddress);
                    cmd.Parameters.AddWithValue("@playdate_city", updatedPlaydate.playdateCity);
                    cmd.Parameters.AddWithValue("@playdate_state", updatedPlaydate.playdateState);
                    cmd.Parameters.AddWithValue("@playdate_zip", updatedPlaydate.playdateZip);
                    cmd.Parameters.AddWithValue("@playdate_status_id", updatedPlaydate.playdateStatusId);
                    cmd.Parameters.AddWithValue("@playdate_id", updatedPlaydate.playdateId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool RequestPlaydate(Playdate updatedPlaydate, int userId, int playdateId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE playdates " +
                        "SET playdate_requested_user_id = @playdate_requested_user_id, playdate_requested_pet_id = @playdate_requested_pet_id, playdate_status_id = 1 " +
                        "WHERE playdate_id = @playdate_id;", conn);
                    cmd.Parameters.AddWithValue("@playdate_requested_pet_id", updatedPlaydate.playdateRequestedPetId);
                    cmd.Parameters.AddWithValue("@playdate_requested_user_id", userId);
                    cmd.Parameters.AddWithValue("@playdate_status_id", updatedPlaydate.playdateStatusId);
                    cmd.Parameters.AddWithValue("@playdate_id", playdateId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool AcceptPlaydate(Playdate updatedPlaydate, int userId, int playdateId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE playdates " +
                        "SET playdate_status_id = 2 " +
                        "WHERE playdate_id = @playdate_id AND playdate_posted_user_id = @playdate_posted_user_id AND playdate_status_id = 1;", conn);
                    cmd.Parameters.AddWithValue("@playdate_posted_user_id", userId);
                    cmd.Parameters.AddWithValue("@playdate_status_id", updatedPlaydate.playdateStatusId);
                    cmd.Parameters.AddWithValue("@playdate_id", playdateId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool RejectPlaydate(Playdate updatedPlaydate, int userId, int playdateId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE playdates " +
                        "SET playdate_status_id = 3 " +
                        "WHERE playdate_id = @playdate_id AND playdate_posted_user_id = @playdate_posted_user_id AND playdate_status_id = 1;", conn);
                    cmd.Parameters.AddWithValue("@playdate_posted_user_id", userId);
                    cmd.Parameters.AddWithValue("@playdate_status_id", updatedPlaydate.playdateStatusId);
                    cmd.Parameters.AddWithValue("@playdate_id", playdateId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool CancelPlaydate(Playdate updatedPlaydate, int userId, int playdateId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE playdates " +
                        "SET playdate_status_id = 4 " +
                        "WHERE playdate_id = @playdate_id AND (playdate_posted_user_id = @playdate_posted_user_id OR playdate_requested_user_id = @playdate_requested_user_id) AND playdate_status_id = 2;", conn);
                    cmd.Parameters.AddWithValue("@playdate_posted_user_id", userId);
                    cmd.Parameters.AddWithValue("@playdate_requested_user_id", userId);
                    cmd.Parameters.AddWithValue("@playdate_status_id", updatedPlaydate.playdateStatusId);
                    cmd.Parameters.AddWithValue("@playdate_id", playdateId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool FinishPlaydate(Playdate updatedPlaydate, int userId, int playdateId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE playdates " +
                        "SET playdate_status_id = 5 " +
                        "WHERE playdate_id = @playdate_id AND (playdate_posted_user_id = @playdate_posted_user_id OR playdate_requested_user_id = @playdate_requested_user_id) AND playdate_status_id = 2;", conn);
                    cmd.Parameters.AddWithValue("@playdate_posted_user_id", userId);
                    cmd.Parameters.AddWithValue("@playdate_requested_user_id", userId);
                    cmd.Parameters.AddWithValue("@playdate_status_id", updatedPlaydate.playdateStatusId);
                    cmd.Parameters.AddWithValue("@playdate_id", playdateId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public bool UpdatePlaydate(Playdate updatedPlaydate, int playdateId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE playdates " +
                        "SET playdate_posted_pet_id = @playdate_posted_pet_id, meeting_time = @meeting_time, playdate_address = @playdate_address, playdate_city = @playdate_city, playdate_state = @playdate_state, " +
                        "playdate_zip = @playdate_zip, playdate_status_id = @playdate_status_id " +
                        "WHERE playdate_id = @playdate_id", conn);
                    cmd.Parameters.AddWithValue("@playdate_posted_pet_id", updatedPlaydate.playdatePostedPetId);
                    cmd.Parameters.AddWithValue("@meeting_time", updatedPlaydate.meetingTime);
                    cmd.Parameters.AddWithValue("@playdate_address", updatedPlaydate.playdateAddress);
                    cmd.Parameters.AddWithValue("@playdate_city", updatedPlaydate.playdateCity);
                    cmd.Parameters.AddWithValue("@playdate_state", updatedPlaydate.playdateState);
                    cmd.Parameters.AddWithValue("@playdate_zip", updatedPlaydate.playdateZip);
                    cmd.Parameters.AddWithValue("@playdate_status_id", updatedPlaydate.playdateStatusId);
                    cmd.Parameters.AddWithValue("@playdate_id", updatedPlaydate.playdateId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        private Playdate CreatePlaydateFromReader(SqlDataReader reader)
        {
            Playdate playdate = new Playdate();
            playdate.playdateId = Convert.ToInt32(reader["playdate_id"]);
            playdate.playdatePostedUserId = Convert.ToInt32(reader["playdate_posted_user_id"]);
            playdate.playdatePostedPetId = Convert.ToInt32(reader["playdate_posted_pet_id"]);
            playdate.playdateRequestedUserId = Convert.ToInt32(reader["playdate_requested_user_id"]);
            playdate.playdateRequestedPetId = Convert.ToInt32(reader["playdate_requested_pet_id"]);
            playdate.meetingTime = Convert.ToDateTime(reader["meeting_time"]);
            playdate.playdateAddress = Convert.ToString(reader["playdate_address"]);
            playdate.playdateCity = Convert.ToString(reader["playdate_city"]);
            playdate.playdateState = Convert.ToString(reader["playdate_state"]);
            playdate.playdateZip = Convert.ToString(reader["playdate_zip"]);
            playdate.playdateStatusId = Convert.ToInt32(reader["playdate_status_id"]);

            return playdate;
        }

        private Playdate CreatePlaydateFromReaderForDisplay(SqlDataReader reader)
        {
            Playdate playdate = new Playdate();
            playdate.playdateId = Convert.ToInt32(reader["playdate_id"]);
            playdate.playdatePostedUserId = Convert.ToInt32(reader["playdate_posted_user_id"]);
            playdate.playdatePostedPetId = Convert.ToInt32(reader["playdate_posted_pet_id"]);
            playdate.playdateRequestedUserId = Convert.ToInt32(reader["playdate_requested_user_id"]);
            playdate.playdateRequestedPetId = Convert.ToInt32(reader["playdate_requested_pet_id"]);
            playdate.meetingTime = Convert.ToDateTime(reader["meeting_time"]);
            playdate.playdateAddress = Convert.ToString(reader["playdate_address"]);
            playdate.playdateCity = Convert.ToString(reader["city_name"]);
            playdate.playdateState = Convert.ToString(reader["state_abbreviation"]);
            playdate.playdateZip = Convert.ToString(reader["zipcode"]);
            playdate.playdateStatusId = Convert.ToInt32(reader["playdate_status_id"]);

            return playdate;
        }
    }
}
