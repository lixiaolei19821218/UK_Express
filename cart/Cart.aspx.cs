using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMOrderService;

public partial class cart_Cart : System.Web.UI.Page
{
    [Ninject.Inject]
    public IRepository repo
    {
        get;
        set;
    }

    private decimal totalPrice = 0m;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public IEnumerable<Order> GetOrders()
    {
        string user = Membership.GetUser().UserName;
        return from o in repo.Orders where o.User == user select o;
    }

    public IEnumerable<Order> GetNoneSheffieldOrders()
    {
        string user = Membership.GetUser().UserName;
        return from o in repo.Orders where o.User == user && !(o.IsSheffieldOrder.HasValue && o.IsSheffieldOrder.Value) select o;
    }

    public IEnumerable<SheffieldOrder> GetSheffieldOrders()
    {
        string user = Membership.GetUser().UserName;
        var r = from o in repo.Context.SheffieldOrders where o.User == user  select o;
        return r;
    }

    public decimal GetOrderPrice(Order order)
    {
        ServiceView sv = new ServiceView(order.Service);
        decimal orderPrice = sv.GetPrice(order);
        totalPrice += orderPrice;
        return orderPrice;
    }

    public string GetOrderTip(Order order)
    {
        ServiceView sv = new ServiceView(order.Service);
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-gb");
        return string.Format("取件费：{0:c2}，加固费：{1:c2}，快递费：{2:c2}", sv.GetPickupPrice(order), sv.GetReinforcePrice(order), sv.GetDeliverPrice(order));
    }

    public decimal GetTotalPrice()
    {
        return totalPrice + GetSheffieldOrders().Sum(so => so.Orders.Sum(o => o.Cost.Value));
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
        //orderCopy.Service = order.Service;
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
                pc.Detail = p.Detail;
                pc.Value = p.Value;

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
    protected void ButtonSheffieldEdit_Click(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse((sender as Button).Attributes["data-id"], out id))
        {
            SheffieldOrder myOrder = repo.SheffieldOrders.Where(o => o.Id == id).FirstOrDefault();
            Session["SheffieldOrder"] = myOrder;
        }
        Response.Redirect("/products/SheffieldOrder.aspx");       
    }
    protected void ButtonSheffieldDel_Click(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse((sender as Button).Attributes["data-id"], out id))
        {
            SheffieldOrder myOrder = repo.SheffieldOrders.Where(o => o.Id == id).FirstOrDefault();
            if (myOrder != null)
            {
                foreach (Order o in myOrder.Orders)
                {
                    repo.Context.Packages.RemoveRange(o.Recipients.ElementAt(0).Packages);
                    repo.Context.Recipients.Remove(o.Recipients.ElementAt(0));
                }
                repo.Context.Orders.RemoveRange(myOrder.Orders);
                repo.Context.SheffieldOrders.Remove(myOrder);
                repo.Context.SaveChanges();
            }
        }
        Response.Redirect(Request.Path);
    }

    public decimal GetAmount()
    {
        string user = Membership.GetUser().UserName;
        var applys = repo.Context.RechargeApplys.Where(r => r.User == user);
        if (applys.Count() == 0)
        {
            return 0;
        }
        else
        {
            return applys.Sum(r => r.ApplyAmount);
        }
    }
    protected void pay_Click(object sender, EventArgs e)
    {        
        decimal balance = GetAmount();
        decimal cost = GetTotalPrice();

        if (balance >= cost)
        {
            string username = "api_test";
            string password = "api_password";

            var noneSheffieldOrders = GetNoneSheffieldOrders();
            foreach (Order o in noneSheffieldOrders)
            {
                foreach (Recipient r in o.Recipients)
                {
                    OrderServiceClient client = new OrderServiceClient();
                    int parcelCount = r.Packages.Count;
                    string[] purposeOfShipment = new string[parcelCount];
                    for (int i = 0; i < purposeOfShipment.Length; i++)
                    {
                        purposeOfShipment[i] = "Gift";
                    }

                    OrderResponse response = client.OrderPlace(
                        r.Packages.Select(p => p.Detail).ToArray(),
                        purposeOfShipment,
                        r.Packages.Select(p => (float)p.Height).ToArray(),
                        r.Packages.Select(p => (float)p.Length).ToArray(),
                        r.Packages.Select(p => (float)p.Width).ToArray(),
                        r.Packages.Select(p => (float)p.Weight).ToArray(),
                        r.Packages.Select(p => (float)p.Value).ToArray(),
                        parcelCount,
                        password,
                        username,
                        r.ZipCode,
                        r.Address,
                        r.City,
                        "Personal",
                        r.Name,
                        "China",
                        r.PhoneNumber,
                        r.Order.SenderAddress1 + " " + r.Order.SenderAddress2 + " " + r.Order.SenderAddress3,
                        r.Order.SenderCity,
                        "Personal",
                        r.Order.SenderName,
                        "UK",
                        r.Order.SenderPhone,//"B29 7sn",
                        r.Order.SenderZipCode,
                        "pf-ipe-pol",
                        DateTime.Now.ToString()
                        );

                    if (response.Errors == null)
                    {
                        //国际追踪号
                        string tracknumber = response.TrackNumber;
                        //WM的订单号，主订单号
                        string wm_leadernumber = response.LeaderOrderNumber;
                        //WM的包裹号，用逗号分隔
                        string wm_ordernumber = response.OrderNumber;
                        //返回的pdf信息，
                        LabelResponse labelResponse_leader = client.GetLabelByWMLeaderNumber(username, password, wm_leadernumber);
                        //订单合并成的一个pdf，输入为主订单号
                        if (labelResponse_leader.Errors == null)
                        {
                            byte[] byt = labelResponse_leader.Label;
                            File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\pdf\\" + wm_leadernumber + ".pdf", byt);
                        }
                        else
                        {
                            //错误保存在Errors里面
                        }
                        //根据包裹号下载pdf
                        string[] packagenumbers = wm_ordernumber.Split(',');
                        LabelResponse labelResponse_package = client.GetLabelByPackgeNumber(username, password, packagenumbers[0]);
                        if (labelResponse_package.Errors == null)
                        {
                            byte[] byt = labelResponse_package.Label;
                            File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\pdf\\" + packagenumbers[0] + ".pdf", byt);

                        }
                        else
                        {
                            //错误保存在Errors里面
                        }
                        //根据国际单号下载pdf
                        string[] tracknumbers = tracknumber.Split(',');
                        LabelResponse labelResponse_track = client.GetLabelByTrackNumber(username, password, tracknumbers[0]);
                        if (labelResponse_track.Errors == null)
                        {
                            byte[] byt = labelResponse_track.Label;
                            File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\pdf\\" + tracknumbers[0] + ".pdf", byt);

                        }
                        else
                        {
                            //错误保存在Errors里面
                        }
                    }
                    //如果出现错误
                    else
                    {
                        for (int i = 0; i < response.Errors.Length; i++)
                        {//错误列表
                            string error = response.Errors[i].ToString();
                        }
                    }
                }
            }
        }
        else
        {
            Response.Redirect("RedirectToRecharge.aspx");
        }
    }
}