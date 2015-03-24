<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>注册 | 速递中国-可靠,快捷,实惠</title>
    <link rel="stylesheet" href="/static/css/bootstrap.min.css">
    <link rel="stylesheet" href="/static/css/bootstrap-responsive.min.css">
    <link rel="stylesheet" href="/static/css/mezzanine.css">

    <link rel="stylesheet" href="/static/css/cartridge.css">

    <link rel="stylesheet" href="/static/css/theme.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ul class="breadcrumb" style="background: none; margin-top: 15px" />
    <div style="background-color: #fff; padding: 30px">
        <fieldset>
            <form id="form1" runat="server">
                <div class="control-group input_id_first_name ">
                    <label class="control-label" for="id_first_name">名字</label>
                    <div class="controls">
                        <input autofocus="" id="id_first_name" maxlength="30" name="first_name" type="text" />

                        <span class="help-inline"></span>
                    </div>
                </div>
                <div class="control-group input_id_last_name">
                    <label class="control-label" for="id_last_name">
                        姓氏
                    </label>
                    <div class="controls">
                        <input id="id_last_name" maxlength="30" name="last_name" type="text" />

                        <span class="help-inline"></span>

                    </div>
                </div>



                <div class="control-group input_id_email 
    ">
                    <label class="control-label" for="id_email">
                        电子邮件地址
                    </label>
                    <div class="controls">
                        <input id="id_email" maxlength="75" name="email" type="text" />

                        <span class="help-inline"></span>

                    </div>
                </div>



                <div class="control-group input_id_username 
    ">
                    <label class="control-label" for="id_username">
                        用户名
                    </label>
                    <div class="controls">
                        <input id="id_username" maxlength="30" name="username" required="" type="text" />

                        <span class="help-inline">只允许英文字母、数字、破折号 &#39;-&#39; 或下横线 &#39;_&#39;</span>

                    </div>
                </div>



                <div class="control-group input_id_password1 
    ">
                    <label class="control-label" for="id_password1">
                        密码
                    </label>
                    <div class="controls">
                        <input autocomplete="off" id="id_password1" name="password1" type="password" />

                        <span class="help-inline"></span>

                    </div>
                </div>



                <div class="control-group input_id_password2 ">
                    <label class="control-label" for="id_password2">
                        密码(重复)
                    </label>
                    <div class="controls">
                        <input autocomplete="off" id="id_password2" name="password2" type="password" />

                        <span class="help-inline"></span>

                    </div>
                </div>



                <div class="control-group input_id_phone">
                    <label class="control-label" for="id_phone">
                        Phone
                    </label>
                    <div class="controls">
                        <input id="id_phone" maxlength="15" name="phone" type="text" />

                        <span class="help-inline"></span>

                    </div>
                </div>



                <div class="form-actions">
                    <asp:Button CssClass="btn btn-primary btn-large" Text="注册" runat="server" />
                </div>
            </form>
        </fieldset>
    </div>
</asp:Content>
