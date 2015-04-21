﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class products_Sheffield : System.Web.UI.Page
{
    private Repository repo = new Repository();
    
    private List<PriceListView> priceListViews;

    protected void Page_Load(object sender, EventArgs e)
    {        
        priceListViews = new List<PriceListView>();
        foreach (PriceList p in repo.PriceLists)
        {
            priceListViews.Add( new PriceListView(p));
        }
    }

    public IEnumerable<SheffieldService> GetSheffieldService()
    {
        return repo.SheffieldServices;
    }

    public IEnumerable<PriceListView> GetPriceListViews(int sheffieldServiceId)
    {       
        foreach (PriceListView pv in priceListViews)
        {            
            pv.SheffieldServiceId = sheffieldServiceId;
            
        }
        return priceListViews;
    }

    public decimal GetPrice(int serviceId, int senderId)
    {        
        PriceList pl = repo.PriceLists.FirstOrDefault(p => p.Id == senderId);
        SheffieldService ss = repo.SheffieldServices.FirstOrDefault(s => s.Id == serviceId);
        Package package = new Package { Weight = ss.PackageWeight, Length = ss.PackageLength, Width = ss.PackageWidth, Height = ss.PackageHeight };
        return new PriceListView(pl).GetPackageDeliverPrice(package);
    }

    protected void ButtonAddToCart_Click(object sender, EventArgs e)
    {

    }
}