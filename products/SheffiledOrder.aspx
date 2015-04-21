<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SheffiledOrder.aspx.cs" Inherits="products_SheffiledOrder" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>谢菲尔德地区服务订单信息 | 速递中国-可靠,快捷,实惠</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form class="form-inline" id="tree" method="post" runat="server">
        <div class="row" style="background-color: #fff; padding: 20px 15px; margin: 20px 0 30px">            
            <!-- main --> 
            <div class="col-sm-10 col-xs-12" style="padding-left: 0">
                <!-- alert messages -->
                <div>
                    <div class="alert alert-success">
                        <a class="close" href="#" onclick="$(this).parent().fadeOut('fast'); return false;">×</a>
                        <ul>
                            <li>收件人信息可以直接中文输入, 拼音地址同步转换呈现。 输入的中文地址会显示在运单条形码文件中， 最大化节省您的时间。</li>
                            <li>中国的手机必须为11位数字, 邮编必须为6位数字。</li>
                            <li>一次下单可添加发往多个收件人的包裹，所产生的文档一键打印即可。</li>
                        </ul>
                    </div>                  
                </div>

                <ol id="root" style="padding-left: 0">
                    <li style="list-style: none">

                        <!-- sender address -->
                        <div class="bg1 rds1 clrw1" style="height: 38px; background-position: 0 0; background-repeat: repeat-x; padding: 10px 20px">
                            发件人地址（英国）
                        </div>

                        <div class="rds2" style="border: 1px solid #ddd; background-color: #F2F8FC; padding: 5px 1px">
                            <div style="float: left; margin: 5px" class="control-group ">
                                <label for="id_billing_detail_name">姓名</label>
                                <input class="input-medium" id="id_billing_detail_name" maxlength="25" name="billing_detail_name" style="width: 115px" type="text" value="" required="required"/>


                            </div>
                            <div style="float: left; margin: 5px" class="control-group ">
                                <label for="id_billing_detail_street">地址</label>
                                <input class="input-xxlarge" id="id_billing_detail_street" maxlength="24" name="billing_detail_street" style="width: 180px" type="text" value="" required="required"/>




                            </div>
                            <div style="float: left; margin: 5px" class="control-group ">
                                <input class="input-medium" id="id_billing_detail_street2" maxlength="24" name="billing_detail_street2" style="width: 180px" type="text" value="" required="required"/>




                            </div>
                            <div style="float: left; margin: 5px" class="control-group ">
                                <input class="input-medium" id="id_billing_detail_street3" maxlength="24" name="billing_detail_street3" style="width: 173px" type="text" value="" required="required"/>




                            </div>
                            <div style="clear: both"></div>

                            <div style="float: left; margin: 5px" class="control-group ">
                                <label for="id_billing_detail_city">城市</label>
                                <input class="input-medium" id="id_billing_detail_city" maxlength="24" name="billing_detail_city" style="width: 115px" type="text" value="" required="required"/>




                            </div>
                            <div style="float: left; margin: 5px" class="control-group ">
                                <label for="id_billing_detail_phone">电话</label>
                                <input class="input-medium" id="id_billing_detail_phone" maxlength="15" name="billing_detail_phone" style="width: 180px" type="text" value="" required="required"/>




                            </div>
                            <div style="float: left; margin: 5px" class="control-group ">
                                <label for="id_billing_detail_postcode">邮编</label>
                                <input class="input-medium" id="id_billing_detail_postcode" maxlength="8" name="billing_detail_postcode" style="width: 100px" type="text" value="" required="required"/>




                            </div>

                            <div style="float: left; margin: 5px">
                                <button type="button" onclick="cp_obj_1.doLookup()">查找地址</button>
                            </div>

                            <div id="crafty_postcode_result_display_1" style="float: left; margin: 5px"></div>

                            <div style="clear: both; display: none">
                                <div style="float: left; margin: 5px" class="control-group ">
                                    <label for="id_billing_detail_email">Email</label>
                                    <input class="input-medium" id="id_billing_detail_email" maxlength="50" name="billing_detail_email" style="width: 180px" type="text" value="" />




                                </div>
                            </div>

                            <div style="clear: both"></div>
                        </div>

                        <div style="margin-top: 10px;">
                            <div class="clrw1 rds1" style="background-color: #7F7983; padding: 5px 20px">
                                取件安排
                            </div>
                            <div class="rds2" style="padding: 5px 2px; border: 1px solid #ddd; background-color: #F2F8FC">
                                <div style="float: left; margin: 5px; font-weight: bold; margin-right: 10px">取件公司 <span id="pickUpCompany" style="background-color: #e1e1e1; padding: 2px 4px; font-size: 12px; font-weight: normal">999parcel</span></div>

                                <div style="float: left; margin: 5px" class="control-group ">
                                    <label for="id_pickup_time_0">日期</label>
                                    <div class="input-append date form_datetime">
                                        <input class="input-medium" foo_bar="prevent_readonly" id="id_pickup_time_0" name="pickup_time_0" type="text" value=""  required="required"/>
                                        <span id="sp" class="add-on"><i class="icon-th"></i></span>
                                    </div>                                   
                                </div>
                                <div style="float: left; margin: 5px; font-weight: bold; margin-left: 10px; margin-right: 10px">
                                    时间

                    
                        <span style="background-color: #BFBFBF; padding: 2px 4px; font-size: 12px; font-weight: normal">9:00-18:00</span>


                                </div>

                                <div style="float: left; margin: 5px" class="control-group ">
                                    <label for="id_additional_instructions">备注</label>
                                    <input class="input-xxlarge" id="id_additional_instructions" maxlength="50" name="additional_instructions" style="width: 290px" type="text" />




                                </div>
                                <div style="clear: both"></div>
                            </div>
                        </div>
                    </li>
                    </ol>
                <!-- terms -->
                <div style="border: 1px solid #ccc; padding: 10px; margin-top: 30px">

                    <div class="sz16 bold" style="padding: 10px 0">特别注意事项及服务条款</div>
                    <p>
                        在您下单之前，您需要认真阅读并同意以下条款，确认您的快递物品没有违反国际运输相关条例，并且检查您所申报的物品是否符合相关
        的保险赔偿条款。
                    </p>
                    <p>
                        １、我已经阅读并同意<a href="/conditions_terms/" target="_blank" style="color: #f00; float: none">速递中国服务条款</a>，<a href="/privacy_policy/" target="_blank" style="color: #f00; float: none">个人隐私保护条款</a>，并且确认所快递物品符合相应国家出口及违禁品控制条例。<br />
                        ２、<span style="color: #f00; float: none">我同意若因本人提供的包裹尺寸或重量与运营商的测量不符，而导致额外费用，速递中国有权从我所提供的银行卡信息对应的银行账户内扣除相应费用。</span><br />
                        ３、我同意由本人或相应收件人承担支付取货国家所产生的所有额外关税。（<a href="http://www.customs.gov.cn/publish/portal0/tab3889/module1188/info362458.htm" target="_blank" style="color: #f00; float: none">详见中国入境个人邮寄物品完税价格表</a>）<br />
                        ４、我确认所提供的包裹内物品信息（种类，数量，价格）与实际包裹内物品相符。
                    </p>


                    <label style="font-weight: bold">
                        <input type="checkbox" class="agreed" style="margin-top: -3px" />
                        我同意服务条款</label>
                </div>
              
            </div>

            <!-- right column -->            
            <div class="col-sm-2 col-xs-12" style="border: 1px solid #69ADDB">
                <div class="panel" style="box-shadow: none">
                    <div style="margin-top: 10px">
                        <img src="" style="width: 130px" />
                    </div>

                    <p><strong></strong></p>
                    <p>
                        <img src="" width="130" height="78" />
                        <span style="font-size: small;">国际快递时间（不包括清关和旺季延误）: 6-10 天 /&nbsp;自带丢件赔偿: &pound;50磅 /&nbsp;包裹上可贴中文地址</span>
                    </p>

                    <div id="details">
                        <br />
                        
                        <br />
                        
                        <br />                        
                        
                                            
                        <br />
                        <br />
                        <p>加固服务: &nbsp;&nbsp;£<span id="reiforcePrice"></span></p>                                               
                    </div>
                    <br />
                    <p>总额:&nbsp;<span class="sz16 bold clrr1">£</span><span id="total" class="sz16 bold clrr1"></span></p>
                    <div style="margin-bottom: 5px">
                        <label style="font-weight: bold">
                            <input type="checkbox" id="needReforce" class="agreed" checked="checked" style="margin-top: -3px" />
                            需要加固服务
                        </label>
                    </div>
                    <asp:Button runat="server" name="order" ClientIDMode="Static" ID="add2cart" CssClass ="btn btn-primary btn-large" Text="添加到购物车"/>
                </div>
            </div>
        </div>
    </form>
</asp:Content>