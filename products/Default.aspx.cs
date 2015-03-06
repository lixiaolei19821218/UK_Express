using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class product_Default : System.Web.UI.Page
{
    private Repository repo = new Repository();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public IEnumerable<Service> GetServices()
    {
        return repo.Services;
    }
}