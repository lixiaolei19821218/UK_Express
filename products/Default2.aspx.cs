using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class products_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "mymymy";
    }
  
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("\\");
    }
    protected void TextBox1_TextChanged1(object sender, EventArgs e)
    {
        this.Title = "mymymy";
    }
}