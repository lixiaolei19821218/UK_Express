using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            string name = Request.Form["username"];
            string password = Request.Form["password"];
            if (name != null && password != null
                    && FormsAuthentication.Authenticate(name, password))
            {
                FormsAuthentication.SetAuthCookie(name, false);
                Response.Redirect(Request["ReturnUrl"] ?? "/");
            }
            else
            {
                ModelState.AddModelError("fail", "Login failed. Please try again!!");
            }
        }
    }
}