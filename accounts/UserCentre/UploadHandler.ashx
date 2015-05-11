<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;
using System.Web.Security;

public class UploadHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        HttpPostedFile file = context.Request.Files["Filedata"];
        string uploadPath = HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\" + Membership.GetUser().UserName + "\\";

        if (file != null)
        {
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            file.SaveAs(uploadPath + file.FileName);
            //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
            context.Response.Write(uploadPath + file.FileName);           
        }
        else
        {
            context.Response.Write("0");
        } 
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}