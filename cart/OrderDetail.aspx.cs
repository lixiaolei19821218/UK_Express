using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cart_OrderDetail : System.Web.UI.Page
{
    [Ninject.Inject]
    public IRepository repo
    {
        get;
        set;
    }

    private Order order;
    private ServiceView sv;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["id"] == null)        
        {
            Response.Redirect("/");
        }

        int id;        
        if (int.TryParse(Session["id"].ToString(), out id))
        {
            order = repo.Context.Orders.Find(id);
        }
        if (order == null)
        {
            Response.Redirect("/");
        }
        Service service = repo.Services.FirstOrDefault(s => s.Id == order.ServiceID);
        sv = new ServiceView(service);
    }

    public IEnumerable<Recipient> GetRecipients()
    {        
        int id;
        if (int.TryParse(Session["id"].ToString(), out id))
        {
            Order order = repo.Context.Orders.Find(id);
            if (order != null)
            {
                return order.Recipients;
            }
        }       
        return new Recipient[0];
    }

    public Order Order
    {
        get
        {
            return order;
        }
    }

    public decimal GetPackagePrice(Package package)
    {
        return sv.GetPackageDeliverPrice(package);
    }
}