<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reinforce.aspx.cs" Inherits="products_Reinforce" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>包裹加固 | 速递中国-可靠,快捷,实惠</title>

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
                    <img width="750" src="/static/img/banner/sudi_shop3.jpg" style="cursor:pointer" title="选取加固方式一"/>
                </div>
                <div style="border: 0px solid #ccc; padding: 0px; margin-top: 30px">
                    <img width="750" src="/static/img/banner/sudi.png" style="cursor:pointer" title="选取加固方式二"/>
                </div>
            </div>

            <div class="col-sm-2 col-xs-12" style="border: 1px solid #69ADDB">
                <div class="panel" style="box-shadow: none">
                    <div style="margin-top: 10px">
                        <img src="/static/media/carriers/logo/bpost-logo.png" style="width: 130px"/>
                    </div>

                    <p><strong>Bpost - UKMail 取件</strong></p>
                    <p>
                        <p>
                            <img src="/static/media/uploads/ukmail_collection.png" width="130" height="78" />
                        </p>
                        <p><span style="font-size: small;">国际快递时间（不包括清关和旺季延误）: 6-10 天 /&nbsp;自带丢件赔偿: &pound;50磅 /&nbsp;包裹上可贴中文地址</span></p>
                    </p>

                    <div id="details">
                        <br />
                        UK Mail:
                                <br />
                        £5.00
                                <br />
                        -----------------
                                <br />

                        <br />
                        包裹 1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;£24.10<br />
                        -----------------
                                                                    
                                
                    </div>
                    <p>总额: <span id="ContentPlaceHolder1_total" class="sz16 bold clrr1">£29.10</span></p>
                    <div style="margin-bottom: 5px">
                        <label style="font-weight: bold">
                            <input type="checkbox" class="agreed" style="margin-top: -3px" />
                            我同意服务条款
                        </label>
                    </div>
                    <input type="submit" name="ctl00$ContentPlaceHolder1$add2cart" value="添加到购物车" id="add2cart" disabled="disabled" class="aspNetDisabled btn btn-primary btn-large" name="order" />

                    <div style="margin: 10px">

                        <a id="ContentPlaceHolder1_LinkButtonEdit" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$LinkButtonEdit&#39;,&#39;&#39;)">添加/修改包裹</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
