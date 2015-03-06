using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Repository 的摘要说明
/// </summary>
public class Repository
{
    private UK_ExpressEntities context = new UK_ExpressEntities(); 
	public Repository()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public IEnumerable<Service> Services
    {
        get
        {
            return context.Services;
        }
    }
}