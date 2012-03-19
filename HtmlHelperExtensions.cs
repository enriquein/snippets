
namespace System.Web.Mvc
{
    public static class HtmlHelpers
    {
        const string pubDir = "content";
        const string cssDir = "css";
        const string imageDir = "images";
        const string scriptDir = "js";

        public static string Script(this HtmlHelper helper, ViewPage pg, string fileName)
        {
            return Script(helper, pg.SiteRoot(), fileName);
        }

        public static string Script(this HtmlHelper helper, ViewPage pg, string viewDir, string fileName)
        {
            return Script(helper, pg.SiteRoot(), viewDir, fileName);
        }

        public static string Script(this HtmlHelper helper, ViewUserControl pg, string fileName)
        {
            return Script(helper, pg.SiteRoot(), fileName);
        }

        public static string Script(this HtmlHelper helper, ViewUserControl pg, string viewDir, string fileName)
        {
            return Script(helper, pg.SiteRoot(), viewDir, fileName);
        }

        public static string Script(this HtmlHelper helper, ViewMasterPage pg, string fileName)
        {
            return Script(helper, pg.SiteRoot(), fileName);
        }

        public static string Script(this HtmlHelper helper, ViewMasterPage pg, string viewDir, string fileName)
        {
            return Script(helper, pg.SiteRoot(), viewDir, fileName);
        }

        public static string Script(this HtmlHelper helper, string root, string fileName)
        {
            return Script(helper, root, string.Empty, fileName);
        }

        public static string Script(this HtmlHelper helper, string root, string viewDir, string fileName)
        {
            if (!fileName.EndsWith(".js"))
                fileName += ".js";

            if (!string.IsNullOrEmpty(viewDir))
                viewDir += "/";

            var jsPath = string.Format("<script src='{0}/{1}/{2}/{3}{4}' ></script>\n", root, pubDir, scriptDir, viewDir, helper.AttributeEncode(fileName));
            return jsPath;
        }

        public static string CSS(this HtmlHelper helper, ViewMasterPage pg, string fileName)
        {
            return CSS(helper, pg.SiteRoot(), fileName);
        }

        public static string CSS(this HtmlHelper helper, ViewPage pg, string fileName)
        {
            return CSS(helper, pg.SiteRoot(), fileName);
        }

        public static string CSS(this HtmlHelper helper, ViewUserControl pg, string fileName)
        {
            return CSS(helper, pg.SiteRoot(), fileName);
        }

        public static string CSS(this HtmlHelper helper, string root, string fileName)
        {
            return CSS(helper, root, fileName, "screen");
        }

        public static string CSS(this HtmlHelper helper, string root, string fileName, string media)
        {
            if (!fileName.EndsWith(".css"))
                fileName += ".css";
            var jsPath = string.Format("<link rel='stylesheet' type='text/css' href='{0}/{1}/{2}/{3}'  media='{4}'/>\n", root, pubDir, cssDir, helper.AttributeEncode(fileName), media);
            return jsPath;
        }

        public static string Image(this HtmlHelper helper, ViewPage pg, string fileName)
        {
            return Image(helper, pg.SiteRoot(), fileName);
        }

        public static string Image(this HtmlHelper helper, ViewMasterPage pg, string fileName)
        {
            return Image(helper, pg.SiteRoot(), fileName);
        }

        public static string Image(this HtmlHelper helper, ViewUserControl pg, string fileName)
        {
            return Image(helper, pg.SiteRoot(), fileName);
        }

        public static string Image(this HtmlHelper helper, ViewPage pg, string fileName, string attributes)
        {
            return Image(helper, pg.SiteRoot(), fileName, attributes);
        }

        public static string Image(this HtmlHelper helper, ViewMasterPage pg, string fileName, string attributes)
        {
            return Image(helper, pg.SiteRoot(), fileName, attributes);
        }

        public static string Image(this HtmlHelper helper, ViewUserControl pg, string fileName, string attributes)
        {
            return Image(helper, pg.SiteRoot(), fileName, attributes);
        }

        public static string Image(this HtmlHelper helper, string root, string fileName)
        {
            return Image(helper, root, fileName, "");
        }

        public static string Image(this HtmlHelper helper, string root, string fileName, string attributes)
        {
            fileName = string.Format("{0}/{1}/{2}/{3}", root, pubDir, imageDir, fileName);
            return string.Format("<img src='{0}' '{1}' />", helper.AttributeEncode(fileName), helper.AttributeEncode(attributes));
        }
    }
}
