<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="accounts_UserCentre_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <label>Your name:</label><input type="text" id="name" runat="server" /></div>
            <div>
                <label>Your email:</label><input type="text" id="email" runat="server" />
            </div>
            <div>
                <label>Your phone:</label><input type="text" id="phone" runat="server" />
            </div>
        </div>
        <div>
            <button type="submit">Submit RSVP</button>
        </div>
    </form>
</body>
</html>
