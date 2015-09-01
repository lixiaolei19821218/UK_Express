﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
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

        normalField.Visible = normalOrders == null || normalOrders.Count() != 0 ? true : false;
        sheffieldField.Visible = sheffieldOrders == null || sheffieldOrders.Count() != 0 ? true : false;
    }

    public IEnumerable<Order> GetNoneSheffieldOrders()
    {               
        return normalOrders;
    }

    public IEnumerable<SheffieldOrder> GetSheffieldOrders()
    {
        //return new List<SheffieldOrder>();
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
                    case "Bpost - 免费取件":
                    case "Bpost - UKMail 取件":
                        SendBpostLciFile(o);
                        return;
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

            Thread sendThread = new Thread(SendThreadMethod);
            object[] mail = new object[] { Membership.GetUser().Email, "您在999Parcel的订单", "请查收您在999Parcel的订单。", attachmentPaths.ToArray() };
            sendThread.Start(mail);

            //改成异步
            //SendEmail(Membership.GetUser().Email, "您在999Parcel的订单", "请查收您在999Parcel的订单。", attachmentPaths.ToArray());
            Response.Redirect("Paid.aspx");            
        }
        else
        {
            Response.Redirect("RedirectToRecharge.aspx");
        }
    }

    public void SendThreadMethod(object obj)
    {
        object[] contents = obj as object[];
        string address = contents[0] as string;
        string title = contents[1] as string;
        string content = contents[2] as string;
        string[] attachments = contents[3] as string[];
        SendEmail(address, title, content, attachments);
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

    private void SendBpostLciFile(Order o)
    {
        foreach (Recipient r in o.Recipients)
        {
            foreach (Package p in r.Packages)
            {
                Bpost.GenerateLciFile("BPI/2015/9320", p.Id.ToString(), p);
            }
        }        
    }

    private void SendBpost(Order o)
    {
        foreach (Recipient r in o.Recipients)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlTextWriter xtw = new XmlTextWriter(sw);
                xtw.WriteStartElement("?xml version=\"1.0\" encoding=\"utf-8\" ?");

                xtw.WriteStartElement("ImportRequest");
                xtw.WriteStartElement("Login");
                xtw.WriteStartElement("Username");
                xtw.WriteString("demoapi");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Password");
                xtw.WriteString("demo123");
                xtw.WriteEndElement();
                xtw.WriteEndElement();
                xtw.WriteStartElement("Test");
                xtw.WriteString("true");
                xtw.WriteEndElement();
                xtw.WriteStartElement("ClientID");
                xtw.WriteString("218");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Reference");
                xtw.WriteString("3245325");
                xtw.WriteEndElement();
                xtw.WriteStartElement("ShipTo");
                xtw.WriteStartElement("Name");
                xtw.WriteString("Test Company");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Attention");
                xtw.WriteString(r.Name);
                xtw.WriteEndElement();
                xtw.WriteStartElement("Address1");
                xtw.WriteString(r.Address);
                xtw.WriteEndElement();
                xtw.WriteStartElement("Address2");
                xtw.WriteString(string.Empty);
                xtw.WriteEndElement();
                xtw.WriteStartElement("Address3");
                xtw.WriteString(string.Empty);
                xtw.WriteEndElement();
                xtw.WriteStartElement("City");
                xtw.WriteString(r.City);
                xtw.WriteEndElement();
                xtw.WriteStartElement("State");
                xtw.WriteString("ON");
                xtw.WriteEndElement();
                xtw.WriteStartElement("PostalCode");
                xtw.WriteString(r.ZipCode);
                xtw.WriteEndElement();
                xtw.WriteStartElement("Country");
                xtw.WriteString("CN");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Phone");
                xtw.WriteString(r.PhoneNumber);
                xtw.WriteEndElement();
                xtw.WriteStartElement("Email");
                xtw.WriteString(string.Empty);
                xtw.WriteEndElement();
                xtw.WriteStartElement("Region");
                xtw.WriteString("Canada");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Residential");
                xtw.WriteString("true");
                xtw.WriteEndElement();
                xtw.WriteEndElement();
                xtw.WriteStartElement("ShipMethod");
                xtw.WriteString("LGINTSTD");
                xtw.WriteEndElement();
                xtw.WriteStartElement("ItemsCurrency");
                xtw.WriteString("USD");
                xtw.WriteEndElement();
                xtw.WriteStartElement("ProduceLabel");
                xtw.WriteString("true");
                xtw.WriteEndElement();
                xtw.WriteStartElement("LabelFormat");
                xtw.WriteString("PDF");
                xtw.WriteEndElement();
                xtw.WriteStartElement("LabelEncoding");
                xtw.WriteString("LINKS");
                xtw.WriteEndElement();
                xtw.WriteStartElement("ShipOptions");
                xtw.WriteStartElement("Option");
                xtw.WriteStartElement("Name");
                xtw.WriteString("dummy_option");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Value");
                xtw.WriteString("true");
                xtw.WriteEndElement();
                xtw.WriteEndElement();
                xtw.WriteEndElement();
                xtw.WriteStartElement("VendorInformation");
                xtw.WriteStartElement("VendorName");
                xtw.WriteString("Test Company Legal Name");
                xtw.WriteEndElement();
                xtw.WriteStartElement("VendorAddress1");
                xtw.WriteString("Sample Company Street");
                xtw.WriteEndElement();
                xtw.WriteStartElement("VendorAddress2");
                xtw.WriteString("Suite 135");
                xtw.WriteEndElement();
                xtw.WriteStartElement("VendorCity");
                xtw.WriteString("Santa Barbara");
                xtw.WriteEndElement();
                xtw.WriteStartElement("VendorState");
                xtw.WriteString("CA");
                xtw.WriteEndElement();
                xtw.WriteStartElement("VendorPostalCode");
                xtw.WriteString("93101");
                xtw.WriteEndElement();
                xtw.WriteStartElement("VendorCountry");
                xtw.WriteString("US");
                xtw.WriteEndElement();
                xtw.WriteEndElement();
                xtw.WriteStartElement("AdditionalFields");
                xtw.WriteStartElement("Field1");
                xtw.WriteString("Any type of data");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Field2");
                xtw.WriteString("Purchased with Credit Card");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Field3");
                xtw.WriteString("99000029327172321");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Field4");
                xtw.WriteString("123198012");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Field5");
                xtw.WriteString("Stored information");
                xtw.WriteEndElement();
                xtw.WriteEndElement();
                xtw.WriteStartElement("PickSlipAdditions");
                xtw.WriteStartElement("Charges");
                xtw.WriteStartElement("Charge");
                xtw.WriteStartElement("Description");
                xtw.WriteString("Gift Card Code: DISCOUNTHOUND");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Value");
                xtw.WriteString("-7.25");
                xtw.WriteEndElement();
                xtw.WriteEndElement();
                xtw.WriteStartElement("Charge");
                xtw.WriteStartElement("Description");
                xtw.WriteString("Sales Tax");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Value");
                xtw.WriteString("1.59");
                xtw.WriteEndElement();
                xtw.WriteEndElement();
                xtw.WriteEndElement();
                xtw.WriteStartElement("Memos");
                xtw.WriteStartElement("Memo");
                xtw.WriteString("You will receive 15% off your next order with coupon code SAVE15");
                xtw.WriteEndElement();
                xtw.WriteEndElement();
                xtw.WriteEndElement();               

                foreach (Package p in r.Packages)
                {
                    xtw.WriteStartElement("Package");
                    xtw.WriteStartElement("Weight");
                    xtw.WriteString(p.Weight.ToString());
                    xtw.WriteEndElement();
                    xtw.WriteStartElement("Length");
                    xtw.WriteString(p.Length.ToString());
                    xtw.WriteEndElement();
                    xtw.WriteStartElement("Width");
                    xtw.WriteString(p.Width.ToString());
                    xtw.WriteEndElement();
                    xtw.WriteStartElement("Height");
                    xtw.WriteString(p.Height.ToString());
                    xtw.WriteEndElement(); ;
                    xtw.WriteStartElement("PackageReference");
                    xtw.WriteString("98233312");
                    xtw.WriteEndElement();
                    xtw.WriteEndElement();
                }
               
                xtw.WriteStartElement("Items");
                xtw.WriteStartElement("Item");
                xtw.WriteStartElement("Sku");
                xtw.WriteString("7224059");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Quantity");
                xtw.WriteString("2");
                xtw.WriteEndElement();
                xtw.WriteStartElement("UnitPrice");
                xtw.WriteString("93.99");
                xtw.WriteEndElement();
                xtw.WriteStartElement("Description");
                xtw.WriteString("Women's Shoes");
                xtw.WriteEndElement();
                xtw.WriteStartElement("HSCode");
                xtw.WriteString("640399.30.00");
                xtw.WriteEndElement();
                xtw.WriteStartElement("CountryOfOrigin");
                xtw.WriteString("CN");
                xtw.WriteEndElement();
                xtw.WriteStartElement("CIFValue");
                xtw.WriteString("23.00");
                xtw.WriteEndElement();
                xtw.WriteEndElement();
                xtw.WriteEndElement();

                xtw.WriteEndElement();

                PostHelp ph = new PostHelp();
                string result = ph.PostXml("https://mercury.landmarkglobal.com/api/api.php", sw.ToString());

                StreamWriter w = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\result.xml");
                w.Write(result);
                w.Close();
            }
        }
    }
}

