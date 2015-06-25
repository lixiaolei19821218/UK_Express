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

    public IEnumerable<Order> GetNoneSheffieldOrders()
    {
        string user = Membership.GetUser().UserName;
        return from o in repo.Orders where o.User == user && !(o.IsSheffieldOrder ?? false) && (o.HasPaid ?? false) select o;
    }

    public IEnumerable<SheffieldOrder> GetSheffieldOrders()
    {
        string user = Membership.GetUser().UserName;
        var r = from o in repo.Context.SheffieldOrders where o.User == user select o;
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
}