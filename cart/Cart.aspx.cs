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

    }

    public IEnumerable<Order> GetOrders()
    {
        string user = Membership.GetUser().UserName;
        return from o in repo.Orders where o.User == user select o;
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
    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        int id = int.Parse((sender as Button).ToolTip);
        Order order = repo.Context.Orders.Find(id);

        Order orderCopy = new Order();
        orderCopy.IsValid = order.IsValid;
        orderCopy.HasPaid = order.HasPaid;
        orderCopy.OrderTime = order.OrderTime;
        orderCopy.PickupTime = order.PickupTime;
        orderCopy.SenderAddress1 = order.SenderAddress1;
        orderCopy.SenderAddress2 = order.SenderAddress2;
        orderCopy.SenderAddress3 = order.SenderAddress3;
        orderCopy.SenderCity = order.SenderCity;
        orderCopy.SenderName = order.SenderName;
        orderCopy.SenderPhone = order.SenderPhone;
        orderCopy.SenderZipCode = order.SenderZipCode;
        orderCopy.ServiceID = order.ServiceID;
        orderCopy.User = order.User;
        orderCopy.Id = order.Id;

        foreach (Recipient r in order.Recipients)
        {
            Recipient rc = new Recipient();
            rc.Name = r.Name;
            rc.PhoneNumber = r.PhoneNumber;
            rc.ZipCode = r.ZipCode;
            rc.Address = r.Address;
            rc.City = r.City;

            foreach (Package p in r.Packages)
            {
                Package pc = new Package();
                pc.Height = p.Height;
                pc.Length = p.Length;
                pc.Width = p.Width;
                pc.Weight = p.Weight;
                pc.WaybillNumber = p.WaybillNumber;

                rc.Packages.Add(pc);
            }

            orderCopy.Recipients.Add(rc);
        }

        Session["Order"] = orderCopy;
        Response.Redirect("/products/product.aspx");
    }

    protected void ButtonDel_Click(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse((sender as Button).ToolTip, out id))
        {
            Order myOrder = repo.Orders.Where(o => o.Id == id).FirstOrDefault();
            if (myOrder != null)
            {
                foreach (Recipient r in myOrder.Recipients)
                {
                    repo.Context.Packages.RemoveRange(r.Packages);
                }
                repo.Context.Recipients.RemoveRange(myOrder.Recipients);
                repo.Context.Orders.Remove(myOrder);

                repo.Context.SaveChanges();
            }
        }
        Response.Redirect(Request.Path);        
    }
}