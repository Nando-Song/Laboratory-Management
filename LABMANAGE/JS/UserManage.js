var userRole = $("#hidden").val();
var curPage = 1;
var vsPage = 7;
var selectIsTea = false;
var roomID = 0;

$(".size").change(function () {
    $('.Page').empty();
    $('.Page').removeData("twbs-pagination");
    $('.Page').unbind("page");

    if ($(this).val().indexOf('学') != -1) {
        userRole = "R002";
        getUserManageList(1, false);
    }
    else if ($(this).val().indexOf('教') != -1) {
        userRole = $("#hidden").val();
        selectIsTea = true;
        getUserManageList(1, false);
    }
    else {
        userRole = $("#hidden").val();
        selectIsTea = false;
        getUserManageList(1, false);
    }
});

$(".room").change(function () {
    $('.Page').empty();
    $('.Page').removeData("twbs-pagination");
    $('.Page').unbind("page");
    roomID = $(this).val();
    getUserManageList(1, false); 
});

getUserManageList(1, false);

function getUserManageList(curPage, flag) {
    var userName = $("#search").val();
    var pageSize = $("#pageSize").val();
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        async: true,
        cache:false,
        url: "/UserManage/getUserManage",
        data: "{'userName':'" + userName + "', 'pageSize':'" + pageSize + "', 'curPage':'" + curPage + "', 'userRole':'"+ userRole +"', 'selectIsTea':'"+ selectIsTea +"', 'roomID':'"+ roomID +"'}",
        dataType: "json",
        success: function (data) {
            var obj = JSON.parse(data);
            if (obj.recordCount > pageSize) {
                ShowPage(obj.recordCount, pageSize, "Page", curPage, flag);
            }
            var html = '';
            var index = (curPage - 1) * pageSize + 1;
            var identify;
            for (var i=0; i<obj.adminList.length; i++)
            {
                if (obj.adminList[i].U_Role == 2) identify = "教师";
                else if (obj.adminList[i].U_Role == 3) identify = "学生";
                html += '<tr><td>' + (index++) + '</td><td>' + obj.adminList[i].Room_Name + '</td><td>' + identify + '</td><td>' + obj.adminList[i].Name + '</td><td>' + obj.adminList[i].Real_Name + '</td><td id="' + obj.adminList[i].Email + '" class="uEmail">' + obj.adminList[i].Email + '</td><td>'
                + obj.adminList[i].Phone + '</td><td>';
                if (obj.adminList[i].IsExamine)
                    html += '<span id="' + obj.adminList[i].ID + '" class="mCheck">审核通过</span></td>';
                else
                    html += '<button class="mCheck btn btn-info" id="' + obj.adminList[i].ID + '">待审核</button></td>';
                if (obj.adminList[i].IsExamine) 
                    html += '<td class="ope"><button class="btn btn-info mDelete" id="' + obj.adminList[i].Name + '">删除</button><button class="btn btn-info change">修改</button></td>';
                else
                    html += '<td class="ope"><button class="btn btn-info mDelete" id="' + obj.adminList[i].Name + '">删除</button><button class="btn btn-info change" disabled>修改</button></td>';
            }
            if (obj.adminList.length == 0)
            {
                $(".Page").empty();
                html = '<p class="empty">暂无该用户信息</p>';
            }
            $(".List").empty().html(html);

            $(".mCheck").click(function () {
                var email = $(this).parent().siblings(".uEmail").attr("id");
                UserCheck($(this).attr("id"), email);
            });

            $(".mDelete").click(function () {
                var mId = $(this).parent().siblings().children(".mCheck").attr("id");
                layer.confirm('确定删除该用户？', {
                    icon: 3,
                    btn: ['确定', '取消'] //按钮
                }, function () {                    
                    UserDel(mId);
                }, function () {
                    layer.msg('再思考一下？', {
                        time:2000,
                        icon: 6
                    });
                });            
            });

            $(".change").click(function () {
                var name = $(this).siblings().attr("id");
                ChangeUser(name);
            });
        },
        error: function () {
            layer.alert('出错了，请通知管理员！', {
                icon: 2,
                skin: 'layer-ext-moon' //该皮肤由layer.seaning.com友情扩展。关于皮肤的扩展规则，去这里查阅
            })
        }
    });
}

