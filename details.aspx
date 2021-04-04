<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="details.aspx.cs" Inherits="BeTheBank.details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous" />
    <link href="css/StyleSheet1.css" rel="stylesheet" />
</head>
<body>
    <%--    <div class="container2">
        <link href="style.css" rel="StyleSheet3.css" />
<svg viewBox="0 0 500 150" preserveAspectRatio="none" style="height: 100%; width: 100%;"><path d="M570.26,1.95 C268.90,4.03 266.08,79.03 -104.12,142.08 L543.16,186.30 L582.10,-12.70 Z" style="stroke: none; fill: rgba(255, 255, 255, 0.7);"></path></svg>

<svg viewBox="0 0 500 150" preserveAspectRatio="none" style="height: 100%; width: 100%;"><path d="M515.52,-5.23 C269.47,-13.89 267.21,104.73 -100.73,154.03 L543.16,186.30 L568.00,-11.80 Z" style="stroke: none; fill: rgba(255, 255, 255, 0.7);"></path></svg>
    </div>--%>


   
    <div class="container" dir="rtl">

        <form id="form2" runat="server">

            <h1>אנא השלימו את הפרטים הבאים</h1>
            <br />
            <br />
            <br />
            <div class="row">
                <div class="col">
                    <div class="input-group input-group-lg">
                        <asp:Label runat="server" CssClass="input-group-text" Style="background: white; border: none; border-radius: 0px 0px 0px 0px;">שם פרטי:</asp:Label>
                        <asp:TextBox runat="server" class="form-control form-control-lg" ID="fName" Style="border-radius: 0px 0px 0px 0px; height: 80px;" type="text" disabled placeholder=""></asp:TextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="input-group input-group-lg">
                        <asp:Label runat="server" CssClass="input-group-text" Style="background: white; border: none; border-radius: 0px 0px 0px 0px;">שם משפחה:</asp:Label>

                        <asp:TextBox runat="server" class="form-control form-control-lg" ID="sName" type="text" Style="border-radius: 0px 0px 0px 0px; height: 80px;" disabled placeholder=""></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col">
                    <div class="input-group input-group-lg">
                        <asp:Label runat="server" CssClass="input-group-text" Style="background: white; border: none; border-radius: 0px 0px 0px 0px;">תעודת זהות:</asp:Label>

                        <asp:TextBox runat="server" CssClass="form-control form-control-lg" ID="idNum" type="text" Style="border-radius: 0px 0px 0px 0px; height: 80px;" disabled placeholder=""></asp:TextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="input-group input-group-lg">
                        <asp:Label runat="server" for="example-date-input" CssClass="input-group-text" Style="background: white; border: none; border-radius: 0px 0px 0px 0px;">תאריך לידה:</asp:Label>

                        <asp:TextBox runat="server" CssClass="form-control form-control-lg" ID="birthDate" Style="border-radius: 0px 0px 0px 0px; height: 80px; text-align: right;" type="date" Visible="True"></asp:TextBox>
                        <asp:TextBox runat="server" CssClass="form-control form-control-lg" ID="savedBirthDate" Style="border-radius: 0px 0px 0px 0px; height: 80px; text-align: right;" type="text" Visible="False"></asp:TextBox>

                    </div>
                    <asp:Label ID="errorBirthDateLabel" runat="server" CssClasss="alert alert-danger" Style="color: red;" role="alert" Visible="False"></asp:Label>
                </div>

            </div>
            <br />
            <div class="row">
                <div class="col">
                    <div class="input-group input-group-lg">
                        <asp:Label runat="server" CssClass="input-group-text" Style="background: white; border: none; border-radius: 0px 0px 0px 0px;">טלפון:</asp:Label>

                        <asp:TextBox runat="server" class="form-control form-control-lg" ID="phoneNum" type="text" Style="border-radius: 0px 0px 0px 0px; height: 80px;" placeholder=""></asp:TextBox>
                    </div>
                    <asp:Label ID="errorPhoneNum" runat="server" CssClasss="alert alert-danger" Style="color: red;" role="alert" Visible="False"></asp:Label>

                </div>
                <div class="col">
                    <div class="input-group input-group-lg">
                        <asp:Label runat="server" CssClass="input-group-text" Style="background: white; border: none; border-radius: 0px 0px 0px 0px;">דואר אלקטרוני:</asp:Label>

                        <asp:TextBox runat="server" class="form-control form-control-lg" ID="email" type="text" Style="border-radius: 0px 0px 0px 0px; height: 80px;" disabled placeholder=""></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col">
                    <div class="input-group input-group-lg">
                        <asp:Label runat="server" CssClass="input-group-text" Style="background: white; border: none; border-radius: 0px 0px 0px 0px;">שם העסק:</asp:Label>

                        <asp:TextBox runat="server" class="form-control form-control-lg" ID="compName" type="text" Style="border-radius: 0px 0px 0px 0px; height: 80px;" placeholder=""></asp:TextBox>
                    </div>
                    <asp:Label ID="errorCompNameLabel" runat="server" CssClasss="alert alert-danger" Style="color: red;" role="alert" Visible="False"></asp:Label>

                </div>
                <div class="col">
                    <div class="input-group input-group-lg">
                        <asp:Label runat="server" CssClass="input-group-text" Style="background: white; border: none; border-radius: 0px 0px 0px 0px;">ח.פ/שותפות/עמותה:</asp:Label>

                        <asp:TextBox runat="server" class="form-control form-control-lg" ID="compNum" type="text" Style="border-radius: 0px 0px 0px 0px; height: 80px;" placeholder=""></asp:TextBox>
                    </div>
                    <asp:Label ID="errorCompNumLabel" runat="server" CssClasss="alert alert-danger" Style="color: red;" role="alert" Visible="False"></asp:Label>

                </div>
            </div>
            <asp:Label ID="errorLabel" runat="server" CssClasss="alert alert-danger" Style="color: red;" role="alert" Visible="False"></asp:Label>

            <asp:Button runat="server" OnClick="Details_Click" class="btn" type="submit"></asp:Button>

        </form>
    </div>
</body>
</html>
