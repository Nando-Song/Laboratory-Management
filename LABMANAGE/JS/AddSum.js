$(function () {
    var msg = $("#Mssg").val();
    if (msg != "") {
        var index = parent.layer.getFrameIndex(window.name); //获取窗口索引
        parent.layer.msg(msg, { time: 2000 });
        parent.layer.close(index);
        window.parent.location.reload();
    }
    $("#btnA").click(function () {
        var index = parent.layer.getFrameIndex(window.name); //获取窗口索引
        parent.layer.close(index);
    });
});

