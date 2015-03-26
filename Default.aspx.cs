using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Security;

public partial class Default2 : System.Web.UI.Page
{
    private Order order = new Order();

    public Order Order
    {
        get
        {
            return order;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = Request["login"];

            if (action == null)
            {
                if (Request.IsAuthenticated)
                {
                    loginDiv.Visible = false;
                    logoutDiv.Visible = true;
                }
                else
                {
                    loginDiv.Visible = true;
                    logoutDiv.Visible = false;
                }
            }
            else//点击登录
            {
                string user = Request["username"];
                string pass = Request["password"];
                
                if (Membership.ValidateUser(user, pass))
                {
                    FormsAuthentication.RedirectFromLoginPage(user, false);
                }
                else
                {
                    Response.Redirect("accounts/login/");
                }
            }
        }       
    }

    protected string GetGreeting()
    {
        return Context.User.Identity.Name;
    }

    protected DateTime GetLastLoginTime()
    {
        return Membership.GetUser(Context.User.Identity.Name).LastLoginDate;
    }    

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //pkgAttr is like addr_x-x-weight
        var pkgAttrs = from p in Request.Form.AllKeys where Regex.Match(p, @"addr_\d-\d-weight|addr_\d-\d-length|addr_\d-\d-width|addr_\d-\d-height").Success select p;
        var groups = from p in pkgAttrs group p by p[5];
       
        foreach (var g in groups)
        {            
            Recipient r = new Recipient();
            order.Recipients.Add(r);

            var pkgs = from p in g group p by p[7];
            foreach (var pkg in pkgs)
            {
                Package package = new Package();
                r.Packages.Add(package);
                
                foreach (string pkgAttr in pkg)
                {
                    if (pkgAttr.Contains("weight"))
                    {
                        decimal weight;
                        if (decimal.TryParse(Request.Form.Get(pkgAttr), out weight) && weight > 0m && weight < 30)
                        {
                            package.Weight = weight;
                        }
                        else
                        {
                            LabelError.Text = "请正确输入重量";
                            LabelError.Visible = true;
                            return;
                        }
                    }
                    else if (pkgAttr.Contains("length"))
                    {
                        decimal length;
                        if (decimal.TryParse(Request.Form.Get(pkgAttr), out length) && length > 0m)
                        {
                            package.Length = length;
                        }
                        else
                        {
                            LabelError.Text = "请正确输入长度";
                            LabelError.Visible = true;
                            return;
                        }
                    }
                    else if (pkgAttr.Contains("width"))
                    {
                        decimal width;
                        if (decimal.TryParse(Request.Form.Get(pkgAttr), out width) && width > 0m)
                        {
                            package.Width = width;
                        }
                        else
                        {
                            LabelError.Text = "请正确输入宽度";
                            LabelError.Visible = true;
                            return;
                        }
                    }
                    else if (pkgAttr.Contains("height"))
                    {
                        decimal height;
                        if (decimal.TryParse(Request.Form.Get(pkgAttr), out height) && height > 0m)
                        {
                            package.Height = height;
                        }
                        else
                        {
                            LabelError.Text = "请正确输入高度";
                            LabelError.Visible = true;
                            return;
                        }
                    }
                }               
            }  
        }        
        
        Session.Add("Order", order); 
        Response.Redirect("/products/");
    }
}