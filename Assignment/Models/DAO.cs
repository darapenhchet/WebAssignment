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
    public class DAO
    {

        private static string cs = "Data Source=RAVUTHZ;Initial Catalog=AssigmentDB;Integrated Security=True;Pooling=False";
        //private static string cs = ConfigurationManager.ConnectionStrings["MyDBConnectionString1"].ConnectionString;


        public static DataSet Query(string sql)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset;
            }
        }

        public static DataSet Query(string sql, Dictionary<string, object> fields)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);

                //adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                // add value to sql with it name
                foreach (KeyValuePair<string, object> item in fields)
                {
                    adapter.SelectCommand.Parameters.AddWithValue(item.Key, item.Value);
                }


                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset;
            }
        }

        public static DataSet Query(string sql, string key, object value)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                adapter.SelectCommand.Parameters.AddWithValue(key, value);

                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset;
            }
        }


    }

    public class MyDB {
        private static SqlConnection conn;
        private static string conStr = "MyDBConnectionString1";

        public static SqlConnection Connection
        {
            get { return conn; }
        }

        public static void Connect()
        {
            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings[conStr].ToString();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        public static int ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                Connect();
                command.Connection = conn;
                //int result = command.ExecuteNonQuery();
                //return result;
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
            }
        }

        public static SqlDataReader ExecuteReader(SqlCommand command)
        {
            try
            {
                Connect();
                command.Connection = conn;
                SqlDataReader result = command.ExecuteReader(CommandBehavior.CloseConnection);
                return result;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public static object ExecuteScalar(SqlCommand command)
        {
            try
            {
                Connect();
                command.Connection = conn;

                object value = command.ExecuteScalar();
                if (value is DBNull)
                {
                    return default(decimal);
                }
                else
                {
                    return value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ClearPool()
        {
            SqlConnection.ClearAllPools();
        }
    
    }
}