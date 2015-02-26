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
    public class AccountDAO
    {
        private static int Id;
        private static string Username, Password, Firstname, Lastname, Email, Address;

        private static string cs = "Data Source=RAVUTHZ;Initial Catalog=AssigmentDB;Integrated Security=True;Pooling=False";
        //private static string cs = ConfigurationManager.ConnectionStrings["MyDBConnectionString1"].ConnectionString;

        public static bool SignIn(SignIn u)
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
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO [dbo].[Users] ([Username], [Password], [Email], [Firstname], [Lastname], [Address])"
                    + " VALUES (@user,  @pass, @email, @first, @last, @address)";
            cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar)).Value = u.Username;
            cmd.Parameters.Add(new SqlParameter("@pass", SqlDbType.NVarChar)).Value = u.Password;
            cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar)).Value = u.Email;
            cmd.Parameters.Add(new SqlParameter("@first", SqlDbType.NVarChar)).Value = u.Firstname;
            cmd.Parameters.Add(new SqlParameter("@last", SqlDbType.NVarChar)).Value = u.Lastname;
            cmd.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar)).Value = u.Address;
            return DB.Action(cmd);
        }

        public static GetUser DetailUser(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"SELECT * FROM [dbo].[Users] WHERE Id = @id";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                GetUser user = new GetUser();
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

        public static GetUser FindUser(object user)
        {
            string sql = null;
            SqlCommand cmd = null;

            using (SqlConnection con = new SqlConnection(cs))
            {
                if (user is int)
                {
                    sql = @"SELECT * FROM [dbo].[Users] WHERE Id = @p";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.Int)).Value = (int)user;
                }
                else
                {
                    sql = @"SELECT * FROM [dbo].[Users] WHERE Username = @p";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.NVarChar)).Value = user.ToString();
                }
                

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                GetUser us = null;
                if (reader.Read())
                {
                    Id = (int)reader["Id"];
                    Username = reader["Username"].ToString();
                    Password = reader["Password"].ToString();
                    Firstname = reader["Firstname"].ToString();
                    Lastname = reader["Lastname"].ToString();
                    Email = reader["Email"].ToString();
                    Address = reader["Address"].ToString();
                }
                return us;
            }
        }

        public static bool DeleteUser(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"DELETE [dbo].[Users] WHERE Id = @id";
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
            return DB.Action(cmd);
        }
        
        public static bool UpdateUser(UpdateUser u)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"UPDATE [dbo].[Users] SET [Username] = @user, [Email] = @email, "
                + "[Firstname] = @first, [Lastname] = @last, [Address] = @address WHERE Id = @id";
            cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar)).Value = u.Username;
            cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar)).Value = u.Email;
            cmd.Parameters.Add(new SqlParameter("@first", SqlDbType.NVarChar)).Value = u.Firstname;
            cmd.Parameters.Add(new SqlParameter("@last", SqlDbType.NVarChar)).Value = u.Lastname;
            cmd.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar)).Value = u.Address;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = u.Id;
            return DB.Action(cmd);
        }
        
        public static bool ChangePassword(ChangePassword p)
        {
            SqlCommand cmd = new SqlCommand();
            if (p.OldPassword == AccountDAO.Password)
            {
                cmd.CommandText = @"UPDATE [dbo].[Users] SET [Password] = @pw WHERE Id = @id";
                cmd.Parameters.Add(new SqlParameter("@pw", SqlDbType.NVarChar)).Value = p.NewPassword;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = AccountDAO.Id;
            }
            return DB.Action(cmd);
        }

        public static List<GetUser> ListAllUsers()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                List<GetUser> list = new List<GetUser>();
                string sql = @"SELECT * FROM [dbo].[Users]";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    GetUser user = new GetUser();
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