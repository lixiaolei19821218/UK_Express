using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class products_Product : System.Web.UI.Page
{
    private Repository repo = new Repository();

    private ServiceView sv;
    private List<Recipient> recipients;

    protected void Page_Load(object sender, EventArgs e)
    {
        int id = (int)Session["ServiceID"];
        Service service = repo.Services.FirstOrDefault(s => s.Id == id);
        sv = new ServiceView(service);

        recipients = (List<Recipient>)Session["Recipients"];
    }

    public IEnumerable<Recipient> GetRecipients()
    {
        return recipients;
    }

    public decimal GetTotalPrice()
    {
        return sv.GetPrice(recipients);
    }
    protected void add2cart_Click(object sender, EventArgs e)
    {
        string ctrl = Request.Form["__EVENTTARGET"];
        List<Recipient> recipientList = (List<Recipient>)Session["Recipients"];

        for (int i = 0; i < recipientList.Count; i++)
        {
            Recipient recipient = recipientList[i];
            recipient.Name = Request.Form.Get(string.Format("addr-{0}-cn_name", i));
            recipient.City = Request.Form.Get(string.Format("addr-{0}-cn_city", i));
            recipient.Address = Request.Form.Get(string.Format("addr-{0}-cn_street", i));
            recipient.PhoneNumber = Request.Form.Get(string.Format("addr-{0}-phone", i));
            recipient.ZipCode = Request.Form.Get(string.Format("addr-{0}-postcode", i));
        }

        Sender s = new Sender();
        s.Name = Request.Form.Get("billing_detail_name");
        s.City = Request.Form.Get("billing_detail_city");
        s.Phone = Request.Form.Get("billing_detail_phone");
        s.Address1 = Request.Form.Get("billing_detail_street");
        s.Address2 = Request.Form.Get("billing_detail_street2");
        s.Address3 = Request.Form.Get("billing_detail_street3");
        s.ZipCode = Request.Form.Get("billing_detail_postcode");

        Order order = new Order();
        order.Recipients = recipientList;
        order.Sender = s;

        Session["Recipients"] = recipientList;        
        Session["Order"] = order;
        repo.SaveOrder(order);
        Response.Redirect("/cart/cart.aspx");
    }

    public IEnumerable<Package> GetAllPackages()
    {
        List<Package> packages = new List<Package>();
        foreach (Recipient r in recipients)
        {
            packages.AddRange(r.Packages);
        }

        return packages;
    }

    public decimal GetPackagePrice(Package package)
    {
        return sv.GetPackageDeliverPrice(package);
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
            return recipients;
        }
    }
}