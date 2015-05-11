<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpLoad.aspx.cs" Inherits="UploadifyDemo_UpLoad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Jquery Uploadify上传带进度条，且多参数</title>  
    <link href="../static/uploadify/example/css/default.css" rel="stylesheet" type="text/css" />
    <link href="../static/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.8.0.min.js" type="text/javascript"></script> 
    <script type="text/javascript" src="../static/uploadify/example/scripts/swfobject.js"></script>
    <script type="text/javascript" src="../static/uploadify/jquery.uploadify.v2.1.0.min.js"></script> 
    <script type="text/javascript">  
        $(document).ready(function () {  
            $("#pics").hide();  
            $("#uploadify").uploadify({  
                'uploader': '/static/uploadify/uploadify.swf', //uploadify.swf文件的路径  
                'script': 'UploadHandler.ashx?type=add', //处理文件上传的后台脚本的路径  
                'cancelImg': '/static/uploadify/example/cancel.png',
                'buttonText': 'Select Image',  
                'folder': 'UploadFile/<% = DateTime.Now.ToString("yyyyMMdd") %>/', //上传文件夹的路径按20130416  
                'queueID': 'fileQueue', //页面中，你想要用来作为文件队列的元素的id  
                'auto': true, //当文件被添加到队列时，自动上传  
                'multi': false, //设置为true将允许多文件上传  
                'fileExt': '*.jpg;*.gif;*.png', //允许上传的文件后缀  
                'queueSizeLimit': 5,  
                'fileDesc': 'Web Image Files (.JPG, .GIF, .PNG)', //在浏览窗口底部的文件类型下拉菜单中显示的文本  
                'sizeLimit': 1024000,  //上传文件的大小限制，单位为字节 1024*1000 1M  
                'onComplete': function (event, queueID, fileObj, response, data) {  
                    var html = "";  
                    html += "    <li class=\'myli\'>";  
                    html += "    <img src=\"" + response + "\" class=\'myimg\'/>";  
                    html += "    <div style=\" position:absolute; right:8px; bottom:5px\">";  
                    html += "        <img title=\"点击删除\" src=\"/static/uploadify/example/cancel.png\" style=\"cursor: pointer\" title=\"点击删除凭证\" onclick=\"delImage(\'" + response + "\');\" />";
                    //html += "        <a>shanchu</a>";
                    html += "    </div>";  
                    html += "    </li>";  
                    $("#pics").append(html);  
                },  
                'onAllComplete': function (event, data) { //当上传队列中的所有文件都完成上传时触发  
                    //alert(data.filesUploaded + ' 个文件上传成功!');  
                    $("#pics").fadeIn();  
                }  
            });  
        });  
  
        function uploadpara() {  
            //自定义传递参数  
            $('#uploadify').uploadifySettings('scriptData', { 'name': $('#txtName').val(), 'albums': $('#txtAlbums').val() });  
            $('#uploadify').uploadifyUpload();  
        }  
  
        function delImage(picurl) {  
            jsonAjax("UploadHandler.ashx", "type=del&picurl=" + picurl, "text", callBackTxt);  
        }  
  
        function jsonAjax(url, param, datat, callback) {  
            $.ajax({  
                type: "post",  
                url: url,  
                data: param,  
                dataType: datat,  
                success: callback,  
                error: function () {  
                    jQuery.fn.mBox({  
                        message: '恢复失败'  
                    });  
                }  
            });  
        }  
  
        function callBackTxt(data) {  
            var o = data;  
            if (o != "") {  
                var e = $(".myli img[src='" + data + "']");  
                e.each(function () {  
                    $(this).parent().remove();  
                })  
            } else {  
                alert("删除失败");  
            }  
        };    
    </script>  
    <style type="text/css">  
    .myul  
    {  
      margin:5px 5px 5px 5px;  
      padding:0px;  
    }  
    .myli  
    {  
        list-style-type:none;  
        width:150px;  
        height:150px;  
        display:inline;   
        position:relative;  
    }  
    .myimg  
    {  
        width:120px;  
        height:120px;  
    }  
    </style>  
</head>  
<body>  
    <form id="form1" runat="server">  
    <div>  
        <img src="../static/img/999parcel.png" style="cursor: pointer" title="zmmeiyouaaa!!!" />
    <div id="fileQueue"></div>  
    <input type="file" name="uploadify" id="uploadify" />  
    <div id="pics">  
    <ul class="myul">  
    </ul>  
    </div>  
    <div>  
    <p>  
        <a href="javascript:void(0);" onclick="uploadpara();">上传</a>|   
        <a href="javascript:$('#uploadify').uploadifyClearQueue()">取消上传</a>  
    </p>  
    </div>  
    </div>  
    </form>  
</body> 
</html>
