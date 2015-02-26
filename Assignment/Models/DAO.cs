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

        public static bool Action(SqlCommand cmd) {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                cmd.Connection = con;
                if (cmd.ExecuteNonQuery() != 0)
                {
                    cmd.Dispose();
                    return true;
                }
                cmd.Dispose();
                return false;
            }
        }

        public static List<SqlDataReader> Query(SqlCommand cmd) 
        {
            List<SqlDataReader> list = null;
            SqlDataReader data = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                cmd.Connection = con;
                data = cmd.ExecuteReader();

                //if (data.Read()){
                //    return data;
                //}
                while(data.Read())
                {
                    list.Add(data);
                }
                return list;
            }
        }



        public static string Select() {
            string sql = @"SELECT * FROM [dbo].[Articles] WHERE Id = @id";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = 1;

            SqlDataReader data = Query(cmd);

            if (data != null) {
                return "OK";           
            }
            return "Fail";
            
        }

        public static void Insert() {
            string sql = @"INSERT INTO [dbo].[Articles] ([Title], [Content])"
                    + " VALUES (@title,  @content)";

            //SqlCommand cmd = new SqlCommand(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("@title", SqlDbType.NVarChar)).Value = "insertOK1";
            cmd.Parameters.Add(new SqlParameter("@content", SqlDbType.NVarChar)).Value = "OK1";
            Action(cmd);
        }

       
    }
}