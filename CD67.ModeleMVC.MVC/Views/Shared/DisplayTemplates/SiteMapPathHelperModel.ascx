<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[[MvcSiteMapProvider.Web.Html.Models.SiteMapPathHelperModel,MvcSiteMapProvider]]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="MvcSiteMapProvider.Web.Html.Models" %>


<%if (Model.Nodes.Count >= 0)
{%>
<ul id="breadcrumbs-one" class="background-color1">
    <%foreach (var node in Model.Nodes)
        {
            if (node != Model.Nodes.Last())
            {
                string url = node.IsClickable ? node.Url : "#";%>
            <li><a href="<%=node.Url%>"><%=node.Title%></a><span class="divider"></span></li>
            <li><a>></a><span class="divider"></span></li>
            <% }
            else
            {
            %>
            <li><a><%=node.Title%></a></li>
            <% }
        }%>
</ul>
<%}%>