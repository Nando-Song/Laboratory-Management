var nickTime = "";
var nickName = "";
$(function () {
    //var nickName = $("#searchNames").val();
    //var nickTime = $("#searchTimes").val();
    var vsPage = 7;//可见页数
    var pagesCount = 10;//总页数
    var curPage = 1;//当前页
    GetSumList(1, false);
    function GetSumList(curPage, flag) {
        var j = 0;
        nickName = $("#searchNames").val();
        var roomID = $("#room").val();
        if (roomID == undefined)
            var roomID = "";
        else
             roomID = $("#room").val();
        if (roomID == "--请选择实验室--")
            roomID = "";
        $.ajax({
            url: "/Summary/GetSumList",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{nickName:'" + nickName + "',nickTime:'" + nickTime + "',curPage:'" + curPage + "',roomID:'" + roomID + "'}",
            //data: "{nickName:'" + nickName + "',nickTime:'" + nickTime + "',curPage:'" + curPage + "'}",
            //async: true,
            cache: false,
            type: "POST",
            success: function (data) {
                var obj = JSON.parse(data);
                var pageSize = obj.pageSize;
                if (obj.recordCount > pageSize) {
                    ShowPage(obj.recordCount, pageSize, "pagesSums", curPage, flag);
                }
              
                var html = '';
                
                var index = (curPage - 1) * pageSize + 1;
                for (var i = 0 ; i < obj.lists.length ; i++) {
                    //j++;
                    var time = obj.lists[i].Time.replace(/T/g, ' ').split('.')[0];
                    html += '<tr><td>' + (index++) + '</td><td>' + obj.lists[i].Real_Name + '</td><td class="title" style="cursor:pointer;">' + obj.lists[i].Title + '</td><td class="progresst" style="cursor:pointer;">' + obj.lists[i].Progress + '</td>';
                    if (obj.lists[i].Problem == null) {
                        html += '<td>无</td>';
                        html += '<td></td>';
                    }
                    else {
                        html += '<td class="problem" style="cursor:pointer;">' + obj.lists[i].Problem + '</td>';
                        if (obj.lists[i].IS_Solve == 0)
                            html += '<td>已解决</td>';
                        else if (obj.lists[i].IS_Solve == 1) {
                            html += '<td>尚未解决</td>';
                        }
                    }
                    if (obj.lists[i].Teacher_evaluation == null)
                        html += '<td>暂无</td><td>' + time + '</td>';
                    else
                        html += '<td class="eval" style="cursor:pointer;">' + obj.lists[i].Teacher_evaluation + '</td><td>' + time + '</td>';
                    //html += '<td>' + obj.lists[i].IS_Solve + '</td>';
                    html += '<td><a class="btnt btn-info btn_more" href="/Summary/Person?id=' + obj.lists[i].User_ID + '">更多</a>';
                    //onclick=\"Update(\'' + id + '\')\"
                    var roleCode = $("#roleCode").val();
                    if (roleCode == "R002")
                    {
                        html += '&nbsp;&nbsp;&nbsp;<a class="btnt btn-info btn_eval" data-toggle="modal" data-target="#Evaluation"  id="' + obj.lists[i].ID + '">回复</a></td></tr> ';
                    }
                }
                if (obj.lists.length == 0) {
                    $("#pagesSums").empty();
                    html = '<p>暂无相关总结</p>';
                }
                $("#sumItems").empty().html(html);


                $(".btn_eval").on("click", function () {
                    var id = $(this).attr("id");
                    openEvalSum(id);
                });

                
                $("#searchBtn").unbind();
                $("#searchBtn").on("click", function (event) {
                    $('#pagesSums').empty();
                    $('#pagesSums').removeData("twbs-pagination");
                    $('#pagesSums').unbind("page");
                    nickTime = $("#searchTimes").val();
                    GetSumList(1, false);
                });
                $("#export").unbind();
                $("#export").on("click", function () {
                    Export();
                });
                $(".eval").on("click", function () {
                    var Teval = $(this).text();
                    layer.alert(Teval, {
                        title: "教师回复",
                        skin: 'layer-ext-moon' //该皮肤由layer.seaning.com友情扩展。关于皮肤的扩展规则，去这里查阅
                    });
                });
                $(".title").on("click", function () {
                    var Title = $(this).text();
                    layer.alert(Title, {
                        title: "学习/工作内容",
                        skin: 'layer-ext-moon' //该皮肤由layer.seaning.com友情扩展。关于皮肤的扩展规则，去这里查阅
                    });
                });
                $(".progresst").on("click", function () {
                    var Progress = $(this).text();
                    layer.alert(Progress, {
                        title: "学习/工作情况简介",
                        skin: 'layer-ext-moon' //该皮肤由layer.seaning.com友情扩展。关于皮肤的扩展规则，去这里查阅
                    });
                });
                $(".problem").on("click", function () {
                    var Problem = $(this).text();
                    layer.alert(Problem, {
                        title: "遇到的问题",
                        skin: 'layer-ext-moon' //该皮肤由layer.seaning.com友情扩展。关于皮肤的扩展规则，去这里查阅
                    });
                });
                return;

            }
        });
    }
 
    function ShowPage(count, pageSize, id, curPage, flag) {
        pagesCount = Math.floor(count / pageSize) + (count % pageSize == 0 ? 0 : 1);
        vsPage = 7;
        vsPage = pagesCount > vsPage ? vsPage : pagesCount;
        $('#' + id).twbsPagination({
            totalPages: pagesCount,
            visiblePages: vsPage,
            startPage: curPage,
            version: '1.1',
            onPageClick: function (event, page) { //点击分页回调函数
                $('#' + id).find(".page").removeClass("active");
                $('#' + id).find(".page[data-page='" + page + "']").addClass("active");
                if (flag != false || flag == undefined) {
                        GetSumList(page, true);
                }
                else {
                    flag = true;
                }
            }
        });
    }

    function Export() {
         nickName = $("#searchNames").val();
         nickTime = $("#searchTimes").val();
        //var nickName = $("#searchNames").val();
        //var nickTime = $("#searchTimes").val();
        window.location.href = "/Summary/Export?nickName=" + nickName + "&nickTime=" + nickTime;
    }
});
function openEvalSum(id) {
    //iframe层
    layer.open({
        type: 2,
        title: '评价',
        shadeClose: true,
        shade: 0.8,
        area: ['700px', '450px'],
        content: ['/Summary/EvalSum?Id=' + id + "&flag=false", 'no'] //iframe的url
    });
}

