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
    public class ArticleDAO
    {
        //private static string cs = "Data Source=RAVUTHZ;Initial Catalog=AssigmentDB;Integrated Security=True;Pooling=False";
        private static string cs = ConfigurationManager.ConnectionStrings["MyDBConnectionString1"].ConnectionString;

        public static bool Create(SetPost p)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"INSERT INTO [dbo].[Articles] ([Title], [Description], [Type])"
                    + " VALUES (@title,  @description, @type)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("@title", SqlDbType.NVarChar)).Value = p.Title;
                cmd.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar)).Value = p.Description;
                cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar)).Value = p.Type;

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

        public static List<GetPost> List()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                List<GetPost> articles = new List<GetPost>();
                string sql = @"SELECT * FROM [dbo].[Articles] order by Id desc";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader dat = cmd.ExecuteReader();

                while (dat.Read())
                {
                    GetPost post = new GetPost
                    {
                        Id = (int)dat["Id"],
                        Title = dat["Title"].ToString(),
                        Description = dat["Description"].ToString(),
                        Type = dat["Type"].ToString()
                    };
                    articles.Add(post);
                }
                return articles;
            }
        }
    }
}