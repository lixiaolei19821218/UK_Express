using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class product_Default : System.Web.UI.Page
{
    private Repository repo = new Repository();
    private Order order;

    protected void Page_Load(object sender, EventArgs e)
    {
        order = (Order)Session["Order"];

        if (IsPostBack)
        {
            int serviceID;
            if (int.TryParse(Request.Form["order"], out serviceID))
            {
                order.ServiceID = serviceID;
                Response.Redirect("product.aspx");
            }
        }
    }

    public IEnumerable<ServiceView> GetServices()
    {
        List<ServiceView> svs = new List<ServiceView>();

        foreach (Service s in repo.Services)
        {            
            svs.Add(new ServiceView(s));
        }
        
        return svs;
    }

    public Order Order
    {
        get
        {
            return order;
        }
    }
}