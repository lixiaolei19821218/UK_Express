using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class accounts_UserCentre_RechargeList : System.Web.UI.Page
{
    [Ninject.Inject]
    public IRepository repo { get; set; }

    private int pageSize = 4;

    protected void Page_Load(object sender, EventArgs e)
    {
        (Master.FindControl("rechange") as HtmlAnchor).Attributes["class"] = "on";
        (Master.FindControl("default") as HtmlAnchor).Attributes["class"] = "";
    }

    public IEnumerable<RechargeApply> GetRechargeApplys()
    {
        string user = Membership.GetUser().UserName;
        return repo.Context.RechargeApplys.Where(r => r.User == user);
    }

    protected IEnumerable<RechargeApply> GetProducts()
    {
        return repo.Context.RechargeApplys.OrderBy(p => p.Id).Skip((CurrentPage - 1) * pageSize).Take(pageSize);
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
            return (int)Math.Ceiling((decimal)repo.Context.RechargeApplys.Count() / pageSize);
        }
    }
}