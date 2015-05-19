using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class accounts_UserCentre_RechargeList : System.Web.UI.Page
{
    [Ninject.Inject]
    public IRepository repo { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public IEnumerable<RechargeApply> GetRechargeApplys()
    {
        return repo.Context.RechargeApplys;
    }
}