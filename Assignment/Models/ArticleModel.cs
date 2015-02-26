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
        public string Description { get; set; }
        public string Type { get; set; }
    }
    
    public class SetPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength=5)]
        [Display(Name="Title")]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }
    }

}