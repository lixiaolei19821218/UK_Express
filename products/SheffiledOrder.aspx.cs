using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class products_SheffiledOrder : System.Web.UI.Page
{
    SheffieldOrder sOrder;



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sOrder = Session["SheffiledOrder"] as SheffieldOrder;
        }
    }


}