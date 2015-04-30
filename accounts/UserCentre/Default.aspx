
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />  
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>账户充值</title>
<link href="http://express-xc.co.uk/statics/css/reset.css?v=1.1" rel="stylesheet" type="text/css" />
<link href="http://express-xc.co.uk/statics/css/global_v2.0.css" rel="stylesheet" type="text/css" />
<link href="http://express-xc.co.uk/tpl/t4/style.css" rel="stylesheet" type="text/css" />
<link href="http://express-xc.co.uk/statics/js/artDialog/skins/default.css" rel="stylesheet" type="text/css" />
<script src="http://express-xc.co.uk/statics/js/jquery.min.js"></script>
<script src="http://express-xc.co.uk/statics/js/artDialog/jquery.artDialog.js"></script>
<script src="http://express-xc.co.uk/statics/js/artDialog/iframeTools.js"></script>
<script src="http://express-xc.co.uk/statics/js/formValidator/formValidator.min.js"></script>
<script src="http://express-xc.co.uk/statics/js/formValidator/formValidator.regex.js"></script>
</head>
<body>
<div class="top-box">
	<div class="top-fixed">
		<div class="top">
			<ul class="rt">
				<li><a href="/index.php?c=home&a=index">首页</a><span class="gap">|</span></li>
				<li><a href="/index.php?c=home&a=q">帮助中心</a></li>
			</ul>
			<ul>
				<li>欢迎来到星弛快递，<font class="col-f60">735534185@qq.com</font><span class="gap">|</span></li>
				<li><a href="/index.php?c=member&a=logout">退出</a></li>
			</ul>
		</div>
	</div>
</div>
<div class="logo-box">
	<a href="javascript:;"><img src="http://express-xc.co.uk/tpl/t4/image/logo.jpg" width="228" height="40" alt="星弛管理中心" /></a>
</div>

<div id="nav">
    <ul class="menu clearfix">
        <li class="first-node"><a href="/index.php?c=member&a=user_profile" class="on"><span>我的帐户</span></a></li>
        <li><a href="/index.php?c=order&a=user_list" class=""><span>Gpr</span></a></li>
       	<li><a href="/index.php?c=ipe_order&a=user_list" class=""><span>Ipe</span></a></li>
        <li><a href="/index.php?c=bpost_order&a=user_list" class=""><span>星弛专线</span></a></li>
        <li><a href="/index.php?c=postnl_order&a=user_list" class=""><span>Postnl</span></a></li>
		<li><a href="/index.php?c=shop_pay&a=user_list" class=""><span>商城</span></a></li>
    	<li><a href="/index.php?c=daigou_order&a=user_list" class=""><span>代购</span></a></li>
    </ul>
</div>

