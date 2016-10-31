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
        <%=AppDomain.CurrentDomain.BaseDirectory %>
        <br />
        <%=Environment.CurrentDirectory %>
    <br />
    </div>
    </form>
</body>
</html>
