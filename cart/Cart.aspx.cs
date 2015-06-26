using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMOrderService;

public partial class cart_Cart : System.Web.UI.Page
{
    private IEnumerable<Order> normalOrders;    
    private IEnumerable<SheffieldOrder> sheffieldOrders;
    private decimal balance;
    private decimal totalPrice;
    private string username;
    private aspnet_User apUser;

    [Ninject.Inject]
    public IRepository repo
    {
        get;
        set;
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        username = Membership.GetUser().UserName;
        apUser = repo.Context.aspnet_User.First(u => u.UserName == username);

        normalOrders = from o in repo.Orders where o.User == username && !(o.IsSheffieldOrder ?? false) && !(o.HasPaid ?? false) select o;
        sheffieldOrders = from o in repo.Context.SheffieldOrders where o.User == username select o;

        balance = apUser.Balance;
        totalPrice = normalOrders.Sum(o => o.Cost.Value);
        //totalPrice = normalOrders.Sum(o => o.Cost.Value) + sheffieldOrders.Sum(so => so.Orders.Sum(o => o.Cost.Value));

        normalField.Visible = normalOrders.Count() != 0 ? true : false;
        sheffieldField.Visible = sheffieldOrders.Count() != 0 ? true : false;
    }

    public IEnumerable<Order> GetNoneSheffieldOrders()
    {               
        return normalOrders;
    }

    public IEnumerable<SheffieldOrder> GetSheffieldOrders()
    {        
        return sheffieldOrders;
    }

    public string GetOrderTip(Order order)
    {
        ServiceView sv = new ServiceView(order.Service);
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-gb");
        return string.Format("取件费：{0:c2}，加固费：{1:c2}，快递费：{2:c2}", sv.GetPickupPrice(order), sv.GetReinforcePrice(order), sv.GetDeliverPrice(order));
    }

    public decimal GetAmount()
    {
        
        return balance;
    }