public class PostHelp
{
    public string GetWebContent(string url)
    {
        Stream outstream = null;
        Stream instream = null;
        StreamReader sr = null;
        HttpWebResponse response = null;
        HttpWebRequest request = null;
        // 要注意的这是这个编码方式，还有内容的Xml内容的编码方式
        Encoding encoding = Encoding.GetEncoding("UTF-8");
        byte[] data = encoding.GetBytes(url);

        // 准备请求,设置参数
        request = WebRequest.Create(url) as HttpWebRequest;
        request.Method = "POST";
        request.ContentType = "text/xml";
        //request.ContentLength = data.Length;

        outstream = request.GetRequestStream();
        outstream.Write(data, 0, data.Length);
        outstream.Flush();
        outstream.Close();
        //发送请求并获取相应回应数据

        response = request.GetResponse() as HttpWebResponse;
        //直到request.GetResponse()程序才开始向目标网页发送Post请求
        instream = response.GetResponseStream();

        sr = new StreamReader(instream, encoding);
        //返回结果网页(html)代码

        string content = sr.ReadToEnd();
        return content;
    }
    public string PostXml(string url, string strPost)
    {
        string result = "";

        StreamWriter myWriter = null;
        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
        objRequest.Method = "POST";
        //objRequest.ContentLength = strPost.Length;
        objRequest.ContentType = "text/xml";//提交xml 
        //objRequest.ContentType = "application/x-www-form-urlencoded";//提交表单
        try
        {
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(strPost);
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
            myWriter.Close();
        }

        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
        {
            result = sr.ReadToEnd();
            sr.Close();
        }
        return result;
    }
}

