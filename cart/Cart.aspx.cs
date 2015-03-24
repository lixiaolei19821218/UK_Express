using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cart_Cart : System.Web.UI.Page
{
    private Repository repo = new Repository();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public IEnumerable<Order> GetOrders()
    {
        string user = Membership.GetUser().UserName;
        return from o in repo.Orders where o.User == user select o;
    }

    public decimal GetOrderPrice(Order order)
    {

        return 0;
    }
}