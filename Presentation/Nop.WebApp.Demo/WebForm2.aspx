<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Nop.WebApp.Demo.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%=Request.UserHostAddress %>
        <br />
        <%=AppDomain.CurrentDomain.GetData("DataDirectory") as string %>
        <br />
        Base dir <%=AppDomain.CurrentDomain.BaseDirectory %>
        <br />
        Dynamic dir <%=AppDomain.CurrentDomain.DynamicDirectory %>
        <br />
        current <%=Environment.CurrentDirectory %>
    <br />
        <asp:Button Text="clear" ID="Button1" OnClick="Button1_Click" runat="server" />
        <asp:Button Text="clearcontent" ID="Button2" OnClick="Button2_Click" runat="server" />
    <br />
        <asp:Button Text="check" ID="check" OnClick="check_Click" runat="server" />
        <asp:Button Text="check2" ID="check2" OnClick="check_Click2" runat="server" />
        <asp:Button Text="text" ID="set" OnClick="set_Click" runat="server" />

        <br />

        <% foreach (var item in HttpContext.Current.Request.Cookies.AllKeys) { %>
                key: <%= item %>  <br />
         <%    } %>
    </div>
    </form>
</body>
</html>
