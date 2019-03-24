using AdoNet.SMN_182.korean_shop.Entities;
using AdoNet.SMN_182.Unit_1_3;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.SMN_182.korean_shop.Repositories
{
    public class UserRepository
    {
        private string GetTableName()
        {
            return $"[dbo].[Users]";
        }
        public UserEntity Read(int id)
        {
            UserEntity user = new UserEntity();
            string sql = $"SELECT * FROM {GetTableName()} WHERE ID = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user.UserId = Int32.Parse(reader["UserId"].ToString());
                            user.Login = reader["[Login]"].ToString();
                            user.Password = reader["[Password]"].ToString();
                            user.UserName = reader["UserName"].ToString();
                            user.Email = reader["Email"].ToString();
                        }
                    }
                    else throw new Exception("No data found!");
                }
            }
            return user;
        }
        private string GetConnectionString()
        {
            ConnectionStringInAppConfigDemo appConfig =
                new ConnectionStringInAppConfigDemo();
            return appConfig.GetConnectionString();
        }
    }
}
