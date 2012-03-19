using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

public static class Url
{
    public static string SiteRoot()
    {
        return SiteRoot(true);
    }
    public static string SiteRoot(bool includeAppPath)
    {
        var context = HttpContext.Current;
        var Port = context.Request.ServerVariables["SERVER_PORT"];
        if (Port == null || Port == "80" || Port == "443")
            Port = "";
        else
            Port = ":" + Port;
        var Protocol = context.Request.ServerVariables["SERVER_PORT_SECURE"];
        if (Protocol == null || Protocol == "0")
            Protocol = "http://";
        else
            Protocol = "https://";

        var appPath = "";
        if (includeAppPath)
        {
            appPath = context.Request.ApplicationPath;
            if (appPath == "/")
                appPath = "";
        }
        var sOut = Protocol + context.Request.ServerVariables["SERVER_NAME"] + Port + appPath;
        return sOut;

    }
    public static string AbsoluteUrl(string virtualPath)
    {
        var root = SiteRoot(false);
        //this will return everything - including appPath
        var url = SiteUrl(virtualPath);

        return root + url;

    }
    public static string SiteUrl(string virtualUrl)
    {
        return VirtualPathUtility.ToAbsolute(virtualUrl);
    }
    public static string GetReturnUrl()
    {
        var context = HttpContext.Current;
        var returnUrl = "";

        if (context.Request.QueryString["ReturnUrl"] != null)
        {
            returnUrl = context.Request.QueryString["ReturnUrl"];
        }

        return returnUrl;
    }
}
