<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BeTheBank.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LogIn</title>
    <meta charset="utf-8" />

    <link rel="canonical" href="https://getbootstrap.com/docs/5.0/examples/sign-in/" />

    <!-- Bootstrap core CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/js/bootstrap.bundle.min.js" integrity="sha384-b5kHyXgcpbZJO/tY9Ul7kGkf1S0CWuKcCD38l8YkeH8z8QjE0GmW1gYU5S9FOnJ0" crossorigin="anonymous"></script>
    <link href="/docs/5.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous" />

    <!-- Favicons -->
    <link rel="apple-touch-icon" href="/docs/5.0/assets/img/favicons/apple-touch-icon.png" sizes="180x180" />
    <link rel="icon" href="/docs/5.0/assets/img/favicons/favicon-32x32.png" sizes="32x32" type="image/png" />
    <link rel="icon" href="/docs/5.0/assets/img/favicons/favicon-16x16.png" sizes="16x16" type="image/png" />
    <link rel="manifest" href="/docs/5.0/assets/img/favicons/manifest.json" />
    <link rel="mask-icon" href="/docs/5.0/assets/img/favicons/safari-pinned-tab.svg" color="#7952b3" />
    <link rel="icon" href="/docs/5.0/assets/img/favicons/favicon.ico" />
    <meta name="theme-color" content="#7952b3" />


    <style>
      
    </style>


    <!-- Custom styles for this template -->
    <link href="css/Style.css" rel="stylesheet" />
</head>
<body>
    <main class="form-signin">
        <form id="form1" runat="server">
            <h2 class="h1 mb-3 fw-normal" style="text-align:center;font-weight: bold;font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; color:#011268">Be The Bank</h2>
            <h1 class="h3 mb-3 fw-normal" style="font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; color:#011268">כניסה לחשבון</h1>

            <label for="inputLarge" class="visually-hidden">ID</label>
            <asp:TextBox runat="server" id="id" class="form-control" placeholder="מספר תעודת זהות" outocomplete="off" autofocus></asp:TextBox>
         
            <label for="inputPassword" class="visually-hidden">Password</label>
            <asp:TextBox runat="server" type="password" id="password" class="form-control" placeholder="סיסמה"></asp:TextBox>

            <asp:Label ID="errorLabel" runat="server" CssClasss="alert alert-danger" style="color:red;" role="alert" Visible="False"></asp:Label>

            <asp:Button runat="server" class="w-100 btn btn-lg btn-primary" OnClick="Login_Click" type="submit" Text="כניסה"></asp:Button>

        </form>
    </main>
</body>
</html>
