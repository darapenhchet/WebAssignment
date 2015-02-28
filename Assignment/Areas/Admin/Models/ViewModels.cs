using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment.Areas.Admin.Models
{
    /* This is a group of models for one controller or view */
    public class ViewModel
    {
        /* All model for Admin controller and View */
        public GetUser GetUser { get; set; }
        public IEnumerable<GetUser> AllUsers { get; set; }

        public SignIn SignIn { get; set; }
        public InsertUser SignUp { get; set; }

        /* All model for Article controller and View */
        public GetPost GetPost { get; set; }
        public SetPost SetPost { get; set; }
        public IEnumerable<GetPost> AllPosts { get; set; }

        /* All model for Category controller and View */
        public GetCategory GetCategory { get; set; }
        public SetCategory SetCategory { get; set; }
        public IEnumerable<GetCategory> AllCategories { get; set; }
    }

    public class MyModel {
        public static ViewModel getModels()
        {
            ViewModel model = new ViewModel();
            model.AllUsers = AccountDAO.ListAllUsers();
            model.AllCategories = CategoryDAO.List();
            model.AllPosts = ArticleDAO.List();
            return model;
        }
    }
}