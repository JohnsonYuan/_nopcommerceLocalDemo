<%@ Page Language="C#" %>
<%@ import Namespace="System.Drawing" %>
<%@ import Namespace="System.Drawing.Imaging" %>
<%@ import Namespace="System.Drawing.Drawing2D" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>ASP.NET Example</title>
</head>
<body>
    <form id="form1" runat="server">
        
        <%=Request.ServerVariables["HTTP_CLUSTER_HTTPS"] %>
        <br />
        <%=Request.ServerVariables["HTTP_X_FORWARDED_PROTO"] %>
        <br />
        <input id="myBoolean" name="myBoolean" type="checkbox" value="true" />
<input name="myBoolean" type="hidden" value="false" />
        <asp:Button Text="text" runat="server" />
    </form>
</body>
</html>