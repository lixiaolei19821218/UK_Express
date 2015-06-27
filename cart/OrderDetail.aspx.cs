using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cart_OrderDetail : System.Web.UI.Page
{
    private string folderPath;

    [Ninject.Inject]
    public IRepository repo
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        folderPath = "/pdf/" + Membership.GetUser().UserName + "/";
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

    public string GetFolder()
    {
        return folderPath;
    }
}