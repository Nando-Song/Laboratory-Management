$(document).ready(function () {

    $('#calendar').fullCalendar({

        header: {
            left: 'prevYear,nextYear',
            center: 'title',
            right: ' today prev,next'
        },
        aspectRatio: 1.9,
        monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
        monthNamesShort: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
        dayNames: ['星期天', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
        dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
        editable: false,//可以拖动
    });
    
});
$(function () {
    $.ajax({
        url: "/ysl_Sign_In/Sign_json",
        success: function (result) {
            var json = eval("(" + result + ")");
            for (var item in json) {
                $("#calendar").fullCalendar('renderEvent', json[item], true);
            }
            //修改日历中文字的格式;
            var text = document.getElementsByClassName("fc-time");
            for (var itext = 0; itext < text.length; itext++) {
                var time = text[itext].innerText;
                if (time == "8a") {
                    time = time.replace("8a", "8:00 AM  ");
                }
                else if (time == "2p") {
                    time = time.replace("2p", "2:00 PM  ");
                }
                else if (time == "7p") {
                    time = time.replace("7p", "7:00 PM  ");
                }
                time = time.replace("a", " AM  ");
                time = time.replace("p", " PM  ");
                document.getElementsByClassName("fc-time")[itext].innerText = time;
            }
        },
        error: function (result) {
            layer.alert("读取信息错误010--------------------kylin");
        }
    });
});
function userdata() {
    //获取被选中的option标签  
    var UID = $('#userselect option:selected').val();
    var type = $('#typeselect option:selected').val();

    $.ajax({
        url: "/ysl_Sign_In/changeuser",
        //async: false,//请求是否异步，默认异步;
        type: "POST",
        dataType: 'json',
        data: {
            "UID": UID, //用户名&邮箱
            "type": type,//内容
        },
        success: function (result) {
            $('#calendar').fullCalendar('removeEvents');
            $('#calendar').fullCalendar('refetchEvents');
            var json = eval("(" + result + ")");
            for (var item in json) {
                $("#calendar").fullCalendar('renderEvent', json[item], true);
            }
            //修改日历中文字的格式;
            var text = document.getElementsByClassName("fc-time");
            for (var itext = 0; itext < text.length; itext++) {
                var time = text[itext].innerText;
                if (time == "8a") {
                    time = time.replace("8a", "8:00 AM  ");
                }
                else if (time == "2p") {
                    time = time.replace("2p", "2:00 PM  ");
                }
                else if (time == "7p") {
                    time = time.replace("7p", "7:00 PM  ");
                }
                time = time.replace("a", " AM  ");
                time = time.replace("p", " PM  ");
                document.getElementsByClassName("fc-time")[itext].innerText = time;
            }
        },
        error: function (result) {
            layer.alert("读取信息错误02--------------------kylin");
        }
    });
}
//当页面大小改变时
window.onresize = function () {
    document.getElementById("left_user_name").style.height = document.getElementById("calendar").offsetHeight + 'px';
}
