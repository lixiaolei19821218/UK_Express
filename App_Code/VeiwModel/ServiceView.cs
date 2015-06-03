using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ServiceView 的摘要说明
/// </summary>
public class ServiceView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PictureLink { get; set; }
    public string Discribe { get; set; }
    public string DiscribePictureLink { get; set; }
    public bool PickUpService { get; set; }
    public Nullable<int> PriceListID { get; set; }
    public string PickUpCompany { get; set; }
    public virtual PriceList PriceList { get; set; }

	public ServiceView()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public ServiceView(Service service)
    {
        this.Id = service.Id;
        this.Name = service.Name;
        this.PictureLink = service.PictureLink;
        this.Discribe = service.Discribe;
        this.DiscribePictureLink = service.DiscribePictureLink;
        this.PriceListID = service.PriceListID;
        this.PickUpCompany = service.PickUpCompany;
        this.PriceList = service.PriceList;        
    }    

    public decimal GetPrice(Order order)
    {
        return GetReinforcePrice(order) + GetPickupPrice(order) + GetDeliverPrice(order);
    }

    public decimal GetSendPrice(Order order)
    {
        return GetPickupPrice(order) + GetDeliverPrice(order);
    }

    public decimal GetReinforcePrice(Order order)
    {
        decimal price = 0m;

        if (order.ReinforceID.HasValue)
        {
            foreach (Recipient r in order.Recipients)
            {
                foreach (Package p in r.Packages)
                {
                    price += order.Reinforce.Price.HasValue ? order.Reinforce.Price.Value : 0.0m;
                }
            }
        }

        return price;
    }

    public decimal GetPickupPrice(Order order)
    {
        decimal price = 0m;

        switch (Id)
        {
            case 1://Bpost免费取件
            case 11://post nl免费取件
            case 15://Parcelforce
            case 17://Parcelforce
            case 23://Parcelforce
            case 25://Parcelforce
            case 27://Parcelforce
                price = 0m;
                break;
            case 6://Bpost UKMail取件
            case 12://post nl免 UKMail取件
                //ukmail取件费用，单箱5镑，2-3箱7镑，4箱以上每箱2镑的
                int packageCount = order.Recipients.Sum(r => r.Packages.Count);
                if (packageCount == 1)
                {
                    price = 5m;
                }
                else if (packageCount == 2 || packageCount == 3)
                {
                    price = 7m;
                }
                else
                {
                    price = 2m * packageCount;
                }                
                break;
            default:
                price = 0m;
                break;
        }

        return Math.Round(price, 2);
    }

    public decimal GetDeliverPrice(Order order)
    {
        decimal price = 0m;

        foreach (Recipient r in order.Recipients)
        {
            foreach (Package p in r.Packages)
            {
                price += GetPackageDeliverPrice(p);
            }
        }        
        
        return price;
    }

    public decimal GetPackageDeliverPrice(Package package)
    {
        PriceListView pv = new PriceListView(PriceList);
        return pv.GetPackageDeliverPrice(package);        
    }

    public decimal GetPackagesDeliverPrice(IEnumerable<Package> packages)
    {
        return packages.Sum(p => GetPackageDeliverPrice(p));
    }
}