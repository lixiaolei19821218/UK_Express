<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recharge.aspx.cs" Inherits="accounts_UserCentre_Recharge" MasterPageFile="~/accounts/UserCentre/UserCentre.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="Server">
    账户充值
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <div class="uitopg">
        <h3 class="uitopg-title mt10">
            <span class="rt"><a href="/index.php?c=bank&a=user_list" class="back">返回</a></span>
            账户充值
        </h3>
        <div class="form">
            <form id="chargeForm" action="/index.php?c=charge&a=user_submit" method="post" autocomplete="on">
                <table class="table-form">
                    <tr>
                        <th><em>*</em>方式：</th>
                        <td>
                            <span class="f14">
                                <input type="radio" name="type" id="type_3" checked value="3">
                                PayPal/信用卡/借记卡&nbsp;<input type="radio" name="type" id="type_1" value="1">
                                英镑支付(转帐或存现)&nbsp;<input type="radio" name="type" id="type_2" value="2">
                                人民币支付(淘宝拍)&nbsp;<input type="radio" name="type" id="type_4" value="4">
                                支付宝&nbsp;</span>
                            <span id="typeTip"></span>
                            <div id="bankpay" class="message" style="display: none;">
                                <ul>
                                    <li>请转帐或存入英镑现金至公司帐户：</li>
                                    <li><em>·</em><span>Bank：NATWEST</span><span>Account：10902619</span><span>Sort Code：602469</span><span>Name: J CHEN</span></li>
                                    <li><em>·</em>转帐请输入以下信息，并在REF处写上您的邮箱@前部份，以便查帐。例如：123@qq.com，Ref：123</li>
                                    <li><em>·</em>存现金时，请一定告诉银行职员写上您的备注REF。</li>
                                    <li><em>·</em>转帐好后请截图保存并上传给我们。存现的请保留收据，如能拍照上传最好。这样便于我们更快的审核。</li>
                                    <li><em>·</em>审核时间是工作日周一至周五10点至18点。您确认后请给我们1至2小时进行查帐并审核，超过两小时还未看到冲值余额的请联系我们客服查询。18点后冲值的，我们将会在第二个工作日为您审核。</li>
                                    <li><em>·</em>冲值一次超过1万镑的请联系我们客服，我们会按单独汇率计算。</li>
                                </ul>
                            </div>
                            <div id="taobaopay" style="display: none;">
                                <div class="message">
                                    <ul>
                                        <li><em>·</em>请按1比9.57的汇率计算人民币数额到以下地址拍下等额数量即可。如冲值100镑换算人民币是957元，请拍957件即可。拍后请立即确认付款，我们审核通过后您就可以在系统内查到您的冲值余额。</li>
                                        <li><em>·</em><a href="http://item.taobao.com/item.htm?spm=a230r.1.14.4.jQDViP&id=19933634526&initiative_new=1" target="_blank">http://item.taobao.com/item.htm?spm=a230r.1.14.4.jQDViP&id=19933634526&initiative_new=1</a></li>
                                        <li><em>·</em>审核时间是工作日周一至周五10点至18点。您确认后请给我们1至2小时进行查帐并审核，超过两小时还未看到冲值余额的请联系我们客服查询。18点后冲值的，我们将会在第二个工作日为您审核。</li>
                                        <li><em>·</em>冲值一次超过1万镑的请联系我们客服，我们会按单独汇率计算。</li>
                                    </ul>
                                </div>
                                <div class="mt10">
                                    <table class="table-form">
                                        <tr>
                                            <th style="width: 60px;"><em>*</em>旺旺：</th>
                                            <td>
                                                <input type="text" name="ww" id="ww" value="" size="20" class="input-text" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div id="paypalpay" class="message">
                                <ul>
                                    <li><em>·</em>凡使用PAYPAL进行冲值的客户，均可享受冲100镑送3镑的优惠活动。（星弛折扣与冲值奖励不能同时享受）</li>
                                    <li><em>·</em>所有客户使用PAYPAL冲值时均无需支付手续费。</li>
                                    <li><em>·</em>星弛允许客户退回星弛余额里的款项，只需支付退款手续费3%（手续费不足3镑的按3镑收），如享受过奖励活动的，需扣除活动奖励金额。</li>
                                    <li><em>·</em>此活动的最终解释权属星弛快递公司(XC LINK LTD)</li>
                                </ul>
                            </div>
                            <div id="alipay" class="message" style="display: none;">
                                <ul>
                                    <li><em>·</em>淘宝拍付汇率以星弛官网显示为准， 支付宝充值以支付宝实时汇率为准</li>
                                    <li><em>·</em>凡使用支付宝进行冲值的客户，均可享受冲100镑送3镑的优惠活动。（星弛折扣与冲值奖励不能同时享受）</li>
                                    <li><em>·</em>所有客户使用支付宝冲值时均无需支付手续费。</li>
                                    <li><em>·</em>星弛允许客户退回星弛余额里的款项，只需支付退款手续费3%（手续费不足3镑的按3镑收），如享受过奖励活动的，需扣除活动奖励金额。</li>
                                    <li><em>·</em>此活动的最终解释权属星弛快递公司(XC LINK LTD)</li>
                                </ul>
                            </div>
                            <div id="worldpay" class="message" style="display: none;">
                                <ul>
                                    <li><em>·</em>所有支付最终结算货币是英镑,汇率内天随中国银行汇率变动 （We only accept £GBP.)</li>
                                    <li><em>·</em>凡使用worldpay进行冲值的客户，均可享受冲100镑送3镑的优惠活动。（星弛折扣与冲值奖励不能同时享受）</li>
                                    <li><em>·</em>所有客户使用worldpay冲值时均无需支付手续费。</li>
                                    <li><em>·</em>星弛允许客户退回星弛余额里的款项，只需支付退款手续费3%（手续费不足3镑的按3镑收），如享受过奖励活动的，需扣除活动奖励金额。</li>
                                    <li><em>·</em>此活动的最终解释权属星弛快递公司(XC LINK LTD)</li>
                                </ul>
                                <input type="hidden" name="testMode" value="100">
                                <input type="hidden" name="instId" value="1019582">
                                <input type="hidden" name="cartId" value="13570_735534185@qq.com_1430208747">
                                <input type="hidden" name="currency" value="GBP">
                                <input type="hidden" id="amount" name="amount" value="">
                                <input type="hidden" name="desc" value="">
                                <input type="hidden" name="name" value="">
                            </div>
                        </td>
                    </tr>
                    <tr id="car_type" style="display: none;">
                        <th>卡类型：</th>
                        <td>
                            <label class="ib">
                                <input type="radio" name="paymentType" value="VISA" checked="checked">
                                <img src="/tpl/t4/image/visa.gif">
                            </label>
                            <label class="ib">
                                <input type="radio" name="paymentType" value="MSCD">
                                <img src="/tpl/t4/image/mastercard.gif">
                            </label>
                            <label class="ib">
                                <input type="radio" name="paymentType" value="VIED">
                                <img src="/tpl/t4/image/visa_electron.gif">
                            </label>
                            <label class="ib">
                                <input type="radio" name="paymentType" value="VISD">
                                <img src="/tpl/t4/image/visa_debit.gif">
                            </label>
                            <label class="ib">
                                <input type="radio" name="paymentType" value="MAES">
                                <img src="/tpl/t4/image/maestro.gif">
                            </label>
                            <label class="ib">
                                <input type="radio" name="paymentType" value="JCB">
                                <img src="/tpl/t4/image/jcb.gif">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <th><em>*</em>金额：</th>
                        <td>
                            <input type="text" name="mny" id="mny" value="" size="10" class="input-text" />£
				<span id="mnyTip"></span>
                            <div id="mnyRMB"></div>
                        </td>
                    </tr>
                    <tr id="image" style="display: none;">
                        <th>凭证：</th>
                        <td>
                            <link rel="stylesheet" type="text/css" href="http://express-xc.co.uk/statics/js/uploadify/uploadify.css" />
                            <script type="text/javascript" src="http://express-xc.co.uk/statics/js/uploadify/jquery.uploadify.min.js?v=3503"></script>
                            <script>
                                $(function () {
                                    $('#uploadify-257072931300').uploadify({
                                        swf: '/statics/js/uploadify/uploadify.swf',
                                        formData: { 'api': 'uploadify|pic|single', 'token': '6b79a77180e9ec3a7ca351ebe54641a2' },
                                        uploader: '/index.php',
                                        method: 'get',
                                        buttonText: '选择图片',
                                        queueID: 'fileQueue-1',
                                        fileTypeDesc: '支持的格式：',
                                        fileTypeExts: '*.jpg;*.gif;*.jpeg;*.png',
                                        multi: false,
                                        fileSizeLimit: '3MB',
                                        queueSizeLimit: 1,
                                        uploadLimit: 99,
                                        removeTimeout: 1,
                                        fileObjName: 'Filedata',
                                        'onUploadStart': function (file) {
                                            if ($('#upload-file-257072931300').html() != '') {
                                                alert('请先删除上传的文件');
                                                $('#uploadify-257072931300').uploadify('cancel');
                                            }
                                        },
                                        onUploadSuccess: function (file, data, response) {
                                            $('#upload-file-257072931300').html(data);
                                        }
                                    });
                                });
                                $('#removeUploadFile').live('click', function () {
                                    $(this).parents('span').remove()
                                });
                            </script>
                            <input type="file" id="uploadify-257072931300" />
                            <div id="fileQueue-1"></div>
                            <div class="upload-file" id="upload-file-257072931300"></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="text-c">
                            <input type="submit" value="提交" id="dosubmit" class="btn btn-1" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>

    <script>
        $(function () {
            //表单提交
            $.formValidator.initConfig({
                wideWord: false,
                formID: "chargeForm",
                onSuccess: function () {
                    if ($('#type').val() == 2 && $('#ww').val() == '') {
                        $.dialog({ icon: 'warning', time: 3, content: '淘宝充值请填写旺旺号' });
                        return false;
                    } else if ($("input[name='type']:checked").val() == 5) {
                        $('#amount').val($('#mny').val());
                    }
                }
            });
            $('#mny').formValidator({
                onShow: "",
                onFocus: "正数，每次充值金额：0 - 5000镑"
            }).regexValidator({
                regExp: "num",
                dataType: "enum"
            });
        });
        // 切换旺旺
        $("[id^='type_']").click(function () {
            var v = $(this).val();
            if (v == 1) {
                $('#taobaopay,#paypalpay,#alipay,#worldpay,#car_type').hide();
                $('#bankpay,#image').show();
            } else if (v == 2) {
                $('#bankpay,#paypalpay,#alipay,#worldpay,#car_type').hide();
                $('#taobaopay,#image').show();
            } else if (v == 3) {
                $('#bankpay,#taobaopay,#image,#alipay,#worldpay,#car_type').hide();
                $('#paypalpay').show();
            } else if (v == 4) {
                $('#bankpay,#taobaopay,#image,#paypalpay,#worldpay,#car_type').hide();
                $('#alipay').show();
            } else if (v == 5) {
                $('#bankpay,#taobaopay,#image,#paypalpay,#alipay').hide();
                $('#worldpay,#car_type').show();
            }
            if (v == 5) {
                // worldpay
                $('#chargeForm').attr("action", "https://secure-test.worldpay.com/wcc/purchase");
            } else {
                $('#chargeForm').attr("action", "/index.php?c=charge&a=user_submit");
            }
        });
        // 金额换算
        $("#mny").bind('input propertychange', function () {
            var val = parseFloat($(this).val());
            if (isNaN(val)) {
                $(this).val('');
            } else {
                var rmb = val * 9.57;
                $('#mnyRMB').text('RMB：' + rmb.toFixed(0) + '元');
            }
        });
    </script>
</asp:Content>
