using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// PriceListView 的摘要说明
/// </summary>
public class PriceListView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }

    public virtual ICollection<PriceItem> PriceItems { get; set; }
    public virtual ICollection<Service> Services { get; set; }
    public int SheffieldServiceId { get; set; }

	public PriceListView()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public PriceListView(PriceList priceList)
    {
        Id = priceList.Id;
        Name = priceList.Name;
        ShortName = priceList.ShortName;
        PriceItems = priceList.PriceItems;
        Services = priceList.Services;
    }

    public decimal GetPackageDeliverPrice(Package package)
    {
        decimal priceByWeight = PriceItems.OrderBy(i => i.Weight).First(i => i.Weight >= package.Weight).Price;
        decimal priceBySize = package.Length * package.Width * package.Height / 5000m;

        decimal price = priceByWeight > priceBySize ? priceByWeight : priceBySize;

        return Math.Round(price, 2);
    }

    public decimal GetPackagesDeliverPrice(IEnumerable<Package> packages)
    {
        return packages.Sum(p => GetPackageDeliverPrice(p));
    }
}