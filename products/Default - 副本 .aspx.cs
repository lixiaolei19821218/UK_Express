using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class product_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public IEnumerable<Service> GetServices()
    {
        Service s0 = new Service { Id = 0, Name = "Bpost - 免费上门", DiscribePictureLink = "/static/media/uploads/warehouse_dropoff.png", Discribe = "谢菲尔德免费上门取件", PictureLink = "/static/media/carriers/logo/bpost-logo.png" };
        Service s1 = new Service { Id = 0, Name = "Bpost - UKMail上门取件", DiscribePictureLink = "/static/media/uploads/ukmail_collection.png", Discribe = "国际快递时间（不包括清关和旺季延误）: 7-11 天 /&nbsp;自带丢件赔偿: &pound;50磅 /&nbsp;包裹上可贴中文地址", PictureLink = "/static/media/carriers/logo/bpost-logo.png" };
        List<Service> ss = new List<Service>();
        ss.Add(s0);
        ss.Add(s1);
        return ss;
    }
}