﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class accounts_UserCentre_Recharge : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fu.SaveAs(Server.MapPath("~/") + fu.FileName);
        //Label1.Text = "上传成功！"; 
    }
}