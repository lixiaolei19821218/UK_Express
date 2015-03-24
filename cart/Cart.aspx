﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="cart_Cart" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>购物车 | 速递中国-可靠,快捷,实惠</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ul class="breadcrumb" style="background: none; margin-top: 15px">
    </ul>




    <div style="margin-top: 30px">
        <div style="margin-left: auto; margin-right: auto; width: 713px; padding-left: 12px; background: url(/static/img/buzou.png) 1px 0 no-repeat;">
            <a href="/" class="fl buzou" style="padding-left: 25px">快速询价</a>
            <a href="/products/" class="fl buzou" style="padding-left: 20px">产品选择</a>
            <a href="#" class="fl buzou" style="padding-left: 30px">包裹信息</a>
            <a href="" class="fl buzou active" style="padding-left: 5px; width: 150px; margin: 0 -16px">在线支付</a>
            <a href="#" class="fl buzou" style="padding-left: 30px">邮件查收</a>
            <div class="cb"></div>
        </div>
    </div>

    <div class="sz16 bold colorb2" style="margin-top: 20px">
        请确认订单
        <div style="float: right; margin-left: 10px"><a href="/deposit/" class="btn btn-danger" style="line-height: 1; padding: 5px 15px">立即充值</a></div>
        <div style="float: right">帐户余额:<strong style="color: #f00">£0.00</strong></div>
    </div>

    <div style="margin-top: 15px; background-color: #fff; padding: 15px">



        <p class="prompt"></p>




        <form action="" method="post" id="delOrderForm" style="display: none">
            <input type='hidden' name='csrfmiddlewaretoken' value='8WyFROe9ydu80zMt8DIf6Vje0yzvE4Tm' />
            <input type='hidden' name='delOrder' id='delOrder' />
        </form>

        <form action="" method="post" id="placeOrder" style="padding-top: 0px">
            <input type='hidden' name='csrfmiddlewaretoken' value='8WyFROe9ydu80zMt8DIf6Vje0yzvE4Tm' />
            <table class="table table-orders">

                <asp:Repeater runat="server" ItemType="Order" SelectMethod="GetOrders">
                    <HeaderTemplate>
                        <tr>
                            <th class="tac">订单号</th>
                            <th class="left">下单日期</th>
                            <th class="tac">价格</th>
                            <th class="tac">包裹数量</th>
                            <th class="tac">发件人</th>
                            <th>服务</th>
                            <th colspan="2"></th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input type='hidden' name='orders' value='43758' />
                        <tr id="<%#Item.Id %>">
                            <td class="tac"><%#Item.Id %></td>
                            <td class="left">2015/3/7</td>
                            <td class="tac"><%#GetOrderPrice(Item) %></td>
                            <td class="tac"><%#Item.Recipients.Sum(r => r.Packages.Count) %></td>
                            <td class="tac"><%#Item.Sender.Name %></td>
                            <td class="right">China Economy 自送邮局</td>
                            <td colspan="2">
                                <a class="btn btn-info btn-small edit" href="/products/order/43758/edit/">修改</a>
                                <span class="btn btn-danger btn-small del">删除</span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>



            <div>
                <a href="/" class="btn btn-info" style="line-height: 1">继续下单</a>
                <div style="float: right">总金额: £<strong class="total-price" style="color: #f00">312.25</strong></div>
            </div>


            <div style="margin-top: 10px;">
                <div style="font-size: 18px; color: #002060">请选择支付方法：</div>
                <div style="padding: 10px; margin: 10px 0 20px">

                    <button type="submit" name="payment" value="inhouse" style="float: left; min-height: 120px" disabled>
                        <img src="/static/img/inhouse-gray.png" style="height: 120px" />
                    </button>


                    <button type="submit" name="payment" value="debit" style="float: left; margin-left: 20px; min-height: 120px" disabled>
                        <img src="/static/img/debit-gray.png" style="height: 120px" />
                    </button>
                    <button type="submit" name="payment" value="credit" style="float: left; margin-left: 20px; min-height: 120px" disabled>
                        <img src="/static/img/credit-gray.png" style="height: 120px" />
                    </button>




                    <div style="clear: both"></div>
                </div>
            </div>


        </form>



    </div>

    <!-- hidden forms -->
    <div id="dialog-confirm" style="display: none"></div>
    <input type="hidden" name="action" id="action" />


    <div id="expired" style="display: none"></div>



    <div id="insufficient" style="display: none"></div>





</asp:Content>
