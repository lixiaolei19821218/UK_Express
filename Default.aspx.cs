using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //pkgAttr is like addr_x-x-weight
        var pkgAttrs = from p in Request.Form.AllKeys where Regex.Match(p, @"addr_\d-\d-weight|addr_\d-\d-length|addr_\d-\d-width|addr_\d-\d-height").Success select p;
        var groups = from p in pkgAttrs group p by p[5];

        List<Recipient> reciptientList = new List<Recipient>();
        foreach (var g in groups)
        {
            Recipient r = new Recipient();

            var pkgs = from p in g group p by p[7];

            foreach (var pkg in pkgs)
            {
                Package package = new Package();
                
                foreach (string pkgAttr in pkg)
                {
                    if (pkgAttr.Contains("weight"))
                    {
                        package.Weight = decimal.Parse(Request.Form.Get(pkgAttr));
                    }
                    if (pkgAttr.Contains("length"))
                    {
                        package.Length = decimal.Parse(Request.Form.Get(pkgAttr));
                    }
                    if (pkgAttr.Contains("width"))
                    {
                        package.Width = decimal.Parse(Request.Form.Get(pkgAttr));
                    }
                    if (pkgAttr.Contains("height"))
                    {
                        package.Height = decimal.Parse(Request.Form.Get(pkgAttr));
                    }
                }

                r.Packages.Add(package);
            }

            Session.Add("Recipients", reciptientList);
            reciptientList.Add(r);
        }

        Response.Redirect("/products/");
    }
}