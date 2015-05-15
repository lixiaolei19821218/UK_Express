using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class accounts_UserCentre_Recharge : System.Web.UI.Page
{
    [Ninject.Inject]
    public IRepository repo { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        string user = Membership.GetUser().UserName;

        if (repo.Context.RechargeApplys.Where(r => r.User == user && !r.IsApproved.HasValue).Count() != 0)
        {
            applyAmount.Disabled = true;
            dosubmit.Disabled = true;
        }

        if (IsPostBack)
        {            
            RechargeApply apply = new RechargeApply();
            if (TryUpdateModel(apply, new FormValueProvider(ModelBindingExecutionContext)))
            {
                apply.User = user;
                repo.Context.RechargeApplys.Add(apply);
                repo.Context.SaveChanges();
            }
        }
    }    
}
