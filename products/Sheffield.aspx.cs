using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class products_Sheffield : System.Web.UI.Page
{
    private Repository repo = new Repository();

    private IEnumerable<Package> packages;
    private IEnumerable<PriceListView> priceListViews;

    protected void Page_Load(object sender, EventArgs e)
    {
        //packages = from s in repo.SheffieldServices select new Package { Weight = s.PackageWeight, Length = s.PackageLength, Width = s.PackageWidth, Height = s.PackageHeight };
        priceListViews = from pl in repo.PriceLists select new PriceListView(pl);
    }

    public IEnumerable<SheffieldService> GetSheffieldService()
    {
        return repo.SheffieldServices;
    }

    public IEnumerable<PriceListView> GetPriceListViews()
    {
        return priceListViews;
    }
}