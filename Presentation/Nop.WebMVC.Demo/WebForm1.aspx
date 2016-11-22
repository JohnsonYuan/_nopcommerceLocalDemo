<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Nop.WebMVC.Demo.Views.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    UrlReferrer: <%=Request.UrlReferrer %>
        <br />
        apppath: <%=Request.ApplicationPath %>
		<br/>
    Authority: <%=Request.Url.Authority%>
        <br />
    ApplicationVirtualPath: <%= System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath%>
    </div>
    </form>
</body>
</html>
