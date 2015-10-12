using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public string FormatPackageItems(Package package)
    {
        StringBuilder sb = new StringBuilder();
        foreach (PackageItem item in package.PackageItems)
        {
            string line = string.Format("{0} &times; {1}， 总价：{2}<br/>", item.Description, item.Count, item.Value);
            sb.Append(line);
        }
        return sb.ToString();
    }
}