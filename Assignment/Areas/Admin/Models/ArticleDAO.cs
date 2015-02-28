using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Assignment.Areas.Admin.Models
{
    public class ArticleDAO
    {
        public static bool Create(SetPost p)
        {
            string sql = @"INSERT INTO [dbo].[Articles] ([Title], [Content], [Photo],[Category], [ByUser])  VALUES (@p1, @p2, @p3, @p4, @p5)";
            return DB.Action(sql, p.Title, p.Content, p.Photo, AccountDAO.Id, p.Category);
        }

        public static GetPost Detail(int id) {
            string sql = @"SELECT * FROM [dbo].[Articles] WHERE Id = @p1";
            DataSet ds = DB.Query(sql, id);
            return DB.GetPostDS(ds);
        }

        public static List<GetPost> List()
        {
            string sql = @"SELECT * FROM [dbo].[Articles]";
            DataSet ds = DB.Query(sql);
            return DB.GetAllPostDS(ds);
        }
    }
}