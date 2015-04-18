<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sheffield.aspx.cs" Inherits="products_Sheffield" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>谢菲尔德地区服务 | 速递中国-可靠,快捷,实惠</title>
    <script src="../Scripts/jquery-1.8.0.min.js"></script>
    <script type="text/javascript">
       
        $get_values()
        {

        }
        var values = get_values();

        $(document).ready(function () {
            $('#id_sender').onchange = function()
            {
                alert("ddddd");
            }
        }
        )
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <ul class="breadcrumb" style="background: none; margin-top: 15px"></ul>



    <div class="sz16 bold colorb2" style="margin-top: 20px">请选择您需要的服务：</div>

    <div style="margin-top: 15px; background-color: #fff">
        <form runat="server">
            <table class="table table-products">
                <tr>
                    <th style="min-width: 180px">服务</th>
                    <th>描述</th>
                    <th style="min-width: 73px; text-align: center">快递公司</th>
                    <th style="min-width: 73px; text-align: center">数量</th>
                    <th style="min-width: 147px; text-align: center">价格</th>
                </tr>

                <tbody>
                    <asp:Repeater runat="server" ID="rp1" ItemType="SheffieldService" SelectMethod="GetSheffieldService">
                        <ItemTemplate>
                            <tr>
                                <td style="vertical-align: middle">
                                    <img src="<%#Item.PictureLink %>" style="float: left; min-height: 40px; max-width: 60px; margin-right: 3px" />
                                    <%#Item.Name %>
                                </td>
                                <td>
                                    <img src="<%#Item.DiscribePictureLink %>" width="350" height="78" />
                                    <p><span style="font-size: small;"><%#Item.Discribe1 %></span></p>
                                    <p><span style="font-size: small;"><%#Item.Discribe2 %></span></p>
                                </td>
                                <td style="vertical-align: middle; text-align: center">
                                    <i class="glyphicon glyphicon-ok">                                       
                                        <select name="sender" id="id_sender" itemid="<%#Item.Id %>">
                                            <asp:Repeater runat="server" ItemType="PriceListView" SelectMethod="GetPriceListViews">
                                                <ItemTemplate>
                                                    <option value="<%#Item.Id %>"><%#Item.ShortName %></option>
                                                </ItemTemplate>
                                            </asp:Repeater>                                            
                                        </select>                                        
                                    </i>
                                </td>
                                <td style="vertical-align: middle; text-align: center">
                                    <input type="number" style="width: 40px">
                                </td>
                                <td style="vertical-align: middle; text-align: center">
                                    £258.80
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </form>
    </div>

    <div>

        <div style="float: left">总金额: <strong class="total-price" style="color: #f00">£258.80</strong></div>
        <div style="margin-top: -5px; margin-bottom: 10px; float: right"><a href="/" class="btn btn-info" style="line-height: 1">添加到购物车</a></div>
    </div>


</asp:Content>
