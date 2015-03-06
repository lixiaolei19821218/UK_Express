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

	public ServiceView()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public decimal GerPrice()
    {
        return 9m;
    }
}