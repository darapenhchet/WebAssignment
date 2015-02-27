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
            try {
                GetUser user = new GetUser();
                user.Id = (int)ds.Tables[0].Rows[0]["Id"];
                user.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                user.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                user.Firstname = ds.Tables[0].Rows[0]["Firstname"].ToString();
                user.Lastname = ds.Tables[0].Rows[0]["Lastname"].ToString();
                user.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                user.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                return user;
            } 
            catch(Exception ex)
            {
                return null;
            }
        }

        public static List<GetUser> GetAllUserDS(DataSet ds)
        {
            List<GetUser> list = new List<GetUser>();
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetUser user = new GetUser();
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



        public static GetPost GetPostDS(DataSet ds)
        {
            try
            {
                GetPost post = new GetPost();
                post.Id = (int)ds.Tables[0].Rows[0]["Id"];
                post.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                post.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                post.Photo = ds.Tables[0].Rows[0]["Photo"].ToString();
                post.Category = (int)ds.Tables[0].Rows[0]["Category"];
                post.OnDate = (System.DateTime)ds.Tables[0].Rows[0]["OnDate"];
                post.ByUser = (int)ds.Tables[0].Rows[0]["ByUser"];
                return post;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<GetPost> GetAllPostDS(DataSet ds)
        {
            List<GetPost> list = new List<GetPost>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GetPost post = new GetPost();
                post.Id = (int)ds.Tables[0].Rows[0]["Id"];
                post.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                post.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                post.Photo = ds.Tables[0].Rows[0]["Photo"].ToString();
                post.Category = (int)ds.Tables[0].Rows[0]["Category"];
                post.OnDate = (System.DateTime)ds.Tables[0].Rows[0]["OnDate"];
                post.ByUser = (int)ds.Tables[0].Rows[0]["ByUser"];
                list.Add(post);
            }
            return list;
        }

    }
}