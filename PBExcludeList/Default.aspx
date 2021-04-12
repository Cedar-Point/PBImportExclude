<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PBExcludeList.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PB Exlusions</title>
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
        .auto-style2 {
            height: 0px;
            margin-top: 20px;
            margin-bottom: 24px;
        }
        .auto-style3 {
            width: 111px;
        }
        .auto-style4 {
            height: 26px;
            width: 111px;
        }
        .auto-style5 {
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <h1>Phone Book Administrator - Exclusions List</h1>
    <form id="form1" runat="server">
        <hr class="auto-style2" />
        <asp:Table ID="tblDataList" runat="server" GridLines="Both">
            <asp:TableRow runat="server" TableSection="TableHeader">
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server" Font-Bold="True">Start Range</asp:TableCell>
                <asp:TableCell runat="server" Font-Bold="True">End Range</asp:TableCell>
                <asp:TableCell runat="server" Font-Bold="True">Description</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <hr class="auto-style2" />
        <asp:Label ID="lblAddError" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="#FF3300" Height="40px" Text="Please make sure all fields have a value." Visible="False" Width="360px"></asp:Label>
        <table style="width:100%;" id="tblEditTable">
            <tr>
                <td class="auto-style3">Start Range</td>
                <td>
                    <asp:TextBox ID="tbStartRange" runat="server" TextMode="Number" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">End Range</td>
                <td>
                    <asp:TextBox ID="tbEndRange" runat="server" TextMode="Number" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Description</td>
                <td class="auto-style1">
                    <asp:TextBox ID="tbDesc" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4"></td>
                <td class="auto-style1">
                    <asp:Button ID="btnAddSet" runat="server" OnClick="btnAddSet_Click" Text="Add" />
                </td>
            </tr>
        </table>
        <hr class="auto-style2" />
        <p>
            <asp:Button ID="btnCancel" runat="server" CssClass="auto-style5" OnClick="btnCancel_Click" Text="Cancel" Width="113px" />
&nbsp;<asp:Button ID="btnSave0" runat="server" CssClass="auto-style5" OnClick="btnSave0_Click" Text="Save" Width="113px" />
        </p>
    </form>

</body>
</html>
