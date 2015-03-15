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

        bool pass = true;
        string errorMsg = "";

        if (string.IsNullOrEmpty(s.Name))
        {
            pass = false;
            errorMsg = "请输入发件人{0}的姓名";            
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

        Order order = new Order();
        order.Recipients = recipientList;
        order.Sender = s;     
  
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