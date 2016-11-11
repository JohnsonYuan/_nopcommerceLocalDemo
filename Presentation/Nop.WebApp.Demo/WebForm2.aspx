<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Nop.WebApp.Demo.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%=HttpRuntime.BinDirectory %>
        <br />
        <%=AppDomain.CurrentDomain.GetData("DataDirectory") as string %>
        <br />
        Base dir <%=AppDomain.CurrentDomain.BaseDirectory %>
        <br />
        Dynamic dir <%=AppDomain.CurrentDomain.DynamicDirectory %>
        <br />
        current <%=Environment.CurrentDirectory %>
    <br />
        <asp:Button Text="text" ID="check" OnClick="check_Click" runat="server" />
        <asp:Button Text="text" ID="set" OnClick="set_Click" runat="server" />
    </div>
    </form>
</body>
</html>
