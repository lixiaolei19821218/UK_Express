<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sheffield.aspx.cs" Inherits="products_Sheffield" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>谢菲尔德地区服务 | 速递中国-可靠,快捷,实惠</title>
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
                    <tr>
                        <td style="vertical-align: middle">
                            <img src="/static/img/999parcel.png" style="float: left; min-height: 40px; max-width: 60px; margin-right: 3px" />
                            7kg包裹奶粉箱
                        </td>
                        <td>
                            <img src="/static/media/uploads/service.png" width="350" height="78" />
                            <p><span style="font-size: small;">7kg包裹奶粉箱包装10镑+运费</span></p>
                            <p><span style="font-size: small;">可选荷兰邮政，比利时邮政，EMS，Parcelforce</span></p>
                        </td>
                        <td style="vertical-align: middle; text-align: center">
                            <i class="glyphicon glyphicon-ok">
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem>荷兰邮政</asp:ListItem>
                                    <asp:ListItem>比利时邮政</asp:ListItem>
                                    <asp:ListItem>EMS</asp:ListItem>
                                    <asp:ListItem>Parcelforce</asp:ListItem>
                                </asp:DropDownList></i>
                        </td>
                        <td style="vertical-align: middle; text-align: center">
                            
                                <input type="number" style="width: 40px">
                        </td>
                        <td style="vertical-align: middle; text-align:center">                            
                            £258.80
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle">
                            <img src="/static/img/999parcel.png" style="float: left; min-height: 40px; max-width: 60px; margin-right: 3px" />
                            14kg中箱包裹
                        </td>
                        <td>
                            <img src="/static/media/uploads/service.png" width="350" height="78" />
                            <p><span style="font-size: small;">14kg中箱包裹包装15镑+运费</span></p>
                            <p><span style="font-size: small;">可选荷兰邮政，比利时邮政，EMS，Parcelforce</span></p>
                        </td>
                        <td style="vertical-align: middle; text-align: center">
                            <i class="glyphicon glyphicon-ok">
                                <asp:DropDownList ID="DropDownList2" runat="server">
                                    <asp:ListItem>荷兰邮政</asp:ListItem>
                                    <asp:ListItem>比利时邮政</asp:ListItem>
                                    <asp:ListItem>EMS</asp:ListItem>
                                    <asp:ListItem>Parcelforce</asp:ListItem>
                                </asp:DropDownList></i>
                        </td>
                        <td style="vertical-align: middle; text-align: center">
                           
                                <input type="number" style="width: 40px">
                        </td>
                        <td style="vertical-align: middle; text-align:center">                            
                            £258.80
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle">
                            <img src="/static/img/999parcel.png" style="float: left; min-height: 40px; max-width: 60px; margin-right: 3px" />
                            24kg大包裹
                        </td>
                        <td>
                            <img src="/static/media/uploads/service.png" width="350" height="78" />
                            <p><span style="font-size: small;">24kg大包裹包装18镑+运费</span></p>
                            <p><span style="font-size: small;">可选荷兰邮政，比利时邮政，EMS，Parcelforce</span></p>
                        </td>
                        <td style="vertical-align: middle; text-align: center">
                            <i class="glyphicon glyphicon-ok">
                                <asp:DropDownList ID="DropDownList3" runat="server">
                                    <asp:ListItem>荷兰邮政</asp:ListItem>
                                    <asp:ListItem>比利时邮政</asp:ListItem>
                                    <asp:ListItem>EMS</asp:ListItem>
                                    <asp:ListItem>Parcelforce</asp:ListItem>
                                </asp:DropDownList></i>
                        </td>
                        <td style="vertical-align: middle; text-align: center">
                            
                                <input type="number" style="width: 40px">
                        </td>
                        <td style="vertical-align: middle; text-align:center">                            
                            £258.80
                        </td>
                    </tr>
                </tbody>
            </table>
        </form>
    </div>

    <div >
                
          <div style="float:left">      总金额: <strong class="total-price" style="color: #f00">£258.80</strong></div>
       <div style="margin-top:-5px; margin-bottom:10px; float:right"> <a href="/" class="btn btn-info" style="line-height: 1">添加到购物车</a></div>
        </div>

    
</asp:Content>
