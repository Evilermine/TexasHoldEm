using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TexasHoldEm.Models
{
    // Creating Access Layer to the table users
    public class UserDataAccessLayer {

        string ConnectionConfig = "host=localhost;driver=pdo;dbname=pokermanager;username=admin; password=Banane123;";

        // Fetch all users on the database and returns the result
        public List<User> FetchAll() {
            try {
                List<User> users = new List<User>();

                // Creating connection to database
                using (SqlConnection con = new SqlConnection(ConnectionConfig))
                {
                    SqlCommand cmd = new SqlCommand("spFetchAll", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Reading all users on the database and store in List<User>
                    while (reader.Read())
                    {
                        User user = new User();

                        user.Username = reader["username"].ToString();
                        user.Wallet = Convert.ToInt32(reader["wallet"]);

                        users.Add(user);
                    }
                    con.Close();
                }
                return users;
            }
            catch {
                throw;
            }
        }

        // Insert new users in the database
        public int InsertUser(User user) {
            try {

                //Creating connection
                using (SqlConnection con = new SqlConnection(ConnectionConfig)) {
                    SqlCommand cmd = new SqlCommand("spAddUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@wallet", user.Wallet);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch{
                throw;
            }
        }
    }
}
