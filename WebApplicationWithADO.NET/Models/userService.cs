using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace WebApplicationWithADO.NET.Models
{
    public class userService : IUserService
    {
        public string connectionString { get; set; }
        public IConfiguration _configuration { get; set; }

        public SqlConnection con;
        public userService(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public List<user> GetUsers()
        {
            List<user> userList = new List<user>();
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("sp_GetUsers", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        user users = new user();
                        users.Userid = Convert.ToInt32(rdr["Userid"]);
                        users.Name = rdr["Name"].ToString();
                        users.Contact = rdr["Contact"].ToString();
                        users.Address = rdr["Address"].ToString();
                        users.isActive = Convert.ToBoolean(rdr["isActive"]);
                        userList.Add(users);
                    }

                }

                return userList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public user GetUserbyid(int id)
        {
            user user1= new user();
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("sp_GetUsersbyId", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uID", id);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        user1.Userid = Convert.ToInt32(rdr["Userid"]);
                        user1.Name = rdr["Name"].ToString();
                        user1.Contact = rdr["Contact"].ToString();
                        user1.Address = rdr["Address"].ToString();
                        user1.isActive = Convert.ToBoolean(rdr["isActive"]);

                    }

                }

                return user1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int updateUser(user u)
        {
            int result = 0;
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("sp_updateUser", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uID", u.Userid);
                    cmd.Parameters.AddWithValue("@Name", u.Name);
                    cmd.Parameters.AddWithValue("@Address", u.Address);
                    cmd.Parameters.AddWithValue("@contact", u.Contact);
                    result= cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int addUser(user u)
        {
            int result = 0;
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("sp_CreateUser", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uID", u.Userid);
                    cmd.Parameters.AddWithValue("@Name", u.Name);
                    cmd.Parameters.AddWithValue("@Address", u.Address);
                    cmd.Parameters.AddWithValue("@contact", u.Contact);
                    result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int DeleteUser(int id)
        {
            int result = 0;
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("sp_delUser", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uID", id);
                    result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteUsers(string ids)
        {
            int result = 0;
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("deleteusers", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UsersIds", ids);
                    result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ActivateUser(int[] ids)
        {
            int result = 0;
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("ActivateUsers ", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var dt=new DataTable();
                    dt.Columns.Add("id",typeof(int));
                    foreach (int id in ids)
                    {
                            dt.Rows.Add(id);
                    }
                    var parameter= cmd.Parameters.AddWithValue("@userlist", dt);
                    parameter.SqlDbType = SqlDbType.Structured;
                    result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ActivateUser1(List<Tuple<int, bool>> user)
        {
            int result = 0;
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("ActivateUsers1", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var dt = new DataTable();
                    dt.Columns.Add("id", typeof(int));
                    dt.Columns.Add("isactive", typeof(bool));
                    foreach (var row in user)
                    {
                            dt.Rows.Add(row.Item1,row.Item2);
                        
                    }
                    var parameter = cmd.Parameters.AddWithValue("@userlist", dt);
                    parameter.SqlDbType = SqlDbType.Structured;
                    result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ActivateUserwithJason(List<user> user)
        {
            int result = 0;
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("ActivateUsersUsingJson", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //var dt = new DataTable();
                    //dt.Columns.Add("id", typeof(int));
                    //dt.Columns.Add("isactive", typeof(bool));
                    //foreach (var row in user)
                    //{
                    //    dt.Rows.Add(row.Item1, row.Item2);

                    //}
                    string json = JsonConvert.SerializeObject(user);
                    var parameter = cmd.Parameters.AddWithValue("@uJson", json);
                    parameter.SqlDbType = SqlDbType.NVarChar;
                    result = cmd.ExecuteNonQuery();
                    return json;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public interface IUserService
    {
        public List<user> GetUsers();
        public user GetUserbyid(int id);
        public int updateUser(user u);
        public int addUser(user u);
        public int DeleteUsers(string ids);
        public string ActivateUserwithJason(List<user> user);

    }

}
