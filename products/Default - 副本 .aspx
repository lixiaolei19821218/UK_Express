<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default - 副本 .aspx.cs" Inherits="product_Default" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>服务 | 速递中国-可靠,快捷,实惠</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main" style="background-color: #F1F1F1">

        <div class="container" style="background: none; padding-top: 0">





            <ul class="breadcrumb" style="background: none; margin-top: 15px">
            </ul>




            <div style="margin-top: 30px">
                <div style="margin-left: auto; margin-right: auto; width: 713px; padding-left: 12px; background: url(/static/img/buzou.png) 1px 0 no-repeat;">
                    <a href="/" class="fl buzou" style="padding-left: 20px">快速询价</a>
                    <a href="/products/" class="fl buzou active">产品选择</a>
                    <a href="#" class="fl buzou" style="padding-left: 30px">包裹信息</a>
                    <a href="#" class="fl buzou" style="padding-left: 30px">在线支付</a>
                    <a href="#" class="fl buzou" style="padding-left: 10">邮件查收</a>
                    <div class="cb"></div>
                </div>
            </div>

            <div class="sz16 bold colorb2" style="margin-top: 20px">请选择一个服务：</div>

            <div style="margin-top: 15px; background-color: #fff">
                <table class="table table-products">
                    <tr>
                        <th style="min-width: 180px">服务</th>
                        <th>描述</th>
                        <th style="min-width: 73px; text-align: center">上门取件</th>
                        <th style="min-width: 60px; text-align: center">打印</th>
                        <th style="min-width: 160px">价格(<span style="font-size: 12px">包含取件费</span>)</th>
                    </tr>

                    <tbody>
                        <asp:Repeater ItemType="Service" EnableViewState="false"
                            SelectMethod="GetServices" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="vertical-align: middle">
                                        <img src="<%# Item.PictureLink %>" style="float: left; min-height: 40px; max-width: 60px; margin-right: 3px" />
                                        <%# Item.Name %>
                                    </td>
                                    <td>
                                        <p><img src="<%# Item.DiscribePictureLink %>" width="350" height="78" /></p>
                                        <p><span style="font-size: small;"><%# Item.Discribe %>></span></p>
                                    </td>
                                    <td style="vertical-align: middle; text-align: center">
                                        <i class="glyphicon glyphicon-ok"></i>
                                    </td>
                                    <td style="vertical-align: middle; text-align: center">
                                        <i class="glyphicon glyphicon-print"></i>
                                    </td>
                                    <td style="vertical-align: middle">
                                        <%# Item.Id %>
                                        <br />
                                        <br />
                                        <a href="/products/14/" class="btn btn-warning">购买</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>                    
                </table>
            </div>

            <div style="margin-top: 15px; margin-bottom: 35px; text-align: right">
                <a href="/" style="font-size: 20px; text-decoration: underline">&lt;&lt; 返回</a>
            </div>

        </div>

    </div>
</asp:Content>
