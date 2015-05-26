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

    private int pageSize = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        (Master.FindControl("rechange") as HtmlAnchor).Attributes["class"] = "on";
    }

    public decimal GetTotalApplyMoney()
    {
        return repo.Context.RechargeApplys.Sum(r => r.ApplyAmount);
    }

    public IEnumerable<RechargeApply> GetPageApplys()
    {
        string user = Membership.GetUser().UserName;
        return repo.Context.RechargeApplys.Where(r => r.User == user).OrderBy(p => p.Id).Skip((CurrentPage - 1) * pageSize).Take(pageSize);
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
            string user = Membership.GetUser().UserName;
            return (int)Math.Ceiling((decimal)repo.Context.RechargeApplys.Where(r => r.User == user).Count() / pageSize);
        }
    }
}