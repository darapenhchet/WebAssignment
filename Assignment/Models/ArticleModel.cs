using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class GetPost {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Photo { get; set; }
        public int Category { get; set; }
        public System.DateTime OnDate { get; set; }
        public int ByUser { get; set; }
    }
    
    public class SetPost
    {
        [Display(Name="Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength=5)]
        [Display(Name="Title")]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        [Display(Name = "Category")]
        public int Category { get; set; }

        [Display(Name="Date")]
        public System.DateTime OnDate { get; set; }

        [Display(Name="User")]
        public int ByUser { get; set; }
    }

}