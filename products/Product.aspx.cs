using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Security;

public partial class products_Product : System.Web.UI.Page
{
    private Repository repo = new Repository();

    private ServiceView sv;
    private Order order;

    protected void Page_Load(object sender, EventArgs e)
    {
        string order_id = Request["order_id"];
        if (string.IsNullOrEmpty(order_id))
        {
            order = (Order)Session["Order"];
        }
        else
        {
            int id = int.Parse(order_id);
            order = repo.Orders.FirstOrDefault(o => o.Id == id);
            Session["Order"] = order;
        }
        Service service = repo.Services.FirstOrDefault(s => s.Id == order.ServiceID);
        sv = new ServiceView(service);
    }

    public IEnumerable<Recipient> GetRecipients()
    {
        return order.Recipients;
    }

    public decimal GetTotalPrice()
    {
        return sv.GetPrice(order.Recipients);
    }
    protected void add2cart_Click(object sender, EventArgs e)
    {
        List<Recipient> recipientList = order.Recipients.ToList();

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

        bool pass = true;
        string errorMsg = "";

        if (string.IsNullOrEmpty(s.Name))
        {
            pass = false;
            errorMsg = "请输入发件人的姓名";            
        }
        else if (string.IsNullOrEmpty(s.City))
        {
            pass = false;
            errorMsg = "请输入发件人的城市";            
        }
        else if (string.IsNullOrEmpty(s.Address1) || string.IsNullOrEmpty(s.Address2) || string.IsNullOrEmpty(s.Address3))
        {
            pass = false;
            errorMsg = "请输入发件人的地址";
            
        }
        else if (string.IsNullOrEmpty(s.ZipCode))
        {
            pass = false;
            errorMsg = "请输入发件人的邮编";            
        }
        else if (string.IsNullOrEmpty(s.Phone))
        {
            pass = false;
            errorMsg = "请输入发件人的电话";
        }        
  
        for (int i = 0; i < recipientList.Count; i++)
        {
            Recipient r = recipientList[i];
            if (string.IsNullOrEmpty(r.Name))
            {
                pass = false;
                errorMsg = string.Format("请输入收件人{0}的姓名", i + 1);
                break;
            }
            else if (string.IsNullOrEmpty(r.City))
            {
                pass = false;
                errorMsg = string.Format("请输入收件人{0}的城市", i + 1);
                break;
            }
            else if (string.IsNullOrEmpty(r.Address))
            {
                pass = false;
                errorMsg = string.Format("请输入收件人{0}的地址", i + 1);
                break;
            }
            else if (string.IsNullOrEmpty(r.ZipCode))
            {
                pass = false;
                errorMsg = string.Format("请输入收件人{0}的邮编", i + 1);
                break;
            }
            else if (string.IsNullOrEmpty(r.PhoneNumber))
            {
                pass = false;
                errorMsg = string.Format("请输入收件人{0}的电话", i + 1);
                break;
            }
        }

        if (pass)
        {
            order.Sender = s;
            order.IsValid = true;
            order.User = Membership.GetUser().UserName;
            order.PickupTime = DateTime.Parse(Request["pickup_time_0"]);
            order.OrderTime = DateTime.Now;            
            repo.SaveOrder(order);
            Response.Redirect("/cart/cart.aspx");
        }
        else
        {
            LabelError.Visible = true;
            LabelError.Text = errorMsg;            
        }
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
            return order.Recipients;
        }
    }   
}