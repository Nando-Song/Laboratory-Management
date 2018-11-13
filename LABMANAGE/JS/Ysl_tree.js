$(function () {
    $.ajax({
        url: "/ysl_Sign_In/get_tree",
        type: "POST",
        dataType: 'json',
        success: function (result) {
            var obj = eval('(' + result + ')');
            //alert(typeof obj);
            var type;
            layui.use(['tree', 'layer'], function () {
                layui.tree({
                    elem: '#demo1' //指定元素
                  , target: '_blank' //是否新选项卡打开（比如节点返回href才有效）
                  , skin: 'lanse'
                  , click: function (item) { //点击节点回调
                      if(item.isuser == 1)
                      {
                          layer.msg('正在跳转到：' + item.name + '的签到信息');
                          if (document.getElementById("checkbox1").checked) {
                              type = 1;
                          }
                          else if (document.getElementById("checkbox2").checked) {
                              type = 2;
                          }

                          $.ajax({
                              url: "/ysl_Sign_In/changeuser",
                              //async: false,//请求是否异步，默认异步;
                              type: "POST",
                              dataType: 'json',
                              data: {
                                  "UID": item.UID, //用户名&邮箱
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
                                  for(var itext = 0; itext<text.length; itext++)
                                  {
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
                                  layer.alert("无法获取到该学生的签到信息----------------kylin");
                              }
                          });
                      }
                      
                  }
                  , nodes: obj
                });
            });

        },
        error: function (result) {
            layer.alert("读取人员信息错误--------------------kylin");
        }
    });

    
});