    public decimal GetTotalPrice()
    {       
        return totalPrice;
    }
    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse((sender as Button).Attributes["data-id"], out id))
        {
            Order order = repo.Context.Orders.Find(id);

            Order orderCopy = new Order();
            orderCopy.IsValid = order.IsValid;
            orderCopy.HasPaid = order.HasPaid;
            orderCopy.OrderTime = order.OrderTime;
            orderCopy.PickupTime = order.PickupTime;
            orderCopy.SenderAddress1 = order.SenderAddress1;
            orderCopy.SenderAddress2 = order.SenderAddress2;
            orderCopy.SenderAddress3 = order.SenderAddress3;
            orderCopy.SenderCity = order.SenderCity;
            orderCopy.SenderName = order.SenderName;
            orderCopy.SenderPhone = order.SenderPhone;
            orderCopy.SenderZipCode = order.SenderZipCode;
            orderCopy.ServiceID = order.ServiceID;
            //orderCopy.Service = order.Service;
            orderCopy.User = order.User;
            orderCopy.Id = order.Id;
            orderCopy.ReinforceID = order.ReinforceID;

            foreach (Recipient r in order.Recipients)
            {
                Recipient rc = new Recipient();
                rc.Name = r.Name;
                rc.PhoneNumber = r.PhoneNumber;
                rc.ZipCode = r.ZipCode;
                rc.Address = r.Address;
                rc.City = r.City;

                foreach (Package p in r.Packages)
                {
                    Package pc = new Package();
                    pc.Height = p.Height;
                    pc.Length = p.Length;
                    pc.Width = p.Width;
                    pc.Weight = p.Weight;
                    pc.TrackNumber = p.TrackNumber;
                    pc.Detail = p.Detail;
                    pc.Value = p.Value;

                    rc.Packages.Add(pc);
                }

                orderCopy.Recipients.Add(rc);
            }

            Session["Order"] = orderCopy;
            Response.Redirect("/products/product.aspx");
        }
    }

    protected void ButtonDel_Click(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse((sender as Button).Attributes["data-id"], out id))
        {
            Order myOrder = repo.Orders.Where(o => o.Id == id).FirstOrDefault();
            if (myOrder != null)
            {
                foreach (Recipient r in myOrder.Recipients)
                {
                    repo.Context.Packages.RemoveRange(r.Packages);
                }
                repo.Context.Recipients.RemoveRange(myOrder.Recipients);
                repo.Context.Orders.Remove(myOrder);

                repo.Context.SaveChanges();
            }
        }
        Response.Redirect(Request.Path);        
    }
    protected void ButtonSheffieldEdit_Click(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse((sender as Button).Attributes["data-id"], out id))
        {
            SheffieldOrder myOrder = repo.SheffieldOrders.Where(o => o.Id == id).FirstOrDefault();
            Session["SheffieldOrder"] = myOrder;
        }
        Response.Redirect("/products/SheffieldOrder.aspx");       
    }
    protected void ButtonSheffieldDel_Click(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse((sender as Button).Attributes["data-id"], out id))
        {
            SheffieldOrder myOrder = repo.SheffieldOrders.Where(o => o.Id == id).FirstOrDefault();
            if (myOrder != null)
            {
                foreach (Order o in myOrder.Orders)
                {
                    repo.Context.Packages.RemoveRange(o.Recipients.ElementAt(0).Packages);
                    repo.Context.Recipients.Remove(o.Recipients.ElementAt(0));
                }
                repo.Context.Orders.RemoveRange(myOrder.Orders);
                repo.Context.SheffieldOrders.Remove(myOrder);
                repo.Context.SaveChanges();
            }
        }
        Response.Redirect(Request.Path);
    }
    
    protected void pay_Click(object sender, EventArgs e)
    { 
        if (balance >= totalPrice)
        {
            string wmUsername = "api_test";
            string wmPassword = "api_password";

            List<string> attachmentPaths = new List<string>();
            foreach (Order o in normalOrders)
            {
                //ServiceView sv = new ServiceView(o.Service);

                string wmService;
                switch (o.Service.Name.Trim())
                {
                    case "荷兰邮政 - 免费取件":
                    case "荷兰邮政 - UKMail 取件":
                        wmService = "postnl";
                        break;
                    case "Parcelforce Economy - 上门取件":
                        wmService = "pf-ipe-collection";
                        break;
                    case "Parcelforce Priority - 上门取件":
                        wmService = "pf-gpr-collection";
                        break;
                    case "Parcelforce Economy - 自送仓库":
                        wmService = "pf-ipe-depot";
                        break;
                    case "Parcelforce Economy - 自送邮局":
                        wmService = "pf-ipe-pol";
                        break;
                    case "Parcelforce Priority - 自送邮局":
                        wmService = "pf-gpr-delivery";
                        break;
                    default:
                        wmService = "postnl";
                        break;
                }

                foreach (Recipient r in o.Recipients)
                {
                    OrderServiceClient client = new OrderServiceClient();
                    int parcelCount = r.Packages.Count;
                    string[] purposeOfShipment = new string[parcelCount];
                    for (int i = 0; i < purposeOfShipment.Length; i++)
                    {
                        purposeOfShipment[i] = "Gift";
                    }

                    OrderResponse response = client.OrderPlace(
                        r.Packages.Select(p => p.Detail).ToArray(),
                        purposeOfShipment,
                        r.Packages.Select(p => (float)p.Height).ToArray(),
                        r.Packages.Select(p => (float)p.Length).ToArray(),
                        r.Packages.Select(p => (float)p.Width).ToArray(),
                        r.Packages.Select(p => (float)p.Weight).ToArray(),
                        r.Packages.Select(p => (float)p.Value).ToArray(),
                        parcelCount,
                        wmPassword,
                        wmUsername,
                        r.ZipCode,
                        r.Address,
                        r.City,
                        "Personal",
                        r.Name,
                        "China",
                        r.PhoneNumber,
                        r.Order.SenderAddress1 + " " + r.Order.SenderAddress2 + " " + r.Order.SenderAddress3,
                        r.Order.SenderCity,
                        "Personal",
                        r.Order.SenderName,
                        "UK",
                        r.Order.SenderPhone,//"B29 7sn",
                        r.Order.SenderZipCode,
                        wmService,
                        r.Order.PickupTime.ToString()
                        );

                    if (response.Errors == null)
                    {
                        //国际追踪号
                        string tracknumber = response.TrackNumber;
                        //WM的订单号，主订单号
                        string wm_leadernumber = response.LeaderOrderNumber;
                        //WM的包裹号，用逗号分隔
                        string wm_ordernumber = response.OrderNumber;
                        //返回的pdf信息，
                        LabelResponse labelResponse_leader = client.GetLabelByWMLeaderNumber(wmUsername, wmPassword, wm_leadernumber);
                        //订单合并成的一个pdf，输入为主订单号
                        if (labelResponse_leader.Errors == null)
                        {
                            byte[] byt = labelResponse_leader.Label;
                            string folderPath = AppDomain.CurrentDomain.BaseDirectory + "pdf\\" + Membership.GetUser().UserName;
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }
                            string attachment = folderPath + "\\" + wm_leadernumber + ".pdf";
                            File.WriteAllBytes(attachment, byt);
                           
                            r.WMLeaderNumber = wm_leadernumber;
                            r.WMLeaderPdf = wm_leadernumber + ".pdf"; 
                            string[] tracknumbers = tracknumber.Split(',');
                            for (int i = 0; i < r.Packages.Count; i++)
                            {
                                r.Packages.ElementAt(i).TrackNumber = tracknumbers[i];
                            }
                            r.SuccessPaid = true;
                            attachmentPaths.Add(attachment);
                        }
                        else
                        {
                            //错误保存在Errors里面
                            r.SuccessPaid = false;
                            StringBuilder errors = new StringBuilder();
                            foreach (string error in labelResponse_leader.Errors)
                            {
                                errors.Append(error + ";");
                            }

                            r.Errors = errors.ToString();
                        }

                        /*
                        //根据包裹号下载pdf
                        string[] packagenumbers = wm_ordernumber.Split(',');
                        LabelResponse labelResponse_package = client.GetLabelByPackgeNumber(username, password, packagenumbers[0]);
                        if (labelResponse_package.Errors == null)
                        {
                            byte[] byt = labelResponse_package.Label;
                            File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\pdf\\" + packagenumbers[0] + ".pdf", byt);

                        }
                        else
                        {
                            //错误保存在Errors里面
                        }
                        //根据国际单号下载pdf
                        string[] tracknumbers = tracknumber.Split(',');
                        LabelResponse labelResponse_track = client.GetLabelByTrackNumber(username, password, tracknumbers[0]);
                        if (labelResponse_track.Errors == null)
                        {
                            byte[] byt = labelResponse_track.Label;
                            File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\pdf\\" + tracknumbers[0] + ".pdf", byt);

                        }
                        else
                        {
                            //错误保存在Errors里面
                        }
                         * */


                    }
                    //如果出现错误
                    else
                    {
                        for (int i = 0; i < response.Errors.Length; i++)
                        {//错误列表
                            string error = response.Errors[i].ToString();
                        }
                        r.SuccessPaid = false;

                        StringBuilder errors = new StringBuilder();
                        foreach (string error in response.Errors)
                        {
                            errors.Append(error + ";");
                        }

                        r.Errors = errors.ToString();
                    }                    
                }
                o.HasPaid = true;
            }

            apUser.Balance -= totalPrice;            
            repo.Context.SaveChanges();

            //改成异步
            SendEmail(Membership.GetUser().Email, "您在999Parcel的订单", "请查收您在999Parcel的订单。", attachmentPaths.ToArray());
            Response.Redirect("Paid.aspx");            
        }
        else
        {
            Response.Redirect("RedirectToRecharge.aspx");
        }
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="mailTo">要发送的邮箱</param>
    /// <param name="mailSubject">邮箱主题</param>
    /// <param name="mailContent">邮箱内容</param>
    /// <returns>返回发送邮箱的结果</returns>
    public bool SendEmail(string mailTo, string mailSubject, string mailContent, params string[] attachmentPaths)
    {
        // 设置发送方的邮件信息,例如使用网易的smtp
        string smtpServer = "smtp.163.com"; //SMTP服务器
        string mailFrom = "iir_lee@163.com"; //登陆用户名
        string userPassword = "llc^181925";//登陆密码

        // 邮件服务设置
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
        smtpClient.Host = smtpServer; //指定SMTP服务器
        smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

        // 发送邮件设置        
        MailMessage mailMessage = new MailMessage(mailFrom, mailTo); // 发送人和收件人
        mailMessage.Subject = mailSubject;//主题
        mailMessage.Body = mailContent;//内容
        mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
        mailMessage.IsBodyHtml = true;//设置为HTML格式
        mailMessage.Priority = MailPriority.Low;//优先级

        foreach (string path in attachmentPaths)
        {
            mailMessage.Attachments.Add(new Attachment(path));
        }

        try
        {
            smtpClient.Send(mailMessage); // 发送邮件
            return true;
        }
        catch (SmtpException ex)
        {
            return false;
        }
    }
}