<div id="container">
	<div id="sidebar">
		<div class="uitopb uitopb-border">
						<h3>我的账户</h3>
			<ul class="link-list">
				<li><a href="/index.php?c=bank&a=user_list" class="">账户金额</a></li>
				<li><a href="/index.php?c=charge&a=user_form" class="on">我要充值</a></li>
				<li><a href="/index.php?c=bill_clear&a=user_list" class="">补款记录</a></li>
				<li><a href="/index.php?c=spke&a=user_list" class="">星弛箱子</a></li>
				<li><a href="http://express-xc.co.uk/oa/index.php?c=claim&a=form&ek=ek" target="_blank">索赔中心</a></li>
				<li><a href="/index.php?c=member&a=user_profile" class="">个人资料</a></li>
				<li><a href="/index.php?c=member&a=user_reset" class="">重置密码</a></li>				
			</ul>
										        	        				
								</div>
	</div>
	<div id="container-main">
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
				<span class="f14"><input type="radio" name="type" id="type_3" checked value="3"> PayPal/信用卡/借记卡&nbsp;<input type="radio" name="type" id="type_1"  value="1"> 英镑支付(转帐或存现)&nbsp;<input type="radio" name="type" id="type_2"  value="2"> 人民币支付(淘宝拍)&nbsp;<input type="radio" name="type" id="type_4"  value="4"> 支付宝&nbsp;</span>
				<span id="typeTip"></span>
				<div id="bankpay" class="message" style="display:none;">
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
				<div id="taobaopay" style="display:none;">
					<div class="message">
						<ul>
														<li><em>·</em>请按1比9.72的汇率计算人民币数额到以下地址拍下等额数量即可。如冲值100镑换算人民币是972元，请拍972件即可。拍后请立即确认付款，我们审核通过后您就可以在系统内查到您的冲值余额。</li>
							<li><em>·</em><a href="http://item.taobao.com/item.htm?spm=a230r.1.14.4.jQDViP&id=19933634526&initiative_new=1" target="_blank">http://item.taobao.com/item.htm?spm=a230r.1.14.4.jQDViP&id=19933634526&initiative_new=1</a></li>
							<li><em>·</em>审核时间是工作日周一至周五10点至18点。您确认后请给我们1至2小时进行查帐并审核，超过两小时还未看到冲值余额的请联系我们客服查询。18点后冲值的，我们将会在第二个工作日为您审核。</li>
							<li><em>·</em>冲值一次超过1万镑的请联系我们客服，我们会按单独汇率计算。</li>
						</ul>
					</div>
					<div class="mt10">
						<table class="table-form">
						<tr><th style="width:60px;"><em>*</em>旺旺：</th><td><input type="text" name="ww" id="ww" value="" size="20" class="input-text" /></td></tr>
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
				<div id="alipay" class="message" style="display:none;">
					<ul>
						<li><em>·</em>淘宝拍付汇率以星弛官网显示为准， 支付宝充值以支付宝实时汇率为准</li>
						<li><em>·</em>凡使用支付宝进行冲值的客户，均可享受冲100镑送3镑的优惠活动。（星弛折扣与冲值奖励不能同时享受）</li>
						<li><em>·</em>所有客户使用支付宝冲值时均无需支付手续费。</li>
						<li><em>·</em>星弛允许客户退回星弛余额里的款项，只需支付退款手续费3%（手续费不足3镑的按3镑收），如享受过奖励活动的，需扣除活动奖励金额。</li>
						<li><em>·</em>此活动的最终解释权属星弛快递公司(XC LINK LTD)</li>
					</ul>
				</div>
				<div id="worldpay" class="message" style="display:none;">
					<ul>
						<li><em>·</em>所有支付最终结算货币是英镑,汇率内天随中国银行汇率变动 （We only accept £GBP.)</li>
						<li><em>·</em>凡使用worldpay进行冲值的客户，均可享受冲100镑送3镑的优惠活动。（星弛折扣与冲值奖励不能同时享受）</li>
						<li><em>·</em>所有客户使用worldpay冲值时均无需支付手续费。</li>
						<li><em>·</em>星弛允许客户退回星弛余额里的款项，只需支付退款手续费3%（手续费不足3镑的按3镑收），如享受过奖励活动的，需扣除活动奖励金额。</li>
						<li><em>·</em>此活动的最终解释权属星弛快递公司(XC LINK LTD)</li>
					</ul>
					<input type="hidden" name="testMode" value="100">
					<input type="hidden" name="instId" value="1019582">
					<input type="hidden" name="cartId" value="13570_735534185@qq.com_1430360286">
					<input type="hidden" name="currency" value="GBP">
					<input type="hidden" id="amount" name="amount" value="">
					<input type="hidden" name="desc" value="">
					<input type="hidden" name="name" value="">
				</div>
			</td>
		</tr>
		<tr id="car_type" style="display:none;">
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
		<tr id="image" style="display:none;">
			<th>凭证：</th>
			<td><link rel="stylesheet" type="text/css" href="http://express-xc.co.uk/statics/js/uploadify/uploadify.css"/><script type="text/javascript" src="http://express-xc.co.uk/statics/js/uploadify/jquery.uploadify.min.js?v=4237"></script><script>
			                                                                                                                                                                                                                                            $(function () {
			                                                                                                                                                                                                                                                $('#uploadify-968021674600').uploadify({
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
			                                                                                                                                                                                                                                                        if ($('#upload-file-968021674600').html() != '') {
			                                                                                                                                                                                                                                                            alert('请先删除上传的文件');
			                                                                                                                                                                                                                                                            $('#uploadify-968021674600').uploadify('cancel');
			                                                                                                                                                                                                                                                        }
			                                                                                                                                                                                                                                                    },
			                                                                                                                                                                                                                                                    onUploadSuccess: function (file, data, response) {
			                                                                                                                                                                                                                                                        $('#upload-file-968021674600').html(data);
			                                                                                                                                                                                                                                                    }
			                                                                                                                                                                                                                                                });
			                                                                                                                                                                                                                                            });
			                                                                                                                                                                                                                                            $('#removeUploadFile').live('click', function () {
			                                                                                                                                                                                                                                                $(this).parents('span').remove()
			                                                                                                                                                                                                                                            });
