<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Nop.WebApp.Demo.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%=User.Identity.Name %>
        <BR />
    <%= AppDomain.CurrentDomain.BaseDirectory %>
        <br />
    <%= AppDomain.CurrentDomain.DynamicDirectory %>
    </div>
    </form>
</body>
</html>
