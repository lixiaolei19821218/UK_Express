﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class accounts_UserCentre_Recharge : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {            
            RechargeApply apply = new RechargeApply();
            if (TryUpdateModel(apply, new FormValueProvider(ModelBindingExecutionContext)))
            {

            }
        }
    }
}
