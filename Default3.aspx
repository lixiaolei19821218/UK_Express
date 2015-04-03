<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="Scripts/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="Scripts/jQuery.Hz2Py-min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        输入中文：<input name="hz" id="hz" onblur="py.value=($('#hz').toPinyin());" onchange="py.value=($('#hz').toPinyin())" onkeydown="py.value=($('#hz').toPinyin())" type="text" size="50" /> 
显示拼音：<input name="py" id="py" type="text" size="50" /> 
最后，需要说明的是： 
1、onBlur、onChange、onKeydown三种鼠标事件可任选其一或组合使用； 
2、除“-”（连字号）以外，其他标点符号不会被显示，空格显示为“-”（连字号）。 
</asp:Content>

