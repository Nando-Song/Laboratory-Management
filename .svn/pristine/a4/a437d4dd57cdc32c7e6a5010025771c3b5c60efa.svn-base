//公告显示
function show()
{
    $.ajax({
        url: "/WyhDuty/ShowNotice",
        dataType: 'json',
        async: false,//请求是否异步，默认异步;
        type: "POST",
        success: function (result) {
            //alert(result[0]['Text'].toString());
            var date = new Date(parseInt(result[0]['Time'].slice(6, 19)));

            var formatDateTime = GetDate(date);
            //document.getElementById('myModalTitle').innerText = result[0]['Title'];
            //document.getElementById('myModalBody').innerText = result[0]['Text'];
            //document.getElementById('myModalName').innerText = "发布人："+ result[0]['Name'];
            //document.getElementById('myModalTime').innerText = "时间：" + formatDateTime;
            layer.open({
                title: '公告',
                skin: 'demo-class',
                type: 0,
                //0（信息框，默认）1（页面层）2（iframe层）3（加载层）4（tips层）
                area: ['600px', '360px'],
                shadeClose: true, //点击遮罩关闭
                anim: 1,
                content: '<div style="margin:10px 10px 0 10px; height:72%"><span style="font:20px bold;">' +
                result[0]['Title'] +
                '</span><hr style="height:1px;border:none;border-top:1px solid #ddd;" /><div style="margin:5px;">' +
                result[0]['Text'] +
                '</div></div><br /><div style="float:right;"><span style="color:#000; font-size:15px; font-weight: 600;">' +
                result[0]['Name'] +
                '&nbsp;</span><span style="color:#999; font-size:12px">' +
                formatDateTime +
                '</span></div>'
            });
            
            //请求成功处理
        },
        error: function (result) {
            layer.alert("出现未知错误（公告） 请联系管理员");
        }
    });

}

//更新公告表
function Update() {
    var oNoticeTitle = document.getElementById('UpNoticeTitle').value;
    var oNoticeText = document.getElementById('UpNoticeBody').value;
    var oNowTime = new Date().toLocaleTimeString();

    if (oNoticeTitle != "" && oNoticeText != "")
    {
        $.ajax({
            url: "/WyhDuty/InsertDto",
            dataType: 'json',
            data: {
                "oNoticeTitle": oNoticeTitle,
                "oNoticeText": oNoticeText,
                "oNowTime": oNowTime
            },
            type: "POST",
            success: function (result) {
                //var date = new Date(parseInt(result[0]['Time'].slice(6, 19)));
                //var formatDateTime = GetDate(date);
                //document.getElementById('myModalTitle').innerText = result[0]['Title'];
                //document.getElementById('myModalBody').innerText = result[0]['Text'];
                //document.getElementById('myModalName').innerText = "发布人：" + result[0]['Name'];
                //document.getElementById('myModalTime').innerText = "时间：" + formatDateTime;
                layer.msg("公告修改成功！");
                //请求成功处理
            },
            error: function (result) {
                layer.alert("公告修改失败！ 请联系管理员");
            }
        });
    }
    
}

//建议
function Suggest() {
    layer.open({
        title: '感谢您的宝贵意见',
        skin: 'demo-class',
        type: 1,
        area: ['500px', '605px'],
        shadeClose: true, //点击遮罩关闭
        anim: 0,
        content: '<form class="box box-primary wyh_form"><div class="box-body"><div class="form-group"><input class="form-control" placeholder="这里是主题~"></div><div class="form-group"><textarea style="height:400px; width:100%;" id="compose-textarea" class="form-control" placeholder="您的意见将会及时发送到管理人员邮箱^_^"></textarea></div></div><div class="box-footer"><div class="pull-right"><button type="reset" class="btn btn-default"><i class="fa fa-pencil"></i> 重置内容</button><button type="submit" class="btn btn-primary" onclick="OK()"><i class="fa fa-envelope-o"></i> 发送意见</button></div></div></form>'
    });
}
function OK() {
    layer.msg("您的意见将会发送到管理员邮箱^_^");
    setTimeout(function () { location.reload(); }, 1000);
}


function GetDate(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    m = m < 10 ? ('0' + m) : m;
    var d = date.getDate();
    d = d < 10 ? ('0' + d) : d;
    var h = date.getHours();
    var minute = date.getMinutes();
    minute = minute < 10 ? ('0' + minute) : minute;

    return y + '-' + m + '-' + d + ' ' + h + ':' + minute;
}