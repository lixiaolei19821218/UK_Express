using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cart_Paid : System.Web.UI.Page
{
    private int pageSize = 30;

    private IEnumerable<Order> normalOrders;
    private IEnumerable<SheffieldOrder> sheffieldOrders;
    private decimal balance;
    private decimal totalPrice;
    private string username;
    private aspnet_User apUser;

    [Ninject.Inject]
    public IRepository repo
    {
        get;
        set;
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        username = Membership.GetUser().UserName;
        apUser = repo.Context.aspnet_User.First(u => u.UserName == username);

        normalOrders = from o in repo.Orders where o.User == username && !(o.IsSheffieldOrder ?? false) && (o.HasPaid ?? false) select o;
        sheffieldOrders = from o in repo.Context.SheffieldOrders where o.User == username select o;

        balance = apUser.Balance;
        totalPrice = normalOrders.Sum(o => o.Cost.Value);
        //totalPrice = normalOrders.Sum(o => o.Cost.Value) + sheffieldOrders.Sum(so => so.Orders.Sum(o => o.Cost.Value));

        normalField.Visible = normalOrders.Count() != 0 ? true : false;
        sheffieldField.Visible = sheffieldOrders.Count() != 0 ? true : false;
    }

    public IEnumerable<Order> GetNoneSheffieldOrders()
    {        
        return normalOrders;
    }

    public IEnumerable<SheffieldOrder> GetSheffieldOrders()
    {        
        return sheffieldOrders;
    }

    public string GetOrderTip(Order order)
    {
        ServiceView sv = new ServiceView(order.Service);
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-gb");
        return string.Format("取件费：{0:c2}，加固费：{1:c2}，快递费：{2:c2}", sv.GetPickupPrice(order), sv.GetReinforcePrice(order), sv.GetDeliverPrice(order));
    }
    protected void NormalDetail_Click(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse((sender as LinkButton).Attributes["data-id"], out id))
        {           
            Session.Add("id", id);
            Response.Redirect("/cart/OrderDetail.aspx");
        }        
    }

    public IEnumerable<Order> GetPageApplys()
    {
        return normalOrders.OrderBy(p => p.Id).Skip((CurrentPage - 1) * pageSize).Take(pageSize);
    }

    protected int CurrentPage
    {
        get
        {
            int page;
            page = int.TryParse(Request.QueryString["page"], out page) ? page : 1;
            return page > MaxPage ? MaxPage : page;
        }
    }
    protected int MaxPage
    {
        get
        {
            if (normalOrders.Count() == 0)
            {
                return 1;
            }
            else
            {
                return (int)Math.Ceiling((decimal)normalOrders.Count() / pageSize);
            };
        }
    }
}