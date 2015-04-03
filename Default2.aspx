<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="Scripts/jQuery.Hz2Py-min.js"></script>
</head>
<body>
  
    输入中文：<input name="hz" id="hz" onblur="py.value=($('#hz').toPinyin());" onchange="py.value=($('#hz').toPinyin())" onkeydown="py.value=($('#hz').toPinyin())" type="text" size="50" /> 
显示拼音：<input name="py" id="py" type="text" size="50" /> 
最后，需要说明的是： 
1、onBlur、onChange、onKeydown三种鼠标事件可任选其一或组合使用； 
2、除“-”（连字号）以外，其他标点符号不会被显示，空格显示为“-”（连字号）。 
缺点：暂无法识别多音字；
    
</body>
</html>
