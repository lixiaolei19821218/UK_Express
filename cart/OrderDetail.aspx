<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="cart_OrderDetail" MasterPageFile="~/MasterPage.master" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>已发送包裹 | 诚信物流-可靠,快捷,实惠</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="sz16 bold colorb2" style="margin-top: 20px">
        订单详情
    </div>

    <div style="margin-top: 15px; background-color: #fff; padding: 15px">
        <form runat="server" method="post" id="placeOrder" style="padding-top: 0px">
            <asp:Repeater runat="server" ItemType="Recipient" SelectMethod="GetRecipients">
                <ItemTemplate>
                    <fieldset>
                        <legend>收件人：<%#Item.Name %><%#(Item.SuccessPaid ?? false) ? "<div style=\"float:right;font-size:medium;color:green;\">发送成功</div>" : "<div style=\"float:right;font-size:medium;color:red;\">发送失败</div>"%></legend>                        
                        <ul>                            
                            <li>城市：<%#Item.City %></li>
                            <li>地址：<%#Item.Address %></li>
                            <li>手机：<%#Item.PhoneNumber %></li>
                            <li>邮编：<%#Item.ZipCode %></li>
                            <li><%#(Item.SuccessPaid ?? false) ? "单号：" + Item.WMLeaderNumber: "失败原因：" + Item.Errors %></li>  
                            <asp:Repeater runat="server" ItemType="Package" DataSource="<%#Item.Packages %>">
                                <HeaderTemplate>
                                    <tr>
                                        <th class="tac">订单号</th>
                                        <th class="left">下单日期</th>
                                        <th class="tac">价格</th>
                                        <th class="tac">包裹数</th>
                                        <th class="tac">发件人</th>
                                        <th>服务</th>
                                        <th>状态</th>
                                        <th colspan="2"></th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <tr>
                                       <th class="tac"><%#Item.Weight %></th>
                                        <th class="left"><%#Item.Length %></th>
                                        <th class="tac"<%#Item.Width %></th>
                                        <th class="tac"><%#Item.Height %></th>
                                        <th class="tac"><%#Item.Status %></th>
                                        <th><%#Item.TrackNumber %></th>
                                        <th><%#Item.Length %></th>
                                        <th colspan="2"></th>
                                   </tr>
                                </ItemTemplate>
                            </asp:Repeater>                          
                        </ul>    
                        <a <%#(Item.SuccessPaid ?? false) ? "" : "hidden=\"hidden\"" %> href="<%#GetFolder() + Item.WMLeaderPdf %>">点击下载</a>                     
                    </fieldset>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </form>
    </div>
</asp:Content>