<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Uploadify</title>

    <link href="static/uploadify/example/css/default.css" rel="stylesheet" type="text/css" />
    <link href="static/uploadify/uploadify.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="static/uploadify/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="static/uploadify/swfobject.js"></script>
    <script type="text/javascript" src="static/uploadify/jquery.uploadify.v2.1.0.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function()
        {
            $("#uploadify").uploadify({
                'uploader': 'JS/jquery.uploadify-v2.1.0/uploadify.swf',
                'script': 'accounts/UserCentre/UploadHandler.ashx',
                'cancelImg': 'JS/jquery.uploadify-v2.1.0/cancel.png',
                'folder': 'UploadFile',
                'queueID': 'fileQueue',
                'auto': true,
                'multi': true,       
            });
        });  
    </script>

</head>
<body>
    <div id="fileQueue"></div>
    <input type="file" name="uploadify" id="uploadify" />   
</body>
</html>
