using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Assignment.Models;

namespace Assignment
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, object> id = new Dictionary<string,object> { {"@id", 2 } };

            GridView1.DataSource = DAO.Query("select * from articles where Id = @id", id);
            GridView1.DataBind();
        }
    }
}