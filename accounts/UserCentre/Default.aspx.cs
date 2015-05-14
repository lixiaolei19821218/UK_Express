using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class accounts_UserCentre_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            GuestResponse1 rsvp = new GuestResponse1();
            if (TryUpdateModel(rsvp, new FormValueProvider(ModelBindingExecutionContext)))
            {
                
            }
        }
    }
}

public class GuestResponse1
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool? WillAttend { get; set; }
}