using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class products_Reinforce : System.Web.UI.Page
{
    private Repository repo = new Repository();

    private Order order;
    private ServiceView sv;

    protected void Page_Load(object sender, EventArgs e)
    {
        order = (Order)Session["Order"];
        Service service = repo.Services.FirstOrDefault(s => s.Id == order.ServiceID);
        sv = new ServiceView(service);
    }

    public ServiceView ServiceView
    {
        get
        {
            return sv;
        }
    }

    public IEnumerable<Recipient> Recipients
    {
        get
        {
            return order.Recipients;
        }
    }

    public decimal GetPackagePrice(Package package)
    {
        return sv.GetPackageDeliverPrice(package);
    }

    public decimal GetTotalPrice()
    {
        return sv.GetPrice(order.Recipients);
    }

    public IEnumerable<Package> GetAllPackages()
    {
        List<Package> packages = new List<Package>();
        foreach (Recipient r in order.Recipients)
        {
            packages.AddRange(r.Packages);
        }

        return packages;
    }   
}