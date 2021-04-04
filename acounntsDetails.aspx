<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="acounntsDetails.aspx.cs" Inherits="BeTheBank.AcounntsDetails" %>

<!DOCTYPE html dir="rtl" lang="he">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
     <meta content="width=device-width, initial-scale=1" name="viewport" />
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous">
    <link href="css/StyleSheet2.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/js/bootstrap.bundle.min.js" integrity="sha384-b5kHyXgcpbZJO/tY9Ul7kGkf1S0CWuKcCD38l8YkeH8z8QjE0GmW1gYU5S9FOnJ0" crossorigin="anonymous"></script>
</head>
<body>
   
    <div class="container" dir="rtl">
        <form id="form1" runat="server">

            <asp:Button runat="server" class="btn" ID="btnBack" type="submit" style=" background: url(/img/arrow-right-circle.svg);position: absolute;background-size: cover;height: 40px;width: 40px;border: none;"></asp:Button>
            <br />
            <br />
            <br />
            <br />
            <h1>אנא השלימו את הפרטי החשבונות של חברת "<asp:Label runat="server" ID="lbCompName"></asp:Label>"</h1>


            
            <br />

            <p>ח_פ:<asp:Label runat="server" ID="lbCompNum"></asp:Label></p>

            <br />

            <div class="input-group input-group-lg">
                <span class="input-group-text" style="background:white;border-color: forestgreen;border-radius: 0px 10px 10px 0px;">% החזקה</span>
                <asp:DropDownList CssClass="input-group-text" style="background:white;border-color: forestgreen;border-radius: 10px 0px 0px 10px;" ID="percentageHold" runat="Server" Enabled="True"></asp:DropDownList>
                <asp:Label ID="errorPercentLabel" runat="server" Visible="False"></asp:Label>
            </div>

            <br />

            <div class="row">
          
                <asp:PlaceHolder runat="server"  ID="insertForm">

                    <div class="col-4 col-sm-4 col-md-4 col-lg-4 col-xl-3">
                        <div class="input-group input-group-lg">
                            <asp:Label runat="server" CssClass="input-group-text" style="background:white;border-color: forestgreen;border-radius: 0px 10px 10px 0px;">בנק</asp:Label>
                            <asp:DropDownList ID="bank" CssClass="input-group-text  input-group-lg" style="background:white;border-color: forestgreen;border-radius: 10px 0px 0px 10px;width:50%;" runat="server" Enabled="True">
                                <asp:ListItem Text="הפועלים" Value="הפועלים"></asp:ListItem>
                                <asp:ListItem Text="לאומי" Value="לאומי"></asp:ListItem>
                                <asp:ListItem Text="מזרחי טפחות" Value="מזרחי טפחות"></asp:ListItem>
                                <asp:ListItem Text="דיסקונט" Value="דיסקונט"></asp:ListItem>
                                <asp:ListItem Text="הבינלאומי" Value="הבינלאומי"></asp:ListItem>
                                <asp:ListItem Text="יהב" Value="יהב"></asp:ListItem>
                                <asp:ListItem Text="ירושלמים" Value="ירושלמים"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                    </div>

                    <div class="col-4 col-sm-4 col-md-4 col-lg-4 col-xl-3">
                        <div class="input-group input-group-lg">
                            <asp:Label runat="server" CssClass="input-group-text" style="background:white;border-color: forestgreen;border-radius: 0px 10px 10px 0px;">סניף</asp:Label>
                            <asp:TextBox CssClass="form-control" runat="server" ID="branch" style="border-color: forestgreen;border-radius: 10px 0px 0px 10px;width:50%;" Enabled="True"></asp:TextBox>                         
                        </div>
                         <asp:Label ID="errorBranchLabel" runat="server" CssClasss="alert alert-danger" style="color:red;" role="alert" Visible="False"></asp:Label>
                    </div>

                    <div class="col-4 col-sm-4 col-md-4 col-lg-4 col-xl-3">
                        <div class="input-group input-group-lg">
                            <asp:Label runat="server" CssClass="input-group-text" style="background:white;border-color: forestgreen;border-radius: 0px 10px 10px 0px;">חשבון</asp:Label>
                            <asp:TextBox CssClass="form-control" runat="server" style="border-color: forestgreen;border-radius: 10px 0px 0px 10px;width:50%;" ID="account"></asp:TextBox>
                        </div>
                         <asp:Label ID="errorAccountLabel" runat="server" CssClasss="alert alert-danger" style="color:red;" role="alert" Visible="False"></asp:Label>
                         <asp:Label ID="errorExistingUser" runat="server" CssClasss="alert alert-danger" style="color:red;" role="alert" Visible="False"></asp:Label>

                        </div>

                    <div class="col-4 col-sm-4 col-md-4 col-lg-4 col-xl-3">
                        <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-link" type="submit" UseSubmitBehavior="false" disabled Text="- הסר"></asp:Button>
                    </div>
                    <asp:Label ID="errorlb" runat="server" CssClasss="alert alert-danger" style="color:red;" role="alert" Visible="False"></asp:Label>

                </asp:PlaceHolder>

            </div>
            <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-link" type="submit" UseSubmitBehavior="false" Text="+הוסף חשבון"></asp:Button>
             <asp:Button runat="server" class="btn" type="submit" ID="btnNext" style=" background: url(/img/arrow-left-circle.svg);position: absolute;background-size: cover;height: 80px;width: 80px;border: none;right: 85%;top: 95%;"></asp:Button>
        </form>
    </div>
        
</body>
</html>
