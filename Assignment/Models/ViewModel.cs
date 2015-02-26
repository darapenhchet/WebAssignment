using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class ViewModel
    {
        public IEnumerable<GetPost> Posts { get; set; }
        public IEnumerable<GetUser> Users { get; set; }
        
        public GetUser User { get; set; }
        public SignInModel SignIn { get; set; }
    }
}