﻿<html>
<head>
    <script src="/static/bootstrap3/js/jquery-1.11.1.min.js"></script>
    <script>
        $(document).ready(function () {
            $("button").click(function () {

                $.get("http://www.w3school.com.cn/example/jquery/demo_test.asp/", function (data, status) {
                    alert("数据：" + data + "\n状态：" + status);
                });

            });
        });
    </script>
</head>
<body>

    <button>向页面发送 HTTP GET 请求，然后获得返回的结果</button>

</body>
</html>
