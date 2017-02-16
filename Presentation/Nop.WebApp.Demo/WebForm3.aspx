<%@ Page Language="C#" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<script language="CS" runat="server">

    void Page_Load(object sender, System.EventArgs e)
    {
        string cookieToken, formToken;
        System.Web.Helpers.AntiForgery.GetTokens(null, out cookieToken, out formToken);

        Response.Write("cookies : <br/>" + cookieToken + "<br/>" + formToken);
        // Put user code to initialize the page here 
    }
    void btnCheck_Click(object sender, System.EventArgs e)
    {
        if (SqlServerDatabaseExists(tbxInput.Text))
        {
            lblResult.Text = "Connection Success";
            DataSet result = GetSn(tbxInput.Text);
            gv1.DataSource = result;
            gv1.DataBind();
        }
        else
        {
            lblResult.Text = "Connection Failed";
        }
    }

    protected DataSet GetSn(string connStr)
    {
        DataSet ds = new DataSet();
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT TOP 30 SeriNO, NRegProductInfo.*  FROM dbo.NetRegister LEFT OUTER JOIN NRegProductInfo ON NRegProductInfo.Product_Sn=SeriNO WHERE RegState = 0 ", conn))
            {
                adapter.Fill(ds);
            }
        }
        return ds;
    }

    protected bool SqlServerDatabaseExists(string connectionString)
    {
        try
        {
            //just try to connect
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
            }
            return true;
        }
        catch (Exception ex)
        {
            lblError.Text = "exception: "  + ex.Message;
            return false;
        }
    }
</script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%=Request.FilePath %>
            <br />
            <%=HttpRuntime.AppDomainAppVirtualPath%>
            <br />

            <asp:TextBox ID="tbxInput" Text="Data Source=192.168.70.227\sql2005;Initial Catalog=nveruserreg;User ID=gaozg;Password=gaozg123;" Width="650px" runat="server" />
            <br />
            <asp:Button ID="btnCheck" OnClick="btnCheck_Click" Text="text" runat="server" />
            <br />
            <asp:Label ID="lblResult" Text="" runat="server" />
            <br />
            <asp:Label ID="lblError" Text="" runat="server" />

            
            <br />
            <br />
            <asp:GridView EnableViewState="false" ID="gv1" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
