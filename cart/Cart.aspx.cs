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
    private decimal totalPrice = 0m;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            int id;
            if (int.TryParse(Request.Form["delete"], out id))
            {
                Order myOrder = repo.Orders.Where(o => o.Id == id).FirstOrDefault();
                if (myOrder != null)
                {
                    myOrder.IsValid = false;
                    repo.SaveOrder(myOrder);
                }
            }            
            Response.Redirect(Request.Path);
        }
    }

    public IEnumerable<Order> GetOrders()
    {
        string user = Membership.GetUser().UserName;
        return from o in repo.Orders where o.User == user && (o.IsValid ?? false) select o;
    }

    public decimal GetOrderPrice(Order order)
    {
        ServiceView sv = new ServiceView(order.Service);
        decimal orderPrice = sv.GetPrice(order);
        totalPrice += orderPrice;
        return orderPrice;        
    }

    public decimal GetTotalPrice()
    {
        return totalPrice;        
    }  
}