function ShowPage(count, pageSize, Class, curPage, flag){
    pagesCount = Math.floor(count / pageSize) + (count % pageSize == 0 ? 0 : 1);
    vsPage = pagesCount > vsPage ? vsPage : pagesCount;
    $("." + Class).twbsPagination({
        totalPages: pagesCount,
        visiblePages: vsPage,
        startPage: curPage,
        version: '1.1',
        onPageClick: function (event, page) { //点击分页回调函数
            $("." + Class).find(".page").removeClass("active");
            $("." + Class).find(".page[data-page='" + page + "']").addClass("active");
            if (flag != false || flag == undefined) {
                getUserManageList(page, true);
            }
            else {
                flag = true;
            }
        }
    });
}

function UserCheck(userId, email) {
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        url: "/UserManage/UserCheck",
        data: "{'userId':'"+userId+"', 'email':'"+email+"'}",
        dataType: "json",
        success: function (data) {
            if (data == true) {
                layer.alert('审核通过，欢迎新成员吧！', {
                    icon: 1,
                    skin: 'layer-ext-moon' //该皮肤由layer.seaning.com友情扩展。关于皮肤的扩展规则，去这里查阅
                })
                $("#" + userId).parent().siblings(".ope").children(".change").removeAttr("disabled");
                $("#" + userId).parent().html("<span>审核通过</span>");
                setInterval("window.location.reload();", 2000);
            }
            else {
                layer.alert('出错了，请通知管理员！', {
                    icon: 2,
                    skin: 'layer-ext-moon' //该皮肤由layer.seaning.com友情扩展。关于皮肤的扩展规则，去这里查阅
                })
            }
        },
        error: function () {
            layer.alert('出错了，请通知管理员！', {
                icon: 2,
                skin: 'layer-ext-moon' //该皮肤由layer.seaning.com友情扩展。关于皮肤的扩展规则，去这里查阅
            })
        }
    });
}

function UserDel(userId) {
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        url: "/UserManage/UserDel",
        data: "{'userId':'" + userId + "'}",
        dataType: "json",
        success: function (data) {
            if (data == true) {
                layer.alert('删除成功，实验室又少了一名成员！', {
                    icon: 1,
                    skin: 'layer-ext-moon' //该皮肤由layer.seaning.com友情扩展。关于皮肤的扩展规则，去这里查阅
                });
                setInterval("window.location.reload();", 2000);              
            }
            else {
                layer.alert('出错了，请通知管理员！', {
                    icon: 2,
                    skin: 'layer-ext-moon' //该皮肤由layer.seaning.com友情扩展。关于皮肤的扩展规则，去这里查阅
                });
            }
        },
        error: function () {
            layer.alert('出错了，请通知管理员！', {
                icon: 2,
                skin: 'layer-ext-moon' //该皮肤由layer.seaning.com友情扩展。关于皮肤的扩展规则，去这里查阅
            })
        }
    });
}

if (userRole == "R001") {
    $(".add").click(function () {
        addTeaStu();
    });
}

$(".manage").keydown(function () {
    if (event.keyCode == "13") {//keyCode=13是回车键
        $('.Page').empty();
        $('.Page').removeData("twbs-pagination");
        $('.Page').unbind("page");

        getUserManageList(1, false);
    }
});

$(".sea").click(function () {
    getUserManageList(1, false);
});

function addTeaStu() {
    //iframe层
    var index = layer.open({
        type: 2,
        title: '添加教师',
        shadeClose: true,
        shade: 0.8,
        area: ['500px', '600px'],
        content: ['/UserManage/Add?flag=false', 'no'] //iframe的url
    });
    layer.iframeAuto(index);
}

function ChangeUser(name) {
    //iframe层
    layer.open({
        type: 2,
        title: '修改用户信息',
        shadeClose: true,
        shade: 0.8,
        area: ['500px', '470px'],
        content: ['/UserManage/Change?name=' + name + "&flag=false", 'no'] //iframe的url
    });
}