public class XMLHelp
{
    private XDocument _document;

    public XDocument Document
    {
        get { return _document; }
        set { _document = value; }
    }
    private string _fPath = "";

    public string FPath
    {
        get { return _fPath; }
        set { _fPath = value; }
    }

    /// <summary>
    /// 初始化数据文件，当数据文件不存在时则创建。
    /// </summary>
    public void Initialize()
    {
        if (!File.Exists(this._fPath))
        {
            this._document = new XDocument(
            new XElement("entity", string.Empty)
            );
            this._document.Save(this._fPath);
        }
        else
            this._document = XDocument.Load(this._fPath);
    }

    public void Initialize(string xmlData)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlData);

        this._document = XmlDocumentExtensions.ToXDocument(doc, LoadOptions.None);
    }
    /// <summary>
    /// 清空用户信息
    /// </summary>
    public void ClearGuest()
    {
        XElement root = this._document.Root;
        if (root.HasElements)
        {
            XElement entity = root.Element("entity");
            entity.RemoveAll();
        }
        else
            root.Add(new XElement("entity", string.Empty));
    }


    ///LYJ 修改
    /// <summary>
    /// 提交并最终保存数据到文件。
    /// </summary>

    public void Commit()
    {
        try
        {
            this._document.Save(this._fPath);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void UpdateQrState(string PId, string state)
    {
        XElement root = this._document.Root;
        XElement entity = root.Element("entity");

        IEnumerable<XElement> elements = entity.Elements().Where(p =>
        p.Attribute("PId").Value == PId);
        if (elements.Count() == 0)
            return;
        else
        {
            XElement guest = elements.First();
            guest.Attribute("FQdState").Value = state;
            guest.Attribute("FQdTime").Value = DateTime.Now.ToString();
            Commit();
        }
    }

    public IEnumerable<XElement> GetXElement()
    {
        XElement root = this._document.Root;
        IEnumerable<XElement> elements = root.Elements();
        return elements;
    }



    public DataTable GetEntityTable()
    {
        DataTable dtData = new DataTable();
        XElement root = this._document.Root;
        IEnumerable<XElement> elements = root.Elements();

        foreach (XElement item in elements)
        {
            dtData.Columns.Add(item.Name.LocalName);
        }
        DataRow dr = dtData.NewRow();
        int i = 0;
        foreach (XElement item in elements)
        {
            dr[i] = item.Value;
            i = i + 1;
        }
        dtData.Rows.Add(dr);
        return dtData;
    }

}

public static class XmlDocumentExtensions
{
    public static XDocument ToXDocument(this XmlDocument document)
    {
        return document.ToXDocument(LoadOptions.None);
    }

    public static XDocument ToXDocument(this XmlDocument document, LoadOptions options)
    {
        using (XmlNodeReader reader = new XmlNodeReader(document))
        {
            return XDocument.Load(reader, options);
        }
    }
}