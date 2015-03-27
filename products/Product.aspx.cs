﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Security;

public partial class products_Product : System.Web.UI.Page
{
    private Repository repo = new Repository();

    private ServiceView sv;
    private Order order;

    protected void Page_Load(object sender, EventArgs e)
    {
        order = (Order)Session["Order"];
        Service service = repo.Services.FirstOrDefault(s => s.Id == order.ServiceID);
        sv = new ServiceView(service);
    }

    public IEnumerable<Recipient> GetRecipients()
    {
        return order.Recipients;
    }

    public decimal GetTotalPrice()
    {
        return sv.GetPrice(order.Recipients);
    }
    protected void add2cart_Click(object sender, EventArgs e)
    {
        string msg = FillOrder();
        if (msg == "pass")
        {
            if (order.Id == 0)
            {
                order.User = Membership.GetUser().UserName;
                order.OrderTime = DateTime.Now;
                repo.Context.Orders.Add(order);
            }
            else
            {
                Order old = repo.Context.Orders.Find(order.Id);

                foreach (Recipient r in old.Recipients)
                {
                    repo.Context.Packages.RemoveRange(r.Packages);
                }
                repo.Context.Recipients.RemoveRange(old.Recipients);

                foreach (Recipient r in order.Recipients)
                {
                    old.Recipients.Add(r);
                }
            }

            repo.Context.SaveChanges();
            Response.Redirect("/cart/cart.aspx");
        }
        else
        {
            LabelError.Visible = true;
            LabelError.Text = msg;
        }
    }

    public IEnumerable<Package> GetAllPackages()
    {
        List<Package> packages = new List<Package>();
        foreach (Recipient r in order.Recipients)
        {
            packages.AddRange(r.Packages);
        }

        return packages;
    }

    public decimal GetPackagePrice(Package package)
    {
        return sv.GetPackageDeliverPrice(package);
    }

    public ServiceView ServiceView
    {
        get
        {
            return sv;
        }
    }

    public IEnumerable<Recipient> Recipients
    {
        get
        {
            return order.Recipients;
        }
    }

    public Order Order
    {
        get
        {
            return order;
        }
    }
    protected void LinkButtonEdit_Click(object sender, EventArgs e)
    {
        FillOrder();
        Response.Redirect("/products/edit.aspx");
    }

    private string FillOrder()
    {
        order.SenderName = Request.Form.Get("billing_detail_name");
        order.SenderCity = Request.Form.Get("billing_detail_city");
        order.SenderPhone = Request.Form.Get("billing_detail_phone");
        order.SenderAddress1 = Request.Form.Get("billing_detail_street");
        order.SenderAddress2 = Request.Form.Get("billing_detail_street2");
        order.SenderAddress3 = Request.Form.Get("billing_detail_street3");
        order.SenderZipCode = Request.Form.Get("billing_detail_postcode");

        List<Recipient> recipientList = order.Recipients.ToList();
        for (int i = 0; i < recipientList.Count; i++)
        {
            Recipient recipient = recipientList[i];
            recipient.Name = Request.Form.Get(string.Format("addr-{0}-cn_name", i));
            recipient.City = Request.Form.Get(string.Format("addr-{0}-cn_city", i));
            recipient.Address = Request.Form.Get(string.Format("addr-{0}-cn_street", i));
            recipient.PhoneNumber = Request.Form.Get(string.Format("addr-{0}-phone", i));
            recipient.ZipCode = Request.Form.Get(string.Format("addr-{0}-postcode", i));
        }

        DateTime date;
        if (DateTime.TryParse(Request["pickup_time_0"], out date))
        {
            order.PickupTime = date;
        }
        else
        {
            return "请输入取件时间";
        }

        if (string.IsNullOrEmpty(order.SenderName))
        {
            return "请输入发件人的姓名";
        }
        else if (string.IsNullOrEmpty(order.SenderCity))
        {
            return "请输入发件人的城市";
        }
        else if (string.IsNullOrEmpty(order.SenderAddress1) || string.IsNullOrEmpty(order.SenderAddress2) || string.IsNullOrEmpty(order.SenderAddress3))
        {
            return "请输入发件人的地址";
        }
        else if (string.IsNullOrEmpty(order.SenderZipCode))
        {
            return "请输入发件人的邮编";
        }
        else if (string.IsNullOrEmpty(order.SenderPhone))
        {
            return "请输入发件人的电话";
        }

        for (int i = 0; i < recipientList.Count; i++)
        {
            Recipient r = recipientList[i];
            if (string.IsNullOrEmpty(r.Name))
            {
                return string.Format("请输入收件人{0}的姓名", i + 1);
            }
            else if (string.IsNullOrEmpty(r.City))
            {
                return string.Format("请输入收件人{0}的城市", i + 1);
            }
            else if (string.IsNullOrEmpty(r.Address))
            {
                return string.Format("请输入收件人{0}的地址", i + 1);
            }
            else if (string.IsNullOrEmpty(r.ZipCode))
            {
                return string.Format("请输入收件人{0}的邮编", i + 1);
            }
            else if (string.IsNullOrEmpty(r.PhoneNumber))
            {
                return string.Format("请输入收件人{0}的电话", i + 1);
            }
        }

        return "pass";
    }
}