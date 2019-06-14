using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;


namespace CD67.ModeleMVC.MVC.Internal
{
    public static class Navigation
    {
        public static string SMap(SiteMapNode node)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<SiteMapNode> nodes = new List<SiteMapNode>();
            
            nodes.Add(node);
            if (SiteMap.CurrentNode.ParentNode != null)
            {
                SMap(SiteMap.CurrentNode.ParentNode);
            }
            nodes.Reverse();
            sb.Append("<ul>");
            foreach (SiteMapNode nodeelem in nodes)
            {
                sb.Append(string.Concat("<li><a href=\"", nodeelem.Url, "\">", nodeelem.Title, "</a></li>"));
            }
            sb.Append("</ul>");
            
            return sb.ToString();
        }
    }
}
