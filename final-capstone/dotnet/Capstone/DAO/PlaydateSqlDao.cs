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
        public Playdate getPlaydate(int playdateId)
        {
            Playdate playdate = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT playdate_id, playdate_posted_user_id, playdate_requested_user_id, meeting_time, playdate_address, playdate_city, " +
                    "playdate_state, playdate_zip, playdate_status_id FROM playdates WHERE playdate_id = @playdate_id";
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

        private Playdate CreatePlaydateFromReader(SqlDataReader reader)
        {
            Playdate playdate = new Playdate();
            playdate.playdateId = Convert.ToInt32(reader["playdate_id"]);
            playdate.playdatePostedUserId = Convert.ToInt32(reader["playdate_posted_user_id"]);
            playdate.playdateRequestedUserId = Convert.ToInt32(reader["playdate_requested_user_id"]);
            playdate.meetingTime = Convert.ToDateTime(reader["meeting_time"]);
            playdate.playdateAddress = Convert.ToString(reader["playdate_address"]);
            playdate.playdateCity = Convert.ToString(reader["playdate_city"]);
            playdate.playdateState = Convert.ToString(reader["playdate_state"]);
            playdate.playdateZip = Convert.ToString(reader["playdate_zip"]);
            playdate.playdateStatusId = Convert.ToInt32(reader["playdate_status_id"]);

            return playdate;
        }
    }
}