</script>
<input type="file" id="uploadify-968021674600" />
<div id="fileQueue-1"></div>
<div class="upload-file" id="upload-file-968021674600"></div>
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
            var rmb = val * 9.72;
            $('#mnyRMB').text('RMB：' + rmb.toFixed(0) + '元');
        }
    });
</script>	</div>
</div>
<div id="footer">
	<div class="sev">
		<dl>
			<dt><b class="fore1"></b>帮助指南</dt>
			<dd><a href="javascript:;">怎样购物</a></dd>
			<dd><a href="javascript:;">选择服务</a></dd>
			<dd><a href="javascript:;">如何打包</a></dd>
			<dd><a href="javascript:;">是否包税</a></dd>
			<dd><a href="javascript:;">订单状态说明</a></dd>
		</dl>
		<dl>
			<dt><b class="fore2"></b>星弛服务</dt>
			<dd><a href="javascript:;">英国邮政</a></dd>
			<dd><a href="javascript:;">荷兰邮政</a></dd>
			<dd><a href="javascript:;">EMS包清</a></dd>
			<dd><a href="javascript:;">香港包清</a></dd>
		</dl>
		<dl>
			<dt><b class="fore3"></b>关于付款</dt>
			<dd><a href="javascript:;">银行转账</a></dd>
			<dd><a href="javascript:;">支付宝转账</a></dd>
			<dd><a href="javascript:;">站内付款</a></dd>
		</dl>
		<dl>
			<dt><b class="fore4"></b>售后服务</dt>
			<dd><a href="javascript:;">售后政策</a></dd>
			<dd><a href="javascript:;">退款说明</a></dd>
			<dd><a href="javascript:;">取消订单</a></dd>
			<dd><a href="javascript:;">联系客服</a></dd>
		</dl>
		<dl>
			<dt><b class="fore5"></b>关于星弛</dt>
			<dd><a href="/index.php?c=content&a=show&id=6" target="_blank">星弛简介</a></dd>
			<dd><a href="javascript:;">大客户优惠</a></dd>
			<dd><a href="/index.php?c=content&a=show&id=310" target="_blank">Contact Us</a></dd>
			<dd><a href="/index.php?c=content&a=show&id=311" target="_blank">Privacy Policy</a></dd>
			<dd><a href="/index.php?c=content&a=show&id=312" target="_blank">Terms & Conditions</a></dd>
		</dl>
	</div>
	<div class="copyright">
		© Copyright 2010-2013 英国星弛国际快递. All rights reserved.
	</div>
</div>
<div style="position:fixed;top:250px;right:10px;text-align:center;font-size:14px;font-family:微软雅黑">
	<div style="margin:10px 0 0 0;"><script type="text/javascript" src="http://static.b.qq.com/account/bizqq/js/wpa.js?wty=1&type=1&kfuin=800056001&ws=http%3A%2F%2Fwww.express-xc.co.uk&btn1=%E4%BC%81%E4%B8%9AQQ%E4%BA%A4%E8%B0%88&aty=0&a=&key=%0CiWf%041Ta%032%03cWgW%3E%03bSlUoV1ScQ7%05fR6%0Bf%045%051"></script></div>
</div>
<div class="hidden"><script src="http://s11.cnzz.com/stat.php?id=5312818&web_id=5312818&show=pic2" language="JavaScript"></script></div><script>
                                                                                                                                            //全选
                                                                                                                                            $('#chooseAll').click(function () {
                                                                                                                                                $(":checkbox[name^='id_s']").attr('checked', true);
                                                                                                                                            });
                                                                                                                                            // 反选
                                                                                                                                            $('#chooseReverse').click(function () {
                                                                                                                                                $(":checkbox[name^='id_s']").each(function () {
                                                                                                                                                    if ($(this).attr('checked') == true) {
                                                                                                                                                        $(this).attr('checked', false);
                                                                                                                                                    } else {
                                                                                                                                                        $(this).attr('checked', true);
                                                                                                                                                    }
                                                                                                                                                });
                                                                                                                                            });
                                                                                                                                            $('#quickOrder').mouseenter(function () {
                                                                                                                                                $(this).find('div').slideDown(200);
                                                                                                                                            }).mouseleave(function () {
                                                                                                                                                $(this).find('div').slideUp(200);
                                                                                                                                            });
                                                                                                                                            $('.table-list tr:even').addClass('alt');
</script>
</body>
</html>