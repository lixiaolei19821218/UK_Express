using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class accounts_Recover : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    
    protected void Reset_Click(object sender, EventArgs e)
    {
        MembershipUser mUser = Membership.GetUser(Request["username"]);
        string newPassword = mUser.ResetPassword("answer");
    }
}