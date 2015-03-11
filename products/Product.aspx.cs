using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class products_Product : System.Web.UI.Page
{
    private Repository repo = new Repository();
    private ServiceView sv;
    private List<Recipient> recipients;

    protected void Page_Load(object sender, EventArgs e)
    {
        int id = (int)Session["ServiceID"];
        Service service = repo.Services.FirstOrDefault(s => s.Id == id);
        sv = new ServiceView(service);
        recipients = (List<Recipient>)Session["Recipients"];

        recipients[0].Name = "哈哈哈";
        recipients[0].City = "上海";

        FormView1.DataSource = recipients;
        FormView1.DataBind();
    }

    public IEnumerable<Recipient> GetRecipients()
    {
        return recipients;
    }

    public IEnumerable<Recipient> Recipients
    {
        get
        {
            return recipients;
        }
    }

    public decimal GetTotalPrice()
    {
        return sv.GetPrice(recipients);
    }
    protected void Weight_TextChanged(object sender, EventArgs e)
    {
        string n = recipients[0].Name;

    }
}