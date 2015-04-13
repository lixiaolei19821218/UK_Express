<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reinforce.aspx.cs" Inherits="products_Reinforce" MasterPageFile="~/MasterPage.master" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>包裹加固 | 速递中国-可靠,快捷,实惠</title>
    <script src="../Scripts/jquery-1.8.0.min.js"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-top: 30px">
        <div style="margin-left: auto; margin-right: auto; width: 713px; padding-left: 12px; background: url(/static/img/buzou.png) 1px 0 no-repeat;">
            <a href="/" class="fl buzou" style="padding-left: 20px">快速询价</a>
            <a href="/products/" class="fl buzou" style="padding-left: 20px">产品选择</a>
            <a href="" class="fl buzou active" style="margin: 0 -14px; width: 150px">订单信息</a>
            <a href="#" class="fl buzou" style="padding-left: 45px">在线支付</a>
            <a href="#" class="fl buzou" style="padding-left: 27px">邮件查收</a>
            <div class="cb"></div>
        </div>
    </div>

    <form class="form-inline" id="tree" method="post" runat="server">
        <div class="row" style="background-color: #fff; padding: 20px 15px; margin: 20px 0 30px">
            <div class="col-sm-10 col-xs-12" style="padding-left: 0">
                <div style="border: 0px solid #ccc; padding: 0px; margin-top: 0px">
                    <img width="750" src="/static/img/reinforce/reinforce1.jpg"" style="cursor: pointer" title="选取加固方式一" />
                </div>
                <div style="border: 0px solid #ccc; padding: 0px; margin-top: 0px">
                    <input type="radio" name="reforce" id="reforce1" checked="checked"/>普通加固    
                </div>
                <div style="border: 0px solid #ccc; padding: 0px; margin-top: 30px">
                    <img width="750" src="/static/img/reinforce/reinforce2.jpg" style="cursor: pointer" title="选取加固方式二" />
                </div>
                <div style="border: 0px solid #ccc; padding: 0px; margin-top: 0px">
                    <input type="radio" name="reforce" id="reforce2" />用气泡膜和clear wraps
                </div>
            </div>

            <!-- right column -->
            <input hidden="hidden" value="<%:GetTotalPrice() %>" />
            <div class="col-sm-2 col-xs-12" style="border: 1px solid #69ADDB">
                <div class="panel" style="box-shadow: none">
                    <div style="margin-top: 10px">
                        <img src="<%:ServiceView.PictureLink %>" style="width: 130px" />
                    </div>

                    <p><strong><%:ServiceView.Name %></strong></p>
                    <p>
                        <img src="<%:ServiceView.DiscribePictureLink %>" width="130" height="78" />
                        <span style="font-size: small;">国际快递时间（不包括清关和旺季延误）: 6-10 天 /&nbsp;自带丢件赔偿: &pound;50磅 /&nbsp;包裹上可贴中文地址</span>
                    </p>

                    <div id="details">
                        <br />
                        <%:ServiceView.PickUpCompany %>:
                        <br />
                        <%:ServiceView.GetPickupPrice(Recipients).ToString("c", CultureInfo.CreateSpecificCulture("en-GB")) %>
                        <br />                        
                        
                        <asp:Repeater runat="server" ItemType="Package" SelectMethod="GetAllPackages">
                            <ItemTemplate>
                                <br />
                                包裹 <%#Container.ItemIndex + 1 %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#GetPackagePrice(Item).ToString("c", CultureInfo.CreateSpecificCulture("en-GB")) %><br />
                                -----------------
                            </ItemTemplate>
                        </asp:Repeater>                        
                        <br />
                        <br />
                        <p id="reiforcePrice">加固服务：<%:10.ToString("c", CultureInfo.CreateSpecificCulture("en-GB")) %></p>                                               
                    </div>
                    <br />
                    <p>总额: <span id="total" class="sz16 bold clrr1"><%:GetTotalPrice().ToString("c", CultureInfo.CreateSpecificCulture("en-GB")) %></span></p>
                    <div style="margin-bottom: 5px">
                        <label style="font-weight: bold">
                            <input type="checkbox" id="needReforce" class="agreed" checked="checked" style="margin-top: -3px" />
                            需要加固服务
                        </label>
                    </div>
                    <input type="submit" value="添加到购物车" id="add2cart" class="aspNetDisabled btn btn-primary btn-large" name="order" />


                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#needReforce').change(function (e) {
                $.ajax({
                    //要用post方式   
                    type: "Post",
                    //方法所在页面和方法名   
                    url: "/products/reinforce.aspx/SayHello",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        //返回的数据用data.d获取内容   
                        alert(data.d);
                    },
                    error: function (err) {
                        alert(err);
                    }
                });

                if ($('#needReforce')[0].checked) {
                    $('#reforce1')[0].disabled = false;
                    $('#reforce2')[0].disabled = false;
                    $('#reiforcePrice')[0].innerText = "加固服务：10.0";
                }
                else {
                    $('#reforce1')[0].disabled = true;
                    $('#reforce2')[0].disabled = true;
                    $('#reiforcePrice')[0].innerText = "加固服务：0.0";
                }
            });

            $('#reforce1').change(function (e) {

                if ($('#reforce1')[0].checked)
                {
                    $('#total')[0].innerText = "加固服务：1.0";
                }                
            });

            $('#reforce2').change(function (e) {

                if ($('#reforce2')[0].checked) {
                    $('#total')[0].innerText = "加固服务：2.0";
                }
            });
           
        });
    </script>
</asp:Content>
