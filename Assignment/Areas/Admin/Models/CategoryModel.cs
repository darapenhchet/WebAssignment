using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Areas.Admin.Models
{
    public class GetCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime OnDate { get; set; }
        public int ByUser { get; set; }
    }

    public class SetCategory
    {
        [Display(Name="Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Descritpion")]
        public string Description { get; set; }

        [Display(Name = "OnDate")]
        public System.DateTime OnDate { get; set; }

        [Required]
        [Display(Name = "ByUser")]
        public int ByUser { get; set; }
    }

    public class CategoryDAO
    { 
        public static List<GetCategory> List()
        {
            string sql = @"SELECT * FROM [dbo].[Categories]";
            DataSet ds = DB.Query(sql);
            return DB.GetAllCategoryDS(ds);
        }
    }
}