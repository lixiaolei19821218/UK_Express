using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class product_Default : System.Web.UI.Page
{
    private Repository repo = new Repository();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public IEnumerable<ServiceView> GetServices()
    {
        List<ServiceView> svs = new List<ServiceView>();

        foreach (Service s in repo.Services)
        {            
            svs.Add(new ServiceView(s));
        }
        
        return svs;
    }

    public IEnumerable<Recipient> Recipients
    {
        get
        {
            return (List<Recipient>)Session["Recipients"];
        }
    }
}