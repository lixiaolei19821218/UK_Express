<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>登录 | 速递中国-可靠,快捷,实惠</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ul class="breadcrumb" style="background:none;margin-top:15px">
                
            </ul>
            
            
            


    
<div style="background-color:#fff;padding:30px">
<fieldset>
    <form method="post" runat="server">
    




<input type="hidden" name="referrer" value="">
<input type='hidden' name='csrfmiddlewaretoken' value='CBd9bIiMwjrXvo8ggXUxuoDwkccPOIbb' />

<asp:ValidationSummary runat="server" DisplayMode="SingleParagraph"
CssClass="error"/>


<div class="control-group input_id_username 
    ">
    <label class="control-label" for="id_username">
        用户名或电子邮件地址
    </label>
    <div class="controls">
        <input autofocus="" id="id_username" name="username" required="" type="text" />
        
        <span class="help-inline"></span>
        
    </div>
</div>



<div class="control-group input_id_password 
    ">
    <label class="control-label" for="id_password">
        密码
    </label>
    <div class="controls">
        <input id="id_password" name="password" required="" type="password" />
        
        <span class="help-inline"></span>
        
    </div>
</div>



    <div class="form-actions">
        <input class="btn btn-primary btn-large" type="submit" value="登陆"/>
    </div>
    </form>
</fieldset>
</div>

    
    <p>没有账户？立即<a href="/account/signup/?next=">注册</a></p>
    
    
    <p>如果忘记密码，您也可以 <a href="/account/password/reset/?next=/account/update/">重置您的密码</a>。</p>

</asp:Content>

