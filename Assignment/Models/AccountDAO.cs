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
        public static int Id { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string Firstname { get; set; }
        public static string Lastname { get; set; }
        public static string Email { get; set; }
        public static string Address { get; set; }

        public static bool SignIn(SignIn u)
        {
            string sql = @"SELECT * FROM [dbo].[Users] WHERE [Username] = @p1 AND [Password] = @p2";
            DataSet ds = DB.Query(sql, u.Username, u.Password);

            try
            {
                Id = (int)ds.Tables[0].Rows[0]["Id"];
                Username = ds.Tables[0].Rows[0]["Username"].ToString();
                Password = ds.Tables[0].Rows[0]["Password"].ToString();
                Firstname = ds.Tables[0].Rows[0]["Firstname"].ToString();
                Lastname = ds.Tables[0].Rows[0]["Lastname"].ToString();
                Email = ds.Tables[0].Rows[0]["Email"].ToString();
                Address = ds.Tables[0].Rows[0]["Address"].ToString();
                return true;
            }
            catch (Exception ex)
            {
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
            string sql = @"INSERT INTO [dbo].[Users] ([Username], [Password], [Email], [Firstname], [Lastname], [Address])"
                    + " VALUES (@p1,  @p2, @p3, @p4, @p5, @p6)"; 
            return DB.Action(sql, u.Username, u.Password, u.Email, u.Firstname, u.Lastname, u.Address);
        }

        public static GetUser DetailUser(int id)
        {
            string sql = @"SELECT * FROM [dbo].[Users] WHERE Id = @p1";
            DataSet ds = DB.Query(sql, id);  
            return DB.GetUserDS(ds);
        }

        public static void FindUser(object user)
        {
            string sql = @"SELECT * FROM [dbo].[Users] WHERE Username = @p1";
            DataSet ds = DB.Query(sql, user.ToString());
            try
            {
                Id = (int)ds.Tables[0].Rows[0]["Id"];
                Username = ds.Tables[0].Rows[0]["Username"].ToString();
                Password = ds.Tables[0].Rows[0]["Password"].ToString();
                Firstname = ds.Tables[0].Rows[0]["Firstname"].ToString();
                Lastname = ds.Tables[0].Rows[0]["Lastname"].ToString();
                Email = ds.Tables[0].Rows[0]["Email"].ToString();
                Address = ds.Tables[0].Rows[0]["Address"].ToString();
            }
            catch (Exception ex)
            {
            }
        }

        public static bool DeleteUser(int id)
        {
            string sql = @"DELETE [dbo].[Users] WHERE Id = @p1";
            return DB.Action(sql, id);
        }
        
        public static bool UpdateUser(UpdateUser u)
        {
            string sql = @"UPDATE [dbo].[Users] SET [Username] = @p1, [Email] = @p2, "
                + "[Firstname] = @p3, [Lastname] = @p4, [Address] = @p5 WHERE Id = @p6";
            return DB.Action(sql, u.Username, u.Email, u.Firstname, u.Lastname, u.Address, u.Id);
        }
        
        public static bool ChangePassword(ChangePassword p)
        {
            string sql = null;
            if (p.OldPassword == AccountDAO.Password)
            {
                sql =  @"UPDATE [dbo].[Users] SET [Password] = @p1 WHERE Id = @p2";
            }
            return DB.Action(sql, p.NewPassword, AccountDAO.Id);
        }

        public static List<GetUser> ListAllUsers()
        {
            string sql = @"SELECT * FROM [dbo].[Users]";
            DataSet ds = DB.Query(sql);
            return DB.GetAllUserDS(ds);
        }
    }
}