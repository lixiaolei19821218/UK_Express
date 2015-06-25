<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paid.aspx.cs" Inherits="cart_Paid" MasterPageFile="~/MasterPage.master" %>
<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>已付款订单 | 诚信物流-可靠,快捷,实惠</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ul class="breadcrumb" style="background: none; margin-top: 15px">
    </ul>

    

       <div class="sz16 bold colorb2" style="margin-top: 20px">
        已支付订单
      
    </div>

    <div style="margin-top: 15px; background-color: #fff; padding: 15px">






        <form runat="server" method="post" id="placeOrder" style="padding-top: 0px">
            <input type='hidden' name='csrfmiddlewaretoken' value='8WyFROe9ydu80zMt8DIf6Vje0yzvE4Tm' />
            <fieldset>
                <legend>直邮订单</legend>
                <table class="table table-orders">
                    <asp:Repeater runat="server" ItemType="Order" SelectMethod="GetNoneSheffieldOrders">
                        <HeaderTemplate>
                            <tr>
                                <th class="tac">订单号</th>
                                <th class="left">下单日期</th>
                                <th class="tac">价格</th>
                                <th class="tac" >包裹数</th>
                                <th class="tac">发件人</th>
                                <th>服务</th>
                                <th colspan="2"></th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            
                            <tr id="<%#Item.Id %>" title="<%#GetOrderTip(Item) %>">
                                <td class="tac"><%#string.Format("{0:d9}", Item.Id) %></td>
                                <td class="left"><%#Item.OrderTime.Value.ToShortDateString() %></td>
                                <td class="tac"><%#GetOrderPrice(Item).ToString("c", CultureInfo.CreateSpecificCulture("en-GB")) %></td>
                                <td class="tac"><%#Item.Recipients.Sum(r => r.Packages.Count) %></td>
                                <td class="tac"><%#Item.SenderName %></td>
                                <td class="right"><%#Item.Service.Name %></td>
                                <td colspan="2">
                                    <asp:Button ID="ButtonEdit" class="btn btn-info btn-small edit" runat="server" Text="详情" ToolTip="<%#Item.Id %>"  />
                                    <asp:Button ID="ButtonDel" class="btn btn-danger btn-small del" runat="server" Text="下载" ToolTip="<%#Item.Id %>"  />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

            </fieldset>
            <br />
            <fieldset>
                <legend>谢菲尔德地区订单</legend>
                <table class="table table-orders">

                    <asp:Repeater runat="server" ItemType="SheffieldOrder" SelectMethod="GetSheffieldOrders">
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
                            <input type='hidden' name='orders' />
                            <tr id="<%#Item.Id %>"">
                                <td class="tac"><%#string.Format("{0:d9}", Item.Id) %></td>
                                <td class="left"><%#Item.Orders.First().OrderTime.Value.ToShortDateString() %></td>
                                <td class="tac"><%#Item.Orders.Sum(o => o.Cost) %></td>
                                <td class="tac"><%#Item.Orders.Sum(o => o.Recipients.First().Packages.Count)%></td>
                                <td class="tac"><%#Item.Orders.First().SenderName %></td>
                                <td class="right">谢菲尔德地区服务</td>
                                <td colspan="2">
                                    <asp:LinkButton ID="ButtonSheffieldEdit"  runat="server" Text="详情" data-id="<%#Item.Id %>" ToolTip="<%#Item.Id %>" />
                                    <asp:LinkButton ID="ButtonSheffieldDel"  runat="server" Text="下载" data-id="<%#Item.Id %>" ToolTip="<%#Item.Id %>" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

            </fieldset>
         


        </form>



    </div>

</asp:Content>