using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Assignment.Models
{
    public class Account
    {
        private static int Id;
        private static string Username, Password, Firstname, Lastname, Email, Address;

        private static string cs = "Data Source=RAVUTHZ;Initial Catalog=AssigmentDB;Integrated Security=True;Pooling=False";
        //private static string cs = ConfigurationManager.ConnectionStrings["MyDBConnectionString1"].ConnectionString;

        public static void Init(string con, string tbl, string usr, string pw)
        {

        }

        public static bool SignIn(CheckUser u)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"SELECT * FROM [dbo].[Users] WHERE [Username] = @u AND [Password] = @p";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("@u", SqlDbType.NVarChar)).Value = u.Username;
                cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.NVarChar)).Value = u.Password;

                con.Open();
                SqlDataReader dat = cmd.ExecuteReader();
                if (dat.Read())
                {
                    Id = (int)dat["Id"];
                    Username = dat["Username"].ToString();
                    Password = dat["Password"].ToString();
                    Firstname = dat["Firstname"].ToString();
                    Lastname = dat["Lastname"].ToString();
                    Email = dat["Email"].ToString();
                    Address = dat["Address"].ToString();

                    cmd.Dispose();
                    dat.Dispose();
                    return true;
                }
                cmd.Dispose();
                dat.Dispose();
                return false;
            }
        }

        public static void SignOut() {
            Id = 0;
            Username = null;
            Password = null;
            Firstname = null;
            Lastname = null;
            Email = null;
            Address = null;
        }

        public static bool CreateUser(InsertUser u)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"INSERT INTO [dbo].[Users] ([Username], [Password], [Email], [Firstname], [Lastname], [Address])"
                    + " VALUES (@user,  @pass, @email, @first, @last, @address)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar)).Value = u.Username;
                cmd.Parameters.Add(new SqlParameter("@pass", SqlDbType.NVarChar)).Value = u.Password;
                cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar)).Value = u.Email;
                cmd.Parameters.Add(new SqlParameter("@first", SqlDbType.NVarChar)).Value = u.Firstname;
                cmd.Parameters.Add(new SqlParameter("@last", SqlDbType.NVarChar)).Value = u.Lastname;
                cmd.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar)).Value = u.Address;

                con.Open();
                if (cmd.ExecuteNonQuery() != 0)
                {
                    cmd.Dispose();
                    return true;
                }
                cmd.Dispose();
                return false;
            }
        }

        public static User DetailUser(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"SELECT * FROM [dbo].[Users] WHERE Id = @id";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                User user = new User();
                if (reader.Read())
                {
                    user.Username = reader["Username"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.Address = reader["Address"].ToString();
                    user.Firstname = reader["Firstname"].ToString();
                    user.Lastname = reader["Lastname"].ToString();
                    user.Id = (int)reader["Id"];
                }
                return user;
            }
        }
        
        public static bool DeleteUser(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"DELETE [dbo].[Users] WHERE Id =@id";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
                
                con.Open();
                if (cmd.ExecuteNonQuery() != 0)
                {
                    cmd.Dispose();
                    return true;
                }
                cmd.Dispose();
                return false;   
            }
        }
        
        public static bool UpdateUser(UpdateUser u)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"UPDATE [dbo].[Users] SET [Username] = @user, [Email] = @email, "
                    + "[Firstname] = @first, [Lastname] = @last, [Address] = @address WHERE Id = @id";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar)).Value = u.Username;
                cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar)).Value = u.Email;
                cmd.Parameters.Add(new SqlParameter("@first", SqlDbType.NVarChar)).Value = u.Firstname;
                cmd.Parameters.Add(new SqlParameter("@last", SqlDbType.NVarChar)).Value = u.Lastname;
                cmd.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar)).Value = u.Address;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = u.Id;

                con.Open();
                if (cmd.ExecuteNonQuery() != 0)
                {
                    cmd.Dispose();
                    return true;
                }
                    cmd.Dispose();
                    return false;
            }
        }
        
        public static bool ChangePassword(ChangePassword p)
        {
            if (p.OldPassword == Account.Password)
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string sql = @"UPDATE [dbo].[Users] SET [Password] = @pw WHERE Id = @id";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@pw", SqlDbType.NVarChar)).Value = p.NewPassword;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = Account.Id;

                    con.Open();
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        cmd.Dispose();
                        return true;
                    }
                    cmd.Dispose();
                    return false;
                }
            }
            return false;
        }

        public static List<User> ListAllUsers()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                List<User> list = new List<User>();
                string sql = @"SELECT * FROM [dbo].[Users]";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User();
                    user.Username = reader["Username"].ToString();
                    user.Address = reader["Address"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.Firstname = reader["Firstname"].ToString();
                    user.Lastname = reader["Lastname"].ToString();
                    user.Id = (int)reader["Id"];
                    list.Add(user);
                }
                return list;
            }
        }

        public static int getId() 
        {
            return Id;
        }

        public static string getUsername()
        {
            return Username;
        }

    }
}