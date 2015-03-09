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
        this.PriceList = service.PriceList;
    }

    public decimal GetPrice(IEnumerable<Recipient> recipients)
    {
        return GetPickupPrice(recipients) + GetDeliverPrice(recipients);
    }

    public decimal GetPickupPrice(IEnumerable<Recipient> recipients)
    {
        decimal price = 0m;

        switch (Id)
        {
            case 1://Bpost免费取件
            case 11://TPG免费取件
            case 15://Parcelforce Economy
            case 17://Parcelforce Prioity
                price = 0m;
                break;
            case 6://Bpost UKMail取件
            case 12://TPG UKMail取件
                //ukmail取件费用，单箱5镑，2-3箱7镑，4箱以上每箱2镑的
                int packageCount = recipients.Sum(r => r.Packages.Count);
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
        }

        return price;
    }

    public decimal GetDeliverPrice(IEnumerable<Recipient> recipients)
    {
        decimal price = 0m;

        foreach (Recipient r in recipients)
        {
            foreach (Package p in r.Packages)
            {
                price += GetPackageDeliverPrice(p);
            }
        }        
        
        return price;
    }

    private decimal GetPackageDeliverPrice(Package package)
    {
        decimal priceByWeight = PriceList.PriceItems.OrderBy(i => i.Weight).First(i => i.Weight >= package.Weight).Price;
        decimal priceBySize = package.Length * package.Width * package.Height / 5000m;

        decimal price = priceByWeight > priceBySize ? priceByWeight : priceBySize; 

        return price;
    }
}