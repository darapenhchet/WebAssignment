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
    public class DB
    {

        private static string cs = "Data Source=RAVUTHZ;Initial Catalog=AssigmentDB;Integrated Security=True;Pooling=False";
        //private static string cs = ConfigurationManager.ConnectionStrings["MyDBConnectionString1"].ConnectionString;

        public static string ConnectionString { get; set; }

        public static bool Action(string sql, params object[] fields)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;

                int i = 1;
                foreach(var field in fields)
                {
                    cmd.Parameters.AddWithValue("@p" + i, field);
                    i++;
                }

                if (cmd.ExecuteNonQuery() != 0)
                {
                    cmd.Dispose();
                    return true;
                }
                cmd.Dispose();
                return false;
            }
        }

        public static DataSet Query(string sql, params object[] fields)
        {
            DataSet dataset = new DataSet();
            using (SqlConnection con = new SqlConnection(cs))
            {
                
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = con;

                int i = 1;
                foreach (var field in fields)
                {
                    cmd.Parameters.AddWithValue("@p" + i, field);
                    i++;
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                return dataset;
            }
        }

        public static GetUser GetUserDS(DataSet ds) 
        {
            GetUser user = null;
            if (ds.Tables[0].Rows[0] != null)
            {
                user = new GetUser();
                user.Id = (int)ds.Tables[0].Rows[0]["Id"];
                user.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                user.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                user.Firstname = ds.Tables[0].Rows[0]["Firstname"].ToString();
                user.Lastname = ds.Tables[0].Rows[0]["Lastname"].ToString();
                user.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                user.Address = ds.Tables[0].Rows[0]["Address"].ToString();
            }
            return user;
        }

        public static List<GetUser> GetUsersDS(DataSet ds)
        {
            GetUser user = null;
            List<GetUser> list = new List<GetUser>();
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                user = new GetUser();
                user.Id = (int)ds.Tables[0].Rows[0]["Id"];
                user.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                user.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                user.Firstname = ds.Tables[0].Rows[0]["Firstname"].ToString();
                user.Lastname = ds.Tables[0].Rows[0]["Lastname"].ToString();
                user.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                user.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                list.Add(user);
            }
            return list;
        }


    }
}