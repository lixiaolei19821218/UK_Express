<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;
using System.Web.Security;

public class UploadHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        string type = context.Request["type"];
        if (type == "add")
        {
            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadPath = HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\" + Membership.GetUser().UserName;
            string name = context.Request.Params["name"]; //获取传递的参数  
            string albums = context.Request.Params["albums"];
            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = DateTime.Now.Ticks + "_" + file.FileName;
                file.SaveAs(Path.Combine(uploadPath, fileName));
                context.Response.Write(@context.Request["folder"] + "/" + Membership.GetUser().UserName + "/" + fileName);
            }
            else
            {
                context.Response.Write("");
            }
        }
        else if (type == "del")
        {
            string picurl = context.Request["picurl"];
            try
            {
                File.Delete(context.Server.MapPath(picurl));
                context.Response.Write(picurl);
            }
            catch
            {
                context.Response.Write("");
            }
        }
        else
        { }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}