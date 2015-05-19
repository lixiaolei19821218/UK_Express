<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="accounts_UserCentre_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>充值记录</title>
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
        <a href="javascript:;">
            <img src="http://express-xc.co.uk/tpl/t4/image/logo.jpg" width="228" height="40" alt="星弛管理中心" /></a>
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
            <h2>充值明细</h2>
            <div class="sub-nav">
                <span class="rt">充值<em>£0</em>，剩余<em>£0</em>(赠送<em>£0</em>)</span>
                <a href="/index.php?c=bank&a=user_list" class="back">返回</a>
            </div>
            <div class="uitopb uitopb-border mt10">
                <div class="table-div">
                    <table class="table-list">
                        <tr>
                            <th>方式</th>
                            <th>金额(£)</th>
                            <th>凭证</th>
                            <th>时间</th>
                            <th>处理</th>
                        </tr>
                        <tr>
                            <td>英镑支付(转帐或存现)</td>
                            <td>1</td>
                            <td>
                                <a href="/attachment/2015-05-19/91132-1432035602.JPG" target="_blank" title="付款凭证">付款凭证</a><br />
                            </td>
                            <td>2015/05/19 12:40</td>
                            <td>×</td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="pager"><font style="color: red;">1</font>&nbsp;</div>
        </div>
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
    <div style="position: fixed; top: 250px; right: 10px; text-align: center; font-size: 14px; font-family: 微软雅黑">
        <div style="margin: 10px 0 0 0;">
            <script type="text/javascript" src="http://static.b.qq.com/account/bizqq/js/wpa.js?wty=1&type=1&kfuin=800056001&ws=http%3A%2F%2Fwww.express-xc.co.uk&btn1=%E4%BC%81%E4%B8%9AQQ%E4%BA%A4%E8%B0%88&aty=0&a=&key=%0CiWf%041Ta%032%03cWgW%3E%03bSlUoV1ScQ7%05fR6%0Bf%045%051"></script>
        </div>
    </div>
    <div class="hidden">
        <script src="http://s11.cnzz.com/stat.php?id=5312818&web_id=5312818&show=pic2" language="JavaScript"></script>
    </div>
    <script>
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
